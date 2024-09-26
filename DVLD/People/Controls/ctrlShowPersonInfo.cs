using DVLD.Properties;
using DVLD_BusinessTier;
using System;
using System.Windows.Forms;

namespace DVLD
{
    public partial class ctrlShowPersonInfo : UserControl
    {
        clsPerson _Person;

        public clsPerson person
        {
            get { return _Person; }
        }

        public ctrlShowPersonInfo()
        {
            InitializeComponent();
        }

        public void LoadPersonInfo(int personID)
        {
            _Person = clsPerson.Find(personID);
            if (_Person != null)
            {
                lblPersonID.Text = _Person.ID.ToString();
                lblFullName.Text = _Person.FirstName + " " + _Person.SecondName + " " + 
                    _Person.ThirdName + " " + _Person.LastName;
                lblNationalNo.Text = _Person.NationalNo;
                lblEmail.Text = _Person.Email;
                lblAddress.Text = _Person.Address;
                lblPhone.Text = _Person.Phone;
                lblDateOfBirth.Text = _Person.DateOfBirth.ToShortDateString();
                lblCountry.Text = _Person.CountryInfo.Name;

                if(_Person.ImagePath != "")
                {
                    pbPersonPic.ImageLocation = _Person.ImagePath;
                }
                else
                {
                    if (_Person.Gendor == 0)
                        pbPersonPic.Image = Resources.person_boy;
                    else
                        pbPersonPic.Image = Resources.person_girl;
                }

                if (_Person.Gendor == 0)
                    lblGendor.Text = "Male";
                else
                    lblGendor.Text = "Female";
            }
            else
            {
                lblPersonID.Text = "???";
                lblFullName.Text = "???";
                lblNationalNo.Text = "???";
                lblEmail.Text = "???";
                lblAddress.Text = "???";
                lblPhone.Text = "???";
                lblDateOfBirth.Text = "???";
                lblCountry.Text = "???";
                lblGendor.Text = "???";
                pbPersonPic.Image = Resources.person_boy;
                llblEditPerson.Enabled = false;
            }
        }

        private void llblEditPerson_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_Person != null)
            {
                frmPersonInfo frm1 = new frmPersonInfo(_Person.ID);
                frm1.ShowDialog();

                //refresh
                LoadPersonInfo(_Person.ID);
            }
        }

    }
}
