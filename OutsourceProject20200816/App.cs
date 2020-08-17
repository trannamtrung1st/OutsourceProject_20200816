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

        private ValueTuple<double, double> GetArgs()
        {
            var xx = double.Parse(txtXX.Text);
            return (_wtmProcessor.WTM, xx);
        }

        private void InitEScan()
        {
            _eProcessor = new EtherscanProcessor(GetArgs)
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
            _eProcessorYesterday = new EtherscanProcessor(GetArgs)
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

        private void OnEScanCalculated(string res, double maxPrice)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                lblEScanToday.Text = res;
                lblMaxPriceToday.Text = $"Giá max: {maxPrice:N5}";
            }));
        }

        private void OnEScanYesterdayCalculated(string res, double maxPrice)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                this.lblEScanYesterday.Text = res;
                lblMaxPriceYesterday.Text = $"Giá max: {maxPrice:N5}";
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
                lblMaxPriceYesterday.Text = $"Giá max: {_eProcessor.GetCurrentMaxPrice():N5}";
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

        private void txtXX_TextChanged(object sender, EventArgs e)
        {
            var mPText = txtXX.Text;
            double mP;
            if (string.IsNullOrWhiteSpace(mPText) || !double.TryParse(mPText, out mP))
            {
                mP = 0;
                txtXX.Text = mP.ToString();
            }
        }
    }
}
