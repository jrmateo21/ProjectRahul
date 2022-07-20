using NUnit.Framework;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections;
using System.Threading;
using WebDriverManager.DriverConfigs.Impl;

namespace RahulShetty
{
    public class Tests
    {

        IWebDriver driver;


        [SetUp]
        public void Setup()
        {
            //WebDriverManager to download the latest chrome.exe and use it automatically instead of  downloading/installing new version everytime your chrome gets the updated version
            //chromedriver.exe  used for chrome browser
            //geckodriver used for Firefox
            //edgedriver.exe used for Edge 
            //Implicit wait  can be declared globally. Use implicit wait rather thread.sleep();


            


            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            //new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
            //new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());

            


            // set driver as new chromeDriver
            driver = new ChromeDriver();
            //driver = new FirefoxDriver();
            //driver = new EdgeDriver();

            //implicit wait 3 secs
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            driver.Manage().Window.Maximize();
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise";
            TestContext.Progress.WriteLine(driver.Title);TestContext.Progress.WriteLine(driver.Title);
            TestContext.Progress.WriteLine(driver.Url);





        }

        [Test]
        public void Test1()
        {

            //IWebElement logIn = driver.FindElement(By.XPath("//*[@name='login']"));
            IWebElement logIn = driver.FindElement(By.Name("username"));
            logIn.SendKeys("rahulshettyacademy");
            IWebElement password = driver.FindElement(By.XPath("//*[@name='password']"));
            password.SendKeys("learning");


            IWebElement terms = driver.FindElement(By.Id("terms"));
            terms.Click();



            //css  selector
            // tagname[attribute = 'value']
            //IWebElement logInButton = driver.FindElement(By.CssSelector("[type='submit']"));
            //logInButton.Click();


            //xpath selector
            //  //tagname[@attribute = 'value']


            // Dropdown 

            IWebElement dropDown = driver.FindElement(By.CssSelector("select.form-control"));
            SelectElement selectType = new SelectElement(dropDown);
            // selectType.SelectByText("Teacher");
            //selectType.SelectByValue("consult");
            selectType.SelectByIndex(1);


            // Radio Button

            IList rdos = driver.FindElements(By.CssSelector("input[type='radio']"));

            foreach ( IWebElement radioButtonList  in rdos)
            {
                if (radioButtonList.GetAttribute("value").Equals("user"))
                    {
                         radioButtonList.Click();

                    }
                
            }


            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("okayBtn")));
            IWebElement okButton = driver.FindElement(By.Id("okayBtn"));
            okButton.Click();

            Boolean result = driver.FindElement(By.Id("usertype")).Selected;
            //Assert.That(result, Is.True);

           
            






            // Login Button

            IWebElement logInButton = driver.FindElement(By.XPath("//*[@type='submit']"));
            logInButton.Click();



            

            //Assertion

            //WebDriverWait wait = new WebDriverWait(driver,TimeSpan.FromSeconds(8)); // use for explicit wait 
            //IWebElement warning = driver.FindElement(By.XPath("//span[@class='label label-warning']"));  //WORKING
            //wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//span[@class='label label-warning']")));  //WORKING


            // String logInStatus = driver.FindElement(By.XPath("//*[@class ='nav-link disabled']")).Text;
            // TestContext.Progress.WriteLine(logInStatus);
            // String expectedResult = "Hi, Buggy";
            // Assert.AreEqual(expectedResult, logInStatus);

            //driver.Close();

        }

        [Test]
        public void Test2()
        {
            //Console.WriteLine("Test2 ...");
            TestContext.Progress.WriteLine("TestContext--- Test 2");

        }

        [TearDown]

        public void  CloseBrowser()
        {


        }
    }
}