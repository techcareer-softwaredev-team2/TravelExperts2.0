namespace TravelExperts
{
    partial class frmAddUpdateProductSupplier
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
            this.lblProdId = new System.Windows.Forms.Label();
            this.cboProdId = new System.Windows.Forms.ComboBox();
            this.lblSupId = new System.Windows.Forms.Label();
            this.cboSupplierId = new System.Windows.Forms.ComboBox();
            this.lblProdName = new System.Windows.Forms.Label();
            this.lblSupName = new System.Windows.Forms.Label();
            this.btnAccept = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblProdId
            // 
            this.lblProdId.AutoSize = true;
            this.lblProdId.Location = new System.Drawing.Point(24, 47);
            this.lblProdId.Name = "lblProdId";
            this.lblProdId.Size = new System.Drawing.Size(56, 13);
            this.lblProdId.TabIndex = 0;
            this.lblProdId.Text = "Product Id";
            // 
            // cboProdId
            // 
            this.cboProdId.FormattingEnabled = true;
            this.cboProdId.Location = new System.Drawing.Point(86, 44);
            this.cboProdId.Name = "cboProdId";
            this.cboProdId.Size = new System.Drawing.Size(84, 21);
            this.cboProdId.TabIndex = 1;
            this.cboProdId.SelectedIndexChanged += new System.EventHandler(this.cboProdId_SelectedIndexChanged);
            // 
            // lblSupId
            // 
            this.lblSupId.AutoSize = true;
            this.lblSupId.Location = new System.Drawing.Point(24, 84);
            this.lblSupId.Name = "lblSupId";
            this.lblSupId.Size = new System.Drawing.Size(57, 13);
            this.lblSupId.TabIndex = 2;
            this.lblSupId.Text = "Supplier Id";
            // 
            // cboSupplierId
            // 
            this.cboSupplierId.FormattingEnabled = true;
            this.cboSupplierId.Location = new System.Drawing.Point(87, 81);
            this.cboSupplierId.Name = "cboSupplierId";
            this.cboSupplierId.Size = new System.Drawing.Size(84, 21);
            this.cboSupplierId.TabIndex = 3;
            this.cboSupplierId.SelectedIndexChanged += new System.EventHandler(this.cboSupplierId_SelectedIndexChanged);
            // 
            // lblProdName
            // 
            this.lblProdName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblProdName.Location = new System.Drawing.Point(269, 44);
            this.lblProdName.Name = "lblProdName";
            this.lblProdName.Size = new System.Drawing.Size(169, 21);
            this.lblProdName.TabIndex = 4;
            this.lblProdName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSupName
            // 
            this.lblSupName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSupName.Location = new System.Drawing.Point(269, 81);
            this.lblSupName.Name = "lblSupName";
            this.lblSupName.Size = new System.Drawing.Size(169, 21);
            this.lblSupName.TabIndex = 5;
            this.lblSupName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnAccept
            // 
            this.btnAccept.Location = new System.Drawing.Point(38, 141);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(89, 30);
            this.btnAccept.TabIndex = 6;
            this.btnAccept.Text = "&Accept";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(180, 141);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(89, 30);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(187, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Product Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(187, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Supplier Name";
            // 
            // frmAddUpdateProductSupplier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 236);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.lblSupName);
            this.Controls.Add(this.lblProdName);
            this.Controls.Add(this.cboSupplierId);
            this.Controls.Add(this.lblSupId);
            this.Controls.Add(this.cboProdId);
            this.Controls.Add(this.lblProdId);
            this.Name = "frmAddUpdateProductSupplier";
            this.Text = "frmAddUpdateProductSupplier";
            this.Load += new System.EventHandler(this.frmAddUpdateProductSupplier_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblProdId;
        private System.Windows.Forms.ComboBox cboProdId;
        private System.Windows.Forms.Label lblSupId;
        private System.Windows.Forms.ComboBox cboSupplierId;
        private System.Windows.Forms.Label lblProdName;
        private System.Windows.Forms.Label lblSupName;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}