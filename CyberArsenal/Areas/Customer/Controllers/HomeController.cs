﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CyberArsenal.Models;
using CyberArsenal.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using CyberArsenal.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace CyberArsenal.Areas.Customer
{
    [AllowAnonymous]
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var buildList = _unitOfWork.Build.GetAll();

            return View(buildList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #region API CALLS
        public IActionResult GetBuilds()
        {
            var objList = _unitOfWork.Build.GetAll(u => u.Private == false, "ApplicationUser,Cpu,Gpu,Ram,Storage");
            return Json(new { data = objList });
        }
        #endregion
    }
}
