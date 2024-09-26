using DVLD_BusinessTier;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmManagePeople : Form
    {
        DataTable dtAllPeopleList;
        DataTable dtPeopleList;
        public frmManagePeople()
        {
            InitializeComponent();
        }

        private void _RefreshPeopleList()
        {
            dtAllPeopleList = clsPerson.GetPeopleList();
            dtPeopleList = dtAllPeopleList.DefaultView.ToTable(false, "PersonID", "NationalNo", "FirstName",
                "SecondName", "ThirdName", "LastName", "DateOfBirth", "GendorCaption",
                "Phone", "Address", "Email", "CountryName", "ImagePath");
            dgvPeopleList.DataSource = dtPeopleList;
            lblNumOfRecords.Text = dgvPeopleList.Rows.Count.ToString();
            cbFilterBy.SelectedIndex = 0;
            if(dgvPeopleList.Rows.Count > 0)
            {
                dgvPeopleList.Columns[0].HeaderText = "Person ID";
                dgvPeopleList.Columns[0].Width = 100;

                dgvPeopleList.Columns[1].HeaderText = "National No";
                dgvPeopleList.Columns[1].Width = 100;

                dgvPeopleList.Columns[2].HeaderText = "First Name";
                dgvPeopleList.Columns[2].Width = 120;

                dgvPeopleList.Columns[3].HeaderText = "Second Name";
                dgvPeopleList.Columns[3].Width = 120;

                dgvPeopleList.Columns[4].HeaderText = "Third Name";
                dgvPeopleList.Columns[4].Width = 120;

                dgvPeopleList.Columns[5].HeaderText = "Last Name";
                dgvPeopleList.Columns[5].Width = 120;

                dgvPeopleList.Columns[6].HeaderText = "Date Of Birth";
                dgvPeopleList.Columns[6].Width = 100;

                dgvPeopleList.Columns[7].HeaderText = "Gendor";
                dgvPeopleList.Columns[7].Width = 80;

                dgvPeopleList.Columns[8].HeaderText = "Phone";
                dgvPeopleList.Columns[8].Width = 100;

                dgvPeopleList.Columns[9].HeaderText = "Address";
                dgvPeopleList.Columns[9].Width = 150;

                dgvPeopleList.Columns[10].HeaderText = "Email";
                dgvPeopleList.Columns[10].Width = 100;

                dgvPeopleList.Columns[11].HeaderText = "Country";
                dgvPeopleList.Columns[11].Width = 100;

                dgvPeopleList.Columns[12].HeaderText = "Image Path";
                dgvPeopleList.Columns[12].Width = 150;
            }    
        }

        private void frmManagePeople_Load(object sender, EventArgs e)
        {
            _RefreshPeopleList();
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            frmPersonInfo frm1 = new frmPersonInfo(-1);
            frm1.ShowDialog();
            _RefreshPeopleList();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbFilterBy_TextChanged(object sender, EventArgs e)
        {
            string filterColumn = "";
            switch(cbFilterBy.Text)
            {
                case "Person ID":
                    filterColumn = "PersonID";
                    break;
                case "National No":
                    filterColumn = "NationalNo";
                    break;
                case "First Name":
                    filterColumn = "FirstName";
                    break;
                case "Second Name":
                    filterColumn = "SecondName";
                    break;
                case "Third Name":
                    filterColumn = "ThirdName";
                    break;
                case "Last Name":
                    filterColumn = "LastName";
                    break;
                case "Gendor":
                    filterColumn = "Gendor";
                    break;
                case "Phone":
                    filterColumn = "Phone";
                    break;
                default:
                    filterColumn = "None";
                    break;
            }

            if(filterColumn == "None" || tbFilterBy.Text.Trim() == "")
            {
                dtPeopleList.DefaultView.RowFilter = "";
                lblNumOfRecords.Text = dtPeopleList.Rows.Count.ToString();
                return;
            }

            if(filterColumn == "PersonID")
            {
                dtPeopleList.DefaultView.RowFilter = string.Format("{0} = {1}", 
                    filterColumn, tbFilterBy.Text.Trim());
                lblNumOfRecords.Text = dtPeopleList.DefaultView.Count.ToString();
            }
            else
            {
                dtPeopleList.DefaultView.RowFilter = string.Format("{0} like '{1}%'", 
                    filterColumn, tbFilterBy.Text.Trim());
                lblNumOfRecords.Text = dtPeopleList.DefaultView.Count.ToString();
            }
            
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPersonInfo frm1 = new frmPersonInfo(-1);
            frm1.ShowDialog();
            _RefreshPeopleList();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(dgvPeopleList.CurrentRow.Cells[0].Value);
            frmPersonInfo frm1 = new frmPersonInfo(ID);
            frm1.ShowDialog();
            _RefreshPeopleList();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to delete this person?", "Confirmation", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int ID = Convert.ToInt32(dgvPeopleList.CurrentRow.Cells[0].Value);
                clsPerson person = clsPerson.Find(ID);
                if (clsPerson.DeletePerson(ID))
                {
                    MessageBox.Show("Person Deleted Successfully", "Information",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if(person.ImagePath != "")
                        File.Delete(person.ImagePath);

                    _RefreshPeopleList();
                }
                else
                    MessageBox.Show("You can not delete this person", "Information",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(dgvPeopleList.CurrentRow.Cells[0].Value);
            frmShowPersonInfo frm1 = new frmShowPersonInfo(ID);
            frm1.ShowDialog();
            _RefreshPeopleList();
        }

        private void cbFilterBy_SelectedValueChanged(object sender, EventArgs e)
        {
            tbFilterBy.Visible = (cbFilterBy.Text != "None");
            if(cbFilterBy.Text != "None" )
            {
                tbFilterBy.Text = "";
                tbFilterBy.Focus();
            }           
        }

        private void tbFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(cbFilterBy.Text == "Person ID")
            {
                // Allow only digits and control keys (backspace, delete, etc.)
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true; //prevents the character from being entered into the TextBox
                }
            }
        }
    }
}
