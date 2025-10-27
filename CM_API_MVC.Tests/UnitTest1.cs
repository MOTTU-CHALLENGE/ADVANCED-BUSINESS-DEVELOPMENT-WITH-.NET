namespace CM_API_MVC.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {

    }

    [Fact]
    public void TesteSimples_DevePassar()
    {
        // Arrange
        int x = 2;
        int y = 3;

        // Act
        int resultado = x + y;

        // Assert
        Assert.Equal(5, resultado);
    }
}
