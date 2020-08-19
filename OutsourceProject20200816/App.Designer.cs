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
            _etherProcessor.Dispose();
            _eProcessorYesterday?.Dispose();
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
            this.label2 = new System.Windows.Forms.Label();
            this.txtXX = new System.Windows.Forms.TextBox();
            this.lblMaxPriceToday = new System.Windows.Forms.Label();
            this.lblMaxPriceYesterday = new System.Windows.Forms.Label();
            this.lblMaxPricePerBlocks = new System.Windows.Forms.Label();
            this.lblEScanPerBlocks = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbbPerBlocks = new System.Windows.Forms.ComboBox();
            this.btnResetEthermine = new System.Windows.Forms.Button();
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
            this.lblEScanToday.Location = new System.Drawing.Point(12, 113);
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
            this.btnUpdateEScanYesterday.Location = new System.Drawing.Point(569, 65);
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
            this.lblEScanYesterday.Location = new System.Drawing.Point(566, 113);
            this.lblEScanYesterday.Name = "lblEScanYesterday";
            this.lblEScanYesterday.Size = new System.Drawing.Size(83, 17);
            this.lblEScanYesterday.TabIndex = 8;
            this.lblEScanYesterday.Text = "Không chạy";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(909, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 17);
            this.label2.TabIndex = 9;
            this.label2.Text = "Hệ số XX";
            // 
            // txtXX
            // 
            this.txtXX.Location = new System.Drawing.Point(912, 88);
            this.txtXX.Name = "txtXX";
            this.txtXX.Size = new System.Drawing.Size(100, 22);
            this.txtXX.TabIndex = 10;
            this.txtXX.Text = "0";
            this.txtXX.TextChanged += new System.EventHandler(this.txtXX_TextChanged);
            // 
            // lblMaxPriceToday
            // 
            this.lblMaxPriceToday.AutoSize = true;
            this.lblMaxPriceToday.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaxPriceToday.ForeColor = System.Drawing.Color.Blue;
            this.lblMaxPriceToday.Location = new System.Drawing.Point(12, 91);
            this.lblMaxPriceToday.Name = "lblMaxPriceToday";
            this.lblMaxPriceToday.Size = new System.Drawing.Size(85, 17);
            this.lblMaxPriceToday.TabIndex = 11;
            this.lblMaxPriceToday.Text = "Giá max: 0";
            // 
            // lblMaxPriceYesterday
            // 
            this.lblMaxPriceYesterday.AutoSize = true;
            this.lblMaxPriceYesterday.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaxPriceYesterday.ForeColor = System.Drawing.Color.Blue;
            this.lblMaxPriceYesterday.Location = new System.Drawing.Point(566, 91);
            this.lblMaxPriceYesterday.Name = "lblMaxPriceYesterday";
            this.lblMaxPriceYesterday.Size = new System.Drawing.Size(85, 17);
            this.lblMaxPriceYesterday.TabIndex = 12;
            this.lblMaxPriceYesterday.Text = "Giá max: 0";
            // 
            // lblMaxPricePerBlocks
            // 
            this.lblMaxPricePerBlocks.AutoSize = true;
            this.lblMaxPricePerBlocks.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaxPricePerBlocks.ForeColor = System.Drawing.Color.Blue;
            this.lblMaxPricePerBlocks.Location = new System.Drawing.Point(276, 122);
            this.lblMaxPricePerBlocks.Name = "lblMaxPricePerBlocks";
            this.lblMaxPricePerBlocks.Size = new System.Drawing.Size(85, 17);
            this.lblMaxPricePerBlocks.TabIndex = 14;
            this.lblMaxPricePerBlocks.Text = "Giá max: 0";
            // 
            // lblEScanPerBlocks
            // 
            this.lblEScanPerBlocks.AutoSize = true;
            this.lblEScanPerBlocks.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEScanPerBlocks.Location = new System.Drawing.Point(276, 144);
            this.lblEScanPerBlocks.Name = "lblEScanPerBlocks";
            this.lblEScanPerBlocks.Size = new System.Drawing.Size(83, 17);
            this.lblEScanPerBlocks.TabIndex = 13;
            this.lblEScanPerBlocks.Text = "Không chạy";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(276, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 17);
            this.label5.TabIndex = 15;
            this.label5.Text = "Ethermine";
            // 
            // cbbPerBlocks
            // 
            this.cbbPerBlocks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbPerBlocks.FormattingEnabled = true;
            this.cbbPerBlocks.Items.AddRange(new object[] {
            "10",
            "20",
            "50",
            "100",
            "150",
            "200"});
            this.cbbPerBlocks.Location = new System.Drawing.Point(279, 95);
            this.cbbPerBlocks.Name = "cbbPerBlocks";
            this.cbbPerBlocks.Size = new System.Drawing.Size(109, 24);
            this.cbbPerBlocks.TabIndex = 16;
            this.cbbPerBlocks.SelectedValueChanged += new System.EventHandler(this.cbbPerBlocks_SelectedValueChanged);
            // 
            // btnResetEthermine
            // 
            this.btnResetEthermine.Location = new System.Drawing.Point(363, 65);
            this.btnResetEthermine.Name = "btnResetEthermine";
            this.btnResetEthermine.Size = new System.Drawing.Size(113, 23);
            this.btnResetEthermine.TabIndex = 17;
            this.btnResetEthermine.Text = "Reset";
            this.btnResetEthermine.UseVisualStyleBackColor = true;
            this.btnResetEthermine.Click += new System.EventHandler(this.btnResetEthermine_Click);
            // 
            // App
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1055, 450);
            this.Controls.Add(this.btnResetEthermine);
            this.Controls.Add(this.cbbPerBlocks);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblMaxPricePerBlocks);
            this.Controls.Add(this.lblEScanPerBlocks);
            this.Controls.Add(this.lblMaxPriceYesterday);
            this.Controls.Add(this.lblMaxPriceToday);
            this.Controls.Add(this.txtXX);
            this.Controls.Add(this.label2);
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
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtXX;
        private System.Windows.Forms.Label lblMaxPriceToday;
        private System.Windows.Forms.Label lblMaxPriceYesterday;
        private System.Windows.Forms.Label lblMaxPricePerBlocks;
        private System.Windows.Forms.Label lblEScanPerBlocks;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbbPerBlocks;
        private System.Windows.Forms.Button btnResetEthermine;
    }
}

