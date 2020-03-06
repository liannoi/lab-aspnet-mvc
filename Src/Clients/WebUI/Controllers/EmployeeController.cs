using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using HumanResources.Common.Repositories;
using HumanResources.Domain.Entities;
using HumanResources.WebUI.Areas.Exceptions;

namespace HumanResources.WebUI.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeRepository _employeeRepository;

        public EmployeeController()
        {
            _employeeRepository = new EmployeeRepository();
            _employeeRepository.Resolve();
        }

        [HttpGet]
        public ViewResult Index()
        {
            return View(_employeeRepository.Result.Select());
        }

        [HttpGet]
        public ViewResult Update(int id)
        {
            if (id == 0)
            {
                PrepareViewBagForCreate();
                return View(new EmployeeEntity());
            }

            PrepareViewBagForUpdate();
            return View(_employeeRepository.Result.Find(e => e.EmployeeId == id).FirstOrDefault());
        }

        [HttpPost]
        public RedirectToRouteResult Update(EmployeeEntity entity)
        {
            CheckModelState();
            AddOrUpdate(entity);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public HttpStatusCodeResult Delete(int id)
        {
            try
            {
                _employeeRepository.Result.Remove(_employeeRepository.Result.Find(e => e.EmployeeId == id)
                    .FirstOrDefault());
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Conflict);
            }
        }

        private void AddOrUpdate(EmployeeEntity entity)
        {
            if (entity.EmployeeId == 0)
                _employeeRepository.Result.Add(entity);
            else
                _employeeRepository.Result.Update(entity, entity);
        }

        private void CheckModelState()
        {
            if (!ModelState.IsValid) throw new InvalidModelStateException();
        }

        private void PrepareViewBagForCreate()
        {
            ViewBag.PageMessage = "Create new employee";
            ViewBag.ButtonText = "Add";
        }

        private void PrepareViewBagForUpdate()
        {
            ViewBag.PageMessage = "Update this employee";
            ViewBag.ButtonText = "Save";
        }
    }
}