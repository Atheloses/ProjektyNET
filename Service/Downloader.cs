using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Net;
using System.Text;

namespace Service
{

    #region EventArgs

    public delegate void DownloadFinishedEvent(object source, DownloadFinishedEventArgs e);

    public class DownloadFinishedEventArgs : EventArgs
    {
        public string File { get; set; }
        public DownloadFinishedEventArgs(string p_File)
        {
            File = p_File;
        }
    }

    #endregion EventArgs

    public class Downloader
    {
        #region Private

        private readonly ILogger<Worker> _logger;

        #endregion Private

        #region ctor

        public Downloader(ILogger<Worker> p_Logger)
        {
            _logger = p_Logger;
        }

        #endregion ctor

        #region Public

        public event DownloadFinishedEvent DownloadFinished;

        public bool Save(string p_FileName, string p_Text)
        {
            if (String.IsNullOrEmpty(p_Text))
                return false;
            try
            {
                using IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForAssembly();
                using FileStream fs = storage.OpenFile("./" + p_FileName, FileMode.Append);
                using StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(p_Text);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{DateTimeOffset.Now}: Error while saving file: {ex.InnerException}");
                return false;
            }
        }

        public string Read(string p_FileName)
        {
            string output = string.Empty;
            try
            {
                using IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForAssembly();
                using FileStream fs = storage.OpenFile("./" + p_FileName, FileMode.OpenOrCreate);
                using StreamReader sw = new StreamReader(fs);
                output = sw.ReadToEnd();
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{DateTimeOffset.Now}: Error while saving file: {ex.InnerException}");
            }
            return output;
        }

        public string DownloadText(string p_Url)
        {
            string output = string.Empty;
            try
            {
                using (WebClient download = new WebClient())
                {
                    byte[] data = download.DownloadData(p_Url);
                    output = Encoding.UTF8.GetString(data);
                }

                DownloadFinished?.Invoke(this, new DownloadFinishedEventArgs(output));
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{DateTimeOffset.Now}: Error while downloading file: {ex}");
            }
            return output;
        }

        #endregion Public
    }
}
