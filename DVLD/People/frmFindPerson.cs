using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.People
{
    public partial class frmFindPerson : Form
    {
        public frmFindPerson()
        {
            InitializeComponent();
        }

        public delegate void DataBackEventHandler(int personID);

        public event DataBackEventHandler DataBack;

        private void btnClose_Click(object sender, EventArgs e)
        {
            DataBack?.Invoke(ctrlPersonInfoWithFilter1.PersonID);
            this.Close();
        }
    }
}
