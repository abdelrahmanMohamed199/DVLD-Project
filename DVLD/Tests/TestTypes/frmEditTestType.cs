using DVLD_BusinessTier;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmEditTestType : Form
    {
        int _TesttypeID = -1;
        clsTestType _TestType;
        public frmEditTestType(int testTypeID)
        {
            InitializeComponent();
            _TesttypeID = testTypeID;
            _TestType = clsTestType.Find(testTypeID);
            if (_TestType == null ) 
                _TestType = new clsTestType();
        }

        private void tbFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) || char.IsWhiteSpace(e.KeyChar);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmEditTestType_Load(object sender, EventArgs e)
        {
            lblTestTypeID.Text = _TesttypeID.ToString();
            tbTitle.Text = _TestType.Title;
            tbDescription.Text = _TestType.Description;
            tbFees.Text = _TestType.Price.ToString();
            tbTitle.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                MessageBox.Show("There are some fields invalid", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _TestType.Title = tbTitle.Text.Trim();
            _TestType.Description = tbDescription.Text.Trim();
            _TestType.Price = Convert.ToDecimal(tbFees.Text);
            if(_TestType.EditTestType())
            {
                MessageBox.Show("Data saved successfully", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Fail to save data", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tbTitle_Validating(object sender, CancelEventArgs e)
        {
            if(tbTitle.Text.Trim() == "")
            {
                errorProvider1.SetError(tbTitle, "This field is required");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(tbTitle, null);
            }
        }

        private void tbDescription_Validating(object sender, CancelEventArgs e)
        {
            if (tbDescription.Text.Trim() == "")
            {
                errorProvider1.SetError(tbDescription, "This field is required");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(tbDescription, null);
            }
        }

        private void tbFees_Validating(object sender, CancelEventArgs e)
        {
            if (tbFees.Text.Trim() == "")
            {
                errorProvider1.SetError(tbFees, "This field is required");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(tbFees, null);
            }
        }
    }
}
