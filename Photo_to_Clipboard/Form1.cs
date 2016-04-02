using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.OneDrive.Sdk;
using Microsoft.OneDrive.Sdk.WindowsForms;


/*

Useful links:

- C# Api 
https://github.com/onedrive/onedrive-sdk-csharp

- One Drive SDK
https://dev.onedrive.com/sdks.htm





*/

namespace Photo_to_Clipboard
{
    public partial class Form1 : Form
    {
        private AccessToken myAccessToken;
        LoginManager myLoginManager;
        public Form1()
        {
            InitializeComponent();
            myLoginManager = new LoginManager();
        }

        public System.Drawing.Image DownloadImageFromUrl(string imageUrl)
        {
            /*
                Eternal thanks to Forget Code.com for this function. 
                http://forgetcode.com/CSharp/2052-Download-images-from-a-URL       
             */

            System.Drawing.Image image = null;
            try
            {
                System.Net.HttpWebRequest webRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(imageUrl);
                webRequest.AllowWriteStreamBuffering = true;
                webRequest.Timeout = 30000;

                System.Net.WebResponse webResponse = webRequest.GetResponse();

                System.IO.Stream stream = webResponse.GetResponseStream();

                image = System.Drawing.Image.FromStream(stream);

                webResponse.Close();
            }
            catch (Exception ex)
            {
                return null;
            }

            return image;
        }

        private void buttonSetClipboard_Click(object sender, EventArgs e)
        {
            //string desiredText = textBoxDesiredText.Text;
            // Clipboard.SetText(desiredText);
            Clipboard.SetImage(myLoginManager.CurrentImage);
            //Clipboard.SetImage(myLoginManager.CurrentImage);
            labelInformation.Text = "Image has been set!";
        }

        private string GetAccessToken()
        {
            if (myAccessToken != null && myAccessToken.IsValid)
            {
                return myAccessToken.Token;
            }

            using (FormWebBrowser authBrowser = new FormWebBrowser())
            {
                if (authBrowser.ShowDialog() != DialogResult.OK) return string.Empty;
                myAccessToken = new AccessToken();
                myAccessToken.Token = authBrowser.AccessToken;
                return myAccessToken.Token;
                labelInformation.Text = myAccessToken.Token;
            }
        }

        private async void buttonLogin_Click(object sender, EventArgs e)
        {
            await myLoginManager.StartAuthenticationProcess();

            if (myLoginManager.loggedin)
            {
                buttonInvestigate.Enabled = true;
            }
            //labelInformation.Text = "Access Token: " + GetAccessToken();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonInvestigate_Click(object sender, EventArgs e)
        {
            string path = "Pictures/oIhhvI7.jpg";
            myLoginManager.LoadFolderFromPath(path);

        }
    }
}


