﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UKP.Website.Controllers
{
    public partial class EpgController : Controller
    {
        [HttpGet]
        public virtual ActionResult Index()
        {
            return View();
        }
    }
}