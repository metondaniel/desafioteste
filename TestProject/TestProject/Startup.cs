using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using TestProject.Domain.Interfaces.Repositories;
using TestProject.Domain.Interfaces.Services;
using TestProject.Domain.Services;
using MongoDB.Driver;
using TestProject.Repository;
using Confluent.Kafka;
using Microsoft.OpenApi.Models;
using System.Text.Encodings.Web;
using System.Text;
using System.Web;
using System;
using Google.Cloud.PubSub.V1;
using Microsoft.Extensions.Options;
using TestProject.Domain.Entities;

public class Startup
{
    public Startup( IConfiguration configuration )
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices( IServiceCollection services )
    {
        services.AddControllers();

        // Register IMongoClient
        services.AddSingleton<IMongoClient>( serviceProvider =>
        {

            var settings = MongoClientSettings.FromConnectionString( Configuration.GetSection("MongoDb:ConnectionString").Value );
            // Set the ServerApi field of the settings object to set the version of the Stable API on the client
            settings.ServerApi = new ServerApi( ServerApiVersion.V1 );
            // Create a new client and connect to the server
            return new MongoClient( settings );
        } );

        services.AddScoped<IMongoDatabase>( serviceProvider =>
        {
            var client = serviceProvider.GetRequiredService<IMongoClient>();
            var settings = serviceProvider.GetRequiredService<IConfiguration>();
            var databaseName = settings["MongoDb:DatabaseName"];
            return client.GetDatabase( databaseName );
        } );



        services.AddSwaggerGen( c =>
        {
            c.SwaggerDoc( "v1", new OpenApiInfo { Title = "Test", Version = "v1" } );
        } );

        // Register your repositories
        services.AddScoped<IBikeRepository, BikeRepository>();
        services.AddScoped<IDeliveryDriverRepository, DeliveryDriverRepository>();
        services.AddScoped<IFileStorageRepository, FileStorageRepository>();
        services.AddScoped<IRentalRepository, RentalRepository>();

        // Register your services
        services.AddScoped<IBikeService, BikeService>();
        services.AddScoped<IMessagingService, MessagingService>();
        services.AddScoped<IDeliveryDriverService, DeliveryDriverService>();
        services.AddScoped<IRentalService, RentalService>();
        services.AddScoped<IFileStorageService, FileStorageService>();

        // Logging with Serilog
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration( Configuration )
            .CreateLogger();

        services.Configure<PubSubConfig>( Configuration.GetSection( "PubSub" ) );

        // Register Google Pub/Sub publisher client
        services.AddSingleton<PublisherClient>( sp =>
        {
            var pubSubConfig = sp.GetRequiredService<IOptions<PubSubConfig>>().Value;
            var publisherSettings = new PublisherClient.Settings();
            TopicName topicName = TopicName.FromProjectTopic( pubSubConfig.ProjectId, pubSubConfig.TopicId );
            PublisherClient publisher = PublisherClient.Create( topicName );
            return publisher;
        } );
    }

    public void Configure( IApplicationBuilder app, IWebHostEnvironment env )
    {
        if( env.IsDevelopment() )
        {
            app.UseDeveloperExceptionPage();
        }

        // Enable middleware to serve generated Swagger as a JSON endpoint.
        app.UseSwagger();
        // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
        // specifying the Swagger JSON endpoint.
        app.UseSwaggerUI( c =>
        {
            c.SwaggerEndpoint( "/swagger/v1/swagger.json", "Test V1" );
        } );

        app.UseSwaggerUi();

        app.UseSerilogRequestLogging(); // Enables logging of HTTP requests

        app.UseRouting();

        app.UseEndpoints( endpoints =>
        {
            endpoints.MapControllers();
        } );

    }
}
