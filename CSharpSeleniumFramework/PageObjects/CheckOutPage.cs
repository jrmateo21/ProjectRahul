using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSeleniumFramework.PageObjects
{
    public class CheckOutPage
    {
        IWebDriver driver;

        public CheckOutPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);

        }

        //IList<IWebElement> checkOutCards = driver.FindElements(By.CssSelector("h4 a"));

        [FindsBy(How = How.CssSelector, Using = "h4 a")]
        private IList<IWebElement> checkOutCards;

        //By.CssSelector("button[class='btn btn-success']"

        [FindsBy(How = How.CssSelector, Using = "button[class='btn btn-success']")]
        private IWebElement checkOutButton;



        public IList<IWebElement> getCards()
        {

            return checkOutCards;
        }

        public void checkOut()
        {

            checkOutButton.Click();

        }

    }


    

}


