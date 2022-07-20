using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace RahulShetty
{
    public class SortingWebTables
    {

        IWebDriver driver;
        [SetUp]

        public void StartBrowser()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Manage().Window.Maximize();
            driver.Url = "https://rahulshettyacademy.com/seleniumPractise/#/offers";



        }


        [Test]

        public void SortTable()
        {
            ArrayList sortA = new ArrayList();
            SelectElement dropdown = new SelectElement(driver.FindElement(By.Id("page-menu")));
            dropdown.SelectByValue("20");

            //Step 1 : Get all Veggies Names into array List - A
            IList<IWebElement> veggies = driver.FindElements(By.XPath("//tr/td[1]"));

            foreach(IWebElement veggie in veggies)
            {
                
                sortA.Add(veggie.Text);
               

            }


            //Step 2: Sort this array list

            foreach (String element in sortA)
            {

                TestContext.Progress.WriteLine(element);

            }


            TestContext.Progress.WriteLine("After Sorting");
            sortA.Sort();

            foreach(String element in sortA)
            {

                TestContext.Progress.WriteLine(element);

            }


            //Step 3: Go and click the Column

            IWebElement veggieColumnSort = driver.FindElement(By.CssSelector("th[aria-label*='Veg/fruit name']"));
            veggieColumnSort.Click();

            //Step 4 : Get  all Veggies Names into array List - B
            ArrayList sortB = new ArrayList();

            IList<IWebElement> sortedVeggies = driver.FindElements(By.XPath("//tr/td[1]"));

            foreach (IWebElement veggie in sortedVeggies)
            {

                sortB.Add(veggie.Text);


            }



            //arraylist A = Arrylist B

            Assert.AreEqual(sortA,sortB);



        }


        [TearDown]
        public void TearDown()
        {


        }


    }
}
