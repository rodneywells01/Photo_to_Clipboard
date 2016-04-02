using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.OneDrive.Sdk;
using Microsoft.OneDrive.Sdk.WindowsForms;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Drawing;
using System.Net;

namespace Photo_to_Clipboard
{
    class LoginManager
    {
        // Authentication Fields 
        public string AccessToken { get { return _accessToken; } }
        private string _accessToken = string.Empty;
        private static readonly string[] _scope = { "onedrive.readwrite", "wl.offline_access", "wl.signin", "wl.skydrive" }; // Not sure if valid 
        private IOneDriveClient oneDriveClient { get; set; }
        private const string _clientID = "000000004418375A"; // MSA ClientID
        private const string _clientSecret = "tlti8Svm7JfBx0WH56CTg2jo2knaTv9H";
        private const string _signInUrl = "https://login.live.com/oauth20_desktop.srf";
        public bool loggedin = false;

        // Navigation Fields 
        private Item CurrentFolder { get; set; }
        private Item SelectedItem { get; set; }
        public System.Drawing.Image CurrentImage { get; set; }

        public async Task StartAuthenticationProcess()
        {
            if (this.oneDriveClient == null)
            {

                this.oneDriveClient = OneDriveClient.GetMicrosoftAccountClient(
                    _clientID,
                    _signInUrl,
                    _scope,
                    webAuthenticationUi: new FormsWebAuthenticationUi()
                );
                
                try
                {
                    if (!this.oneDriveClient.IsAuthenticated)
                    {
                        // Successful login. 
                        await this.oneDriveClient.AuthenticateAsync();
                        loggedin = true;
                    }
                }
                catch (OneDriveException exception)
                {
                    if (!exception.IsMatch(OneDriveErrorCode.AuthenticationCancelled.ToString()))
                    {
                        if (exception.IsMatch(OneDriveErrorCode.AuthenticationFailure.ToString()))
                        {
                            MessageBox.Show(
                                "Authentication failed",
                                "Authentication failed", MessageBoxButtons.OK);

                            var httpProvider = this.oneDriveClient.HttpProvider as HttpProvider;
                            httpProvider.Dispose();
                            this.oneDriveClient = null;
                        }
                        else
                        {
                            PresentOneDriveException(exception);
                        }
                    }
                }

            }
        }

        private static void PresentOneDriveException(Exception exception)
        {
            string message = null;
            var oneDriveException = exception as OneDriveException;
            if (oneDriveException == null)
            {
                message = exception.Message;
            }
            else
            {
                message = string.Format("{0}{1}", Environment.NewLine, oneDriveException.ToString());
            }

            MessageBox.Show(string.Format("OneDrive reported the following error: {0}", message));
        }

        public async Task LoadFolderFromPath(string path = null)
        {
            /*
            Input: Path to a folder 
            Operation: Attempt to find the folder specified from the path. 
                       Direct 
            */
            if (null == this.oneDriveClient) return;

            //LoadChildren(new Item[0]); // No idea what this does. 

            try
            {
                Item folder;
                var expandvalue = "thumbnails, children(expand=thumbnails)";

                if (path == null) {
                    // Root access. 
                    
                    folder = await this.oneDriveClient.Drive.Root.Request().Expand(expandvalue).GetAsync();
                }
                else
                {
                    folder = await this.oneDriveClient.Drive.Root.ItemWithPath("/" + path)
                            .Request()
                            //.Expand(expandvalue)
                            .GetAsync();

                    string itemID = folder.Id;
                    MessageBox.Show(folder.Name);
                    string message = "";

                    var contentStream = await oneDriveClient
                              .Drive
                              .Items[itemID]
                              .Content
                              .Request()
                              .GetAsync();

                    this.CurrentImage = Bitmap.FromStream(contentStream);  
                    
                    // Let's also set a listener. Will it work? Who knows.

                    
                                     
                }

                ProcessFolder(folder);
            }
            catch (Exception exception)
            {
                PresentOneDriveException(exception);
            }

        }

        private void ProcessFolder(Item folder)
        {
            if (folder != null)
            {
                // Folder was obtained. 
                this.CurrentFolder = folder;

                LoadProperties(folder);

                if (folder.Folder != null && folder.Children != null && folder.Children.CurrentPage != null)
                {
                    LoadChildren(folder.Children.CurrentPage);
                }
            }
        }

        private void LoadProperties(Item item)
        {
            this.SelectedItem = item;
            //this.SelectedItem.
        }

        private void CreateWebhook(string url)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://url"); // need to figure out actual url 
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{ "
            }          
        }

        private void LoadChildren(IList<Item> items)
        {                     
            // Load the children
            foreach (var obj in items)
            {
                AddItemToFolderContents(obj);
            }

            //flowLayoutContents.ResumeLayout();
        }

        private void AddItemToFolderContents(Item obj)
        {
           // flowLayoutContents.Controls.Add(CreateControlForChildObject(obj));
        }
    }
}
