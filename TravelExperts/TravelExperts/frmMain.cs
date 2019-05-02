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

        private void DisplayProductSupplierData()
        {
            productSupplierIds = Products_SuppliersDB.GetProductSupplierIds();
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

        private void DisplaySupplierData()
        {
            supplierIds = SuppliersDB.GetSuppliersIds();
            cboId.DataSource = supplierIds;
            if (supplierIds != null)
            {
                txtName.Text = currentSupplier.SupName;
                cboId.SelectedItem = currentSupplier.SupplierId;
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
            productIds = ProductsDB.GetProductsIds();
            cboId.DataSource = productIds;
            if (productIds != null)
            {
                txtName.Text = currentProd.ProdName;
                cboId.SelectedItem = currentProd.ProductId;
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

        private void DisplayCurrentPackageProductSupplierData()
        {
            if (productSupplierIds != null) // if we have product suppliers to display
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
                packageIds = Packages_Products_SuppliersDB.GetPackageIds();
            }
        }

        private void DisplayCurrentProductSupplierData()
        {
            currentProductSupplier = Products_SuppliersDB.GetProductSupplierById(currentProductSupplier.ProductSupplierId);
            txtName.Text = currentProductSupplier.ProdName;
            txtName2.Text = currentProductSupplier.SupName;
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
            if (currentProd != null)
            {
                txtName.Text = currentProd.ProdName;
            }
            else // null this product does not exist - need to refresh combo box
            {
                productIds = ProductsDB.GetProductsIds();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string tableName = cboTableNames.SelectedValue.ToString();
            if (tableName == "Products")
            {
                frmAddUpdateProducts addProductForm = new frmAddUpdateProducts();
                addProductForm.addProduct = true;
                DialogResult result = addProductForm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    currentProd = addProductForm.product;
                    this.DisplayProductData();
                }
            }
            else if (tableName == "Suppliers")
            {
                frmAddUpdateSuppliers addSupplierForm = new frmAddUpdateSuppliers();
                addSupplierForm.addSupplier = true;
                DialogResult result = addSupplierForm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    currentSupplier = addSupplierForm.supplier;
                    this.DisplaySupplierData();
                }
            }
            else if (tableName == "Products_Suppliers")
            {
                frmAddUpdateProductSupplier addProductSupplierForm = new frmAddUpdateProductSupplier();
                addProductSupplierForm.addProductSupplier = true;
                DialogResult result = addProductSupplierForm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    currentProductSupplier = addProductSupplierForm.productSupplier;
                    this.DisplayProductSupplierData();
                }
            }
            else if (tableName == "Packages")
            {
                frmAddUpdatePackages addPackageForm = new frmAddUpdatePackages();
                addPackageForm.addPackage = true;
                DialogResult result = addPackageForm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    currentPackage = addPackageForm.package;
                    this.DisplayPackages();
                    this.DisplayProductSupplierData();
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string tableName = cboTableNames.SelectedValue.ToString();
            if (tableName == "Products")
            {
                frmAddUpdateProducts updateProductForm = new frmAddUpdateProducts();
                updateProductForm.addProduct = false;
                updateProductForm.product = currentProd;
                DialogResult result = updateProductForm.ShowDialog();
                if (result == DialogResult.OK)
                {
                   currentProd = updateProductForm.product;
                    this.DisplayProductData();
                }
                else if (result == DialogResult.Retry)
                {
                    currentProd=ProductsDB.GetProductById(currentProd.ProductId);
                    if (currentProd != null)
                        this.DisplayProductData();
                }
            }
            else if (tableName == "Suppliers")
            {
                frmAddUpdateSuppliers updateSupplierForm = new frmAddUpdateSuppliers();
                updateSupplierForm.addSupplier= false;
                updateSupplierForm.supplier = currentSupplier;
                DialogResult result = updateSupplierForm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    currentSupplier = updateSupplierForm.supplier;
                    this.DisplaySupplierData();
                }
                else if (result == DialogResult.Retry)
                {
                    currentSupplier = SuppliersDB.GetSupplierById(currentSupplier.SupplierId);
                    if (currentSupplier != null)
                        this.DisplaySupplierData();
                }
            }
            else if (tableName == "Products_Suppliers")
            {
                frmAddUpdateProductSupplier updateProductSupplierForm = new frmAddUpdateProductSupplier();
                updateProductSupplierForm.addProductSupplier = false;
                updateProductSupplierForm.productSupplier = currentProductSupplier;
                DialogResult result = updateProductSupplierForm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    this.DisplayCurrentProductSupplierData();
                    this.DisplayProductSupplierData();
                }
                else if (result == DialogResult.Retry)
                {
                    currentProductSupplier = Products_SuppliersDB.GetProductSupplierById(currentProductSupplier.ProductSupplierId);
                    if (currentProductSupplier != null)
                        this.DisplayCurrentProductSupplierData();
                        this.DisplayProductSupplierData();
                        
                }
            }
            else if (tableName == "Packages")
            {
                frmAddUpdatePackages updatePackageForm = new frmAddUpdatePackages();
                updatePackageForm.addPackage = false;
                updatePackageForm.package = currentPackage;
                updatePackageForm.currentProductSupplierIds = Packages_Products_SuppliersDB.GetProductSupplierIds(currentPackage.PackageId);
                DialogResult result = updatePackageForm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    this.DisplayPackages();
                    DisplayCurrentPackageProductSupplierData();
                }
                else if (result == DialogResult.Retry)
                {
                    currentPackage = PackagesDB.GetPackageById(currentPackage.PackageId);
                    if (currentPackage != null)
                        DisplayCurrentPackageProductSupplierData();
                    this.DisplayPackages();

                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string tableName = cboTableNames.SelectedValue.ToString();
            if(tableName=="Products")
            {
                DialogResult result = MessageBox.Show("Delete " + currentProd.ProdName + "?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        if (!ProductsDB.DeleteProduct(currentProd))
                        {
                            MessageBox.Show("Another user has updated or deleted " +
                                "that product.", "Database Error");
                            currentProd=ProductsDB.GetProductById(currentProd.ProductId);
                            if (currentProd != null)
                                this.DisplayProductData();
                        }
                        else
                            this.DisplayProductData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, ex.GetType().ToString());
                    }
                }
            }
            else if (tableName == "Suppliers")
            {
                DialogResult result = MessageBox.Show("Delete " + currentSupplier.SupName + "?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        if (!SuppliersDB.DeleteSupplier(currentSupplier))
                        {
                            MessageBox.Show("Another user has updated or deleted " +
                                "that supplier.", "Database Error");
                            currentSupplier = SuppliersDB.GetSupplierById(currentSupplier.SupplierId);
                            if (currentSupplier != null)
                                this.DisplaySupplierData();
                        }
                        else
                            this.DisplaySupplierData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, ex.GetType().ToString());
                    }
                }
            }
            else if (tableName == "Products_Suppliers")
            {
                DialogResult result = MessageBox.Show("Delete Product Supplier " + currentProductSupplier.ProductSupplierId + "?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        if (!Products_SuppliersDB.DeleteProductSupplier(currentProductSupplier))
                        {
                            MessageBox.Show("Another user has updated or deleted " +
                                "that product_supplier.", "Database Error");
                            currentProductSupplier = Products_SuppliersDB.GetProductSupplierById(currentProductSupplier.ProductSupplierId);
                            if (currentProductSupplier != null)
                                this.DisplayCurrentProductSupplierData();
                                this.DisplayProductSupplierData();
                        }
                        else
                            this.DisplayCurrentProductSupplierData();
                            this.DisplayProductSupplierData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, ex.GetType().ToString());
                    }
                }
            }
            else if (tableName == "Packages")
            {
                DialogResult result = MessageBox.Show("Delete Packages " + currentPackage.PkgName + "?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        if (!PackagesDB.DeletePackage(currentPackage))
                        {
                            MessageBox.Show("Another user has updated or deleted " +
                                "that package.", "Database Error");
                            currentPackage = PackagesDB.GetPackageById(currentPackage.PackageId);
                            if (currentPackage != null)
                                //this.DisplayCurrentProductSupplierData();
                            this.DisplayProductSupplierData();
                        }
                        else
                            //this.DisplayCurrentProductSupplierData();
                        this.DisplayPackages();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, ex.GetType().ToString());
                    }
                }
            }
        }
    }
}
