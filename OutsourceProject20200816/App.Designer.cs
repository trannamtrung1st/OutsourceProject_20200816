namespace OutsourceProject20200816
{
    partial class App
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            //manual disposing
            _wtmProcessor.Dispose();
            _eProcessor.Dispose();
            _eProcessorYesterday.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblCalResult = new System.Windows.Forms.Label();
            this.btnUpdateWTM = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblEScanToday = new System.Windows.Forms.Label();
            this.btnUpdateEScanToday = new System.Windows.Forms.Button();
            this.btnUpdateEScanYesterday = new System.Windows.Forms.Button();
            this.lblEScanYesterday = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblCalResult
            // 
            this.lblCalResult.AutoSize = true;
            this.lblCalResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCalResult.ForeColor = System.Drawing.Color.Red;
            this.lblCalResult.Location = new System.Drawing.Point(673, 9);
            this.lblCalResult.Name = "lblCalResult";
            this.lblCalResult.Size = new System.Drawing.Size(67, 17);
            this.lblCalResult.TabIndex = 1;
            this.lblCalResult.Text = "Chưa có";
            // 
            // btnUpdateWTM
            // 
            this.btnUpdateWTM.Location = new System.Drawing.Point(536, 6);
            this.btnUpdateWTM.Name = "btnUpdateWTM";
            this.btnUpdateWTM.Size = new System.Drawing.Size(131, 23);
            this.btnUpdateWTM.TabIndex = 2;
            this.btnUpdateWTM.Text = "Cập nhật WTM";
            this.btnUpdateWTM.UseVisualStyleBackColor = true;
            this.btnUpdateWTM.Click += new System.EventHandler(this.btnUpdateWTM_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Kết quả Etherscan";
            // 
            // lblEScanToday
            // 
            this.lblEScanToday.AutoSize = true;
            this.lblEScanToday.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEScanToday.Location = new System.Drawing.Point(12, 91);
            this.lblEScanToday.Name = "lblEScanToday";
            this.lblEScanToday.Size = new System.Drawing.Size(70, 17);
            this.lblEScanToday.TabIndex = 4;
            this.lblEScanToday.Text = "Đang xử lí";
            // 
            // btnUpdateEScanToday
            // 
            this.btnUpdateEScanToday.Location = new System.Drawing.Point(15, 65);
            this.btnUpdateEScanToday.Name = "btnUpdateEScanToday";
            this.btnUpdateEScanToday.Size = new System.Drawing.Size(173, 23);
            this.btnUpdateEScanToday.TabIndex = 6;
            this.btnUpdateEScanToday.Text = "Cập nhật hôm nay";
            this.btnUpdateEScanToday.UseVisualStyleBackColor = true;
            this.btnUpdateEScanToday.Click += new System.EventHandler(this.btnUpdateEScan_Click);
            // 
            // btnUpdateEScanYesterday
            // 
            this.btnUpdateEScanYesterday.Location = new System.Drawing.Point(281, 65);
            this.btnUpdateEScanYesterday.Name = "btnUpdateEScanYesterday";
            this.btnUpdateEScanYesterday.Size = new System.Drawing.Size(205, 23);
            this.btnUpdateEScanYesterday.TabIndex = 7;
            this.btnUpdateEScanYesterday.Text = "Lấy dữ liệu hôm qua";
            this.btnUpdateEScanYesterday.UseVisualStyleBackColor = true;
            this.btnUpdateEScanYesterday.Click += new System.EventHandler(this.btnUpdateEScanYesterday_Click);
            // 
            // lblEScanYesterday
            // 
            this.lblEScanYesterday.AutoSize = true;
            this.lblEScanYesterday.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEScanYesterday.Location = new System.Drawing.Point(278, 91);
            this.lblEScanYesterday.Name = "lblEScanYesterday";
            this.lblEScanYesterday.Size = new System.Drawing.Size(83, 17);
            this.lblEScanYesterday.TabIndex = 8;
            this.lblEScanYesterday.Text = "Không chạy";
            // 
            // App
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblEScanYesterday);
            this.Controls.Add(this.btnUpdateEScanYesterday);
            this.Controls.Add(this.btnUpdateEScanToday);
            this.Controls.Add(this.lblEScanToday);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnUpdateWTM);
            this.Controls.Add(this.lblCalResult);
            this.Name = "App";
            this.Text = "App";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblCalResult;
        private System.Windows.Forms.Button btnUpdateWTM;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblEScanToday;
        private System.Windows.Forms.Button btnUpdateEScanToday;
        private System.Windows.Forms.Button btnUpdateEScanYesterday;
        private System.Windows.Forms.Label lblEScanYesterday;
    }
}

