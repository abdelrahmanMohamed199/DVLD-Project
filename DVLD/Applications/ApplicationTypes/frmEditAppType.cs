using DVLD_BusinessTier;
using System;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmEditAppType : Form
    {
        int _AppTypeID;
        clsApplicationType _AppType;
        public frmEditAppType(int AppTypeID)
        {
            InitializeComponent();
            _AppTypeID = AppTypeID;
            _AppType = clsApplicationType.Find(AppTypeID);
            if(_AppType == null )
                _AppType = new clsApplicationType();
        }

        private void frmEditAppType_Load(object sender, EventArgs e)
        {
            lblAppID.Text = _AppTypeID.ToString();
            tbTitle.Text = _AppType.AppTitle;
            tbFees.Text = Convert.ToString(_AppType.Price);
            tbTitle.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(tbTitle.Text.Trim() == "" || tbFees.Text.Trim() == "")
            {
                MessageBox.Show("Please fill all the fields", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _AppType.AppTypeID = _AppTypeID;
            _AppType.AppTitle = tbTitle.Text.Trim();
            _AppType.Price = Convert.ToDecimal(tbFees.Text.Trim());
            if(_AppType.EditApplication())
            {
                MessageBox.Show("Application type updated successfully", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Fail to update application", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) || char.IsWhiteSpace(e.KeyChar);
        }
    }
}
