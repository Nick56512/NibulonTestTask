using BLL.DTO;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using NibulonTestTask.Models.ViewModels;
using System.Text;

namespace NibulonTestTask.Controllers
{
    public class GrainController : Controller
    {
        IGrainService _grainService;
        public GrainController(IGrainService _grainService)
        {
            this._grainService = _grainService;
        }
        public IActionResult Update(int recordId)
        {
            UpdateGrainViewModel model = new UpdateGrainViewModel();
            GrainDto grain=_grainService.Get(recordId);
            if (grain != null)
            {
                model.RecordId = recordId;
                model.Wetness = grain.Wetness;
                model.Amount = grain.Amount;
                model.Garbage = grain.Garbage;
                model.Infection = grain.Infection;
                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public IActionResult UpdateGrain(UpdateGrainViewModel model)
        {
            if (ModelState.IsValid)
            {
                GrainDto grain = _grainService.Get(model.RecordId);
                StringBuilder message = new StringBuilder($"Останні оновлені дані у запису під номером {model.RecordId}  ");

                if (grain.Amount != model.Amount)
                {
                    message.Append($"кількість - {grain.Amount};") ;
                }
                if (grain.Wetness != model.Wetness)
                {
                    message.Append($"вологість - {grain.Wetness};");
                }
                if (grain.Garbage != model.Garbage)
                {
                    message.Append($"сміття - {grain.Garbage};");
                }
                if (grain.Infection != model.Infection)
                {
                    message.Append($"зараженість - {grain.Infection};");
                }
                grain.Amount = model.Amount;
                grain.Wetness = model.Wetness;
                grain.Garbage = model.Garbage;
                model.Infection = model.Infection;

                _grainService.Update(grain);
                Response.HttpContext.Session.SetString("info",message.ToString());

                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Update", "Grain");
        }

        [HttpPost]
        public IActionResult Delete(int recordId)
        {
            var res=_grainService.Delete(recordId);
            return RedirectToAction("Index", "Home");
        }
    }
}
