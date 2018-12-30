using System;
using System.Web.Mvc;
using Product.Services;

namespace ProductWeb.Controllers
{
    public class MaterialController : Controller
    {
        private readonly IMaterialService _materialService;
        

        public MaterialController(IMaterialService materialService)
        {
            _materialService = materialService;
        }

        // GET
        public ActionResult Index()
        {
            return View(_materialService.GetAll());
        }

        // GET
        public ActionResult View(Int32 id)
        {
            return View(_materialService.GetById(id));
        }

        public ActionResult Merge(Int32 id)
        {
            var materialToDelete = _materialService.GetMaterialToDelete(id);
            var materialToKeep = _materialService.GetMaterialToKeep(id);
            if (materialToDelete != null && materialToDelete.MaterialId == id)
            {
                _materialService.Merge(materialToKeep.MaterialId, materialToDelete.MaterialId);
            }
            return View("Index", _materialService.GetAll());
        }
    }
}