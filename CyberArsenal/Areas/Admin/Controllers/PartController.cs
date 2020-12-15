using CyberArsenal.DataAccess.Repository.IRepository;
using CyberArsenal.Models;
using CyberArsenal.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

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
                        if (score > 300)
                        {
                            score = 300;
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
                    if (score > 300)
                    {
                        score = 300;
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

        //Grabs the reference of each component type and update non-reference scores
        public IActionResult UpdateScores()
        {
            var cpuReference = _unitOfWork.Part.FirstOrDefault(u => u.Type == SD.TYPE_CPU && u.Reference == true);
            var gpuReference = _unitOfWork.Part.FirstOrDefault(u => u.Type == SD.TYPE_GPU && u.Reference == true);
            var ramReference = _unitOfWork.Part.FirstOrDefault(u => u.Type == SD.TYPE_RAM && u.Reference == true);
            var storageReference = _unitOfWork.Part.FirstOrDefault(u => u.Type == SD.TYPE_STORAGE && u.Reference == true);

            var cpuList = _unitOfWork.Part.GetAll(u => u.Type == SD.TYPE_CPU && u.Reference == false);
            var gpuList = _unitOfWork.Part.GetAll(u => u.Type == SD.TYPE_GPU && u.Reference == false);
            var ramList= _unitOfWork.Part.GetAll(u => u.Type == SD.TYPE_RAM && u.Reference == false);
            var storageList= _unitOfWork.Part.GetAll(u => u.Type == SD.TYPE_STORAGE && u.Reference == false);



            var referenceList = new Part[] { cpuReference, gpuReference, ramReference, storageReference };
            var componentList = new Part[][] { 
                cpuList.Count() > 0 ? cpuList.ToArray() : null,
                gpuList.Count() > 0 ? gpuList.ToArray() : null,
                ramList.Count() > 0 ? ramList.ToArray() : null,
                storageList.Count() > 0 ? storageList.ToArray() : null
            };

            for (int i = 0; i < referenceList.Length; i++)
            {
                if(referenceList[i] != null && componentList[i] != null)
                {
                    var reference = referenceList[i];
                    for (int j = 0; j < componentList[i].Length; j++)
                    {
                        var part = componentList[i][j];

                        //Logic for scoring
                        //Zero exceptions since we divide
                        if (reference.BenchmarkPoints == 0)
                        {
                            reference.BenchmarkPoints = 1;
                        }
                        if (part.BenchmarkPoints == 0)
                        {
                            part.BenchmarkPoints = 1;
                        }

                        int score = (part.BenchmarkPoints * 100) / reference.BenchmarkPoints;
                        //Set a cap in case of weird numbers
                        if (score > 300)
                        {
                            score = 300;
                        }

                        part.Score = score;

                        _unitOfWork.Part.Update(part);
                        _unitOfWork.Save();
                    }
                }
            }
            return Json(new { success = true });
        }

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
