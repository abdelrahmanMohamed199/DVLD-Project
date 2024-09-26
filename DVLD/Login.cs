using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using DVLD_BusinessTier;
using Microsoft.Win32;

namespace DVLD
{
    public partial class frmLogin : Form
    {
        string _key = "1234567890123456";
        clsUser _User;        
        public frmLogin()
        {
            InitializeComponent();
            LoadCredentials();
        }

        private bool IsLoginDataValid()
        {
            if(tbUsername.Text.Trim() == "" || tbPassword.Text == "")
            {
                return false; 
            }

            string Password = clsUtil.EncryptPassword(tbPassword.Text.Trim());
            _User = clsUser.Find(tbUsername.Text.Trim(), Password);
            if( _User == null )
            {
                MessageBox.Show("Invalid Username/Password", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!(_User.IsActive))
            {
                MessageBox.Show("Your account is deactivated, Please contact your admin", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void SaveCredentials(string username, string password)
        {
            string KeyPath = @"HKEY_CURRENT_USER\Software\DVLD";
            try
            {
                Registry.SetValue(KeyPath, "Username", username, RegistryValueKind.String);
                Registry.SetValue(KeyPath, "Password", password, RegistryValueKind.String);
            }
            catch { }
        }

        private void ClearCredentials()
        {
            string KeyPath = @"Software\DVLD";
            try
            {
                using(RegistryKey baseKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64))
                {
                    using (RegistryKey key = baseKey.OpenSubKey(KeyPath, true))
                    {
                        if(key != null)
                        {                          
                            key.DeleteValue("Username");
                            key.DeleteValue("Password");
                        }
                    }
                }
            }
            catch {}
        }

        private void LoadCredentials()
        {
            string KeyPath = @"HKEY_CURRENT_USER\Software\DVLD";
            try
            {
                string Username = Registry.GetValue(KeyPath, "Username", null) as string;
                string Password = Registry.GetValue(KeyPath, "Password", null) as string;
                if(Username != null && Password != null)
                {
                    tbUsername.Text = Username;
                    tbPassword.Text = DecryptPasswordForRememberMe(Password, _key);
                    chbRememberMe.Checked = true;
                }
            }
            catch {}
        }

        string EncryptPasswordForRememberMe(string plainText, string key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                // Set the key and IV for AES encryption
                aesAlg.Key = Encoding.UTF8.GetBytes(key);

                /*
                Here, you are setting the IV of the AES algorithm to a block of bytes 
                with a size equal to the block size of the algorithm divided by 8. 
                The block size of AES is typically 128 bits (16 bytes), 
                so the IV size is 128 bits / 8 = 16 bytes.
                 */
                aesAlg.IV = new byte[aesAlg.BlockSize / 8];


                // Create an encryptor
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);


                // Encrypt the data
                using (var msEncrypt = new System.IO.MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    using (var swEncrypt = new System.IO.StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }

                    // Return the encrypted data as a Base64-encoded string
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        string DecryptPasswordForRememberMe(string cipherText, string key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                // Set the key and IV for AES decryption
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.IV = new byte[aesAlg.BlockSize / 8];


                // Create a decryptor
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);


                // Decrypt the data
                using (var msDecrypt = new System.IO.MemoryStream(Convert.FromBase64String(cipherText)))
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                using (var srDecrypt = new System.IO.StreamReader(csDecrypt))
                {
                    // Read the decrypted data from the StreamReader
                    return srDecrypt.ReadToEnd();
                }
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if(IsLoginDataValid())
            {
                if(chbRememberMe.Checked)
                {
                    string password = EncryptPasswordForRememberMe(tbPassword.Text, _key);
                    SaveCredentials(tbUsername.Text, password);
                }  
                else
                {
                    ClearCredentials();
                }
                clsGlobleSettings.CurrentUser = _User;
                this.Hide();
                frmMainScreen frm1 = new frmMainScreen(this);
                frm1.ShowDialog();
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            tbUsername.Focus();
        }

        private void tbUsername_Leave(object sender, EventArgs e)
        {
            if (tbUsername.Text.Trim() == "")
                errorProvider1.SetError(tbUsername, "Enter username");
            else
                errorProvider1.SetError(tbUsername, null);
        }

        private void tbPassword_Leave(object sender, EventArgs e)
        {
            if (tbPassword.Text.Trim() == "")
                errorProvider1.SetError(tbPassword, "Enter username");
            else
                errorProvider1.SetError(tbPassword, null);
        }

        private void tbPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsWhiteSpace(e.KeyChar);
        }
    }
}
