﻿using UKP.Website.Service.Model;

namespace UKP.Website.Service
{
    public interface IRecessService
    {
        RecessMessageModel GetRecessMessage(RecessMessageType type);
    }
}
