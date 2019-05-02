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
    public partial class frmAddUpdateProducts : Form
    {
        public frmAddUpdateProducts()
        {
            InitializeComponent();
        }
        public bool addProduct;
        public Products product;

        private void frmAddUpdateProducts_Load(object sender, EventArgs e)
        {
            if (addProduct)
            {
                this.Text = "Add Product";
            }
            else
            {
                this.Text = "Update Product";
                this.DisplayProduct();
            }
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (addProduct)
            {
                product = new Products();
                this.PutProductData(product);
                try
                {
                    product.ProductId = ProductsDB.AddProduct(product);
                    this.DialogResult = DialogResult.OK;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().ToString());
                }
            }
            else
            {
                Products newProduct = new Products();
                newProduct.ProductId = product.ProductId;
                this.PutProductData(newProduct);
                try
                {
                    if (!ProductsDB.UpdateProduct(product, newProduct))
                    {
                        MessageBox.Show("Another user has updated or " +
                            "deleted that product.", "Database Error");
                        this.DialogResult = DialogResult.Retry;
                    }
                    else
                    {
                        product = newProduct;
                        this.DialogResult = DialogResult.OK;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().ToString());
                }
            }
        }

        private void DisplayProduct()
        {
            txtProdName.Text = product.ProdName;
        }


        private void PutProductData(Products prod)
        {
            prod.ProdName = txtProdName.Text;
        }
    }
}
