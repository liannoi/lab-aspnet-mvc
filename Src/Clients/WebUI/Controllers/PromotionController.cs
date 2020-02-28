using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using HumanResources.Common.Repositories;
using HumanResources.Domain.Entities;
using HumanResources.WebUI.Areas.Exceptions;
using HumanResources.WebUI.Areas.JsonEntities;

namespace HumanResources.WebUI.Controllers
{
    public class PromotionController : Controller
    {
        private readonly EmployeePromotionRepository _employeePromotionRepository;
        private readonly EmployeeRepository _employeeRepository;
        private readonly JobTitleRepository _jobTitleRepository;

        public PromotionController()
        {
            _employeePromotionRepository = new EmployeePromotionRepository();
            _employeeRepository = new EmployeeRepository();
            _jobTitleRepository = new JobTitleRepository();
            _employeePromotionRepository.Resolve();
            _employeeRepository.Resolve();
            _jobTitleRepository.Resolve();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(_employeeRepository.Result.Select());
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            PrepareViewBagsForBlazor();

            if (id == 0)
            {
                PrepareViewBagForCreate();
                return View(new EmployeePromotionEntity());
            }

            PrepareViewBagForUpdate();
            return View(_employeePromotionRepository.Result.Find(e => e.EmpPromotionId == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Update(EmployeePromotionEntity entity, FormCollection collection)
        {
            CheckModelState();
            Restore(entity, collection);
            AddOrUpdate(entity);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                _employeePromotionRepository.Result.Remove(_employeePromotionRepository.Result
                    .Find(e => e.EmpPromotionId == id)
                    .FirstOrDefault());
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Conflict);
            }
        }

        [HttpPost]
        [ActionName("_GetPromotions")]
        public ActionResult GetPromotions(EmployeeJsonEntity json)
        {
            return PartialView(_employeePromotionRepository.Result.Find(e => e.EmployeeId == json.EmployeeId));
        }

        #region Helpers

        private void Restore(EmployeePromotionEntity entity, FormCollection collection)
        {
            entity.EmployeeId = Convert.ToInt32(collection["employeeDropDownList"]);
            entity.JobTitleId = Convert.ToInt32(collection["empPromotionDropDownList"]);
        }

        private void AddOrUpdate(EmployeePromotionEntity entity)
        {
            if (entity.EmpPromotionId == 0)
                _employeePromotionRepository.Result.Add(entity);
            else
                _employeePromotionRepository.Result.Update(entity, entity);
        }

        private void CheckModelState()
        {
            if (!ModelState.IsValid) throw new InvalidModelStateException();
        }

        private void PrepareViewBagsForBlazor()
        {
            ViewBag.Jobs = _jobTitleRepository.Result.Select().Select(x => new SelectListItem
            {
                Value = x.JobTitleId.ToString(),
                Text = x.NameJobTitle
            });
            ViewBag.Employees = _employeeRepository.Result.Select().Select(x => new SelectListItem
            {
                Value = x.EmployeeId.ToString(),
                Text = $"{x.FirstName} {x.LastName}"
            });
        }

        private void PrepareViewBagForCreate()
        {
            ViewBag.PageMessage = "Create new promotion";
            ViewBag.ButtonText = "Add";
        }

        private void PrepareViewBagForUpdate()
        {
            ViewBag.PageMessage = "Update this promotion";
            ViewBag.ButtonText = "Save";
        }

        #endregion
    }
}