using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneakerBot.Framework.PageObjects
{
    public static class PageFactory
    {
        public static object CreateInstance(string strFullyQualifiedName, IWebDriver driver)
        {
            Type t = Type.GetType(strFullyQualifiedName);
            return Activator.CreateInstance(t, driver);
        }
    }
}
