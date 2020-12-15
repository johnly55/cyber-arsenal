using CyberArsenal.DataAccess.Repository.IRepository;
using CyberArsenal.Models;
using CyberArsenal.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CyberArsenal.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.ROLE_ADMIN + "," + SD.ROLE_MOD)]
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;

        public UserController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View(new ApplicationUser());
        }

        #region API CALLS

        public IActionResult GetAll()
        {
            var objList = _userManager.Users;

            return Json(new {data = objList});
        }
        public async Task<IActionResult> LockUnlock(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if(user == null)
            {
                return Json(new { success = false });
            }

            if (user.LockoutEnd > DateTime.Now)
            {
                user.LockoutEnd = DateTime.Now;
            }
            else
            {
                user.LockoutEnd = DateTime.Now.AddYears(2000);
            }

            await _userManager.UpdateAsync(user);
            _unitOfWork.Save();

            return Json(new { success = true });
        }

        #endregion
    }
}
