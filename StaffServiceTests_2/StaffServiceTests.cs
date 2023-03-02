using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace StaffServiceTests_2;

public class StaffServiceTests
{
    private ChromeDriver driver;

    [Test]
    public void AuthorizationTest()
    {
        var options = new ChromeOptions();
        options.AddArguments("--no-sandbox", "--start-maximized", "--disable-extensions");

        driver = new ChromeDriver(options); // Открыть хром

        // 1. Открыть страничку 
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru/");

        // пододождать что страничка открылась/подождать что нужный элемент подгрузился (неявное ожидание)
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

        // 2. Найти поле с логином и выбрать его, чтобы курсор был там
        var login = driver.FindElement(By.Id("Username"));
        // 3. впечатать логин
        login.SendKeys("user");
        // 4. Найти поле с Пароль и выбрать его, чтобы курсор был там
        var password = driver.FindElement(By.Id("Password"));
        // 5. впечатать пароль
        password.SendKeys("1q2w3e4r%T");
        // 6. найти кнопку "войти"
        var enter = driver.FindElement(By.Name("button"));
        // 7. нажать на нее
        enter.Click();


        // 8. проверить что мы оказались на нужной страничке
        // Это можно сделать разными способами: неявное ожидание, явное ожидание элемента на странице

        var titlePageElement = driver.FindElement(By.CssSelector("[data-tid='Title']"));


        // 8.1 неявное ожидание
        // var TitlePageElement = driver.FindElement(By.CssSelector("[data-tid='Title']"));

        // 8.2 явное ожидание selenium webdriver
        // var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
        // IWebElement titleOnPage = wait.Until(driver => TitlePageElement);

        // 8.3 явное ожидание с помощью ExpectedConditions, библиотека SeleniumExtras.WaitHelpers
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
        wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("[data-tid='Title']")));
        wait.Until(ExpectedConditions.TitleIs("Новости"));
        wait.Until(ExpectedConditions.UrlToBe("https://staff-testing.testkontur.ru/news"));

        // 9.1 проверить что мы оказались на нужной страничке
        // var titleInBrowser = driver.Title;
        // Assert.AreEqual("Новости", titlePageElement.Text, "Новости не загрузились");
        // Assert.AreEqual("Новости", titleInBrowser);

        // 9.2 проверить что мы оказались на нужной страничке
        var titleInBrowser = driver.Title;
        Assert.Multiple(() =>
        {
            Assert.AreEqual("Новости", titlePageElement.Text, "Новости не загрузились");
            Assert.AreEqual("Новости", titleInBrowser);
        });
    }


    [TearDown]
    public void TearDown()
    {
        driver.Quit();
    }
}