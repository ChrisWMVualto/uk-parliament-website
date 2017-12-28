using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UKP.Website.Models.Event
{
    public class CreateDownloadResponseModel
    {
        public CreateDownloadResponseModel(string email, bool success = false, string message = null)
        {
            Success = success;
            Message = message;
            InPointHasError = true;
            OutPointHasError = true;
            Email = email;
        }
        public bool Success { get; set; }
        public bool InPointHasError { get; set; }
        public bool OutPointHasError { get; set; }
        public string Message { get; set; }
        public string Email { get; set; }
    }
}