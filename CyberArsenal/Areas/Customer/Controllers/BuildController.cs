using CyberArsenal.DataAccess.Repository.IRepository;
using CyberArsenal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CyberArsenal.Areas.Customer.Controllers
{
    [Authorize]
    [Area("Customer")]
    public class BuildController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public BuildController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var objList = _unitOfWork.Build.GetAll();

            return View(objList);
        }

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            Build obj = new Build();

            if (id == null)
            {
                return View(obj);
            }

            obj = _unitOfWork.Build.Get(id.GetValueOrDefault());

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Build build)
        {
            if (ModelState.IsValid)
            {
                if (build.Id != 0)
                {
                    _unitOfWork.Build.Update(build);
                }
                else
                {
                    _unitOfWork.Build.Add(build);
                }
                _unitOfWork.Save();

                return RedirectToAction(nameof(Index));
            }

            return View(build);
        }

        public IActionResult Detail(int id)
        {
            Build obj;

            obj = _unitOfWork.Build.Get(id);

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        public IActionResult Compare(int id)
        {
            Build obj;

            obj = _unitOfWork.Build.Get(id);

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        #region API CALLS
        public IActionResult GetAll()
        {
            var objList = _unitOfWork.Build.GetAll();

            return Json(new { data = objList });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var obj = _unitOfWork.Build.Get(id);

            if (obj != null)
            {
                _unitOfWork.Build.Remove(obj);
                _unitOfWork.Save();
            }

            return Json(new { success = true });
        }

        #endregion
    }
}
