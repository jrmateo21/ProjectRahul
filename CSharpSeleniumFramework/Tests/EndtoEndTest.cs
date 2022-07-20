using CSharpSeleniumFramework.PageObjects;
using CSharpSeleniumFramework.Utilities;
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
    //run all test files in project parallel;
    [Parallelizable(ParallelScope.Children)]
    public class E2ETest : Base
    {

       

        [Test,Category("Smoke")]
        //Option 1 using TestCase

        //[TestCase("rahulshettyacademy", "learning")]
        //[TestCase("rahulshettyacademy", "learning2")]

        //Option 2 using TestCaseSource

       [TestCaseSource("AddTestDataConfig")]


        //Option 3 using TestCaseSource with json file

        //run all data sets of Test Method in paraller ;

        //run all test files in project parallel;  [Parallelizable(ParallelScope.Children)] - > Before Public Cl ,ass

        //run all test methods in one class parallel ;
        //[Parallelizable(ParallelScope.All)]

        // Run test using Terminal 
        //1. Run all the test in the entire Project  =  C:\Users\JR.Mateo\MVP\Practice Task\ProjectRahul\ProjectRahul\CSharpSeleniumFramework> dotnet test .\CSharpSeleniumFramework.cspro
        //2. Run designated test in a projecy using [Test,Category("Smoke")]  = PS C:\Users\JR.Mateo\MVP\Practice Task\ProjectRahul\ProjectRahul\CSharpSeleniumFramework> dotnet test .\CSharpSeleniumFramework.csproj --filter TestCategory=Smoke

        public void EndtoEndFlow(String username, String password, String[] expectedProducts)
        {
           // String[] expectedProducts = { "iphone X", "Blackberry" };
            String[] actualProducts = new string[2];
           
            LoginPage loginPagePageObj = new LoginPage(getDriver());
            ProductsPage productsPageObj =loginPagePageObj.validLogin(username,password);
            productsPageObj.waitForPageDisplay();           

            
            //Product Page
            IList<IWebElement> products = productsPageObj.getCards();

            foreach (IWebElement product in products)
            {
                if (expectedProducts.Contains(product.FindElement(productsPageObj.getCardTitle()).Text))
                {
                    //click  Add to cart
                    product.FindElement(productsPageObj.getAddToCartButton()).Click();

                }

                TestContext.Progress.WriteLine(product.FindElement(productsPageObj.getAddToCartButton()).Text);


            }


            CheckOutPage checkOutPageObj = productsPageObj.checkOut();
            //Assert if Expected Products are equal to Actual Products

            IList<IWebElement> checkOutCards = checkOutPageObj.getCards();

            for (int i = 0; i < checkOutCards.Count; i++)
            {

                actualProducts[i] = checkOutCards[i].Text;

            }
            Assert.AreEqual(expectedProducts, actualProducts);


            //Check OUT Button
            checkOutPageObj.checkOut();


            //Purchase Page

           // IWebElement addressTextBox = driver.FindElement(By.XPath("//input[@id='country']"));
           // addressTextBox.SendKeys("ind");
           // WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
           // wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.LinkText("India")));
           //driver.FindElement(By.LinkText("India")).Click();

            PurchasePage purchasePageObj = new PurchasePage(getDriver());
            purchasePageObj.getAddress();
            purchasePageObj.waitCountryToDisplay();



            //IWebElement iAgreeTerms = driver.FindElement(By.XPath("//label[@for='checkbox2']"));
            //iAgreeTerms.Click();

            purchasePageObj.iAgree();



            //IWebElement purchaseBtn = driver.FindElement(By.XPath("//input[@value='Purchase']"));
            //purchaseBtn.Click();

            purchasePageObj.purchase();


            //WebDriverWait waitSuccess = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            //waitSuccess.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='alert alert-success alert-dismissible']")));

            purchasePageObj.waitUntilSuccess();

            //Assert 
            String successLabel = driver.Value.FindElement(By.XPath("//div[@class='alert alert-success alert-dismissible']")).Text;
            StringAssert.Contains("Success", successLabel);
            TestContext.Progress.WriteLine(successLabel);









        }

        public static IEnumerable<TestCaseData> AddTestDataConfig()
        {
            yield return new TestCaseData(getDataParser().extractData("username"), getDataParser().extractData("password"), getDataParser().extractDataArray("products"));
            yield return new TestCaseData(getDataParser().extractData("username_wrong"), getDataParser().extractData("password"), getDataParser().extractDataArray("products"));
            yield return new TestCaseData(getDataParser().extractData("username"), getDataParser().extractData("password_wrong"), getDataParser().extractDataArray("products"));



        }


        

    }
}
