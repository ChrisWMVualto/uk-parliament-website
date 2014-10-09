﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UKP.Website.Service.Model
{
    public class VideoCollectionModel
    {
        public VideoCollectionModel(IEnumerable<VideoModel> results, int totalCount, int pageSize)
        {
            Results = results;
            TotalCount = totalCount;
            PageSize = pageSize;
        }

        public IEnumerable<VideoModel> Results { get; private set; }
        public int TotalCount { get; private set; }
        public int PageSize { get; private set; }
    }
}