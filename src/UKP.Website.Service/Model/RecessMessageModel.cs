using System;

namespace UKP.Website.Service.Model
{
    public class RecessMessageModel
    {
        public DateTime ExpirationDate { get; private set; }
        public DateTime ReturnDate { get; private set; }
        public string Message { get; private set; }

        public RecessMessageModel(DateTime expirationDate, string message)
        {
            ExpirationDate = expirationDate;
            Message = message;
            ReturnDate = ExpirationDate.AddDays(1);
        }
    }
}
