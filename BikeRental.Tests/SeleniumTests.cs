using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools;
using OpenQA.Selenium.Support.UI;

namespace BikeRental.Tests
{
    public class SeleniumTests
    {
        [Fact]
        public void ReservationsMainTest()
        {
            ChromeDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://localhost:7296");
            driver.FindElement(By.PartialLinkText("Rowery")).Click();
            driver.FindElement(By.PartialLinkText("Szczegó³y")).Click();
            driver.FindElement(By.PartialLinkText("Zarezerwuj")).Click();
            driver.FindElement(By.Id("Input_Email")).SendKeys("admin@ath.eu");
            driver.FindElement(By.Id("Input_Password")).SendKeys("Az123456$");
            driver.FindElement(By.Id("login-submit")).Click();
            var startDate = driver.FindElement(By.Id("StartDate"));
            startDate.SendKeys("25102023");
            startDate.SendKeys(Keys.Tab);
            startDate.SendKeys("1000AM");
            var endDate = driver.FindElement(By.Id("EndDate"));
            endDate.SendKeys("30102023");
            endDate.SendKeys(Keys.Tab);
            endDate.SendKeys("1000AM");
            driver.FindElement(By.Id("create")).Click();
            driver.FindElement(By.PartialLinkText("Powróæ")).Click();
            driver.FindElement(By.PartialLinkText("Rowery")).Click();
            driver.FindElement(By.PartialLinkText("Szczegó³y")).Click();
            Assert.Empty(driver.FindElements(By.PartialLinkText("Zarezerwuj")));
            driver.Navigate().GoToUrl("https://localhost:7296/Admin/Reservations");
            driver.FindElement(By.PartialLinkText("Zmieñ")).Click();
			driver.FindElement(By.Name("rent")).Click();
			driver.FindElement(By.PartialLinkText("Zmieñ")).Click();
			driver.FindElement(By.Name("return")).Click();
			driver.FindElement(By.PartialLinkText("Zmieñ")).Click();
            Assert.Empty(driver.FindElements(By.Id("change")));
            driver.FindElement(By.PartialLinkText("Wróæ")).Click();
            Assert.Equal("Index - Admin ATH Bike Rental System", driver.Title);
            driver.Navigate().GoToUrl("https://localhost:7296/Vehicles");
            driver.FindElement(By.PartialLinkText("Szczegó³y")).Click();
            driver.FindElement(By.PartialLinkText("Zarezerwuj"));
            driver.Quit();
        }

        [Fact]
        public void ReservationDateValidatorTest()
        {
            ChromeDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://localhost:7296");
            driver.FindElement(By.PartialLinkText("Rowery")).Click();
            driver.FindElement(By.PartialLinkText("Szczegó³y")).Click();
            driver.FindElement(By.PartialLinkText("Zarezerwuj")).Click();
            driver.FindElement(By.Id("Input_Email")).SendKeys("admin@ath.eu");
            driver.FindElement(By.Id("Input_Password")).SendKeys("Az123456$");
            driver.FindElement(By.Id("login-submit")).Click();
            var startDate = driver.FindElement(By.Id("StartDate"));
            startDate.SendKeys("25102022");
            startDate.SendKeys(Keys.Tab);
            startDate.SendKeys("1000AM");
            var endDate = driver.FindElement(By.Id("EndDate"));
            endDate.SendKeys("30102023");
            endDate.SendKeys(Keys.Tab);
            endDate.SendKeys("1000AM");
            Assert.Equal("Stwórz rezerwacje - ATH Bike Rental System", driver.Title);
            startDate = driver.FindElement(By.Id("StartDate"));
            startDate.SendKeys("25102023");
            startDate.SendKeys(Keys.Tab);
            startDate.SendKeys("1000AM");
            endDate = driver.FindElement(By.Id("EndDate"));
            endDate.SendKeys("24102023");
            endDate.SendKeys(Keys.Tab);
            endDate.SendKeys("1000AM");
            Assert.Equal("Stwórz rezerwacje - ATH Bike Rental System", driver.Title);
            driver.Quit();
        }
    }
}