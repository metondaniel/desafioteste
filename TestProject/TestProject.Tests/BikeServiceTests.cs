using Moq;
using FluentAssertions;
using System.Threading.Tasks;
using Xunit;
using TestProject.Domain.Services;
using TestProject.Domain.Entities;
using TestProject.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Logging;

public class BikeServiceTests
{
    private readonly BikeService _bikeService;
    private readonly Mock<IBikeRepository> _bikeRepositoryMock = new Mock<IBikeRepository>();
    private readonly Mock<ILogger<BikeService>> _loggerMock = new Mock<ILogger<BikeService>>();

    public BikeServiceTests()
    {
        _bikeService = new BikeService( _bikeRepositoryMock.Object, _loggerMock.Object );
    }

    [Fact]
    public async Task AddBikeAsync_ShouldAddBike_WhenBikeIsValid()
    {
        // Arrange
        var bike = new Bike { Id = 1, Model = "Mountain", Plate = "XYZ123" };
        _bikeRepositoryMock.Setup( x => x.AddBikeAsync( bike ) ).Returns( Task.CompletedTask );

        // Act
        await _bikeService.AddBikeAsync( bike );

        // Assert
        _bikeRepositoryMock.Verify( x => x.AddBikeAsync( bike ), Times.Once );
    }
}
