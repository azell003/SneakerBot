using System;
using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SneakerBot.Framework;
using SneakerBot.Framework.PageObjects;

// Logic for botting on http://sneakersnstuff.com
namespace SneakerBot.Pages
{
    class SneakersNStuff : PageBase, IPageObject
    {
        private IWebDriver webDriver;

        public SneakersNStuff(IWebDriver driver) : base(driver)
        {
            webDriver = driver;
        }

        public void AddToCart(string shoeSize)
        {
            ReadOnlyCollection<IWebElement> sizes = webDriver.FindElements(By.ClassName("product-sizes__label"));
            foreach (IWebElement size in sizes)
            {
                string text = size.Text;
                if (text.Trim().Equals("US " + shoeSize))
                {
                    size.Click();
                }
            }

            webDriver.FindElement(By.ClassName("product-form__btn")).Click();
        }

        public void Execute(Profile profile)
        {
            if (ElementExistsByClassName("countdown"))
            {
                Browser.TakeScreenshot(webDriver, "countdownFound.png");
                IWebElement days = webDriver.FindElement(By.XPath("//div[contains(@class, 'countdown__item--days')]/span[1]"));
                IWebElement hours = webDriver.FindElement(By.XPath("//div[contains(@class, 'countdown__item--hours')]/span[1]"));
                IWebElement mins = webDriver.FindElement(By.XPath("//div[contains(@class, 'countdown__item--minutes')]/span[1]"));
                IWebElement secs = webDriver.FindElement(By.XPath("//div[contains(@class, 'countdown__item--seconds')]/span[1]"));

                Console.WriteLine(string.Format("{0} days {1} hours {2} minutes {3} seconds", days.Text, hours.Text, mins.Text, secs.Text));

                while (int.Parse(days.Text) != 0 || int.Parse(hours.Text) != 0 || int.Parse(mins.Text) != 0 || int.Parse(secs.Text) != 0)
                {
                    System.Threading.Thread.Sleep(1000);
                }
                System.Threading.Thread.Sleep(500);
                Browser.TakeScreenshot(webDriver, "countdownExpired.png");
            }
            else
            {
                Browser.TakeScreenshot(webDriver, "noSizesFound.png");
                Console.WriteLine("No Countdown found. Proceeding to check if shoe sizes are available...");
            }

            AddToCart(profile.shoeSize);

            System.Threading.Thread.Sleep(5000);
            Browser.TakeScreenshot(webDriver, "addedToCart.png");

            webDriver.SwitchTo().ActiveElement();
            IWebElement checkoutButton = webDriver.FindElement(By.ClassName("mini-cart__btn"));
            WebDriverWait wait = new WebDriverWait(webDriver, new TimeSpan(0, 0, 5));
            wait.Until((d) =>
            {
                var ev = d.FindElement(By.ClassName("mini-cart__btn"));
                return ev.Displayed;
            });
            checkoutButton.Click();
        }
    }
}
