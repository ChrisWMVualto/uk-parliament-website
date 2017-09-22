using System;
using System.Collections.Generic;
using UKP.Website.Service.Model;

namespace UKP.Website.Service
{
    public interface IDownloadService
    {
        DownloadResponseModel CreateDownload(Guid evenId, DateTime startTime, DateTime endTime, string emailAddress, bool audioOnly);
    }
}
