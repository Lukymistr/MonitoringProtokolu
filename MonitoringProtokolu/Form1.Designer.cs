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
            this.radioBtnMiliSecondsPeriod = new System.Windows.Forms.RadioButton();
            this.radioBtnMinsPeriod = new System.Windows.Forms.RadioButton();
            this.radioBtnSecondsPeriod = new System.Windows.Forms.RadioButton();
            this.btnExit = new System.Windows.Forms.Button();
            this.numUpDownBytesToRead = new System.Windows.Forms.NumericUpDown();
            this.lblBody = new System.Windows.Forms.Label();
            this.lblGlobalPeriod = new System.Windows.Forms.Label();
            this.numUpDownGlobalPeriod = new System.Windows.Forms.NumericUpDown();
            this.numUpDownPeriod = new System.Windows.Forms.NumericUpDown();
            this.panel2 = new System.Windows.Forms.Panel();
            this.radioBtnMiliSecondsGlobalPeriod = new System.Windows.Forms.RadioButton();
            this.radioBtnMinsGlobalPeriod = new System.Windows.Forms.RadioButton();
            this.radioBtnSecondsGlobalPeriod = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownLines)).BeginInit();
            this.panelSizes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownSize)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownBytesToRead)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownGlobalPeriod)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownPeriod)).BeginInit();
            this.panel2.SuspendLayout();
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
            this.panel1.Controls.Add(this.radioBtnMiliSecondsPeriod);
            this.panel1.Controls.Add(this.radioBtnMinsPeriod);
            this.panel1.Controls.Add(this.radioBtnSecondsPeriod);
            this.panel1.Location = new System.Drawing.Point(309, 174);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(253, 31);
            this.panel1.TabIndex = 10;
            // 
            // radioBtnMiliSecondsPeriod
            // 
            this.radioBtnMiliSecondsPeriod.AutoSize = true;
            this.radioBtnMiliSecondsPeriod.Location = new System.Drawing.Point(10, 7);
            this.radioBtnMiliSecondsPeriod.Name = "radioBtnMiliSecondsPeriod";
            this.radioBtnMiliSecondsPeriod.Size = new System.Drawing.Size(89, 19);
            this.radioBtnMiliSecondsPeriod.TabIndex = 13;
            this.radioBtnMiliSecondsPeriod.TabStop = true;
            this.radioBtnMiliSecondsPeriod.Text = "milisekundy";
            this.radioBtnMiliSecondsPeriod.UseVisualStyleBackColor = true;
            // 
            // radioBtnMinsPeriod
            // 
            this.radioBtnMinsPeriod.AutoSize = true;
            this.radioBtnMinsPeriod.Location = new System.Drawing.Point(180, 7);
            this.radioBtnMinsPeriod.Name = "radioBtnMinsPeriod";
            this.radioBtnMinsPeriod.Size = new System.Drawing.Size(63, 19);
            this.radioBtnMinsPeriod.TabIndex = 12;
            this.radioBtnMinsPeriod.TabStop = true;
            this.radioBtnMinsPeriod.Text = "minuty";
            this.radioBtnMinsPeriod.UseVisualStyleBackColor = false;
            // 
            // radioBtnSecondsPeriod
            // 
            this.radioBtnSecondsPeriod.AutoSize = true;
            this.radioBtnSecondsPeriod.Location = new System.Drawing.Point(105, 7);
            this.radioBtnSecondsPeriod.Name = "radioBtnSecondsPeriod";
            this.radioBtnSecondsPeriod.Size = new System.Drawing.Size(69, 19);
            this.radioBtnSecondsPeriod.TabIndex = 11;
            this.radioBtnSecondsPeriod.TabStop = true;
            this.radioBtnSecondsPeriod.Text = "sekundy";
            this.radioBtnSecondsPeriod.UseVisualStyleBackColor = true;
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
            this.numUpDownBytesToRead.Location = new System.Drawing.Point(182, 254);
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
            this.lblBody.Size = new System.Drawing.Size(164, 15);
            this.lblBody.TabIndex = 18;
            this.lblBody.Text = "velikost těla emailu (v bytech)";
            // 
            // lblGlobalPeriod
            // 
            this.lblGlobalPeriod.AutoSize = true;
            this.lblGlobalPeriod.Location = new System.Drawing.Point(476, 80);
            this.lblGlobalPeriod.Name = "lblGlobalPeriod";
            this.lblGlobalPeriod.Size = new System.Drawing.Size(233, 15);
            this.lblGlobalPeriod.TabIndex = 19;
            this.lblGlobalPeriod.Text = "globální perioda (opakovat celé hledání za)";
            // 
            // numUpDownGlobalPeriod
            // 
            this.numUpDownGlobalPeriod.Location = new System.Drawing.Point(476, 98);
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
            // panel2
            // 
            this.panel2.Controls.Add(this.radioBtnMiliSecondsGlobalPeriod);
            this.panel2.Controls.Add(this.radioBtnMinsGlobalPeriod);
            this.panel2.Controls.Add(this.radioBtnSecondsGlobalPeriod);
            this.panel2.Location = new System.Drawing.Point(602, 98);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(253, 31);
            this.panel2.TabIndex = 14;
            // 
            // radioBtnMiliSecondsGlobalPeriod
            // 
            this.radioBtnMiliSecondsGlobalPeriod.AutoSize = true;
            this.radioBtnMiliSecondsGlobalPeriod.Location = new System.Drawing.Point(10, 7);
            this.radioBtnMiliSecondsGlobalPeriod.Name = "radioBtnMiliSecondsGlobalPeriod";
            this.radioBtnMiliSecondsGlobalPeriod.Size = new System.Drawing.Size(89, 19);
            this.radioBtnMiliSecondsGlobalPeriod.TabIndex = 13;
            this.radioBtnMiliSecondsGlobalPeriod.TabStop = true;
            this.radioBtnMiliSecondsGlobalPeriod.Text = "milisekundy";
            this.radioBtnMiliSecondsGlobalPeriod.UseVisualStyleBackColor = true;
            // 
            // radioBtnMinsGlobalPeriod
            // 
            this.radioBtnMinsGlobalPeriod.AutoSize = true;
            this.radioBtnMinsGlobalPeriod.Location = new System.Drawing.Point(180, 7);
            this.radioBtnMinsGlobalPeriod.Name = "radioBtnMinsGlobalPeriod";
            this.radioBtnMinsGlobalPeriod.Size = new System.Drawing.Size(63, 19);
            this.radioBtnMinsGlobalPeriod.TabIndex = 12;
            this.radioBtnMinsGlobalPeriod.TabStop = true;
            this.radioBtnMinsGlobalPeriod.Text = "minuty";
            this.radioBtnMinsGlobalPeriod.UseVisualStyleBackColor = false;
            // 
            // radioBtnSecondsGlobalPeriod
            // 
            this.radioBtnSecondsGlobalPeriod.AutoSize = true;
            this.radioBtnSecondsGlobalPeriod.Location = new System.Drawing.Point(105, 7);
            this.radioBtnSecondsGlobalPeriod.Name = "radioBtnSecondsGlobalPeriod";
            this.radioBtnSecondsGlobalPeriod.Size = new System.Drawing.Size(69, 19);
            this.radioBtnSecondsGlobalPeriod.TabIndex = 11;
            this.radioBtnSecondsGlobalPeriod.TabStop = true;
            this.radioBtnSecondsGlobalPeriod.Text = "sekundy";
            this.radioBtnSecondsGlobalPeriod.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(867, 328);
            this.Controls.Add(this.panel2);
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
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
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
        private RadioButton radioBtnMinsPeriod;
        private RadioButton radioBtnSecondsPeriod;
        private RadioButton radioBtnB;
        private Button btnExit;
        private NumericUpDown numUpDownBytesToRead;
        private Label lblBody;
        private Label lblGlobalPeriod;
        private NumericUpDown numUpDownGlobalPeriod;
        private NumericUpDown numUpDownPeriod;
        private RadioButton radioBtnMiliSecondsPeriod;
        private Panel panel2;
        private RadioButton radioBtnMiliSecondsGlobalPeriod;
        private RadioButton radioBtnMinsGlobalPeriod;
        private RadioButton radioBtnSecondsGlobalPeriod;
    }
}