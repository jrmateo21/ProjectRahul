using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSeleniumFramework.PageObjects
{
    public class LoginPage
    {

        private IWebDriver driver;

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver,this);

        }

        //Page Object Factory
        [FindsBy(How = How.Id, Using = "username")]
        private IWebElement username;

        [FindsBy(How = How.Name, Using = "password")]
        private IWebElement password;

        [FindsBy(How = How.Id, Using = "terms")]
        private IWebElement terms;

        [FindsBy(How = How.XPath, Using = "//*[@type='submit']")]
        private IWebElement signInButton;



        public ProductsPage validLogin(string user, string pass)
        {
            username.SendKeys(user);
            password.SendKeys(pass);
            terms.Click();
            signInButton.Click();
            return new ProductsPage(driver);


        }
        

        
        
       

    }
}
