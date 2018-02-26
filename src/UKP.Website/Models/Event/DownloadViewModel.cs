using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UKP.Website.Service.Model;

namespace UKP.Website.Models.Event
{
    public class DownloadViewModel
    {
        public DownloadViewModel(VideoModel videoModel)
        {
            VideoModel = videoModel;
            DefaultStartDate = VideoModel.RequestedInPoint.HasValue ? VideoModel.RequestedInPoint.Value : VideoModel.Event.DisplayStartDate;
            DefaultEndDate = VideoModel.RequestedOutPoint.HasValue ? VideoModel.RequestedOutPoint.Value : VideoModel.Event.DisplayEndDate;

            var firstDay = videoModel.Event.DisplayStartDate.ToLocalTime();
            var secondDay = videoModel.Event.DisplayStartDate.AddDays(1).ToLocalTime();
            var startFirstSelected = DefaultStartDate.ToLocalTime().Date == firstDay.Date;
            var startSecondSelected = DefaultStartDate.ToLocalTime().Date == secondDay.Date;
            var endFirstSelected = DefaultEndDate.ToLocalTime().Date == firstDay.Date;
            var endSecondSelected = DefaultEndDate.ToLocalTime().Date == secondDay.Date || !endFirstSelected;

            StartDates = new List<SelectListItem>()
            {
                new SelectListItem() { Text = firstDay.ToString("d MMMM yyyy"), Value = firstDay.ToString("yyyy-MM-dd"), Selected = startFirstSelected},
                new SelectListItem() { Text = secondDay.ToString("d MMMM yyyy"), Value = secondDay.ToString("yyyy-MM-dd"), Selected = startSecondSelected}
            };

            EndDates = new List<SelectListItem>()
            {
                new SelectListItem() { Text = firstDay.ToString("d MMMM yyyy"), Value = firstDay.ToString("yyyy-MM-dd"), Selected = endFirstSelected},
                new SelectListItem() { Text = secondDay.ToString("d MMMM yyyy"), Value = secondDay.ToString("yyyy-MM-dd"), Selected = endSecondSelected}
            };
        }

        public VideoModel VideoModel { get; private set; }
        public DateTime DefaultStartDate { get; private set; }
        public DateTime DefaultEndDate { get; private set; }
        public IEnumerable<SelectListItem> StartDates { get; private set; }
        public IEnumerable<SelectListItem> EndDates { get; private set; }
    }
}