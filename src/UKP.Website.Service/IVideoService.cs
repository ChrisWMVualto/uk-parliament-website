using System;
using UKP.Website.Service.Model;

namespace UKP.Website.Service
{
    public interface IVideoService
    {
        VideoModel GetVideo(Guid id, DateTime? inPoint = null, DateTime? outPoint = null, bool? audioOnly = null);
        VideoModel GetLegacyVideo(int id);
    }
}