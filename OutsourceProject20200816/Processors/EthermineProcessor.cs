﻿using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OutsourceProject20200816.Processors
{
    public class EthermineProcessor
    {
        public int CurrentPage { get; set; } = 1;
        public SortedList<long, double> Blocks { get; set; }
        public IWebDriver Driver { get; }
        public bool Disposed { get; private set; } = false;
        private bool isLastParsed = false;
        private Func<ValueTuple<double, double, int?>> _getArgs;

        public EthermineProcessor(Func<ValueTuple<double, double, int?>> getArgs)
        {
            _getArgs = getArgs;
            Blocks = new SortedList<long, double>(new DescendingComparer());
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
            Disposed = true;
            Driver.Quit();
            Driver.Dispose();
        }

        public Task Start(Action<(string, double)> onCalculated)
        {
            return Task.Run(() =>
            {
                while (!Disposed)
                {
                    try
                    {
                        var args = _getArgs();
                        var perBlocks = args.Item3;
                        if (perBlocks == null)
                        {
                            Thread.Sleep(10000);
                            continue;
                        }
                        var url = Program.Config.EtherscanUrl + $"?ps=100&p={CurrentPage}";
                        Driver.Navigate().GoToUrl(url);
                        var source = Driver.PageSource;
                        var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
                        wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                        var blocks = wait.Until(driver =>
                        {
                            return driver.FindElements(By.XPath(Program.Config.BlockXPath));
                        });
                        foreach (var b in blocks)
                        {
                            var id = long.Parse(b.FindElement(By.XPath("td[1]")).Text);
                            if (Blocks.ContainsKey(id))
                            {
                                if (!isLastParsed) continue;
                                CurrentPage = 0;
                                break;
                            }
                            var rewardText = b.FindElement(By.XPath("td[10]")).Text.Split(' ')[0];
                            var reward = double.Parse(rewardText);
                            Blocks.Add(id, reward);
                        }
                        CurrentPage++;
                        onCalculated(GetResultPerBlocks());
                    }
                    catch (WebDriverException ex)
                    {
                        return;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        return;
                    }
                    if (isLastParsed)
                        Thread.Sleep(10000);
                }
            });
        }

        public double GetCurrentMaxPrice(double mean)
        {
            var args = _getArgs();
            var maxPrice = args.Item1 * (mean / 2) * args.Item2;
            return maxPrice;
        }

        public (string, double) GetResultPerBlocks()
        {
            var maxPriceSub = Blocks.Take(150).ToList();
            var maxPrice150 = 0.0;
            if (maxPriceSub.Count == 150)
            {
                var mean150 = maxPriceSub.Average(o => o.Value);
                maxPrice150 = GetCurrentMaxPrice(mean150);
            }

            var args = _getArgs();
            var blocks = args.Item3;
            var resStr = "Không chạy";
            if (blocks != null)
            {
                var count = Blocks.Count;
                if (count >= 200)
                {
                    isLastParsed = true;
                    if (count > 200)
                    {
                        Blocks = new SortedList<long, double>(new DescendingComparer());
                        foreach (var o in Blocks.Take(200))
                            Blocks.Add(o.Key, o.Value);
                    }
                }
                var subset = Blocks.Take(blocks.Value);
                var sum = subset.Sum(o => o.Value);
                var mean = subset.Average(o => o.Value);
                resStr =
                    $"Đầu: {subset.First().Key} - Cuối: {subset.Last().Key}\n" +
                    $"Tổng reward: {sum:N5}\n" +
                    $"Trung bình reward: {mean:N5}\n" +
                    $"Số block gần nhất: {blocks:N0}\n" +
                    $"Cập nhật vào: {DateTime.Now:dd/MM/yyyy HH:mm:ss}\n" +
                    $"---------------------------\n" +
                    $"Giá max (150): {maxPrice150:N5}\n";
            }
            return (resStr, maxPrice150);
        }
    }

    public class DescendingComparer : IComparer<long>
    {
        public int Compare(long x, long y)
        {
            return (int)(y - x);
        }
    }

}