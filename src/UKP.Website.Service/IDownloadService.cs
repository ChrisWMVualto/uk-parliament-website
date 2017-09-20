using System;
using System.Collections.Generic;
using UKP.Website.Service.Model;

namespace UKP.Website.Service
{
    public interface IDownloadService
    {
        void CreateDownload(string emailAddress);
        void DownloadCallback(string emailAddress);
    }
}
