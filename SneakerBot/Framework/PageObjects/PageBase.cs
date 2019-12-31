using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneakerBot.Framework.PageObjects
{
    class PageBase
    {
        private IWebDriver webDriver;

        public PageBase(IWebDriver driver)
        {
            webDriver = driver;
        }

        public bool ElementExistsByClassName(string classname)
        {
            try
            {
                webDriver.FindElement(By.ClassName(classname));
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
