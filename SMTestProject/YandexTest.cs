using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SCTestApp.Pages;

namespace SCTestApp.Tests
{
    [TestFixture]
    public class RaspTest
    {
        IWebDriver driver;

        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
        }

        [TestCase]
        public void SearchTest()
        {
            YandexHomePage homePage = new YandexHomePage(driver);
            homePage.Navigate();
            YandexRaspPage raspPage = homePage.GoToRaspPage();

            var sat = GetNextSaturday();
            var resultsPage = raspPage.SearchFor("Екатеринбург", "Каменск-Уральский", sat);
                       
            Assert.AreEqual(
                "Расписание транспорта и билеты на поезд, электричку и автобус из Екатеринбурга в Каменск-Уральский",
                resultsPage.GetResultsHeaderName()
            );

            Assert.AreEqual(
                sat.ToString("d MMMM, dddd").ToLower(),
                resultsPage.GetResultsHeaderDateString()
            );
        }

        /// <summary>
        /// Возвращает ближайшую следующую субботу
        /// </summary>
        private DateTime GetNextSaturday()
        {
            var d = DateTime.Now;
            if ((int)d.DayOfWeek < 6) d = d.AddDays(6 - (int)d.DayOfWeek);
            else d = d.AddDays(7);
            
            return d;
        }

        [TearDown]
        public void TearDown()
        {
            driver.Close();
            driver.Quit();
        }
    }
}
