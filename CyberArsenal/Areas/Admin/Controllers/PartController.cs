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
                var currentReference = _unitOfWork.Part.FirstOrDefault(u => u.Type == part.Type && u.Reference == true);

                //If there is no reference, this is now the reference
                if (currentReference == null)
                {
                    part.Reference = true;
                    part.Score = 100;
                }
                //If score wasn't already 100 it is now
                else if(currentReference.Id == part.Id)
                {
                    part.Score = 100;
                }
                //Skip logic if this part is the current reference
                else if (currentReference.Id != part.Id)
                {
                    //If part is a reference, if there is already a reference of this type, find it and make it false
                    if (part.Reference)
                    {
                        currentReference.Reference = false;
                        _unitOfWork.Part.Update(currentReference);
                        _unitOfWork.Save();

                        part.Score = 100;
                    }
                    //Otherwise just score the part based off the reference
                    else
                    {
                        //Zero exceptions since we divide
                        if (currentReference.BenchmarkPoints == 0)
                        {
                            currentReference.BenchmarkPoints = 1;
                        }
                        if (part.BenchmarkPoints == 0)
                        {
                            part.BenchmarkPoints = 1;
                        }

                        int score = (part.BenchmarkPoints * 100 )/ currentReference.BenchmarkPoints;
                        //Set a cap in case of weird numbers
                        if (score > 150)
                        {
                            score = 150;
                        }
                        part.Score = score;
                    }
                }

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

        [HttpGet]
        public IActionResult MassAdd()
        {
            Part obj = new Part();

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MassAdd(Part part)
        {
            if (ModelState.IsValid)
            {
                var currentReference = _unitOfWork.Part.FirstOrDefault(u => u.Type == part.Type && u.Reference == true);

                foreach (var name in part.Name.Split(new char[] { ';' }, System.StringSplitOptions.RemoveEmptyEntries))
                {
                    Part tempPart = new Part
                    {
                        Id = 0,
                        Name = name,
                        Benchmark = part.Benchmark,
                        BenchmarkPoints = part.BenchmarkPoints,
                        Type = part.Type,
                        Reference = false,
                        ReleaseDate = part.ReleaseDate,
                        Price = part.Price,
                    };

                    //Logic for scoring
                    //Zero exceptions since we divide
                    if (currentReference.BenchmarkPoints == 0)
                    {
                        currentReference.BenchmarkPoints = 1;
                    }
                    if (tempPart.BenchmarkPoints == 0)
                    {
                        tempPart.BenchmarkPoints = 1;
                    }

                    int score = (tempPart.BenchmarkPoints * 100) / currentReference.BenchmarkPoints;
                    //Set a cap in case of weird numbers
                    if (score > 150)
                    {
                        score = 150;
                    }
                    tempPart.Score = score;

                    _unitOfWork.Part.Add(tempPart);
                    _unitOfWork.Save();
                }

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
