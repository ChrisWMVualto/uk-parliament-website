using System;
using UKP.Website.Service.Model;

namespace UKP.Website.Service
{
    public interface IVideoService
    {
        VideoModel GetVideo(Guid id);
        VideoModel GetLegacyVideo(int id);
    }
}