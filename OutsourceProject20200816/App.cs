using OpenQA.Selenium;
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
        private WhatToMineProcessor wtmProcessor;
        private EtherscanProcessor eProcessor;

        public App()
        {
            InitializeComponent();
            InitWTM();
            InitEScan();
        }

        private void InitWTM()
        {
            wtmProcessor = new WhatToMineProcessor();
            wtmProcessor.Start(this.OnWTMCalculated);
        }

        private void InitEScan()
        {
            eProcessor = new EtherscanProcessor();
            eProcessor.Start(this.OnEScanCalculated);
        }


        private void btnUpdateWTM_Click(object sender, EventArgs e)
        {
            try
            {
                this.lblCalResult.Text = "Đang xử lí";
                var url = wtmProcessor.Driver.Url;
                wtmProcessor.Start(this.OnWTMCalculated);
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
                this.lblEScanRes.Text = res;
            }));
        }

        private void OnWTMCalculated(string res)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                this.lblCalResult.Text = res;
            }));
        }

        private void btnEScan_Click(object sender, EventArgs e)
        {
            try
            {
                this.lblEScanRes.Text = "Đang xử lí";
                eProcessor.Dispose();
                InitEScan();
            }
            catch (WebDriverException ex)
            {
                InitEScan();
            }

        }

        private void btnUpdateEScan_Click(object sender, EventArgs e)
        {
            eProcessor.SaveResult();
            this.lblEScanRes.Text = eProcessor.GetResult();
        }
    }
}
