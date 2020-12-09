using CyberArsenal.DataAccess.Repository.IRepository;
using CyberArsenal.Models;
using CyberArsenal.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CyberArsenal.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.ROLE_ADMIN + "," + SD.ROLE_MOD)]
    [Area("Admin")]
    public class PartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public PartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var obj = new Part();
            return View(obj);
        }

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            Part obj = new Part();

            if (id == null)
            {
                return View(obj);
            }

            obj = _unitOfWork.Part.Get(id.GetValueOrDefault());

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Part part)
        {
            if (ModelState.IsValid)
            {
                if (part.Id != 0)
                {
                    _unitOfWork.Part.Update(part);
                }
                else
                {
                    _unitOfWork.Part.Add(part);
                }
                _unitOfWork.Save();

                return RedirectToAction(nameof(Index));
            }

            return View(part);
        }

        public IActionResult Detail(int id)
        {
            Part obj;

            obj = _unitOfWork.Part.Get(id);

            if (obj == null)
            {
                return NotFound();
            }

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

        public IActionResult GetAll(int partType)
        {
            var objList = _unitOfWork.Part.GetAll();

            return Json(new {data = objList});
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var obj = _unitOfWork.Part.Get(id);

            if(obj != null)
            {
                _unitOfWork.Part.Remove(obj);
                _unitOfWork.Save();
            }

            return Json(new {success = true});
        }

        #endregion
    }
}
