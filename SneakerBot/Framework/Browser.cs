using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using System.Windows.Forms;
using System.Drawing;

namespace SneakerBot.Framework
{
    internal enum BrowserType { Chrome, Firefox }

    class Browser
    {
        private static readonly string ScreenshotPath = System.IO.Directory.GetCurrentDirectory() + "/Screenshots/";

        public static IWebDriver Firefox()
        {
            return new FirefoxDriver();
        }

        public static IWebDriver Chrome()
        {
            return new ChromeDriver();
        }

        public static IWebDriver SetDriver(BrowserType browserName)
        {
            string currentDirectory = System.IO.Directory.GetCurrentDirectory();
            switch (browserName)
            {
                case BrowserType.Chrome:
                    ChromeOptions chromeOpts = new ChromeOptions();
                    //options.AddArgument("--headless");
                    return new ChromeDriver(currentDirectory, chromeOpts);
                default:
                    FirefoxOptions FirefoxOpts = new FirefoxOptions();
                    //options.AddArgument("--headless");
                    return new FirefoxDriver(currentDirectory, FirefoxOpts);
            }
        }

        public static void TakeScreenshot(IWebDriver driver, string fileName)
        {            
            System.IO.Directory.CreateDirectory(ScreenshotPath);
            Screenshot scrFile = ((ITakesScreenshot)driver).GetScreenshot();
            scrFile.SaveAsFile(ScreenshotPath + fileName, ScreenshotImageFormat.Jpeg);
        }

        public static object ExecuteJSCommand(IWebDriver driver, string jsCMD)
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            object obj = jse.ExecuteScript(jsCMD);
            return obj;
        }
    }
}