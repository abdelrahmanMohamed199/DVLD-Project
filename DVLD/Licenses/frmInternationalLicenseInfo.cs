﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Licenses
{
    public partial class frmInternationalLicenseInfo : Form
    {
        public frmInternationalLicenseInfo(int InternationalLicenseID)
        {
            InitializeComponent();
            ctrlInterLicenseInfo1.LoadData(InternationalLicenseID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
