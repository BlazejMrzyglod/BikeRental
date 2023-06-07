using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace BikeRental.Tests
{
    public class SeleniumTests
    {
        [Fact]
        public void ReservationsTest()
        {
            ChromeDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://localhost:7296/Vehicles");
            driver.FindElement(By.PartialLinkText("Szczegó³y")).Click();
            Assert.Equal("Szczegó³y - ATH Bike Rental System", driver.Title);
            driver.FindElement(By.PartialLinkText("Zarezerwuj")).Click();
            Assert.Equal("Log in - ATH Bike Rental System", driver.Title);
            driver.FindElement(By.Id("Input_Email")).
            driver.Quit();
        }
    }
}