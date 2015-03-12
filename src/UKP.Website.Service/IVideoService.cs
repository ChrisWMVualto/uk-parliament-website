using System;
using UKP.Website.Service.Model;

namespace UKP.Website.Service
{
    public interface IVideoService
    {
        VideoModel GetVideo(Guid id, string requestedUsersIPAddress, DateTime? inPoint = null, DateTime? outPoint = null, bool? audioOnly = null, bool? autoStart = null, bool? statsEnabled = false, bool? processLogs = null);
        VideoModel GetLegacyVideo(int id);   
    }
}