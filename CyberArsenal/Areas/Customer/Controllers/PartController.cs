using CyberArsenal.DataAccess.Repository.IRepository;
using CyberArsenal.Models;
using CyberArsenal.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CyberArsenal.Areas.Customer.Controllers
{
    [Authorize]
    [Area("Customer")]
    public class PartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public PartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Cpu()
        {
            var obj = new Part();
            return View(obj);
        }

        public IActionResult Gpu()
        {
            var obj = new Part();
            return View(obj);
        }

        public IActionResult Ram()
        {
            var obj = new Part();
            return View(obj);
        }

        public IActionResult Storage()
        {
            var obj = new Part();
            return View(obj);
        }

        public IActionResult Compare(int id)
        {
            Part obj;

            obj = _unitOfWork.Part.Get(id);

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        #region API CALLS

        public IActionResult GetCpu()
        {
            var objList = _unitOfWork.Part.GetAll(u => u.Type == "Cpu");

            return Json(new {data = objList});
        }

        public IActionResult GetGpu()
        {
            var objList = _unitOfWork.Part.GetAll(u => u.Type == "Gpu");

            return Json(new { data = objList });
        }

        public IActionResult GetRam()
        {
            var objList = _unitOfWork.Part.GetAll(u => u.Type == "Ram");

            return Json(new { data = objList });
        }

        public IActionResult GetStorage()
        {
            var objList = _unitOfWork.Part.GetAll(u => u.Type == "Storage");

            return Json(new { data = objList });
        }

        #endregion
    }
}
