using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OutsourceProject20200816.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutsourceProject20200816.Processors
{
    public class WhatToMineProcessor : IDisposable
    {
        public IWebDriver Driver { get; }

        public WhatToMineProcessor()
        {
            var chromeDriverService = ChromeDriverService.CreateDefaultService();
            chromeDriverService.HideCommandPromptWindow = true;
            Driver = new ChromeDriver(chromeDriverService, new ChromeOptions());
        }

        public void Dispose()
        {
            Driver.Quit();
            Driver.Dispose();
        }

        public Task Start(Action<string> onCalculated)
        {
            return Task.Run(() =>
            {
                var url = Program.Config.WtmUrl;
                Driver.Navigate().GoToUrl(url);
                var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                try
                {
                    var brEle = wait.Until(driver =>
                    {
                        return driver.FindElement(By.XPath(Program.Config.BrXPath));
                    });
                    var brText = brEle.Text.Split('\n')[1].Split(' ')[1];
                    var brVal = double.Parse(brText);
                    var rev24hEle = Driver.FindElement(By.XPath(Program.Config.Rev24hXPath));
                    var rev24hText = rev24hEle.Text.Split('\n')[0];
                    var rev24hVal = double.Parse(rev24hText);
                    var result = rev24hVal / brVal * 2000;
                    onCalculated(result.ToString("0.000000"));
                }
                catch (WebDriverTimeoutException)
                {
                    onCalculated("Lỗi");
                }
            });
        }

    }
}
