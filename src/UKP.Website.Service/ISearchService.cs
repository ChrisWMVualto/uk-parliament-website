﻿using System;
using UKP.Website.Service.Model;

namespace UKP.Website.Service
{
    public interface ISearchService
    {
        VideoCollectionModel Search(string keywords, int? memberId, string house, string business, DateTime period, int pageNum);
    }
}
