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
    public class EtherscanProcessor
    {
        public DateTime ParsingDate { get; set; }
        public int CurrentPage { get; set; } = 1;
        public SortedSet<long> BlockIds { get; set; }
        public double SumReward { get; set; } = 0;
        public double MeanReward { get; set; } = 0;
        public long CountBlocks { get; set; } = 0;
        public IWebDriver Driver { get; }
        public bool Disposed { get; private set; } = false;
        private bool isLastParsed = false;
        public bool IsToday { get; set; }
        private Func<ValueTuple<double, double, int?>> _getArgs;

        private void ResetNewDate()
        {
            ParsingDate = DateTime.Now.Subtract(TimeSpan.FromHours(7));
            CurrentPage = 1;
            SumReward = 0;
            MeanReward = 0;
            CountBlocks = 0;
            isLastParsed = false;
            BlockIds = new SortedSet<long>();
        }

        public EtherscanProcessor(Func<ValueTuple<double, double, int?>> getArgs)
        {
            _getArgs = getArgs;
            BlockIds = new SortedSet<long>();
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
            IsToday = true;
            ParsingDate = DateTime.Now.Subtract(TimeSpan.FromHours(7));
        }

        public void Dispose()
        {
            Disposed = true;
            Driver.Quit();
            Driver.Dispose();
        }

        public Task Start(Action<string, double> onCalculated)
        {
            return Task.Run(() =>
            {
                while (!Disposed)
                {
                    try
                    {
                        var url = Program.Config.EtherscanUrl + $"?ps=100&p={CurrentPage}";
                        Driver.Navigate().GoToUrl(url);
                        var source = Driver.PageSource;
                        var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
                        wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                        var blocks = wait.Until(driver =>
                        {
                            var bls = driver.FindElements(By.XPath(Program.Config.BlockXPath));
                            return bls.Count > 0 ? bls : null;
                        });
                        var cont = true;
                        if (!IsToday)
                        {
                            var lastBlocks = blocks.Last();
                            var id = long.Parse(lastBlocks.FindElement(By.XPath("td[1]")).Text, Program.GlobalCulture);
                            var dateStr = lastBlocks.FindElement(By.XPath("td[2]")).GetAttribute("textContent");
                            var utcDate = DateTime.ParseExact(dateStr, "yyyy-MM-dd H:m:s",
                                CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);
                            var localDate = utcDate.ToLocalTime();
                            if (localDate >= ParsingDate.Date.AddDays(1).AddHours(7)) cont = false;
                        }
                        if (cont)
                            foreach (var b in blocks)
                            {
                                var id = long.Parse(b.FindElement(By.XPath("td[1]")).Text, Program.GlobalCulture);
                                var dateStr = b.FindElement(By.XPath("td[2]")).GetAttribute("textContent");
                                var utcDate = DateTime.ParseExact(dateStr, "yyyy-MM-dd H:m:s",
                                    CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);
                                var localDate = utcDate.ToLocalTime();
                                if (localDate >= ParsingDate.Date.AddDays(1).AddHours(7)) continue;
                                if (localDate < ParsingDate.Date.AddHours(7))
                                {
                                    isLastParsed = true;
                                    SaveResult();
                                    CurrentPage = 0;
                                    break;
                                }
                                if (IsToday && ParsingDate.Date.AddDays(1).AddHours(7) < DateTime.Now)
                                {
                                    SaveResult();
                                    ResetNewDate();
                                    CurrentPage = 0;
                                    break;
                                }
                                if (BlockIds.Contains(id))
                                {
                                    if (!isLastParsed) continue;
                                    CurrentPage = 0;
                                    break;
                                }
                                BlockIds.Add(id);
                                var rewardText = b.FindElement(By.XPath("td[10]")).Text.Split(' ')[0];
                                var reward = double.Parse(rewardText, Program.GlobalCulture);
                                CountBlocks++;
                                SumReward += reward;
                                MeanReward = SumReward / CountBlocks;
                            }
                        CurrentPage++;
                        onCalculated(GetResult(), GetCurrentMaxPrice(MeanReward));
                        if (isLastParsed)
                            if (IsToday)
                            {
                                SaveResult();
                                Thread.Sleep(10000);
                            }
                            else
                            {
                                SaveResult();
                                Dispose();
                                return;
                            }
                    }
                    catch (WebDriverException ex)
                    {
                        if (!ex.Message.Contains("Timed out"))
                            return;
                        Thread.Sleep(10000);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        Thread.Sleep(10000);
                    }
                }
            });
        }

        public void SaveResult()
        {
            Directory.CreateDirectory("ket-qua");
            File.WriteAllText("ket-qua\\" + DateTime.Now.ToString("dd-MM-yyyy") + ".txt", GetResult());
        }

        public double GetCurrentMaxPrice(double mean)
        {
            var args = _getArgs();
            var maxPrice = args.Item1 * (mean / 2) * args.Item2;
            return maxPrice;
        }

        public string GetResult()
        {
            return
                $"Đầu: {BlockIds.LastOrDefault()} - Cuối: {BlockIds.FirstOrDefault()}\n" +
                $"Tổng reward: {SumReward.ToString("N5", Program.GlobalCulture)}\n" +
                $"Trung bình reward: {MeanReward.ToString("N5", Program.GlobalCulture)}\n" +
                $"Tổng số block: {CountBlocks.ToString("N0", Program.GlobalCulture)}\n" +
                $"Dữ liệu cho ngày: {ParsingDate:dd/MM/yyyy}\n" +
                $"Cập nhật vào: {DateTime.Now:dd/MM/yyyy HH:mm:ss}\n" +
                $"---------------------------\n" +
                $"Giá max: {GetCurrentMaxPrice(MeanReward).ToString("N5", Program.GlobalCulture)}\n";
        }

    }

}
