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
    public partial class frmAddUpdateSuppliers : Form
    {
        public frmAddUpdateSuppliers()
        {
            InitializeComponent();
        }
        public bool addSupplier;
        public Suppliers supplier;


        private void frmAddUpdateSuppliers_Load(object sender, EventArgs e)
        {
            if (addSupplier)
            {
                this.Text = "Add Supplier";
            }
            else
            {
                this.Text = "Update Supplier";
                this.DisplaySupplier();
            }
        }

        private void DisplaySupplier()
        {
            txtSupName.Text = supplier.SupName;
        }

        private void PutSupplierData(Suppliers supplier)
        {
            supplier.SupName = txtSupName.Text;
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (addSupplier)
            {
                supplier = new Suppliers();
                this.PutSupplierData(supplier);
                try
                {
                    supplier.SupplierId = SuppliersDB.AddSupplier(supplier);
                    this.DialogResult = DialogResult.OK;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().ToString());
                }
            }
            else
            {
                Suppliers newSupplier = new Suppliers();
                newSupplier.SupplierId = supplier.SupplierId;
                this.PutSupplierData(newSupplier);
                try
                {
                    if (!SuppliersDB.UpdateSupplier(supplier, newSupplier))
                    {
                        MessageBox.Show("Another user has updated or " +
                            "deleted that supplier.", "Database Error");
                        this.DialogResult = DialogResult.Retry;
                    }
                    else
                    {
                        supplier = newSupplier;
                        this.DialogResult = DialogResult.OK;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().ToString());
                }

            }
        }
    }
}
