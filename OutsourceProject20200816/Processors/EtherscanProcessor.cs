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
        public DateTime CurrentDate = DateTime.Now;
        public int CurrentPage { get; set; } = 1;
        public SortedSet<long> BlockIds { get; set; }
        public double SumReward { get; set; } = 0;
        public double MeanReward { get; set; } = 0;
        public long CountBlocks { get; set; } = 0;
        public IWebDriver Driver { get; }
        private bool disposed = false;
        private bool isLastParsed = false;

        private void ResetNewDate()
        {
            CurrentDate = DateTime.Now;
            CurrentPage = 1;
            SumReward = 0;
            MeanReward = 0;
            CountBlocks = 0;
            isLastParsed = false;
            BlockIds = new SortedSet<long>();
        }

        public EtherscanProcessor()
        {
            BlockIds = new SortedSet<long>();
            var chromeDriverService = ChromeDriverService.CreateDefaultService();
            chromeDriverService.HideCommandPromptWindow = true;
            Driver = new ChromeDriver(chromeDriverService, new ChromeOptions());
        }

        public void Dispose()
        {
            disposed = true;
            Driver.Quit();
            Driver.Dispose();
        }

        public Task Start(Action<string> onCalculated)
        {
            return Task.Run(() =>
            {
                while (!disposed)
                {
                    try
                    {
                        var url = Program.Config.EtherscanUrl + $"?ps=100&p={CurrentPage}";
                        Driver.Navigate().GoToUrl(url);
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
                            if (localDate.Date < CurrentDate.Date || localDate.TimeOfDay.Hours < 7)
                            {
                                isLastParsed = true;
                                CurrentPage = 0;
                                SaveResult();
                                break;
                            }
                            if (CurrentDate.Date < DateTime.Now.Date)
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
                                SaveResult();
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
                        onCalculated(GetResult());
                    }
                    catch (WebDriverException ex)
                    {
                        return;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    if (isLastParsed)
                        Thread.Sleep(10000);
                }
            });
        }

        public void SaveResult()
        {
            Directory.CreateDirectory("ket-qua");
            File.WriteAllText("ket-qua\\" + DateTime.Now.ToString("dd-MM-yyyy") + ".txt", GetResult());
        }

        public string GetResult()
        {
            return
                $"Đầu: {BlockIds.Max} - Cuối: {BlockIds.Min}\n" +
                $"Tổng reward: {SumReward:N5}\n" +
                $"Trung bình reward: {MeanReward:N5}\n" +
                $"Tổng số block: {CountBlocks:N0}\n" +
                $"Cập nhật vào: {DateTime.Now:dd/MM/yyyy HH:mm:ss}";
        }
    }
}
