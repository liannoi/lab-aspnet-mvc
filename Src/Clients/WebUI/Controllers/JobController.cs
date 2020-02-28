using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HumanResources.Common.Repositories;

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
        public ActionResult Index()
        {
            return View(_jobTitleRepository.Result.Select());
        }
    }
}