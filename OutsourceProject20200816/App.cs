using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OutsourceProject20200816.Processors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OutsourceProject20200816
{
    public partial class App : Form
    {
        private WhatToMineProcessor _wtmProcessor;
        private EtherscanProcessor _eProcessor;
        private EtherscanProcessor _eProcessorYesterday;

        public App()
        {
            InitializeComponent();
            InitWTM();
            InitEScan();
        }

        private void InitWTM()
        {
            _wtmProcessor = new WhatToMineProcessor();
            _wtmProcessor.Loop(this.OnWTMCalculated);
        }

        private void InitEScan()
        {
            _eProcessor = new EtherscanProcessor()
            {
                //IsTodayOnly = true,
                //ParsingDate = DateTime.Now
            };
            _eProcessor.Start(this.OnEScanCalculated);
        }

        private void InitEScanYesterday()
        {
            if (_eProcessorYesterday != null) _eProcessorYesterday.Dispose();
            this.lblEScanYesterday.Text = "Đang xử lí";
            _eProcessorYesterday = new EtherscanProcessor()
            {
                IsToday = false,
                ParsingDate = DateTime.Now.Subtract(TimeSpan.FromDays(1)).Date
            };
            _eProcessorYesterday.Start(this.OnEScanYesterdayCalculated);
        }

        private void btnUpdateWTM_Click(object sender, EventArgs e)
        {
            try
            {
                this.lblCalResult.Text = "Đang xử lí";
                var url = _wtmProcessor.Driver.Url;
                Task.Run(() =>
                {
                    _wtmProcessor.Start(this.OnWTMCalculated);
                });
            }
            catch (WebDriverException ex)
            {
                InitWTM();
            }
        }

        private void OnEScanCalculated(string res)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                this.lblEScanToday.Text = res;
            }));
        }

        private void OnEScanYesterdayCalculated(string res)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                this.lblEScanYesterday.Text = res;
            }));
        }

        private void OnWTMCalculated(string res)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                this.lblCalResult.Text = res;
            }));
        }

        private void btnUpdateEScan_Click(object sender, EventArgs e)
        {
            try
            {
                if (_eProcessor.Disposed) throw new Exception("Already disposed");
                _eProcessor.SaveResult();
                this.lblEScanToday.Text = _eProcessor.GetResult();
            }
            catch (Exception)
            {
                InitEScan();
            }
        }

        private void btnUpdateEScanYesterday_Click(object sender, EventArgs e)
        {
            InitEScanYesterday();
        }
    }
}
