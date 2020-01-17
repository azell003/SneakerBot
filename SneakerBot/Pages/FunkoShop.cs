using OpenQA.Selenium;
using SneakerBot.Framework;
using SneakerBot.Framework.PageObjects;

//Logic for botting on https://shop.funko.com/
namespace SneakerBot.Pages
{
    class FunkoShop : PageBase, IPageObject
    {
        private IWebDriver webDriver;

        public FunkoShop(IWebDriver driver) : base(driver)
        {
            webDriver = driver;
        }

        public void AddToCart(string quantity = "1")
        {
            webDriver.FindElement(By.Id("AddToCart-funko-product-template")).Click();

            webDriver.FindElement(By.ClassName("js-qty__input")).Click();
            webDriver.FindElement(By.ClassName("js-qty__input")).Clear();
            webDriver.FindElement(By.ClassName("js-qty__input")).SendKeys(quantity);
        }

        public void Execute(Profile profile)
        {
            AddToCart(profile.quantity);

            System.Threading.Thread.Sleep(5000);
            Browser.TakeScreenshot(webDriver, "addedToCart.png");

            IWebElement checkoutButton = webDriver.FindElement(By.XPath("//button[@name='checkout']"));
            checkoutButton.Click();

            //Contact Information
            webDriver.FindElement(By.Id("checkout_email")).SendKeys(profile.email);
            webDriver.FindElement(By.Id("checkout_shipping_address_first_name")).SendKeys(profile.firstName);
            webDriver.FindElement(By.Id("checkout_shipping_address_last_name")).SendKeys(profile.lastName);
            webDriver.FindElement(By.Id("checkout_shipping_address_address1")).SendKeys(profile.billingAddressLine1);
            webDriver.FindElement(By.Id("checkout_shipping_address_address2")).SendKeys(profile.billingAddressLine2);
            webDriver.FindElement(By.Id("checkout_shipping_address_city")).SendKeys(profile.city);
            webDriver.FindElement(By.Id("checkout_shipping_address_zip")).SendKeys(profile.zipcode);
            webDriver.FindElement(By.Id("checkout_shipping_address_first_name")).SendKeys(profile.firstName);

            //Captcha
            System.Threading.Thread.Sleep(45000);

            webDriver.FindElement(By.XPath( "//button[@class='step__footer__continue-btn btn' and @data-trekkie-id='continue_to_shipping_method_button']/span[@class='btn__content']")).Click();
            webDriver.FindElement(By.XPath( "//button[@class='step__footer__continue-btn btn' and @data-trekkie-id='continue_to_payment_method_button']/span[@class='btn__content']")).Click();

            //CC
            webDriver.FindElement(By.Id("number")).SendKeys(profile.ccNumber);
            webDriver.FindElement(By.Id("name")).SendKeys(profile.firstName + " " + profile.lastName);
            webDriver.FindElement(By.Id("expiry")).SendKeys(profile.ccExpDate);
            webDriver.FindElement(By.Id("verification_value")).SendKeys(profile.ccCvc);

            //Complete Order
            webDriver.FindElement(By.XPath("//button[@class='step__footer__continue-btn btn' and @data-trekkie-id='complete_order_button']/span[@class='btn__content']")).Click();
        }
    }
}