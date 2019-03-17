using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;

namespace SCTestApp.Pages
{
    public class YandexRaspPage
    {
        protected IWebDriver driver;

        /// <summary>
        /// Станция отправления
        /// </summary>
        [FindsBy(How = How.Id, Using = "from")]
        public IWebElement fromInput;

        /// <summary>
        /// Станция прибытия
        /// </summary>
        [FindsBy(How = How.Id, Using = "to")]
        public IWebElement toInput;

        /// <summary>
        /// Время 
        /// </summary>
        [FindsBy(How = How.Id, Using = "when")]
        public IWebElement whenInput;

        [FindsBy(How = How.XPath, Using = "//button[contains(@class, 'SearchForm__submit')]")]
        public IWebElement submitButton;

        /// <summary>
        /// Осуществляет поиск
        /// </summary>
        /// <param name="from">Станция отправления</param>
        /// <param name="to">Станция прибытия</param>
        /// <param name="when">Дата</param>
        /// <returns>PageObject-экземпляр страницы с результатами поиска</returns>
        public YandexRaspResultsPage SearchFor(string from, string to, DateTime when)
        {
            fromInput.Click();
            fromInput.SendKeys(from);

            toInput.Click();
            toInput.SendKeys(to);

            whenInput.Click();
            whenInput.SendKeys(when.ToString("dd.MM"));

            submitButton.Click();

            YandexRaspResultsPage p = new YandexRaspResultsPage(driver);
            p.WaitUntilLoads();
            return p;
        }

        public YandexRaspPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public virtual void WaitUntilLoads()
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(10)).
                Until(
                    drv => drv.FindElement(
                        By.XPath("//div[@class = 'Slogan']/h1[text() = 'Расписание пригородного и междугородного транспорта']")
                    )
                );
        }
    }

    public class YandexRaspResultsPage : YandexRaspPage
    {
        [FindsBy(How = How.ClassName, Using = "SearchHeader")]
        public IWebElement resultsListHeader;

        public YandexRaspResultsPage(IWebDriver driver) : base(driver)
        {

        }

        public override void WaitUntilLoads()
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(10)).
                Until(
                    drv => drv.FindElement(
                        By.ClassName("SearchHeader")
                    )
                );
        }

        public string GetResultsHeaderName()
        {
            return resultsListHeader.FindElement(By.XPath("./header[@class = 'SearchTitle']//h1/span")).Text;
        }

        public string GetResultsHeaderDateString()
        {
            return resultsListHeader.FindElement(By.XPath(".//span[@class = 'SearchTitle__subtitle']")).Text;
        }
    }
}