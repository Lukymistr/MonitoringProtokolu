namespace MonitoringProtokolu {
    partial class Form1 {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.lblName = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.lblSize = new System.Windows.Forms.Label();
            this.lblLines = new System.Windows.Forms.Label();
            this.numUpDownLines = new System.Windows.Forms.NumericUpDown();
            this.lblChoose = new System.Windows.Forms.Label();
            this.checkBoxFile = new System.Windows.Forms.CheckBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnEnd = new System.Windows.Forms.Button();
            this.panelSizes = new System.Windows.Forms.Panel();
            this.radioBtnB = new System.Windows.Forms.RadioButton();
            this.radioBtnGB = new System.Windows.Forms.RadioButton();
            this.radioBtnMB = new System.Windows.Forms.RadioButton();
            this.radioBtnKB = new System.Windows.Forms.RadioButton();
            this.numUpDownSize = new System.Windows.Forms.NumericUpDown();
            this.lblPeriod = new System.Windows.Forms.Label();
            this.lblComment = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radioBtnMins = new System.Windows.Forms.RadioButton();
            this.radioBtnSeconds = new System.Windows.Forms.RadioButton();
            this.btnExit = new System.Windows.Forms.Button();
            this.numUpDownBytesToRead = new System.Windows.Forms.NumericUpDown();
            this.lblBody = new System.Windows.Forms.Label();
            this.lblGlobalPeriod = new System.Windows.Forms.Label();
            this.numUpDownGlobalPeriod = new System.Windows.Forms.NumericUpDown();
            this.numUpDownPeriod = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownLines)).BeginInit();
            this.panelSizes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownSize)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownBytesToRead)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownGlobalPeriod)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownPeriod)).BeginInit();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(12, 25);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(133, 15);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "název souboru/adresáře";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(175, 25);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(279, 23);
            this.textBoxName.TabIndex = 1;
            // 
            // lblSize
            // 
            this.lblSize.AutoSize = true;
            this.lblSize.Location = new System.Drawing.Point(12, 95);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(47, 15);
            this.lblSize.TabIndex = 2;
            this.lblSize.Text = "velikost";
            // 
            // lblLines
            // 
            this.lblLines.AutoSize = true;
            this.lblLines.Location = new System.Drawing.Point(7, 139);
            this.lblLines.Name = "lblLines";
            this.lblLines.Size = new System.Drawing.Size(69, 15);
            this.lblLines.TabIndex = 4;
            this.lblLines.Text = "počet řádek";
            // 
            // numUpDownLines
            // 
            this.numUpDownLines.Location = new System.Drawing.Point(96, 139);
            this.numUpDownLines.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numUpDownLines.Name = "numUpDownLines";
            this.numUpDownLines.Size = new System.Drawing.Size(120, 23);
            this.numUpDownLines.TabIndex = 8;
            // 
            // lblChoose
            // 
            this.lblChoose.AutoSize = true;
            this.lblChoose.Location = new System.Drawing.Point(12, 62);
            this.lblChoose.Name = "lblChoose";
            this.lblChoose.Size = new System.Drawing.Size(119, 15);
            this.lblChoose.TabIndex = 6;
            this.lblChoose.Text = "poslat upozornění při";
            // 
            // checkBoxFile
            // 
            this.checkBoxFile.AutoSize = true;
            this.checkBoxFile.Location = new System.Drawing.Point(12, 229);
            this.checkBoxFile.Name = "checkBoxFile";
            this.checkBoxFile.Size = new System.Drawing.Size(350, 19);
            this.checkBoxFile.TabIndex = 13;
            this.checkBoxFile.Text = "načíst data ze souboru (výchozí hodnota je poslední spuštění)";
            this.checkBoxFile.UseVisualStyleBackColor = true;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(12, 293);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 14;
            this.btnStart.Text = "spustit";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnEnd
            // 
            this.btnEnd.Location = new System.Drawing.Point(96, 293);
            this.btnEnd.Name = "btnEnd";
            this.btnEnd.Size = new System.Drawing.Size(186, 23);
            this.btnEnd.TabIndex = 15;
            this.btnEnd.Text = "ukončit testování";
            this.btnEnd.UseVisualStyleBackColor = true;
            this.btnEnd.Click += new System.EventHandler(this.btnEnd_Click);
            // 
            // panelSizes
            // 
            this.panelSizes.Controls.Add(this.radioBtnB);
            this.panelSizes.Controls.Add(this.radioBtnGB);
            this.panelSizes.Controls.Add(this.radioBtnMB);
            this.panelSizes.Controls.Add(this.radioBtnKB);
            this.panelSizes.Location = new System.Drawing.Point(240, 95);
            this.panelSizes.Name = "panelSizes";
            this.panelSizes.Size = new System.Drawing.Size(214, 28);
            this.panelSizes.TabIndex = 3;
            // 
            // radioBtnB
            // 
            this.radioBtnB.AutoSize = true;
            this.radioBtnB.Location = new System.Drawing.Point(3, 3);
            this.radioBtnB.Name = "radioBtnB";
            this.radioBtnB.Size = new System.Drawing.Size(32, 19);
            this.radioBtnB.TabIndex = 4;
            this.radioBtnB.TabStop = true;
            this.radioBtnB.Text = "B";
            this.radioBtnB.UseVisualStyleBackColor = true;
            // 
            // radioBtnGB
            // 
            this.radioBtnGB.AutoSize = true;
            this.radioBtnGB.Location = new System.Drawing.Point(128, 3);
            this.radioBtnGB.Name = "radioBtnGB";
            this.radioBtnGB.Size = new System.Drawing.Size(40, 19);
            this.radioBtnGB.TabIndex = 7;
            this.radioBtnGB.TabStop = true;
            this.radioBtnGB.Text = "GB";
            this.radioBtnGB.UseVisualStyleBackColor = true;
            // 
            // radioBtnMB
            // 
            this.radioBtnMB.AutoSize = true;
            this.radioBtnMB.Location = new System.Drawing.Point(81, 3);
            this.radioBtnMB.Name = "radioBtnMB";
            this.radioBtnMB.Size = new System.Drawing.Size(43, 19);
            this.radioBtnMB.TabIndex = 6;
            this.radioBtnMB.TabStop = true;
            this.radioBtnMB.Text = "MB";
            this.radioBtnMB.UseVisualStyleBackColor = true;
            // 
            // radioBtnKB
            // 
            this.radioBtnKB.AutoSize = true;
            this.radioBtnKB.Location = new System.Drawing.Point(37, 3);
            this.radioBtnKB.Name = "radioBtnKB";
            this.radioBtnKB.Size = new System.Drawing.Size(39, 19);
            this.radioBtnKB.TabIndex = 5;
            this.radioBtnKB.TabStop = true;
            this.radioBtnKB.Text = "KB";
            this.radioBtnKB.UseVisualStyleBackColor = true;
            // 
            // numUpDownSize
            // 
            this.numUpDownSize.Location = new System.Drawing.Point(94, 98);
            this.numUpDownSize.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numUpDownSize.Name = "numUpDownSize";
            this.numUpDownSize.Size = new System.Drawing.Size(120, 23);
            this.numUpDownSize.TabIndex = 2;
            // 
            // lblPeriod
            // 
            this.lblPeriod.AutoSize = true;
            this.lblPeriod.Location = new System.Drawing.Point(12, 183);
            this.lblPeriod.Name = "lblPeriod";
            this.lblPeriod.Size = new System.Drawing.Size(47, 15);
            this.lblPeriod.TabIndex = 12;
            this.lblPeriod.Text = "perioda";
            // 
            // lblComment
            // 
            this.lblComment.AutoSize = true;
            this.lblComment.Location = new System.Drawing.Point(76, 183);
            this.lblComment.Name = "lblComment";
            this.lblComment.Size = new System.Drawing.Size(75, 15);
            this.lblComment.TabIndex = 14;
            this.lblComment.Text = "(1 soubor za)";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radioBtnMins);
            this.panel1.Controls.Add(this.radioBtnSeconds);
            this.panel1.Location = new System.Drawing.Point(309, 174);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(145, 31);
            this.panel1.TabIndex = 10;
            // 
            // radioBtnMins
            // 
            this.radioBtnMins.AutoSize = true;
            this.radioBtnMins.Location = new System.Drawing.Point(69, 7);
            this.radioBtnMins.Name = "radioBtnMins";
            this.radioBtnMins.Size = new System.Drawing.Size(63, 19);
            this.radioBtnMins.TabIndex = 12;
            this.radioBtnMins.TabStop = true;
            this.radioBtnMins.Text = "minuty";
            this.radioBtnMins.UseVisualStyleBackColor = false;
            // 
            // radioBtnSeconds
            // 
            this.radioBtnSeconds.AutoSize = true;
            this.radioBtnSeconds.Location = new System.Drawing.Point(3, 7);
            this.radioBtnSeconds.Name = "radioBtnSeconds";
            this.radioBtnSeconds.Size = new System.Drawing.Size(61, 19);
            this.radioBtnSeconds.TabIndex = 11;
            this.radioBtnSeconds.TabStop = true;
            this.radioBtnSeconds.Text = "vteřiny";
            this.radioBtnSeconds.UseVisualStyleBackColor = true;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(379, 293);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 16;
            this.btnExit.Text = "odejít";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // numUpDownBytesToRead
            // 
            this.numUpDownBytesToRead.Location = new System.Drawing.Point(139, 254);
            this.numUpDownBytesToRead.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numUpDownBytesToRead.Name = "numUpDownBytesToRead";
            this.numUpDownBytesToRead.Size = new System.Drawing.Size(120, 23);
            this.numUpDownBytesToRead.TabIndex = 17;
            // 
            // lblBody
            // 
            this.lblBody.AutoSize = true;
            this.lblBody.Location = new System.Drawing.Point(12, 254);
            this.lblBody.Name = "lblBody";
            this.lblBody.Size = new System.Drawing.Size(108, 15);
            this.lblBody.TabIndex = 18;
            this.lblBody.Text = "velikost těla emailu";
            // 
            // lblGlobalPeriod
            // 
            this.lblGlobalPeriod.AutoSize = true;
            this.lblGlobalPeriod.Location = new System.Drawing.Point(265, 254);
            this.lblGlobalPeriod.Name = "lblGlobalPeriod";
            this.lblGlobalPeriod.Size = new System.Drawing.Size(93, 15);
            this.lblGlobalPeriod.TabIndex = 19;
            this.lblGlobalPeriod.Text = "globální perioda";
            // 
            // numUpDownGlobalPeriod
            // 
            this.numUpDownGlobalPeriod.Location = new System.Drawing.Point(364, 254);
            this.numUpDownGlobalPeriod.Maximum = new decimal(new int[] {
            31556926,
            0,
            0,
            0});
            this.numUpDownGlobalPeriod.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUpDownGlobalPeriod.Name = "numUpDownGlobalPeriod";
            this.numUpDownGlobalPeriod.Size = new System.Drawing.Size(120, 23);
            this.numUpDownGlobalPeriod.TabIndex = 20;
            this.numUpDownGlobalPeriod.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numUpDownPeriod
            // 
            this.numUpDownPeriod.Location = new System.Drawing.Point(162, 183);
            this.numUpDownPeriod.Maximum = new decimal(new int[] {
            31556926,
            0,
            0,
            0});
            this.numUpDownPeriod.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUpDownPeriod.Name = "numUpDownPeriod";
            this.numUpDownPeriod.Size = new System.Drawing.Size(120, 23);
            this.numUpDownPeriod.TabIndex = 21;
            this.numUpDownPeriod.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(510, 328);
            this.Controls.Add(this.numUpDownPeriod);
            this.Controls.Add(this.numUpDownGlobalPeriod);
            this.Controls.Add(this.lblGlobalPeriod);
            this.Controls.Add(this.lblBody);
            this.Controls.Add(this.numUpDownBytesToRead);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblComment);
            this.Controls.Add(this.lblPeriod);
            this.Controls.Add(this.numUpDownSize);
            this.Controls.Add(this.panelSizes);
            this.Controls.Add(this.btnEnd);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.checkBoxFile);
            this.Controls.Add(this.lblChoose);
            this.Controls.Add(this.numUpDownLines);
            this.Controls.Add(this.lblLines);
            this.Controls.Add(this.lblSize);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.lblName);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "MonitoringProtokolu";
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownLines)).EndInit();
            this.panelSizes.ResumeLayout(false);
            this.panelSizes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownSize)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownBytesToRead)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownGlobalPeriod)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownPeriod)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label lblName;
        private TextBox textBoxName;
        private Label lblSize;
        private Label lblLines;
        private NumericUpDown numUpDownLines;
        private Label lblChoose;
        private CheckBox checkBoxFile;
        private Button btnStart;
        private Button btnEnd;
        private Panel panelSizes;
        private RadioButton radioBtnGB;
        private RadioButton radioBtnMB;
        private RadioButton radioBtnKB;
        private NumericUpDown numUpDownSize;
        private Label lblPeriod;
        private Label lblComment;
        private Panel panel1;
        private RadioButton radioBtnMins;
        private RadioButton radioBtnSeconds;
        private RadioButton radioBtnB;
        private Button btnExit;
        private NumericUpDown numUpDownBytesToRead;
        private Label lblBody;
        private Label lblGlobalPeriod;
        private NumericUpDown numUpDownGlobalPeriod;
        private NumericUpDown numUpDownPeriod;
    }
}