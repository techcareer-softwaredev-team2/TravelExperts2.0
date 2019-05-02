using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TravelExpertsData;

namespace TravelExperts
{
    public partial class frmAddUpdateProductSupplier : Form
    {
        public frmAddUpdateProductSupplier()
        {
            InitializeComponent();
        }
        List<int> productIds = null;
        List<int> supplierIds = null;
        Products prod;
        Suppliers sup;
        public bool addProductSupplier;
        public Products_Suppliers productSupplier;
        private void frmAddUpdateProductSupplier_Load(object sender, EventArgs e)
        {
            productIds = ProductsDB.GetProductsIds();
            cboProdId.DataSource = productIds;
            supplierIds =SuppliersDB.GetSuppliersIds();
            cboSupplierId.DataSource = supplierIds;
            if (addProductSupplier)
            {
                this.Text = "Add Product Supplier";
                cboProdId.SelectedIndex = 0;
                cboSupplierId.SelectedIndex =0;
            }
            else
            {
                this.Text = "Update Product Supplier";
                this.DisplayCustomer();
            }
        }

        private void DisplayCustomer()
        {
            cboProdId.SelectedItem = productSupplier.ProductId;
            lblProdName.Text = productSupplier.ProdName;
            cboSupplierId.SelectedItem= productSupplier.SupplierId;
            lblSupName.Text = productSupplier.SupName;
        }

        private void cboProdId_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedID = (int)cboProdId.SelectedValue;
            prod = ProductsDB.GetProductById(selectedID);
            lblProdName.Text = prod.ProdName;
        }

        private void cboSupplierId_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedID = (int)cboSupplierId.SelectedValue;
            sup = SuppliersDB.GetSupplierById(selectedID);
            lblSupName.Text = sup.SupName;
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (addProductSupplier)
            {
                productSupplier = new Products_Suppliers();
                this.PutProductSupplierData(productSupplier);
                try
                {
                    productSupplier.ProductSupplierId = Products_SuppliersDB.AddProductSupplier(productSupplier);
                    this.DialogResult = DialogResult.OK;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().ToString());
                }
            }
            else
            {
                Products_Suppliers newProductSupplier = new Products_Suppliers();
                newProductSupplier.ProductSupplierId = productSupplier.ProductSupplierId;
                this.PutProductSupplierData(newProductSupplier);
                try
                {
                    if (!Products_SuppliersDB.UpdateProductSupplier(productSupplier, newProductSupplier))
                    {
                        MessageBox.Show("Another user has updated or " +
                            "deleted that product supplier.", "Database Error");
                        this.DialogResult = DialogResult.Retry;
                    }
                    else
                    {
                        productSupplier = newProductSupplier;
                        this.DialogResult = DialogResult.OK;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().ToString());
                }

            }
        }

        private void PutProductSupplierData(Products_Suppliers productSupplier)
        {
            productSupplier.ProductId = (int)cboProdId.SelectedValue;
            productSupplier.SupplierId = (int)cboSupplierId.SelectedValue;
        }
    }
}
