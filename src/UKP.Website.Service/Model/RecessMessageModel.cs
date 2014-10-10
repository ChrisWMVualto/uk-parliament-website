using System;

namespace UKP.Website.Service.Model
{
    public class RecessMessageModel
    {
        public DateTime ExpirationDate { get; set; }
        public string Message { get; set; }

        public RecessMessageModel(DateTime expirationDate, string message)
        {
            ExpirationDate = expirationDate;
            Message = message;
        }
    }
}
