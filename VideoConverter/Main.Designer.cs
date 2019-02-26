namespace VideoConverter
{
    partial class Main
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
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.button2 = new MetroFramework.Controls.MetroButton();
            this.comboBox1 = new MetroFramework.Controls.MetroComboBox();
            this.htmlLabel1 = new MetroFramework.Drawing.Html.HtmlLabel();
            this.htmlLabel2 = new MetroFramework.Drawing.Html.HtmlLabel();
            this.comboBox2 = new MetroFramework.Controls.MetroComboBox();
            this.progressBar1 = new MetroFramework.Controls.MetroProgressBar();
            this.button4 = new MetroFramework.Controls.MetroButton();
            this.label3 = new MetroFramework.Drawing.Html.HtmlLabel();
            this.button3 = new MetroFramework.Controls.MetroButton();
            this.button1 = new MetroFramework.Controls.MetroButton();
            this.listView1 = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(24, 65);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(102, 30);
            this.button2.TabIndex = 0;
            this.button2.Text = "选择视频文件";
            this.button2.UseSelectable = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.ItemHeight = 23;
            this.comboBox1.Items.AddRange(new object[] {
            "极高",
            "高",
            "一般",
            "普通",
            "低"});
            this.comboBox1.Location = new System.Drawing.Point(384, 65);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(70, 29);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.UseSelectable = true;
            // 
            // htmlLabel1
            // 
            this.htmlLabel1.AutoScroll = true;
            this.htmlLabel1.AutoScrollMinSize = new System.Drawing.Size(70, 22);
            this.htmlLabel1.AutoSize = false;
            this.htmlLabel1.BackColor = System.Drawing.SystemColors.Window;
            this.htmlLabel1.Location = new System.Drawing.Point(303, 71);
            this.htmlLabel1.Name = "htmlLabel1";
            this.htmlLabel1.Size = new System.Drawing.Size(75, 23);
            this.htmlLabel1.TabIndex = 2;
            this.htmlLabel1.Text = "视频清晰度";
            // 
            // htmlLabel2
            // 
            this.htmlLabel2.AutoScroll = true;
            this.htmlLabel2.AutoScrollMinSize = new System.Drawing.Size(89, 22);
            this.htmlLabel2.AutoSize = false;
            this.htmlLabel2.BackColor = System.Drawing.SystemColors.Window;
            this.htmlLabel2.Location = new System.Drawing.Point(469, 71);
            this.htmlLabel2.Name = "htmlLabel2";
            this.htmlLabel2.Size = new System.Drawing.Size(95, 23);
            this.htmlLabel2.TabIndex = 3;
            this.htmlLabel2.Text = "CPU核心使用数";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.ItemHeight = 23;
            this.comboBox2.Location = new System.Drawing.Point(570, 65);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(70, 29);
            this.comboBox2.TabIndex = 4;
            this.comboBox2.UseSelectable = true;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(24, 288);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(616, 16);
            this.progressBar1.TabIndex = 6;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(23, 309);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(102, 30);
            this.button4.TabIndex = 7;
            this.button4.Text = "选择输出路径";
            this.button4.UseSelectable = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label3
            // 
            this.label3.AutoScroll = true;
            this.label3.AutoScrollMinSize = new System.Drawing.Size(190, 22);
            this.label3.AutoSize = false;
            this.label3.BackColor = System.Drawing.SystemColors.Window;
            this.label3.Location = new System.Drawing.Point(132, 316);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(313, 23);
            this.label3.TabIndex = 8;
            this.label3.Text = "C:\\Users\\Administrator\\Desktop";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(451, 309);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(90, 30);
            this.button3.TabIndex = 9;
            this.button3.Text = "移除选中项";
            this.button3.UseSelectable = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(558, 309);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(82, 30);
            this.button1.TabIndex = 10;
            this.button1.Text = "开始转码";
            this.button1.UseSelectable = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listView1
            // 
            this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listView1.Location = new System.Drawing.Point(23, 102);
            this.listView1.MaximumSize = new System.Drawing.Size(617, 180);
            this.listView1.MinimumSize = new System.Drawing.Size(617, 180);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(617, 180);
            this.listView1.TabIndex = 11;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.ColumnWidthChange);
            this.listView1.SizeChanged += new System.EventHandler(this.FormMain_SizeChanged);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 350);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.htmlLabel2);
            this.Controls.Add(this.htmlLabel1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(664, 350);
            this.MinimumSize = new System.Drawing.Size(664, 350);
            this.Name = "Main";
            this.Text = "上业科技视频转码工具";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroButton button2;
        private MetroFramework.Controls.MetroComboBox comboBox1;
        private MetroFramework.Drawing.Html.HtmlLabel htmlLabel1;
        private MetroFramework.Drawing.Html.HtmlLabel htmlLabel2;
        private MetroFramework.Controls.MetroComboBox comboBox2;
        private MetroFramework.Controls.MetroProgressBar progressBar1;
        private MetroFramework.Controls.MetroButton button4;
        private MetroFramework.Drawing.Html.HtmlLabel label3;
        private MetroFramework.Controls.MetroButton button3;
        private MetroFramework.Controls.MetroButton button1;
        private System.Windows.Forms.ListView listView1;
    }
}