using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools;
using OpenQA.Selenium.Support.UI;

namespace BikeRental.Tests
{
    public class SeleniumTests
    {
        //G��wny test sprawdzaj�cy z�o�enie rezerwacji, dost�pno�� roweru i zarz�dzanie rezerwacjami
        [Fact]
        public void ReservationsMainTest()
        {
            ChromeDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://localhost:7296");

            //Wybranie roweru i przej�cie do rezerwacji
            driver.FindElement(By.PartialLinkText("Rowery")).Click();
            driver.FindElement(By.PartialLinkText("Szczeg�y")).Click();
            driver.FindElement(By.PartialLinkText("Zarezerwuj")).Click();

            //Logowanie na konto admina
            driver.FindElement(By.Id("Input_Email")).SendKeys("admin@ath.eu");
            driver.FindElement(By.Id("Input_Password")).SendKeys("Az123456$");
            driver.FindElement(By.Id("login-submit")).Click();

            //Z�o�enie rezerwacji
            var startDate = driver.FindElement(By.Id("StartDate"));
            startDate.SendKeys("25102023");
            startDate.SendKeys(Keys.Tab);
            startDate.SendKeys("1000AM");
            var endDate = driver.FindElement(By.Id("EndDate"));
            endDate.SendKeys("30102023");
            endDate.SendKeys(Keys.Tab);
            endDate.SendKeys("1000AM");
            driver.FindElement(By.Id("create")).Click();

            //Ponowne wybranie roweru i sprawdzenie czy rezerwacja jest niemo�liwa
            driver.FindElement(By.PartialLinkText("Powr��")).Click();
            driver.FindElement(By.PartialLinkText("Rowery")).Click();
            driver.FindElement(By.PartialLinkText("Szczeg�y")).Click();
            Assert.Empty(driver.FindElements(By.PartialLinkText("Zarezerwuj")));

            //Przej�cie do area dla Admina i przej�cie przez wszystkie mo�liwe zmiany statusu
            driver.Navigate().GoToUrl("https://localhost:7296/Admin/Reservations");
            driver.FindElement(By.PartialLinkText("Zmie�")).Click();
			driver.FindElement(By.Name("rent")).Click();
			driver.FindElement(By.PartialLinkText("Zmie�")).Click();
			driver.FindElement(By.Name("return")).Click();
			driver.FindElement(By.PartialLinkText("Zmie�")).Click();
            Assert.Empty(driver.FindElements(By.Id("change")));
            driver.FindElement(By.PartialLinkText("Wr��")).Click();
            Assert.Equal("Index - Admin ATH Bike Rental System", driver.Title);

            //Sprawdzenie czy rower zn�w mo�na zarezerwowa�
            driver.Navigate().GoToUrl("https://localhost:7296/Vehicles");
            driver.FindElement(By.PartialLinkText("Szczeg�y")).Click();
            driver.FindElement(By.PartialLinkText("Zarezerwuj"));

            driver.Quit();
        }

        /*Test sprawdzaj�cy czy formularz nie przepu�ci daty startowej wcze�niejszej od teraz
        i daty ko�cowej wcze�niejszej od startowej*/
        [Fact]
        public void ReservationDateValidatorTest()
        {
            ChromeDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://localhost:7296");

            //Wybranie roweru i przej�cie do rezerwacji
            driver.FindElement(By.PartialLinkText("Rowery")).Click();
            driver.FindElement(By.PartialLinkText("Szczeg�y")).Click();
            driver.FindElement(By.PartialLinkText("Zarezerwuj")).Click();

            //Logowanie na konto admina
            driver.FindElement(By.Id("Input_Email")).SendKeys("admin@ath.eu");
            driver.FindElement(By.Id("Input_Password")).SendKeys("Az123456$");
            driver.FindElement(By.Id("login-submit")).Click();

            //Podanie daty startowej wcze�niejszej od teraz
            var startDate = driver.FindElement(By.Id("StartDate"));
            startDate.SendKeys("25102022");
            startDate.SendKeys(Keys.Tab);
            startDate.SendKeys("1000AM");
            var endDate = driver.FindElement(By.Id("EndDate"));
            endDate.SendKeys("30102023");
            endDate.SendKeys(Keys.Tab);
            endDate.SendKeys("1000AM");
            driver.FindElement(By.Id("create")).Click();
            Assert.Equal("Stw�rz rezerwacje - ATH Bike Rental System", driver.Title);

            //Podanie daty ko�cowej wcze�niejszej od startowej
            startDate = driver.FindElement(By.Id("StartDate"));
            startDate.SendKeys("25102023");
            startDate.SendKeys(Keys.Tab);
            startDate.SendKeys("1000AM");
            endDate = driver.FindElement(By.Id("EndDate"));
            endDate.SendKeys("24102023");
            endDate.SendKeys(Keys.Tab);
            endDate.SendKeys("1000AM");
            driver.FindElement(By.Id("create")).Click();
            Assert.Equal("Stw�rz rezerwacje - ATH Bike Rental System", driver.Title);

            driver.Quit();
        }
    }
}