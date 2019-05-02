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
    public partial class frmAddUpdatePackages : Form
    {
        
        public frmAddUpdatePackages()
        {
            InitializeComponent();
        }
        public bool addPackage;
        public Packages package;
        public Packages_Products_Suppliers pps=null;
        List<int> productSupplierIds = null; // product supplierIds
        public List<int> currentProductSupplierIds = null; // current package product supplierIds
        private void frmAddUpdatePackages_Load(object sender, EventArgs e)
        {
            DisplayProductSupplierData();
            if (addPackage)
            {
                this.Text = "Add Package";
                gbProdSup.Visible = false;
                this.Width = 600;
            }
            else
            {
                this.Text = "Update Package";
                gbProdSup.Visible = true;
                this.Width = 840;
                this.DisplayPackage();
            }
        }

        private void DisplayPackage()
        {
            txtPackageId.Text = package.PackageId.ToString();
            txtPkgName.Text = package.PkgName;
            txtPkgDesc.Text = package.PkgDesc;
            txtBasePrice.Text = package.PkgBasePrice.ToString();
            currentProductSupplierIds = Packages_Products_SuppliersDB.GetProductSupplierIds(package.PackageId);

            DisplayCurrentPackageProductSupplierData();

            if (package.PkgStartDate == null)
                txtStartDate.Text = "";
            else
            {
                DateTime startDate = (DateTime)package.PkgStartDate;
                txtStartDate.Text = startDate.ToShortDateString();
            }

            if (package.PkgEndDate == null)
                txtEndDate.Text = "";
            else
            {
                DateTime endDate = (DateTime)package.PkgEndDate;
                txtEndDate.Text = endDate.ToShortDateString();
            }

            if (package.PkgAgencyCommission.Equals(null))
                txtCommission.Text = "";
            else
                txtCommission.Text = Convert.ToDecimal(package.PkgAgencyCommission).ToString();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (addPackage)
            {
                package = new Packages();
                this.PutPackageData(package);
                try
                {
                    package.PackageId = PackagesDB.AddPackage(package);
                    this.DialogResult = DialogResult.OK;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().ToString());
                }
            }
            else
            {
                Packages newPackage = new Packages();
                newPackage.PackageId = package.PackageId;
                this.PutPackageData(newPackage);
                try
                {
                    if (!PackagesDB.UpdatePackage(package, newPackage))
                    {
                        MessageBox.Show("Another user has updated or " +
                            "deleted that package.", "Database Error");
                        this.DialogResult = DialogResult.Retry;
                    }
                    else
                    {
                        package = newPackage;
                        this.DialogResult = DialogResult.OK;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().ToString());
                }
            }
        }

        private void PutPackageData(Packages package)
        {
           
            package.PkgName= txtPkgName.Text;
            package.PkgDesc= txtPkgDesc.Text;
            package.PkgBasePrice=Convert.ToDecimal(txtBasePrice.Text);

            if (txtStartDate.Text == "")
                package.PkgStartDate = null;
            else
            {
                package.PkgStartDate= Convert.ToDateTime(txtStartDate.Text);
            }

            if (txtEndDate.Text == "")
                package.PkgEndDate = null;
            else
            {
                package.PkgEndDate = Convert.ToDateTime(txtEndDate.Text);
            }

            if (txtCommission.Text == "")
                package.PkgAgencyCommission = null;
            else
                package.PkgAgencyCommission =Convert.ToDecimal(txtCommission.Text);
        }

        private void DisplayProductSupplierData()
        {
            productSupplierIds = Products_SuppliersDB.GetProductSupplierIds();
            lstProdSup.Items.Clear();//start with empty list box
            lstProdSup.Items.Add("Id " + ": " + "Product Name" + ",  " + "Supplier Name");
            foreach (int id in productSupplierIds)
            {
               Products_Suppliers ps = Products_SuppliersDB.GetProductSupplierById(id);
               lstProdSup.Items.Add(ps);
            }
        }

        private void DisplayCurrentPackageProductSupplierData()
        {
            if (currentProductSupplierIds != null) // if we have product suppliers to display
            {
                lstPackProdSupp.Items.Clear();//start with empty list box
                lstPackProdSupp.Items.Add("Id " + ": " + "Product Name" + ",  " + "Supplier Name");
                foreach (int id in currentProductSupplierIds)
                {
                    Products_Suppliers ps = Products_SuppliersDB.GetProductSupplierById(id);
                    lstPackProdSupp.Items.Add(ps);
                }
            }
            else // null this product does not exist - need to refresh combo box
            {
                lstPackProdSupp.Items.Clear();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int index = lstProdSup.SelectedIndex;  // index of the selected product supplier
            if (index < 1) // no selection
            {
                MessageBox.Show("Please select product supplier to add");
            }
            else // user selected a product to add
            {
                Products_Suppliers ps = Products_SuppliersDB.GetProductSupplierById(productSupplierIds[index-1]); // selected product

                    //add selected product
                currentProductSupplierIds.Add(ps.ProductSupplierId); // add to current  product supplier list
                try
                {
                    if(Packages_Products_SuppliersDB.AddPackageProductSupplier(package.PackageId,ps.ProductSupplierId))
                    this.DialogResult = DialogResult.OK;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().ToString());
                }
                DisplayCurrentPackageProductSupplierData();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int index = lstPackProdSupp.SelectedIndex;  // index of the selected product supplier
            if (index < 1) // no selection
            {
                MessageBox.Show("Please select product supplier to delete");
            }
            else // user selected a product to delete
            {
                Products_Suppliers pps = Products_SuppliersDB.GetProductSupplierById(currentProductSupplierIds[index-1]); // selected product
                DialogResult answer =
                    MessageBox.Show("Are you sure to delete " + currentProductSupplierIds[index-1]+"?",
                    "Please Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (answer == DialogResult.Yes)
                {
                    //delete selected package product supplier
                    try
                    {
                        if (!Packages_Products_SuppliersDB.DeletePackageProductSupplier(package.PackageId, pps.ProductSupplierId))
                        {
                            MessageBox.Show("Another user has updated or deleted " +
                                "that product.", "Database Error");
                        }
                        else
                            currentProductSupplierIds.RemoveAt(index - 1);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, ex.GetType().ToString());
                    }
                  // remove from the current  product supplier  list
                    DisplayCurrentPackageProductSupplierData();
                }
            }
        }
    }
}
