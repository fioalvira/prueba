using Obligatorio1.Controllers;

namespace TestProject1;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void Get_ShouldReturnFiveForecasts()
    {
        // Arrange
        var controller = new WeatherForecastController();

        // Act
        var result = controller.Get();

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(5, result.Count());
    }
}
