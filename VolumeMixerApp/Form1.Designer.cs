namespace VolumeMixerApp
{
    partial class MixForm
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
            this.InstructionsLabel = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.AppLabel1 = new System.Windows.Forms.Label();
            this.AppLabel2 = new System.Windows.Forms.Label();
            this.AppLabel3 = new System.Windows.Forms.Label();
            this.comboBoxPorts = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.StopButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.searchBox1 = new System.Windows.Forms.ComboBox();
            this.searchBox2 = new System.Windows.Forms.ComboBox();
            this.searchBox3 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // InstructionsLabel
            // 
            this.InstructionsLabel.AutoSize = true;
            this.InstructionsLabel.Location = new System.Drawing.Point(22, 23);
            this.InstructionsLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.InstructionsLabel.Name = "InstructionsLabel";
            this.InstructionsLabel.Size = new System.Drawing.Size(203, 13);
            this.InstructionsLabel.TabIndex = 1;
            this.InstructionsLabel.Text = "Select the serial port used by the Arduino.";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(135, 153);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 4;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(262, 153);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 21);
            this.comboBox2.TabIndex = 5;
            // 
            // comboBox3
            // 
            this.comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(389, 153);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(121, 21);
            this.comboBox3.TabIndex = 6;
            // 
            // AppLabel1
            // 
            this.AppLabel1.AutoSize = true;
            this.AppLabel1.Location = new System.Drawing.Point(159, 110);
            this.AppLabel1.Name = "AppLabel1";
            this.AppLabel1.Size = new System.Drawing.Size(68, 13);
            this.AppLabel1.TabIndex = 7;
            this.AppLabel1.Text = "Application 1";
            // 
            // AppLabel2
            // 
            this.AppLabel2.AutoSize = true;
            this.AppLabel2.Location = new System.Drawing.Point(287, 110);
            this.AppLabel2.Name = "AppLabel2";
            this.AppLabel2.Size = new System.Drawing.Size(68, 13);
            this.AppLabel2.TabIndex = 8;
            this.AppLabel2.Text = "Application 2";
            // 
            // AppLabel3
            // 
            this.AppLabel3.AutoSize = true;
            this.AppLabel3.Location = new System.Drawing.Point(412, 110);
            this.AppLabel3.Name = "AppLabel3";
            this.AppLabel3.Size = new System.Drawing.Size(68, 13);
            this.AppLabel3.TabIndex = 9;
            this.AppLabel3.Text = "Application 3";
            // 
            // comboBoxPorts
            // 
            this.comboBoxPorts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPorts.FormattingEnabled = true;
            this.comboBoxPorts.Location = new System.Drawing.Point(294, 20);
            this.comboBoxPorts.Name = "comboBoxPorts";
            this.comboBoxPorts.Size = new System.Drawing.Size(61, 21);
            this.comboBoxPorts.TabIndex = 10;
            this.comboBoxPorts.SelectedIndexChanged += new System.EventHandler(this.comboBoxPorts_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(233, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Serial Port";
            // 
            // StopButton
            // 
            this.StopButton.Location = new System.Drawing.Point(280, 185);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(75, 23);
            this.StopButton.TabIndex = 12;
            this.StopButton.Text = "Stop";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 56);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(413, 26);
            this.label2.TabIndex = 13;
            this.label2.Text = "Select what application each slider will control. \r\nYou can select from a list of" +
    " common applications (e.g. Chrome) to narrow your search.\r\n";
            // 
            // searchBox1
            // 
            this.searchBox1.FormattingEnabled = true;
            this.searchBox1.Location = new System.Drawing.Point(135, 126);
            this.searchBox1.Name = "searchBox1";
            this.searchBox1.Size = new System.Drawing.Size(121, 21);
            this.searchBox1.TabIndex = 14;
            this.searchBox1.SelectedIndexChanged += new System.EventHandler(this.SearchBox1_SelectedIndexChanged);
            // 
            // searchBox2
            // 
            this.searchBox2.FormattingEnabled = true;
            this.searchBox2.Location = new System.Drawing.Point(262, 126);
            this.searchBox2.Name = "searchBox2";
            this.searchBox2.Size = new System.Drawing.Size(121, 21);
            this.searchBox2.TabIndex = 15;
            // 
            // searchBox3
            // 
            this.searchBox3.FormattingEnabled = true;
            this.searchBox3.Location = new System.Drawing.Point(389, 126);
            this.searchBox3.Name = "searchBox3";
            this.searchBox3.Size = new System.Drawing.Size(121, 21);
            this.searchBox3.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 129);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Common Applications";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 156);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "Running Applications";
            // 
            // MixForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(538, 217);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.searchBox3);
            this.Controls.Add(this.searchBox2);
            this.Controls.Add(this.searchBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxPorts);
            this.Controls.Add(this.AppLabel3);
            this.Controls.Add(this.AppLabel2);
            this.Controls.Add(this.AppLabel1);
            this.Controls.Add(this.comboBox3);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.InstructionsLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "MixForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "Volume Mixer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label InstructionsLabel;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label AppLabel1;
        private System.Windows.Forms.Label AppLabel2;
        private System.Windows.Forms.Label AppLabel3;
        private System.Windows.Forms.ComboBox comboBoxPorts;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox searchBox1;
        private System.Windows.Forms.ComboBox searchBox2;
        private System.Windows.Forms.ComboBox searchBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}

