using System;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmPersonInfo : Form
    {
        int _PersonID;

        public delegate void DataBackEventHandler(object sender, int PersonID);

        public event DataBackEventHandler DataBack;
        public frmPersonInfo(int personID)
        {
            InitializeComponent();
            _PersonID = personID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmPersonInfo_Load(object sender, EventArgs e)
        {
            lblTitle.Text = "Add New Person";
            this.ctrlPersonInfo1.ResetDefaultValues();
            if (_PersonID != -1)
            {
                lblTitle.Text = "Update Person";
                this.ctrlPersonInfo1.LoadPersonInfo(_PersonID);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(this.ctrlPersonInfo1.SavePerson())
            {
                lblTitle.Text = "Update Person";
                _PersonID = this.ctrlPersonInfo1.ID;
                DataBack?.Invoke(this, _PersonID);
            }
        }


    }
}
