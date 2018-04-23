using Benchmark.Models;
using Benchmark.Services.Business;
using Benchmark.Services.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


/*
 * Jordan Riley
 * 4-22-2018
 * BibleController class
 * Directs flow of bible views and form submissions
 * 
 */




namespace Benchmark.Controllers
{
    public class BibleController : Controller
    {


        private readonly ILogger logger;


        public BibleController(ILogger logger)
        {
            this.logger = logger;
        }


        // GET: Bible
        public ActionResult Index()
        {
            logger.Info("BibleController::Index()");
            return View("Create");
        }


        // GET: Bible
        public ActionResult Search()
        {
            logger.Info("BibleController::Search()");
            return View("Search");
        }

        [HttpPost]
        public ActionResult doEntry(BibleVerse bibleVerse)
        {
            logger.Info("doEntry:: Enter");

            //validate data
            if (ModelState.IsValid)
            {
                logger.Info("doEntry:: Model is valid");

                BibleService bibleService = new BibleService();
                if (bibleService.createVerse(bibleVerse))
                {

                    return View("../Home/Index", new ActionResponse("Successfully entered verse"));
                }
            }


            logger.Info("doEntry:: Model isn't valid");

            return View("Create");

        }

        [HttpGet]
        public ActionResult doSearch(SearchRequest searchRequest)
        {
            logger.Info("doSearch:: Enter");
            //validate data
            if (ModelState.IsValid)
            {
                logger.Info("doSearch:: model is valid");
                BibleService bibleService = new BibleService();

                BibleVerse bibleVerse = bibleService.findVerse(searchRequest);

                if (bibleVerse != null)
                {
                    logger.Info("doSearch:: bible verse found");
                    return View("Result", bibleVerse);
                }
            }
            else
            {
                logger.Info("doSearch:: model invalid");
                return View("Search");
            }

            return View("Result");

        }

    }



}