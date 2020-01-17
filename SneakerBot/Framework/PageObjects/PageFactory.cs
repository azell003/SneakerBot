using OpenQA.Selenium;
using System;

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
