using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using UKP.Website.Service.Model;

namespace UKP.Website.Models.Event
{
    public class ClippingViewModel
    {
        public ClippingViewModel(VideoModel videoModel, TimeSpan? start, TimeSpan? end, DateTime defaultStartDate, DateTime defaultEndDate)
        {
            VideoModel = videoModel;
            Start = start;
            End = end;
            DefaultStartDate = defaultStartDate;
            DefaultEndDate = defaultEndDate;

            var firstDay = videoModel.Event.DisplayStartDate.ToLocalTime();
            var secondDay = videoModel.Event.DisplayStartDate.AddDays(1).ToLocalTime();
            var startFirstSelected = defaultStartDate.ToLocalTime().Date == firstDay.Date;
            var startSecondSelected = defaultStartDate.ToLocalTime().Date == secondDay.Date;
            var endFirstSelected = defaultEndDate.ToLocalTime().Date == firstDay.Date;
            var endSecondSelected = defaultEndDate.ToLocalTime().Date == secondDay.Date || !endFirstSelected;

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
        public TimeSpan? Start { get; private set; }
        public TimeSpan? End { get; private set; }
        public DateTime DefaultStartDate { get; private set; }
        public DateTime DefaultEndDate { get; private set; }
        public IEnumerable<SelectListItem> StartDates { get; private set; }
        public IEnumerable<SelectListItem> EndDates { get; private set; }
    }
}