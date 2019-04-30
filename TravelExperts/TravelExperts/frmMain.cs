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
    public partial class frmMain : Form
    {

        List<string> tableNames = null; //all table names
        Products currentProd = null; // empty product
        List<int> productIds = null; // productIds
        Suppliers currentSupplier = null; // empty supplier
        List<int> supplierIds = null; // supplierIds
        Products_Suppliers currentProductSupplier = null; // empty product supplier
        List<int> productSupplierIds = null; // product supplierIds
        Packages currentPackage = null;// empty package
        List<int> packageIds = null;//package Ids
        List<Packages> packages = null;//empty pacakge list
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            LoadComboBox();
        }

        private void LoadComboBox()
        {
            tableNames = TravelExpertsDB.GetTableNames();
            if (tableNames.Count > 0) // if there are tables
            {
                cboTableNames.DataSource = tableNames;
                cboTableNames.SelectedIndex = 0; // triggers SelectedIndexChanged
            }
            else // no members
            {
                MessageBox.Show("There are no data tables. " +
                    "Add some data tables in the database, and restart the application ", "Empty Load");
                Application.Exit();
            }
        }

        private void cboTableNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tableName = cboTableNames.SelectedValue.ToString();
            if (tableName == "Products")
            {
                pnlName.Visible = true;
                pnlName2.Visible = false;
                lblId.Text = "Product Id";
                productIds = ProductsDB.GetProductsIds();

                if (productIds.Count > 0) // if there are prodcuts
                {
                    cboId.DataSource = productIds;
                    cboId.SelectedIndex = 0; // triggers SelectedIndexChanged
                }
                else // no members
                {
                    MessageBox.Show("There are no products. " +
                        "Add some products in the database, and restart the application ", "Empty Load");
                    Application.Exit();
                }
                DisplayProductData();
            }
            else if (tableName == "Suppliers")
            {
                lblId.Text = "Supplier Id";
                supplierIds = SuppliersDB.GetSuppliersIds();
                pnlName.Visible = true;
                pnlName2.Visible = false;
                if (supplierIds.Count > 0) // if there are suppliers
                {
                    cboId.DataSource = supplierIds;
                    cboId.SelectedIndex = 0; // triggers SelectedIndexChanged
                }
                else // no members
                {
                    MessageBox.Show("There are no suppliers. " +
                        "Add some supplier in the database, and restart the application ", "Empty Load");
                    Application.Exit();
                }
                DisplaySupplierData();
            }
            else if (tableName == "Products_Suppliers")
            {
                lblId.Text = "ProductSupplier Id";
                lblName.Text = "Product Name";
                lblName2.Text = "Supplier Name";
                pnlName.Visible = true;
                pnlName2.Visible = true;
                productSupplierIds = Products_SuppliersDB.GetProductSupplierIds();

                if (productSupplierIds.Count > 0) // if there are suppliers
                {
                    cboId.DataSource = productSupplierIds;
                    cboId.SelectedIndex = 0; // triggers SelectedIndexChanged
                }
                else // no members
                {
                    MessageBox.Show("There are no product_suppliers. " +
                        "Add some product_supplier in the database, and restart the application ", "Empty Load");
                    Application.Exit();
                }
                DisplayProductSupplierData();
            }
            else if (tableName == "Packages_Products_Suppliers")
            {
                lblId.Text = "Package Id";
                packageIds = Packages_Products_SuppliersDB.GetPackageIds();
                pnlName.Visible = false;
                pnlName2.Visible = false;

                if (packageIds.Count > 0) // if there are packages
                {
                    cboId.DataSource = packageIds;
                    cboId.SelectedIndex = 0; // triggers SelectedIndexChanged
                }
                else // no members
                {
                    MessageBox.Show("There are no packages with products_suppliers. " +
                        "Add some packages with product_supplier in the database, and restart the application ", "Empty Load");
                    Application.Exit();
                }
            }
            else if (tableName == "Packages")
            {
                lblId.Text = "Package Id";
                pnlName.Visible = true;
                pnlName2.Visible = false;
                packageIds = PackagesDB.GetPackageIds();
                DisplayPackages();
                if (packageIds.Count > 0) // if there are suppliers
                {
                    cboId.DataSource = packageIds;
                    cboId.SelectedIndex = 0; // triggers SelectedIndexChanged
                }
                else // no members
                {
                    MessageBox.Show("There are no packages. " +
                        "Add some packages in the database, and restart the application ", "Empty Load");
                    Application.Exit();
                }
            }
        }

        private void DisplaySupplierData()
        {
            if (supplierIds != null)
            {
                txtName.Text = currentSupplier.SupName;
                lstProductSupplierId.Items.Clear();//start with empty list box
                lstProductSupplierId.Items.Add("Id " + ": " + "Supplier Name");
                foreach (int id in supplierIds)
                {
                    Suppliers s = SuppliersDB.GetSupplierById(id);
                    lstProductSupplierId.Items.Add(s);
                }
            }
            else // null this product does not exist - need to refresh combo box
            {
                supplierIds = SuppliersDB.GetSuppliersIds();
            }
        }

        private void DisplayProductData()
        {
            if (productIds != null)
            {
                txtName.Text = currentProd.ProdName;
                lstProductSupplierId.Items.Clear();//start with empty list box
                lstProductSupplierId.Items.Add("Id " + ": " + "Product Name");
                foreach (int id in productIds)
                {
                    Products s = ProductsDB.GetProductById(id);
                    lstProductSupplierId.Items.Add(s);
                }
            }
            else // null this product does not exist - need to refresh combo box
            {
                productIds = ProductsDB.GetProductsIds();
            }
        }

        private void cboId_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tableName = cboTableNames.SelectedValue.ToString();
            if (tableName == "Products")
            {
                int selectedID = (int)cboId.SelectedValue;
                lblName.Text = "Product Name";
                try
                {
                    currentProd = ProductsDB.GetProductById(selectedID);
                    DisplayCurrentProductData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while retrieving product with selected ID: " + ex.Message,
                        ex.GetType().ToString());
                }
            }
            else if (tableName == "Suppliers")
            {
                int selectedID = (int)cboId.SelectedValue;
                lblName.Text = "Supplier Name";
                try
                {
                    currentSupplier = SuppliersDB.GetSupplierById(selectedID);
                    DisplayCurrentSupplierData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while retrieving supplier with selected ID: " + ex.Message,
                        ex.GetType().ToString());
                }
            }
            else if (tableName == "Products_Suppliers")
            {
                int selectedID = (int)cboId.SelectedValue;
                try
                {
                    currentProductSupplier = Products_SuppliersDB.GetProductSupplierById(selectedID);
                    DisplayCurrentProductSupplierData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while retrieving product supplier with selected ID: " + ex.Message,
                        ex.GetType().ToString());
                }
            }
            else if (tableName == "Packages_Products_Suppliers")
            {
                int selectedID = (int)cboId.SelectedValue;
                try
                {
                    productSupplierIds = Packages_Products_SuppliersDB.GetProductSupplierIds(selectedID);
                    DisplayCurrentPackageProductSupplierData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while retrieving package product supplier with selected ID: " + ex.Message,
                        ex.GetType().ToString());
                }
            }
            else if (tableName == "Packages")
            {
                int selectedID = (int)cboId.SelectedValue;
                lblName.Text = "Package Name";
                try
                {
                    productSupplierIds = Packages_Products_SuppliersDB.GetProductSupplierIds(selectedID);
                    DisplayCurrentPackageProductSupplierData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while retrieving package product supplier with selected ID: " + ex.Message,
                        ex.GetType().ToString());
                }
                try
                {
                    currentPackage = PackagesDB.GetPackageById(selectedID);
                    txtName.Text = currentPackage.PkgName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while retrieving package with selected ID: " + ex.Message,
                        ex.GetType().ToString());
                }
            }
        }

        private void DisplayCurrentProductSupplierData()
        {
            txtName.Text = currentProductSupplier.ProdName;
            txtName2.Text = currentProductSupplier.SupName;
        }

        private void DisplayPackages()
        {
            packages = PackagesDB.GetPackages();
            if (packages != null) // if we have product suppliers to display
            {
                lstPackages.Items.Clear();//start with empty list box
                foreach (Packages pkg in packages)
                {
                    lstPackages.Items.Add(pkg);
                }
            }
            else // null this product does not exist - need to refresh combo box
            {
                packageIds = PackagesDB.GetPackageIds();
            }
        }

        private void DisplayCurrentPackageProductSupplierData()
        {
            if (productSupplierIds != null) // if we have product suppliers to display
            {
                lstProductSupplierId.Items.Clear();//start with empty list box
                lstProductSupplierId.Items.Add("Id " + ": " + "Product Name" + ",  " + "Supplier Name");
                foreach(int id in productSupplierIds)
                {
                    Products_Suppliers ps = Products_SuppliersDB.GetProductSupplierById(id);
                    lstProductSupplierId.Items.Add(ps);
                 }
            }
            else // null this product does not exist - need to refresh combo box
            {
                packageIds = Packages_Products_SuppliersDB.GetPackageIds();
            }
        }

        private void DisplayProductSupplierData()
        {
            if (currentProductSupplier != null) // if we have product suppliers to display
            {
                lstProductSupplierId.Items.Clear();//start with empty list box
                lstProductSupplierId.Items.Add("Id " + ": " + "Product Name" + ",  " + "Supplier Name");
                foreach (int id in productSupplierIds)
                {
                    Products_Suppliers ps = Products_SuppliersDB.GetProductSupplierById(id);
                    lstProductSupplierId.Items.Add(ps);
                }
            }
            else // null this product does not exist - need to refresh combo box
            {
                productSupplierIds = Products_SuppliersDB.GetProductSupplierIds();
            }
        }

        private void DisplayCurrentSupplierData()
        {
            if (currentSupplier != null)
            {
                txtName.Text = currentSupplier.SupName;
            }
            else // null this product does not exist - need to refresh combo box
            {
                supplierIds = SuppliersDB.GetSuppliersIds();
            }
        }

        private void DisplayCurrentProductData()
        {
            if ( currentProd != null)
            {
                txtName.Text = currentProd.ProdName;
            }
            else // null this product does not exist - need to refresh combo box
            {
                productIds = ProductsDB.GetProductsIds();
            }
        }

    }
}
