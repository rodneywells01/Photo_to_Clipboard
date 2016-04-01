﻿using System;
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

namespace Photo_to_Clipboard
{
    public partial class FormWebBrowser : Form
    {
        /* Code is courtesy of:
        http://www.codeguru.com/columns/dotnet/authenticating-a-onedrive-account.html
        */

        public string AccessToken { get { return _accessToken; } }
        private string _accessToken = string.Empty;
        private static readonly string[] _scope = { "onedrive.readwrite", "wl.offline_access", "wl.signin" }; // Not sure if valid 
        private IOneDriveClient oneDriveClient { get; set; }

        private const string _clientID = "000000004418375A"; // MSA ClientID
        private const string _clientSecret = "tlti8Svm7JfBx0WH56CTg2jo2knaTv9H";
        /* private const string _signInUrl = @"https://login.live.com/
         oauth20_authorize.srf?client_id={0}&redirect_uri=
         https://login.live.com/oauth20_desktop.srf&response_type=
         token&scope={1}";*/


        private const string _signInUrl = "https://login.live.com/oauth20_desktop.srf";

        private Timer _closeTimer;

        
        
        public FormWebBrowser()
        {
            InitializeComponent();
            StartAuthenticationProcess();
        }

        public async void StartAuthenticationProcess()
        {
            if (this.oneDriveClient == null)
            {
                OneDriveClient.GetMicrosoftAccountClient(
                    _clientID,
                    _signInUrl,
                    _scope,
                    //webAuthenticationUi: new FormsWebAuthenticationUi(),
                    webAuthenticationUi: webBrowser1
                    
                    );
            }


            /*
            try
            {
                if(!this.oneDriveClient.IsAuthenticated)
                {
                    await this.oneDriveClient.AuthenticateAsync();
                }
                UpdateCo
                // await LoadFolderFromPath();
            }
*/
            //webBrowser1.DocumentCompleted += webBrowser1_DocumentCompleted; // No idea what this is
            //webBrowser1.Navigate(string.Format(_signInUrl, _clientID, _scope));

        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (e.Url.AbsoluteUri.Contains("#access_token="))
            {
                var x = e.Url.AbsoluteUri.Split(new[]
                { "#access_token" }, StringSplitOptions.None);
                _accessToken = x[1].Split(new[] { '&' })[0];
                _closeTimer = new Timer { Interval = 500 };
                _closeTimer.Tick += timer1_Tick; // Not sure I did this timer correctly
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _closeTimer.Enabled = false;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
