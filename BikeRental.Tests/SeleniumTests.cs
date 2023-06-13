using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools;
using OpenQA.Selenium.Support.UI;

namespace BikeRental.Tests
{
    public class SeleniumTests
    {
        [Fact]
        public void ReservationsTest()
        {
            ChromeDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://localhost:7296");
            driver.FindElement(By.PartialLinkText("Rowery")).Click();
            Assert.Equal("Rowery - ATH Bike Rental System", driver.Title);
            driver.FindElement(By.PartialLinkText("Szczegó³y")).Click();
            Assert.Equal("Szczegó³y - ATH Bike Rental System", driver.Title);
            driver.FindElement(By.PartialLinkText("Zarezerwuj")).Click();
            Assert.Equal("Log in - ATH Bike Rental System", driver.Title);
            driver.FindElement(By.Id("Input_Email")).SendKeys("admin@ath.eu");
            driver.FindElement(By.Id("Input_Password")).SendKeys("Az123456$");
            driver.FindElement(By.Id("login-submit")).Click();
            Assert.Equal("Stwórz rezerwacje - ATH Bike Rental System", driver.Title);
            var startDate = driver.FindElement(By.Id("StartDate"));
            startDate.SendKeys("25102023");
            startDate.SendKeys(Keys.Tab);
            startDate.SendKeys("1000AM");
            var endDate = driver.FindElement(By.Id("EndDate"));
            endDate.SendKeys("30102023");
            endDate.SendKeys(Keys.Tab);
            endDate.SendKeys("1000AM");
            driver.FindElement(By.Id("create")).Click();
            Assert.Equal("Rezerwacja udana - ATH Bike Rental System", driver.Title);
            driver.FindElement(By.PartialLinkText("Powróæ")).Click();
            Assert.Equal("Strona g³ówna - ATH Bike Rental System", driver.Title);
            driver.FindElement(By.PartialLinkText("Rowery")).Click();
            Assert.Equal("Rowery - ATH Bike Rental System", driver.Title);
            driver.FindElement(By.PartialLinkText("Szczegó³y")).Click();
            Assert.Equal("Szczegó³y - ATH Bike Rental System", driver.Title);
            Assert.Empty(driver.FindElements(By.PartialLinkText("Zarezerwuj")));
            driver.Navigate().GoToUrl("https://localhost:7296/Admin/Reservations");
            Assert.Equal("Index - Admin ATH Bike Rental System", driver.Title);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.FindElement(By.PartialLinkText("Zmieñ status")).Click();
            //driver.Quit();
        }
    }
}