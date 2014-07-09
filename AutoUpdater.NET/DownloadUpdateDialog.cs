// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DownloadUpdateDialog.cs" company="">
//   
// </copyright>
// <summary>
//   The download update dialog.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AutoUpdaterDotNET
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.IO;
    using System.Net;
    using System.Net.Cache;
    using System.Windows.Forms;

    /// <summary>
    /// The download update dialog.
    /// </summary>
    internal partial class DownloadUpdateDialog : Form
    {
        #region Fields

        /// <summary>
        /// The _download url.
        /// </summary>
        private readonly string _downloadURL;

        /// <summary>
        /// The _temp path.
        /// </summary>
        private string _tempPath;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DownloadUpdateDialog"/> class.
        /// </summary>
        /// <param name="downloadURL">
        /// The download url.
        /// </param>
        public DownloadUpdateDialog(string downloadURL)
        {
            this.InitializeComponent();

            this._downloadURL = downloadURL;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The get file name.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string GetFileName(string url)
        {
            var fileName = string.Empty;

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
            httpWebRequest.Method = "HEAD";
            httpWebRequest.AllowAutoRedirect = false;
            var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            if (httpWebResponse.StatusCode.Equals(HttpStatusCode.Redirect)
                || httpWebResponse.StatusCode.Equals(HttpStatusCode.Moved)
                || httpWebResponse.StatusCode.Equals(HttpStatusCode.MovedPermanently))
            {
                if (httpWebResponse.Headers["Location"] != null)
                {
                    var location = httpWebResponse.Headers["Location"];
                    fileName = GetFileName(location);
                    return fileName;
                }
            }

            if (httpWebResponse.Headers["content-disposition"] != null)
            {
                var contentDisposition = httpWebResponse.Headers["content-disposition"];
                if (!string.IsNullOrEmpty(contentDisposition))
                {
                    const string lookForFileName = "filename=";
                    var index = contentDisposition.IndexOf(lookForFileName, StringComparison.CurrentCultureIgnoreCase);
                    if (index >= 0)
                    {
                        fileName = contentDisposition.Substring(index + lookForFileName.Length);
                    }

                    if (fileName.StartsWith("\"") && fileName.EndsWith("\""))
                    {
                        fileName = fileName.Substring(1, fileName.Length - 2);
                    }
                }
            }

            if (string.IsNullOrEmpty(fileName))
            {
                var uri = new Uri(url);

                fileName = Path.GetFileName(uri.LocalPath);
            }

            return fileName;
        }

        /// <summary>
        /// The download update dialog load.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void DownloadUpdateDialogLoad(object sender, EventArgs e)
        {
            var webClient = new WebClient();

            var uri = new Uri(this._downloadURL);

            this._tempPath = string.Format(@"{0}{1}", Path.GetTempPath(), GetFileName(this._downloadURL));

            webClient.DownloadProgressChanged += this.OnDownloadProgressChanged;

            webClient.DownloadFileCompleted += this.OnDownloadComplete;

            webClient.DownloadFileAsync(uri, this._tempPath);
        }

        /// <summary>
        /// The on download complete.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void OnDownloadComplete(object sender, AsyncCompletedEventArgs e)
        {
            var processStartInfo = new ProcessStartInfo { FileName = this._tempPath, UseShellExecute = true };
            Process.Start(processStartInfo);
            if (Application.MessageLoop)
            {
                Application.Exit();
            }
            else
            {
                Environment.Exit(1);
            }
        }

        /// <summary>
        /// The on download progress changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void OnDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            this.progressBar.Value = e.ProgressPercentage;
        }

        #endregion
    }
}