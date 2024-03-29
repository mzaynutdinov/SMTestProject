﻿using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace SCTestApp.Pages
{
    class YandexHomePage : BasePage
    {
        [FindsBy(How = How.XPath, Using = "//a[contains(@class, 'home-link') and text() = 'Расписания']")]
        public IWebElement raspLink;

        public YandexHomePage(IWebDriver driver) : base(driver)
        {

        }

        public override void Navigate()
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
