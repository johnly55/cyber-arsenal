using CyberArsenal.DataAccess.Repository.IRepository;
using CyberArsenal.Models;
using Microsoft.AspNetCore.Mvc;

namespace CyberArsenal.Areas.Customer.Controllers
{
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
    }
}
