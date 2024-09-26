using DVLD_BusinessTier;
using System;
using System.Windows.Forms;

namespace DVLD.People.Controls
{
    public partial class ctrlPersonInfoWithFilter : UserControl
    {
        int _PersonID = -1;

        public int PersonID
        {
            get { return _PersonID; }
        }

        bool _showAdd = true;

        public bool ShowAdd
        {
            get { return _showAdd; }
            set
            {
                _showAdd = value;
                btnAdd.Enabled = _showAdd;
            }
        }

        bool _filterShow = true;
        public bool FilterEnable
        {
            get { return _filterShow; }
            set
            {
                _filterShow = value;
                groupBox1.Enabled = _filterShow;
            }
        }

        public event Action<int> OnPersonSelected;

        protected virtual void PersonSelected(int  personID)
        {
            Action<int> handler = OnPersonSelected;
            if(handler != null)
                handler(personID);
        }
        public ctrlPersonInfoWithFilter()
        {
            InitializeComponent();
        }

        public void LoadPersonInfo(int ID)
        {
            if (!clsPerson.IsPersonExist(ID))
            {
                MessageBox.Show("Person doesn't exist", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbFilterBy.Text = string.Empty;
                tbFilterBy.Focus();
                return;
            }
            this.ctrlShowPersonInfo1.LoadPersonInfo(ID);
            tbFilterBy.Text = ID.ToString();
            cbFilterBy.SelectedIndex = 0;
            _PersonID = ctrlShowPersonInfo1.person.ID;
        }

        public void LoadPersonInfo(string NationalNo)
        {
            if (!clsPerson.IsPersonExist(NationalNo))
            {
                MessageBox.Show("Person doesn't exist", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.ctrlShowPersonInfo1.LoadPersonInfo(clsPerson.Find(NationalNo).ID);
            tbFilterBy.Text = NationalNo;
            cbFilterBy.SelectedIndex = 1;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if(cbFilterBy.Text == "Person ID" && tbFilterBy.Text != "")
            {
                LoadPersonInfo(Convert.ToInt32(tbFilterBy.Text));
            }
            else
            {
                if(tbFilterBy.Text != "")
                    LoadPersonInfo(tbFilterBy.Text);
            }

            if(ctrlShowPersonInfo1.person != null)
                _PersonID = ctrlShowPersonInfo1.person.ID;

            if (OnPersonSelected != null && groupBox1.Enabled)
                PersonSelected(_PersonID);
        }

        private void tbFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            // if user press enter key
            if(e.KeyChar == (char)13)
            {
                btnSearch.PerformClick();
            }

            if (cbFilterBy.Text == "Person ID")
            {  
                e.Handled = !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmPersonInfo frm = new frmPersonInfo(-1);
            frm.DataBack += frmPersonInfo_DataBack;
            frm.ShowDialog();
            
        }

        private void frmPersonInfo_DataBack(object sender, int ID)
        {
            _PersonID = ID;
            LoadPersonInfo(ID);
            cbFilterBy.SelectedIndex = 0;
            tbFilterBy.Text = ID.ToString();
        }
    }
}
