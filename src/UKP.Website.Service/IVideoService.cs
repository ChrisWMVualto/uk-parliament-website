using System;
using UKP.Website.Service.Model;

namespace UKP.Website.Service
{
    public interface IVideoService
    {
        VideoModel GetVideo(Guid id, DateTime? inPoint = null, DateTime? outPoint = null, bool? audioOnly = null, bool? autoStart = null, bool? statsEnabled = false,
            bool? processLogs = null, string requestedUsersIPAddress = null, bool? noCache = false);
        VideoModel GetLegacyVideo(int id);   
    }
}