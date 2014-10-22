using System.Collections.Generic;
using UKP.Website.Service.Model;

namespace UKP.Website.Models
{
    public class SearchViewModel
    {
        public SearchViewModel(SearchFormModel formModel, VideoCollectionModel videoCollectionResult = null)
        {
            VideoCollectionResult = videoCollectionResult;
            FormModel = formModel;
        }

        public SearchViewModel()
        {
            
        }

        public SearchFormModel FormModel { get; set; }
        public VideoCollectionModel VideoCollectionResult { get; set; }
    }
}