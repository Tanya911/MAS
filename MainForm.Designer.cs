namespace MAS
{
    partial class DESMainForm
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
            this.buttonStart = new System.Windows.Forms.Button();
            this.richTextBoxCA = new System.Windows.Forms.RichTextBox();
            this.numericUpDownTime = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownH = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownW = new System.Windows.Forms.NumericUpDown();
            this.groupBoxCAP = new System.Windows.Forms.GroupBox();
            this.checkBoxInterOutput = new System.Windows.Forms.CheckBox();
            this.checkBoxCognitive = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownVirRate = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownInhRate = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonClear = new System.Windows.Forms.Button();
            this.groupBoxParamViruses = new System.Windows.Forms.GroupBox();
            this.numericUpDownLifetime = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.checkBoxRndInfection = new System.Windows.Forms.CheckBox();
            this.numericUpDownInfectionRate = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBoxParamInhabitants = new System.Windows.Forms.GroupBox();
            this.checkBoxRndRecovery = new System.Windows.Forms.CheckBox();
            this.numericUpDownRecoveryRate = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.pb_info = new System.Windows.Forms.ProgressBar();
            this.cb_const_viruses_nmb = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownW)).BeginInit();
            this.groupBoxCAP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVirRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInhRate)).BeginInit();
            this.groupBoxParamViruses.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLifetime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInfectionRate)).BeginInit();
            this.groupBoxParamInhabitants.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRecoveryRate)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(12, 1128);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(378, 63);
            this.buttonStart.TabIndex = 1;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.button1_Click);
            // 
            // richTextBoxCA
            // 
            this.richTextBoxCA.Location = new System.Drawing.Point(12, 13);
            this.richTextBoxCA.Name = "richTextBoxCA";
            this.richTextBoxCA.Size = new System.Drawing.Size(1111, 1036);
            this.richTextBoxCA.TabIndex = 5;
            this.richTextBoxCA.Text = "";
            // 
            // numericUpDownTime
            // 
            this.numericUpDownTime.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownTime.Location = new System.Drawing.Point(223, 196);
            this.numericUpDownTime.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownTime.Name = "numericUpDownTime";
            this.numericUpDownTime.ReadOnly = true;
            this.numericUpDownTime.Size = new System.Drawing.Size(120, 38);
            this.numericUpDownTime.TabIndex = 6;
            this.numericUpDownTime.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // numericUpDownH
            // 
            this.numericUpDownH.Location = new System.Drawing.Point(223, 144);
            this.numericUpDownH.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownH.Name = "numericUpDownH";
            this.numericUpDownH.Size = new System.Drawing.Size(120, 38);
            this.numericUpDownH.TabIndex = 7;
            this.numericUpDownH.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownH.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numericUpDown_KeyPress);
            // 
            // numericUpDownW
            // 
            this.numericUpDownW.Location = new System.Drawing.Point(223, 92);
            this.numericUpDownW.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownW.Name = "numericUpDownW";
            this.numericUpDownW.Size = new System.Drawing.Size(120, 38);
            this.numericUpDownW.TabIndex = 8;
            this.numericUpDownW.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownW.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numericUpDown_KeyPress);
            // 
            // groupBoxCAP
            // 
            this.groupBoxCAP.Controls.Add(this.checkBoxInterOutput);
            this.groupBoxCAP.Controls.Add(this.checkBoxCognitive);
            this.groupBoxCAP.Controls.Add(this.label5);
            this.groupBoxCAP.Controls.Add(this.numericUpDownTime);
            this.groupBoxCAP.Controls.Add(this.label2);
            this.groupBoxCAP.Controls.Add(this.label1);
            this.groupBoxCAP.Controls.Add(this.numericUpDownW);
            this.groupBoxCAP.Controls.Add(this.numericUpDownH);
            this.groupBoxCAP.Location = new System.Drawing.Point(1144, 24);
            this.groupBoxCAP.Name = "groupBoxCAP";
            this.groupBoxCAP.Size = new System.Drawing.Size(367, 347);
            this.groupBoxCAP.TabIndex = 9;
            this.groupBoxCAP.TabStop = false;
            this.groupBoxCAP.Text = "Cellular automata parameters";
            // 
            // checkBoxInterOutput
            // 
            this.checkBoxInterOutput.AutoSize = true;
            this.checkBoxInterOutput.Location = new System.Drawing.Point(23, 294);
            this.checkBoxInterOutput.Name = "checkBoxInterOutput";
            this.checkBoxInterOutput.Size = new System.Drawing.Size(298, 36);
            this.checkBoxInterOutput.TabIndex = 17;
            this.checkBoxInterOutput.Text = "intermediate output";
            this.checkBoxInterOutput.UseVisualStyleBackColor = true;
            // 
            // checkBoxCognitive
            // 
            this.checkBoxCognitive.AutoSize = true;
            this.checkBoxCognitive.Location = new System.Drawing.Point(24, 252);
            this.checkBoxCognitive.Name = "checkBoxCognitive";
            this.checkBoxCognitive.Size = new System.Drawing.Size(314, 36);
            this.checkBoxCognitive.TabIndex = 16;
            this.checkBoxCognitive.Text = "cognitive inhabitants";
            this.checkBoxCognitive.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 198);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 32);
            this.label5.TabIndex = 15;
            this.label5.Text = "Time";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 146);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 32);
            this.label2.TabIndex = 10;
            this.label2.Text = "Height";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 32);
            this.label1.TabIndex = 9;
            this.label1.Text = "Width";
            // 
            // numericUpDownVirRate
            // 
            this.numericUpDownVirRate.DecimalPlaces = 2;
            this.numericUpDownVirRate.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.numericUpDownVirRate.Location = new System.Drawing.Point(223, 79);
            this.numericUpDownVirRate.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            131072});
            this.numericUpDownVirRate.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.numericUpDownVirRate.Name = "numericUpDownVirRate";
            this.numericUpDownVirRate.ReadOnly = true;
            this.numericUpDownVirRate.Size = new System.Drawing.Size(120, 38);
            this.numericUpDownVirRate.TabIndex = 14;
            this.numericUpDownVirRate.Value = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            // 
            // numericUpDownInhRate
            // 
            this.numericUpDownInhRate.DecimalPlaces = 2;
            this.numericUpDownInhRate.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.numericUpDownInhRate.Location = new System.Drawing.Point(223, 70);
            this.numericUpDownInhRate.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            65536});
            this.numericUpDownInhRate.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.numericUpDownInhRate.Name = "numericUpDownInhRate";
            this.numericUpDownInhRate.ReadOnly = true;
            this.numericUpDownInhRate.Size = new System.Drawing.Size(120, 38);
            this.numericUpDownInhRate.TabIndex = 13;
            this.numericUpDownInhRate.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(136, 64);
            this.label4.TabIndex = 12;
            this.label4.Text = "The rate \r\nof viruses\r\n";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(186, 64);
            this.label3.TabIndex = 11;
            this.label3.Text = "The rate \r\nof inhabitants";
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(409, 1128);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(378, 63);
            this.buttonClear.TabIndex = 10;
            this.buttonClear.Text = "Clear output box";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // groupBoxParamViruses
            // 
            this.groupBoxParamViruses.Controls.Add(this.cb_const_viruses_nmb);
            this.groupBoxParamViruses.Controls.Add(this.numericUpDownLifetime);
            this.groupBoxParamViruses.Controls.Add(this.label8);
            this.groupBoxParamViruses.Controls.Add(this.checkBoxRndInfection);
            this.groupBoxParamViruses.Controls.Add(this.numericUpDownVirRate);
            this.groupBoxParamViruses.Controls.Add(this.label4);
            this.groupBoxParamViruses.Controls.Add(this.numericUpDownInfectionRate);
            this.groupBoxParamViruses.Controls.Add(this.label6);
            this.groupBoxParamViruses.Location = new System.Drawing.Point(1144, 393);
            this.groupBoxParamViruses.Name = "groupBoxParamViruses";
            this.groupBoxParamViruses.Size = new System.Drawing.Size(367, 457);
            this.groupBoxParamViruses.TabIndex = 11;
            this.groupBoxParamViruses.TabStop = false;
            this.groupBoxParamViruses.Text = "Parameters of viruses";
            // 
            // numericUpDownLifetime
            // 
            this.numericUpDownLifetime.Location = new System.Drawing.Point(223, 320);
            this.numericUpDownLifetime.Maximum = new decimal(new int[] {
            150,
            0,
            0,
            0});
            this.numericUpDownLifetime.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownLifetime.Name = "numericUpDownLifetime";
            this.numericUpDownLifetime.ReadOnly = true;
            this.numericUpDownLifetime.Size = new System.Drawing.Size(120, 38);
            this.numericUpDownLifetime.TabIndex = 18;
            this.numericUpDownLifetime.UseWaitCursor = true;
            this.numericUpDownLifetime.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(17, 322);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(116, 32);
            this.label8.TabIndex = 17;
            this.label8.Text = "Lifetime";
            // 
            // checkBoxRndInfection
            // 
            this.checkBoxRndInfection.AutoSize = true;
            this.checkBoxRndInfection.Location = new System.Drawing.Point(23, 234);
            this.checkBoxRndInfection.Name = "checkBoxRndInfection";
            this.checkBoxRndInfection.Size = new System.Drawing.Size(331, 68);
            this.checkBoxRndInfection.TabIndex = 16;
            this.checkBoxRndInfection.Text = "Random intection rate\r\nfor each instances";
            this.checkBoxRndInfection.UseVisualStyleBackColor = true;
            this.checkBoxRndInfection.CheckedChanged += new System.EventHandler(this.checkBoxRndInfection_CheckedChanged);
            // 
            // numericUpDownInfectionRate
            // 
            this.numericUpDownInfectionRate.DecimalPlaces = 2;
            this.numericUpDownInfectionRate.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.numericUpDownInfectionRate.Location = new System.Drawing.Point(223, 149);
            this.numericUpDownInfectionRate.Maximum = new decimal(new int[] {
            95,
            0,
            0,
            131072});
            this.numericUpDownInfectionRate.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.numericUpDownInfectionRate.Name = "numericUpDownInfectionRate";
            this.numericUpDownInfectionRate.ReadOnly = true;
            this.numericUpDownInfectionRate.Size = new System.Drawing.Size(120, 38);
            this.numericUpDownInfectionRate.TabIndex = 15;
            this.numericUpDownInfectionRate.Value = new decimal(new int[] {
            2,
            0,
            0,
            65536});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 151);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(147, 64);
            this.label6.TabIndex = 14;
            this.label6.Text = "Infection \r\nprobability";
            // 
            // groupBoxParamInhabitants
            // 
            this.groupBoxParamInhabitants.Controls.Add(this.checkBoxRndRecovery);
            this.groupBoxParamInhabitants.Controls.Add(this.numericUpDownRecoveryRate);
            this.groupBoxParamInhabitants.Controls.Add(this.label7);
            this.groupBoxParamInhabitants.Controls.Add(this.label3);
            this.groupBoxParamInhabitants.Controls.Add(this.numericUpDownInhRate);
            this.groupBoxParamInhabitants.Location = new System.Drawing.Point(1148, 866);
            this.groupBoxParamInhabitants.Name = "groupBoxParamInhabitants";
            this.groupBoxParamInhabitants.Size = new System.Drawing.Size(367, 323);
            this.groupBoxParamInhabitants.TabIndex = 12;
            this.groupBoxParamInhabitants.TabStop = false;
            this.groupBoxParamInhabitants.Text = "Parameters of inhabitants";
            // 
            // checkBoxRndRecovery
            // 
            this.checkBoxRndRecovery.AutoSize = true;
            this.checkBoxRndRecovery.Location = new System.Drawing.Point(23, 235);
            this.checkBoxRndRecovery.Name = "checkBoxRndRecovery";
            this.checkBoxRndRecovery.Size = new System.Drawing.Size(338, 68);
            this.checkBoxRndRecovery.TabIndex = 17;
            this.checkBoxRndRecovery.Text = "Random recovery rate \r\nfor each instance";
            this.checkBoxRndRecovery.UseVisualStyleBackColor = true;
            this.checkBoxRndRecovery.CheckedChanged += new System.EventHandler(this.checkBoxRndRecovery_CheckedChanged);
            // 
            // numericUpDownRecoveryRate
            // 
            this.numericUpDownRecoveryRate.DecimalPlaces = 2;
            this.numericUpDownRecoveryRate.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.numericUpDownRecoveryRate.Location = new System.Drawing.Point(223, 156);
            this.numericUpDownRecoveryRate.Maximum = new decimal(new int[] {
            95,
            0,
            0,
            131072});
            this.numericUpDownRecoveryRate.Name = "numericUpDownRecoveryRate";
            this.numericUpDownRecoveryRate.ReadOnly = true;
            this.numericUpDownRecoveryRate.Size = new System.Drawing.Size(120, 38);
            this.numericUpDownRecoveryRate.TabIndex = 16;
            this.numericUpDownRecoveryRate.Value = new decimal(new int[] {
            20,
            0,
            0,
            131072});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(25, 143);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(147, 64);
            this.label7.TabIndex = 15;
            this.label7.Text = "Recovery \r\nprobability";
            // 
            // pb_info
            // 
            this.pb_info.Location = new System.Drawing.Point(12, 1070);
            this.pb_info.Name = "pb_info";
            this.pb_info.Size = new System.Drawing.Size(1111, 37);
            this.pb_info.TabIndex = 13;
            // 
            // cb_const_viruses_nmb
            // 
            this.cb_const_viruses_nmb.AutoSize = true;
            this.cb_const_viruses_nmb.Location = new System.Drawing.Point(23, 374);
            this.cb_const_viruses_nmb.Name = "cb_const_viruses_nmb";
            this.cb_const_viruses_nmb.Size = new System.Drawing.Size(277, 68);
            this.cb_const_viruses_nmb.TabIndex = 19;
            this.cb_const_viruses_nmb.Text = "Constant number \r\nof viruses";
            this.cb_const_viruses_nmb.UseVisualStyleBackColor = true;
            // 
            // DESMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1527, 1201);
            this.Controls.Add(this.pb_info);
            this.Controls.Add(this.groupBoxParamInhabitants);
            this.Controls.Add(this.groupBoxParamViruses);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.richTextBoxCA);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.groupBoxCAP);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DESMainForm";
            this.Text = "DES";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownW)).EndInit();
            this.groupBoxCAP.ResumeLayout(false);
            this.groupBoxCAP.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVirRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInhRate)).EndInit();
            this.groupBoxParamViruses.ResumeLayout(false);
            this.groupBoxParamViruses.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLifetime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInfectionRate)).EndInit();
            this.groupBoxParamInhabitants.ResumeLayout(false);
            this.groupBoxParamInhabitants.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRecoveryRate)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.RichTextBox richTextBoxCA;
        private System.Windows.Forms.NumericUpDown numericUpDownTime;
        private System.Windows.Forms.NumericUpDown numericUpDownH;
        private System.Windows.Forms.NumericUpDown numericUpDownW;
        private System.Windows.Forms.GroupBox groupBoxCAP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDownInhRate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numericUpDownVirRate;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.CheckBox checkBoxCognitive;
        private System.Windows.Forms.CheckBox checkBoxInterOutput;
        private System.Windows.Forms.GroupBox groupBoxParamViruses;
        private System.Windows.Forms.CheckBox checkBoxRndInfection;
        private System.Windows.Forms.NumericUpDown numericUpDownInfectionRate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBoxParamInhabitants;
        private System.Windows.Forms.CheckBox checkBoxRndRecovery;
        private System.Windows.Forms.NumericUpDown numericUpDownRecoveryRate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numericUpDownLifetime;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ProgressBar pb_info;
        private System.Windows.Forms.CheckBox cb_const_viruses_nmb;
    }
}

