using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        public Form1()
        {
            InitializeComponent();
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
            string desiredText = textBoxDesiredText.Text;
            Clipboard.SetText(desiredText);
            string imagelocation = "http://i.imgur.com/4DDzfxa.jpg";
            // imagelocation = "C:/Users/Rodney/Desktop/W9OTW.jpg";
            Image mypic = DownloadImageFromUrl(imagelocation);  
            Clipboard.SetImage(mypic);
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

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            labelInformation.Text = "Access Token: " + GetAccessToken();
        }
    }



}


