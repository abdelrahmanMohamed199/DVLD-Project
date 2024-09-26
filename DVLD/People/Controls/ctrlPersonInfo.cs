using DVLD_BusinessTier;
using System;
using System.IO;
using System.Data;
using System.Windows.Forms;
using DVLD.Properties;
using System.ComponentModel;


namespace DVLD
{
    public partial class ctrlPersonInfo : UserControl
    {
        public enum enMode { AddNew, Update }

        public enMode Mode;

        clsPerson _Person;

        public int ID
        {
            get { return _Person.ID; }
        }

        public ctrlPersonInfo()
        {
            InitializeComponent();
        }

        private void FillCountriesInComboBox()
        {
            DataTable dtCountries = clsCountry.GetCountryList();
            foreach (DataRow row in dtCountries.Rows)
            {
                cbCountries.Items.Add(row["CountryName"].ToString());
            }
        }

        public void ResetDefaultValues()
        {
            _Person = new clsPerson();
            Mode = enMode.AddNew;
            FillCountriesInComboBox();
            dtpDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);
            dtpDateOfBirth.MinDate = DateTime.Now.AddYears(-100);
            rbMale.Checked = true;
            pbPersonPic.Image = Resources.person_boy;
            cbCountries.SelectedIndex = cbCountries.FindString("Egypt");
            llblRemove.Visible = false;
        }

        public void LoadPersonInfo(int PersonID)
        {
            _Person = clsPerson.Find(PersonID);
            Mode = enMode.Update;
            if (_Person == null)
            {
                MessageBox.Show("Person doesn't exist");
                this.FindForm().Close();
            }
            else
            {
                lblPersonID.Text = _Person.ID.ToString();
                tbFirstName.Text = _Person.FirstName;
                tbSecondName.Text = _Person.SecondName;
                tbThirdName.Text = _Person.ThirdName;
                tbLastName.Text = _Person.LastName;
                tbNationalNo.Text = _Person.NationalNo;
                dtpDateOfBirth.Value = _Person.DateOfBirth;
                tbPhone.Text = _Person.Phone;
                tbAddress.Text = _Person.Address;              
                cbCountries.Text = _Person.CountryInfo.Name;
                tbEmail.Text = _Person.Email;
                if (_Person.Gendor == 1)
                    rbFemale.Checked = true;
                else
                    rbMale.Checked = true;

                if(_Person.ImagePath != "")
                    pbPersonPic.ImageLocation = _Person.ImagePath;

                llblRemove.Visible = (pbPersonPic.ImageLocation != null);
            }
        }

        public bool SavePerson()
        {
            // check if required fields are filled correctly
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Please complete the person information",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (tbFirstName.Text.Trim() == "" || tbSecondName.Text.Trim() == ""
                || tbThirdName.Text.Trim() == "" || tbLastName.Text.Trim() == ""
                || tbNationalNo.Text.Trim() == "" || tbPhone.Text.Trim() == ""
                || tbAddress.Text.Trim() == "")
            {
                MessageBox.Show("Please complete the person information",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!HandleImage())
                return false;

            _Person.FirstName = tbFirstName.Text.Trim();
            _Person.SecondName = tbSecondName.Text.Trim();
            _Person.ThirdName = tbThirdName.Text.Trim();
            _Person.LastName = tbLastName.Text.Trim();
            _Person.NationalNo = tbNationalNo.Text.Trim();
            _Person.Address = tbAddress.Text.Trim();
            _Person.Email = tbEmail.Text.Trim();
            _Person.Phone = tbPhone.Text.Trim();
            _Person.DateOfBirth = dtpDateOfBirth.Value;
            _Person.Gendor = Convert.ToByte(lblGendor.Tag);
            _Person.CountryID = clsCountry.Find(cbCountries.Text).ID;

            if (pbPersonPic.ImageLocation != null)
                _Person.ImagePath = pbPersonPic.ImageLocation.ToString();
            else
                _Person.ImagePath = "";

            if (_Person.Save())
            {
                MessageBox.Show("Person saved successfully");
                lblPersonID.Text = _Person.ID.ToString();
                Mode = enMode.Update;
                return true;
            }
            else
            {
                MessageBox.Show("Fail to save person");
                return false;
            }
        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            lblGendor.Tag = 0;
            if (pbPersonPic.ImageLocation == null)
            {
                pbPersonPic.Image = Resources.person_boy;
            }
        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            lblGendor.Tag = 1;
            if (pbPersonPic.ImageLocation == null)
            {
                pbPersonPic.Image = Resources.person_girl;
            }
        }

        private void llblSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.bmp;*.jpg;*.jpeg;*.png;*.gif";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pbPersonPic.ImageLocation = openFileDialog1.FileName;
                llblRemove.Visible = true;
            }
        }

        private void llblRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(rbMale.Checked)
            {
                pbPersonPic.Image = Resources.person_boy;
                pbPersonPic.ImageLocation = null;
                llblRemove.Visible = false;
            }
            else
            {
                pbPersonPic.Image = Resources.person_girl;
                pbPersonPic.ImageLocation = null;
                llblRemove.Visible = false;
            }
        }

        private void ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {
            TextBox Temp = (TextBox)sender;
            if(string.IsNullOrEmpty(Temp.Text.Trim()) )
            {
                //e.Cancel = true;
                errorProvider1.SetError(Temp, "This field is required");
            }
            else
            {
                errorProvider1.SetError(Temp, null);
            }
        }

        private void tbEmail_Validating(object sender, CancelEventArgs e)
        {
            if(tbEmail.Text.Trim() == "")
                { return; }

            if(!clsValidation.ValidateEmail(tbEmail.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(tbEmail, "Invalid Email");
            }
            else
            {
                errorProvider1.SetError(tbEmail, null);
            }
        }

        private void tbNationalNo_Validating(object sender, CancelEventArgs e)
        {
            if(tbNationalNo.Text.Trim() != _Person.NationalNo && 
                clsPerson.IsPersonExist(tbNationalNo.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(tbNationalNo, "This National No is existed");
            }
            else
            {
                if(tbNationalNo.Text.Trim() == "")
                    ValidateEmptyTextBox(sender, e);
                else
                    errorProvider1.SetError(tbNationalNo, null);
            }
        }

        private void tbFirstName_Validating(object sender, CancelEventArgs e)
        {
            ValidateEmptyTextBox(sender, e);
        }

        private void tbSecondName_Validating(object sender, CancelEventArgs e)
        {
            ValidateEmptyTextBox(sender, e);
        }

        private void tbThirdName_Validating(object sender, CancelEventArgs e)
        {
            ValidateEmptyTextBox(sender, e);
        }

        private void tbLastName_Validating(object sender, CancelEventArgs e)
        {
            ValidateEmptyTextBox(sender, e);
        }

        private void tbPhone_Validating(object sender, CancelEventArgs e)
        {
            ValidateEmptyTextBox(sender, e);
        }

        private void tbAddress_Validating(object sender, CancelEventArgs e)
        {
            ValidateEmptyTextBox(sender, e);
        }

        private bool HandleImage()
        {
            // check if the image changed and not removed(imageLocation = null)
            if (pbPersonPic.ImageLocation != _Person.ImagePath && pbPersonPic.ImageLocation != null)
            {
                string sourceFile = pbPersonPic.ImageLocation;
                if(clsUtil.CopyImageToProjectFolder(ref sourceFile))
                {
                    if(_Person.ImagePath != "")
                        File.Delete(_Person.ImagePath);
                    pbPersonPic.ImageLocation = sourceFile;
                    return true;
                }
                return false;
            }

            // if the user remove image and click save
            if (pbPersonPic.ImageLocation != _Person.ImagePath && _Person.ImagePath != "")
                File.Delete(_Person.ImagePath);

            return true;
        }


    }
}
