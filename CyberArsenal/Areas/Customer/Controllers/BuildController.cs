using CyberArsenal.DataAccess.Repository.IRepository;
using CyberArsenal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using CyberArsenal.Models.ViewModels;
using CyberArsenal.Utilities;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CyberArsenal.Areas.Customer.Controllers
{
    [Authorize]
    [Area("Customer")]
    public class BuildController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;

        public BuildController(
            IUnitOfWork unitOfWork, 
            UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var obj = new Build(); 

            return View(obj);
        }

        [HttpGet]
        public IActionResult Upsert(int? id, int cpuId = 0, int gpuId = 0, int ramId = 0, int storageId = 0)
        {
            //If new build, start adding components: cpu, gpu, ram, storage first
            if (id == null)
            {
                return RedirectToAction(nameof(AddComponent));
            }
            else if(id == 0)
            {
                Build newBuild;

                //Skip component selection
                if(cpuId == 0 && gpuId == 0 && ramId == 0 && storageId == 0)
                {
                    newBuild = new Build
                    {
                        ApplicationUserId = _userManager.GetUserId(User)
                    };
                }
                //Component search finished and finalizing PC
                else
                {
                    var cpu = _unitOfWork.Part.Get(cpuId);
                    var gpu = _unitOfWork.Part.Get(gpuId);
                    var ram = _unitOfWork.Part.Get(ramId);
                    var storage = _unitOfWork.Part.Get(storageId);

                    newBuild = new Build
                    {
                        ApplicationUserId = _userManager.GetUserId(User),

                        CpuId = cpu?.Id,
                        GpuId = gpu?.Id,
                        RamId = ram?.Id,
                        StorageId = storage?.Id,

                        CpuName = cpu?.Name,
                        GpuName = gpu?.Name,
                        RamName = ram?.Name,
                        StorageName = storage?.Name
                    };
                }

                return View(newBuild);
            }

            var build = _unitOfWork.Build.FirstOrDefault(u => u.Id == id.GetValueOrDefault(), "Cpu,Gpu,Ram,Storage");

            if (build == null)
            {
                return NotFound();
            }

            //If user besides creator tries to edit, redirect
            if(build.ApplicationUserId != _userManager.GetUserId(User))
            {
                return RedirectToAction(nameof(Index));
            }

            return View(build);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Build build)
        {
            if (ModelState.IsValid)
            {
                //Checks for parameters that could be public and seen by everyone
                //if that component is customized, it is automatically priavte
                if(build.CpuId == 0 || build.CpuId == null ||
                    build.GpuId == 0 || build.GpuId == null ||
                    build.RamId == 0 || build.RamId == null ||
                    build.StorageId == 0 || build.StorageId == null)
                {
                    build.Private = true;

                    build.CpuId = null;
                    build.GpuId = null;
                    build.RamId = null;
                    build.StorageId = null;
                }
                //If the user picked a component before but now customized it, private it
                //otherwise, score it
                else
                {
                    var cpu = _unitOfWork.Part.Get(build.CpuId.GetValueOrDefault());
                    var gpu = _unitOfWork.Part.Get(build.GpuId.GetValueOrDefault());
                    var ram = _unitOfWork.Part.Get(build.RamId.GetValueOrDefault());
                    var storage = _unitOfWork.Part.Get(build.StorageId.GetValueOrDefault());

                    if(build.CpuName != cpu.Name ||
                       build.GpuName != gpu.Name ||
                       build.RamName != ram.Name ||
                       build.StorageName != storage.Name)
                    {
                        build.Private = true;
                        build.CpuId = null;
                        build.GpuId = null;
                        build.RamId = null;
                        build.StorageId = null;
                        build.Score = 0;
                    }
                    else
                    {
                        build.Score = (int)(cpu.Score + gpu.Score + 
                            ram.Score + storage.Score) / 4;
                    }
                }

                if (build.Id != 0)
                {
                    _unitOfWork.Build.Update(build);
                }
                else
                {
                    _unitOfWork.Build.Add(build);
                }
                _unitOfWork.Save();

                return RedirectToAction(nameof(Detail), new { id = build.Id });
            }

            return View(build);
        }

        public IActionResult Detail(int id)
        {
            Build obj;

            obj = _unitOfWork.Build.FirstOrDefault(u => u.Id == id, "Cpu,Gpu,Ram,Storage");

            if (obj == null)
            {
                return NotFound();
            }

            //If user besides creator tries to see details, redirect
            if (obj.ApplicationUserId != _userManager.GetUserId(User))
            {
                return RedirectToAction(nameof(Index));
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

        public IActionResult Cpu(BuildViewModel buildViewModel)
        {
            return View(buildViewModel);
        }

        public IActionResult Gpu(BuildViewModel buildViewModel)
        {
            return View(buildViewModel);
        }

        public IActionResult Ram(BuildViewModel buildViewModel)
        {
            return View(buildViewModel);
        }

        public IActionResult Storage(BuildViewModel buildViewModel)
        {
            return View(buildViewModel);
        }

        #region API CALLS

        [HttpGet]
        public IActionResult AddComponent(int cpuId = 0, int gpuId = 0, int ramId = 0, int storageId = 0, int page = 0)
        {
            var buildViewModel = new BuildViewModel
            {
                CpuId = cpuId,
                GpuId = gpuId,
                RamId = ramId,
                StorageId = storageId,
                Page = page
            };

            buildViewModel.Page++;

            if (buildViewModel.Page == 1)
            {
                return View(nameof(Cpu), buildViewModel);
            }
            else if (buildViewModel.Page == 2)
            {
                return View(nameof(Gpu), buildViewModel);
            }
            else if (buildViewModel.Page == 3)
            {
                return View(nameof(Ram), buildViewModel);
            }
            else if (buildViewModel.Page == 4)
            {
                return View(nameof(Storage), buildViewModel);
            }
            else
            {
                return RedirectToAction(nameof(Upsert), new { id = 0, cpuId = cpuId, gpuId = gpuId, ramId = ramId, storageId = storageId });
            }
        }

        public IActionResult GetCpu()
        {
            var objList = _unitOfWork.Part.GetAll(u => u.Type == "Cpu");

            return Json(new { data = objList });
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

        public IActionResult GetAll()
        {
            var objList = _unitOfWork.Build.GetAll
                (
                    u => u.ApplicationUserId == _userManager.GetUserId(User),
                    "Cpu,Gpu,Ram,Storage"
                );

            return Json(new { data = objList });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var obj = _unitOfWork.Build.Get(id);

            if (obj.ApplicationUserId == _userManager.GetUserId(User))
            {

                if (obj != null)
                {
                    _unitOfWork.Build.Remove(obj);
                    _unitOfWork.Save();
                }

                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }

        #endregion
    }
}
