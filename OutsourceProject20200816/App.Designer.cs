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
            wtmProcessor.Dispose();
            eProcessor.Dispose();
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
            this.lblEScanRes = new System.Windows.Forms.Label();
            this.btnEScan = new System.Windows.Forms.Button();
            this.btnUpdateEScan = new System.Windows.Forms.Button();
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
            // lblEScanRes
            // 
            this.lblEScanRes.AutoSize = true;
            this.lblEScanRes.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEScanRes.Location = new System.Drawing.Point(12, 61);
            this.lblEScanRes.Name = "lblEScanRes";
            this.lblEScanRes.Size = new System.Drawing.Size(70, 17);
            this.lblEScanRes.TabIndex = 4;
            this.lblEScanRes.Text = "Đang xử lí";
            // 
            // btnEScan
            // 
            this.btnEScan.Location = new System.Drawing.Point(268, 32);
            this.btnEScan.Name = "btnEScan";
            this.btnEScan.Size = new System.Drawing.Size(101, 23);
            this.btnEScan.TabIndex = 5;
            this.btnEScan.Text = "Chạy lại";
            this.btnEScan.UseVisualStyleBackColor = true;
            this.btnEScan.Click += new System.EventHandler(this.btnEScan_Click);
            // 
            // btnUpdateEScan
            // 
            this.btnUpdateEScan.Location = new System.Drawing.Point(161, 32);
            this.btnUpdateEScan.Name = "btnUpdateEScan";
            this.btnUpdateEScan.Size = new System.Drawing.Size(101, 23);
            this.btnUpdateEScan.TabIndex = 6;
            this.btnUpdateEScan.Text = "Cập nhật";
            this.btnUpdateEScan.UseVisualStyleBackColor = true;
            this.btnUpdateEScan.Click += new System.EventHandler(this.btnUpdateEScan_Click);
            // 
            // App
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnUpdateEScan);
            this.Controls.Add(this.btnEScan);
            this.Controls.Add(this.lblEScanRes);
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
        private System.Windows.Forms.Label lblEScanRes;
        private System.Windows.Forms.Button btnEScan;
        private System.Windows.Forms.Button btnUpdateEScan;
    }
}

