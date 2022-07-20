using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace RahulShetty
{
    public class E2E
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

        public void EndtoEndFlow()
        {
            IWebElement logIn = driver.FindElement(By.Name("username"));
            logIn.SendKeys("rahulshettyacademy");
            IWebElement password = driver.FindElement(By.XPath("//*[@name='password']"));
            password.SendKeys("learning");
            IWebElement terms = driver.FindElement(By.Id("terms"));
            terms.Click();
            IWebElement logInButton = driver.FindElement(By.XPath("//*[@type='submit']"));
            logInButton.Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.PartialLinkText("Checkout")));


            String[] expectedProducts = { "iphone X", "Blackberry" };
            String[] actualProducts = new string[2];

            IList<IWebElement> products = driver.FindElements(By.TagName("app-card"));

            foreach ( IWebElement product  in products)
            {
                if(expectedProducts.Contains(product.FindElement(By.CssSelector(".card-title a")).Text))
                {
                    //click  Add to cart
                    product.FindElement(By.CssSelector(".card-footer button")).Click();

                }

                TestContext.Progress.WriteLine(product.FindElement(By.CssSelector(".card-title a")).Text);


            }            
            
            IWebElement checkoutBtn = driver.FindElement(By.XPath("//a[@class='nav-link btn btn-primary']"));
            checkoutBtn.Click();

            
            //Assert if Expected Products are equal to Actual Products
            
            IList<IWebElement> checkOutCards = driver.FindElements(By.CssSelector("h4 a"));

            for(int i = 0; i <checkOutCards.Count; i++ )
            {

                actualProducts[i] = checkOutCards[i].Text;

            }

            Assert.AreEqual(expectedProducts, actualProducts);
           
            //Check OUT

            IWebElement finalCheckoutBtn = driver.FindElement(By.CssSelector("button[class='btn btn-success']"));
            finalCheckoutBtn.Click();



            IWebElement addressTextBox = driver.FindElement(By.XPath("//input[@id='country']"));
            addressTextBox.SendKeys("ind");
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.LinkText("India")));
            driver.FindElement(By.LinkText("India")).Click();


            IWebElement iAgreeTerms = driver.FindElement(By.XPath("//label[@for='checkbox2']"));
            iAgreeTerms.Click();


            IWebElement purchaseBtn = driver.FindElement(By.XPath("//input[@value='Purchase']"));
            purchaseBtn.Click();


            WebDriverWait waitSuccess = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            waitSuccess.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='alert alert-success alert-dismissible']")));
            
            //Assert 
            String successLabel = driver.FindElement(By.XPath("//div[@class='alert alert-success alert-dismissible']")).Text;
            StringAssert.Contains("Success", successLabel);
            TestContext.Progress.WriteLine(successLabel);
            

            

           
            



        }


        [TearDown]
        public void TearDown()
        {

        }

    }
}
