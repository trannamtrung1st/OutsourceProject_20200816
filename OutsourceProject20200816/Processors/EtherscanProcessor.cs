using OpenQA.Selenium;
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
        private Func<ValueTuple<double, double>> _getArgs;

        private void ResetNewDate()
        {
            ParsingDate = DateTime.Now;
            CurrentPage = 1;
            SumReward = 0;
            MeanReward = 0;
            CountBlocks = 0;
            isLastParsed = false;
            BlockIds = new SortedSet<long>();
        }

        public EtherscanProcessor(Func<ValueTuple<double, double>> getArgs)
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
            ParsingDate = DateTime.Now;
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
                            return driver.FindElements(By.XPath(Program.Config.BlockXPath));
                        });
                        foreach (var b in blocks)
                        {
                            var id = long.Parse(b.FindElement(By.XPath("td[1]")).Text);
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
                            var reward = double.Parse(rewardText);
                            CountBlocks++;
                            SumReward += reward;
                            MeanReward = SumReward / CountBlocks;
                        }
                        CurrentPage++;
                        onCalculated(GetResult(), GetCurrentMaxPrice());
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
            });
        }

        public void SaveResult()
        {
            Directory.CreateDirectory("ket-qua");
            File.WriteAllText("ket-qua\\" + DateTime.Now.ToString("dd-MM-yyyy") + ".txt", GetResult());
        }

        private double GetCurrentMaxPrice()
        {
            var args = _getArgs();
            var maxPrice = args.Item1 * (MeanReward / 2) * args.Item2;
            return maxPrice;
        }

        public string GetResult()
        {
            return
                $"Đầu: {BlockIds.Max} - Cuối: {BlockIds.Min}\n" +
                $"Tổng reward: {SumReward:N5}\n" +
                $"Trung bình reward: {MeanReward:N5}\n" +
                $"Tổng số block: {CountBlocks:N0}\n" +
                $"Dữ liệu cho ngày: {ParsingDate:dd/MM/yyyy}\n" +
                $"Cập nhật vào: {DateTime.Now:dd/MM/yyyy HH:mm:ss}\n" +
                $"---------------------------\n" +
                $"Giá max: {GetCurrentMaxPrice():N5}\n";
        }
    }
}
