namespace TravelExperts
{
    partial class frmMain
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
            this.cboTableNames = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblId = new System.Windows.Forms.Label();
            this.cboId = new System.Windows.Forms.ComboBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lstProductSupplierId = new System.Windows.Forms.ListBox();
            this.lstPackages = new System.Windows.Forms.ListBox();
            this.pnlName = new System.Windows.Forms.Panel();
            this.lblName2 = new System.Windows.Forms.Label();
            this.txtName2 = new System.Windows.Forms.TextBox();
            this.pnlName2 = new System.Windows.Forms.Panel();
            this.pnlName.SuspendLayout();
            this.pnlName2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cboTableNames
            // 
            this.cboTableNames.FormattingEnabled = true;
            this.cboTableNames.Location = new System.Drawing.Point(152, 11);
            this.cboTableNames.Name = "cboTableNames";
            this.cboTableNames.Size = new System.Drawing.Size(183, 21);
            this.cboTableNames.TabIndex = 0;
            this.cboTableNames.SelectedIndexChanged += new System.EventHandler(this.cboTableNames_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select the Database";
            // 
            // lblId
            // 
            this.lblId.Location = new System.Drawing.Point(42, 43);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(104, 23);
            this.lblId.TabIndex = 2;
            this.lblId.Text = "ID";
            this.lblId.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cboId
            // 
            this.cboId.FormattingEnabled = true;
            this.cboId.Location = new System.Drawing.Point(152, 45);
            this.cboId.Name = "cboId";
            this.cboId.Size = new System.Drawing.Size(183, 21);
            this.cboId.TabIndex = 3;
            this.cboId.SelectedIndexChanged += new System.EventHandler(this.cboId_SelectedIndexChanged);
            // 
            // lblName
            // 
            this.lblName.Location = new System.Drawing.Point(7, 10);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(104, 23);
            this.lblName.TabIndex = 4;
            this.lblName.Text = "Product Name";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtName
            // 
            this.txtName.Enabled = false;
            this.txtName.Location = new System.Drawing.Point(117, 13);
            this.txtName.Name = "txtName";
            this.txtName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtName.Size = new System.Drawing.Size(182, 20);
            this.txtName.TabIndex = 5;
            // 
            // lstProductSupplierId
            // 
            this.lstProductSupplierId.FormattingEnabled = true;
            this.lstProductSupplierId.Location = new System.Drawing.Point(341, 11);
            this.lstProductSupplierId.Name = "lstProductSupplierId";
            this.lstProductSupplierId.Size = new System.Drawing.Size(289, 134);
            this.lstProductSupplierId.TabIndex = 9;
            // 
            // lstPackages
            // 
            this.lstPackages.FormattingEnabled = true;
            this.lstPackages.Location = new System.Drawing.Point(49, 162);
            this.lstPackages.Name = "lstPackages";
            this.lstPackages.Size = new System.Drawing.Size(580, 199);
            this.lstPackages.TabIndex = 10;
            // 
            // pnlName
            // 
            this.pnlName.Controls.Add(this.lblName);
            this.pnlName.Controls.Add(this.txtName);
            this.pnlName.Location = new System.Drawing.Point(36, 72);
            this.pnlName.Name = "pnlName";
            this.pnlName.Size = new System.Drawing.Size(299, 36);
            this.pnlName.TabIndex = 11;
            // 
            // lblName2
            // 
            this.lblName2.Location = new System.Drawing.Point(3, 8);
            this.lblName2.Name = "lblName2";
            this.lblName2.Size = new System.Drawing.Size(104, 23);
            this.lblName2.TabIndex = 12;
            this.lblName2.Text = "Supplier Name";
            this.lblName2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtName2
            // 
            this.txtName2.Enabled = false;
            this.txtName2.Location = new System.Drawing.Point(119, 10);
            this.txtName2.Name = "txtName2";
            this.txtName2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtName2.Size = new System.Drawing.Size(180, 20);
            this.txtName2.TabIndex = 13;
            // 
            // pnlName2
            // 
            this.pnlName2.Controls.Add(this.txtName2);
            this.pnlName2.Controls.Add(this.lblName2);
            this.pnlName2.Location = new System.Drawing.Point(36, 114);
            this.pnlName2.Name = "pnlName2";
            this.pnlName2.Size = new System.Drawing.Size(299, 33);
            this.pnlName2.TabIndex = 14;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(653, 365);
            this.Controls.Add(this.pnlName2);
            this.Controls.Add(this.pnlName);
            this.Controls.Add(this.lstPackages);
            this.Controls.Add(this.lstProductSupplierId);
            this.Controls.Add(this.cboId);
            this.Controls.Add(this.lblId);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboTableNames);
            this.Name = "frmMain";
            this.Text = "Database Management System";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.pnlName.ResumeLayout(false);
            this.pnlName.PerformLayout();
            this.pnlName2.ResumeLayout(false);
            this.pnlName2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboTableNames;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.ComboBox cboId;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.ListBox lstProductSupplierId;
        private System.Windows.Forms.ListBox lstPackages;
        private System.Windows.Forms.Panel pnlName;
        private System.Windows.Forms.Label lblName2;
        private System.Windows.Forms.TextBox txtName2;
        private System.Windows.Forms.Panel pnlName2;
    }
}

