namespace VaultLab.IntegrationTests.API
{
    public class HealthControllerTest(VaultLabApiFactory factory) : IClassFixture<VaultLabApiFactory>
    {
        [Fact]

        public async Task GetHealth_ReturnsHealthyStatus()
        {
            //Arrange
            var client = factory.CreateClient();

            //Act
            var response = await client.GetAsync("/api/health");

            //Assert
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            Assert.Contains("Healthy", content);
        }

    }
}