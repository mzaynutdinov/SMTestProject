using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace SCTestApp.Pages
{
    public abstract class BasePage
    {
        protected  IWebDriver driver;

        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public virtual void Navigate()
        {

        }
    }
}
