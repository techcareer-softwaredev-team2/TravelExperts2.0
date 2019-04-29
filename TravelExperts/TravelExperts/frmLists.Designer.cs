namespace TravelExperts
{
    partial class frmLists
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
			this.grpSelect = new System.Windows.Forms.GroupBox();
			this.radPackages = new System.Windows.Forms.RadioButton();
			this.radProducts = new System.Windows.Forms.RadioButton();
			this.radSuppliers = new System.Windows.Forms.RadioButton();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.grpSelect.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.SuspendLayout();
			// 
			// grpSelect
			// 
			this.grpSelect.Controls.Add(this.radSuppliers);
			this.grpSelect.Controls.Add(this.radProducts);
			this.grpSelect.Controls.Add(this.radPackages);
			this.grpSelect.Location = new System.Drawing.Point(13, 13);
			this.grpSelect.Name = "grpSelect";
			this.grpSelect.Size = new System.Drawing.Size(265, 49);
			this.grpSelect.TabIndex = 0;
			this.grpSelect.TabStop = false;
			this.grpSelect.Text = "Select a View";
			// 
			// radPackages
			// 
			this.radPackages.AutoSize = true;
			this.radPackages.Location = new System.Drawing.Point(7, 19);
			this.radPackages.Name = "radPackages";
			this.radPackages.Size = new System.Drawing.Size(73, 17);
			this.radPackages.TabIndex = 0;
			this.radPackages.TabStop = true;
			this.radPackages.Text = "Packages";
			this.radPackages.UseVisualStyleBackColor = true;
			// 
			// radProducts
			// 
			this.radProducts.AutoSize = true;
			this.radProducts.Location = new System.Drawing.Point(99, 19);
			this.radProducts.Name = "radProducts";
			this.radProducts.Size = new System.Drawing.Size(67, 17);
			this.radProducts.TabIndex = 1;
			this.radProducts.TabStop = true;
			this.radProducts.Text = "Products";
			this.radProducts.UseVisualStyleBackColor = true;
			// 
			// radSuppliers
			// 
			this.radSuppliers.AutoSize = true;
			this.radSuppliers.Location = new System.Drawing.Point(191, 19);
			this.radSuppliers.Name = "radSuppliers";
			this.radSuppliers.Size = new System.Drawing.Size(68, 17);
			this.radSuppliers.TabIndex = 2;
			this.radSuppliers.TabStop = true;
			this.radSuppliers.Text = "Suppliers";
			this.radSuppliers.UseVisualStyleBackColor = true;
			// 
			// dataGridView1
			// 
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Location = new System.Drawing.Point(13, 68);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.Size = new System.Drawing.Size(420, 432);
			this.dataGridView1.TabIndex = 1;
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(449, 512);
			this.Controls.Add(this.dataGridView1);
			this.Controls.Add(this.grpSelect);
			this.Name = "frmMain";
			this.Text = "Form1";
			this.grpSelect.ResumeLayout(false);
			this.grpSelect.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.ResumeLayout(false);

        }

		#endregion

		private System.Windows.Forms.GroupBox grpSelect;
		private System.Windows.Forms.RadioButton radSuppliers;
		private System.Windows.Forms.RadioButton radProducts;
		private System.Windows.Forms.RadioButton radPackages;
		private System.Windows.Forms.DataGridView dataGridView1;
	}
}

