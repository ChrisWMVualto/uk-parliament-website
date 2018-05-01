using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UKP.Website.Service.Model
{
    public class DownloadUrlModel
    {
        public DownloadUrlModel(Guid id, string url)
        {
            Id = id;
            Url = url;
        }

        public Guid Id { get; set; }
        public string Url { get; set; }
    }
}
