using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OutsourceProject20200816.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OutsourceProject20200816.Processors
{
    public class WhatToMineProcessor : IDisposable
    {
        public IWebDriver Driver { get; }
        private bool disposed = false;

        public WhatToMineProcessor()
        {
            var chromeDriverService = ChromeDriverService.CreateDefaultService();
            chromeDriverService.HideCommandPromptWindow = true;
            var options = new ChromeOptions();
            options.AddArguments("--window-size=1920,1080",
                "--disable-gpu",
                "--disable-extensions",
                "--enable-javascript",
                $"user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.125 Safari/537.36",
                "--proxy-server='direct://'",
                "--proxy-bypass-list=*",
                "--start-maximized",
                "--headless");
            Driver = new ChromeDriver(chromeDriverService, options);
        }

        public void Dispose()
        {
            disposed = true;
            Driver.Quit();
            Driver.Dispose();
        }

        public Task Loop(Action<string> onCalculated)
        {
            return Task.Run(() =>
            {
                while (!disposed)
                {
                    try
                    {
                        Start(onCalculated);
                    }
                    catch (WebDriverException ex)
                    {
                        return;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    Thread.Sleep(30000);
                }
            });
        }

        public void Start(Action<string> onCalculated)
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
        }

    }
}
