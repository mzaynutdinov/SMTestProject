using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCTestApp.Pages
{
    class YandexHomePage
    {
        private IWebDriver driver;

        [FindsBy(How = How.XPath, Using = "//a[contains(@class, 'home-link') and text() = 'Расписания']")]
        public IWebElement raspLink;

        public YandexHomePage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public void Navigate()
        {
            driver.Navigate().GoToUrl("http://yandex.ru");
        }

        public YandexRaspPage GoToRaspPage()
        {
            raspLink.Click();
            var yrp = new YandexRaspPage(driver);
            yrp.WaitUntilLoads();
            return yrp;
        }
    }
}
