using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using HumanResources.Common.Repositories;
using HumanResources.Domain.Entities;
using HumanResources.WebUI.Areas.Exceptions;

namespace HumanResources.WebUI.Controllers
{
    public class JobController : Controller
    {
        private readonly JobTitleRepository _jobTitleRepository;

        public JobController()
        {
            _jobTitleRepository = new JobTitleRepository();
            _jobTitleRepository.Resolve();
        }

        [HttpGet]
        public ViewResult Index()
        {
            return View(_jobTitleRepository.Result.Select());
        }

        [HttpGet]
        public ViewResult Update(int id)
        {
            if (id == 0)
            {
                PrepareViewBagForCreate();
                return View(new JobTitleEntity());
            }

            PrepareViewBagForUpdate();
            return View(_jobTitleRepository.Result.Find(e => e.JobTitleId == id).FirstOrDefault());
        }

        [HttpPost]
        public RedirectToRouteResult Update(JobTitleEntity entity)
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
                _jobTitleRepository.Result.Remove(_jobTitleRepository.Result.Find(e => e.JobTitleId == id)
                    .FirstOrDefault());
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Conflict);
            }
        }

        private void AddOrUpdate(JobTitleEntity entity)
        {
            if (entity.JobTitleId == 0)
                _jobTitleRepository.Result.Add(entity);
            else
                _jobTitleRepository.Result.Update(entity, entity);
        }

        private void CheckModelState()
        {
            if (!ModelState.IsValid) throw new InvalidModelStateException();
        }

        private void PrepareViewBagForCreate()
        {
            ViewBag.PageMessage = "Create new job";
            ViewBag.ButtonText = "Add";
        }

        private void PrepareViewBagForUpdate()
        {
            ViewBag.PageMessage = "Update this job";
            ViewBag.ButtonText = "Save";
        }
    }
}