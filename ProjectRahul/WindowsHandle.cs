using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace RahulShetty
{
    internal class WindowsHandle
    {
        IWebDriver driver;
        [SetUp]


        public void StartBrowser()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Manage().Window.Maximize();
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise";



        }

        [Test]
        public void WindowHandle()
        {
            String email = "mentor@rahulshettyacademy.com";
            String parentWindowID = driver.CurrentWindowHandle;

            driver.FindElement(By.CssSelector(".blinkingText")).Click();

            //Assert if we got 2 windows  tab open
            Assert.AreEqual(2, driver.WindowHandles.Count);

            //Switch from current Windows Tab  to another  new Windows Tab
            String childWindowName = driver.WindowHandles[1];            
            driver.SwitchTo().Window(childWindowName);

            // Asser via Test Console]            
            TestContext.Progress.WriteLine(driver.FindElement(By.CssSelector(".red")).Text);

            // splitting text
            String text = driver.FindElement(By.CssSelector(".red")).Text;
            
            String[] splittedText= text.Split("at");            
            String[] trimmedString = splittedText[1].Trim().Split(" ");
            
            //Assertion 
            
            Assert.AreEqual(email, trimmedString[0]);
            TestContext.Progress.WriteLine(trimmedString[0]);

            // GO back to the First window and  use the String to log in
            driver.SwitchTo().Window(parentWindowID);
            driver.FindElement(By.Id("username")).SendKeys(trimmedString[0]);

        }


    }
}
