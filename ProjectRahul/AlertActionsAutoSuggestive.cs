using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace RahulShetty
{
    public class AlertActionsAutoSuggestive
    {

        IWebDriver driver;
        [SetUp]

        public void StartBrowser()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Manage().Window.Maximize();
            driver.Url = "https://rahulshettyacademy.com/AutomationPractice/";
           



        }


        [Test]

        public void testAlert()
        {
            String name = "Jewhy";
            IWebElement nameTextbox = driver.FindElement(By.XPath("//input[@id='name']"));
            nameTextbox.SendKeys("Jewhy");

            IWebElement submitButton = driver.FindElement(By.XPath("//input[@id='confirmbtn']"));
            submitButton.Click();

            
            //use in web alert
            string alertText = driver.SwitchTo().Alert().Text;
            driver.SwitchTo().Alert().Accept();
            //cancel the alert
            //driver.SwitchTo().Alert().Dismiss(); 

            //this will assert if the string name is present on the alertText
            StringAssert.Contains(name,alertText);


        }


        [Test]

        public void testAutoSugggestiveDropDown()
        {

            IWebElement country = driver.FindElement(By.Id("autocomplete"));
            country.SendKeys("Ind");
            Thread.Sleep(3000);
            IList<IWebElement> options = driver.FindElements(By.CssSelector(".ui-menu-item div"));

            foreach (IWebElement option in options)
            {

                if (option.Text.Equals("India"))
                {
                    option.Click();

                }

            }

            // this will help you write  - dynamically pass value   - For run time  to grab  use GetAttribute  rather Text
            TestContext.Progress.WriteLine(driver.FindElement(By.Id("autocomplete")).GetAttribute("value"));


        }


        [Test]

        public void testActions()
        {
            driver.Url = "https://www.rahulshettyacademy.com/";
            Actions action = new Actions(driver);
            action.MoveToElement(driver.FindElement(By.CssSelector("a.dropdown-toggle"))).Perform();
            

            WebDriverWait waitSuccess = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            waitSuccess.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//ul[@class='dropdown-menu']/li[1]/a")));

            //option 1 using Actions
            action.MoveToElement(driver.FindElement(By.XPath("//ul[@class='dropdown-menu']/li[1]/a"))).Click().Perform();

            //option 2
            //IWebElement aboutUs = driver.FindElement(By.XPath("//ul[@class='dropdown-menu']/li[1]/a"));
            //aboutUs.Click();

           




        }


        [Test]
        public void testDragAndDrop()
        {


            driver.Url = "https://demoqa.com/droppable";
            Actions action = new Actions(driver);
            Thread.Sleep(3000);
            action.DragAndDrop(driver.FindElement(By.Id("draggable")), driver.FindElement(By.Id("droppable"))).Perform();


        }




        [Test]

        public void  testFrames()
        {
            //scroll down you can  only use javascript to achieve this 

            IWebElement frameScroll = driver.FindElement(By.Id("courses-iframe"));
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", frameScroll);

            
            // id, name, index
            driver.SwitchTo().Frame("courses-iframe");
            IWebElement allAccessPlanTab = driver.FindElement(By.LinkText("All Access Plan"));
            allAccessPlanTab.Click();
            Thread.Sleep(1000);
            TestContext.Progress.WriteLine(driver.FindElement(By.CssSelector("h1")).Text);

            //switch back from Frame to  Default 
            driver.SwitchTo().DefaultContent();
            TestContext.Progress.WriteLine(driver.FindElement(By.CssSelector("h1")).Text);
            


        }


        
    }
}
