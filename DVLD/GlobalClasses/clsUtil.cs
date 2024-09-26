using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace DVLD
{
    internal class clsUtil
    {
        static string GenerateGUID()
        {
            return Guid.NewGuid().ToString();
        }

        static public bool CopyImageToProjectFolder(ref string sourceFile)
        {
            string destinationFile = @"C:\NewDVLD\DVLD_PeopleImages\";
            FileInfo extension = new FileInfo(sourceFile);
            destinationFile += GenerateGUID() + extension.Extension;
            try
            {
                File.Copy(sourceFile, destinationFile, true);
            }
            catch (IOException iox)
            {
                MessageBox.Show(iox.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            sourceFile = destinationFile; 
            return true;
        }

        static public string EncryptPassword(string Password)
        {
            //SHA is Secutred Hash Algorithm.
            // Create an instance of the SHA-256 algorithm
            using (SHA256 sha256 = SHA256.Create())
            {
                // Compute the hash value from the UTF-8 encoded input string
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(Password));

                // Convert the byte array to a lowercase hexadecimal string
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }
}
