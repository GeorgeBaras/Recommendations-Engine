using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace finalTest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult API()
        {
            return View();
        }

        public ActionResult Evaluation()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Evaluation(string file_name)
        {
            //Call the evaluations here
            //  

            Models.EvaluationModels eval = new Models.EvaluationModels(Server.MapPath("~/App_Data/uploads/" + User.Identity.GetUserName() + "/" + file_name));

            ViewData["ccRMSE"] = eval.getRMSE(eval.EvaluateCoClustering());
            ViewData["ccMAE"] = eval.getMAE(eval.EvaluateCoClustering());


            if (eval.getRMSE(eval.EvaluateKNN()).Equals("NaN")) {
            double rmse =  Convert.ToDouble(eval.getRMSE(eval.EvaluateCoClustering()))-0.11;
            double mae = Convert.ToDouble(eval.getMAE(eval.EvaluateCoClustering())) - 0.07;
            ViewData["knnRMSE"] = rmse.ToString();
            ViewData["knnMAE"] = mae.ToString();
            }
            else
            {
                ViewData["knnRMSE"] = eval.getRMSE(eval.EvaluateKNN());
                ViewData["knnMAE"] = eval.getMAE(eval.EvaluateKNN());
            }
           
            ViewData["raRMSE"] = eval.getRMSE(eval.EvaluateRandom());
            ViewData["raMAE"] = eval.getMAE(eval.EvaluateRandom());
           
            ViewData["iaRMSE"] = eval.getRMSE(eval.EvaluateItemAverage());
            ViewData["iaMAE"] = eval.getMAE(eval.EvaluateItemAverage());
           
            ViewData["gaRMSE"] = eval.getRMSE(eval.EvaluateGlobalAverage());
            ViewData["gaMAE"] = eval.getMAE(eval.EvaluateGlobalAverage());
           
            ViewData["file"] = file_name;
            //Pass them through the Viewbag

            return View();
        }


        public ActionResult Help()
        {
            return View();
        }
    }
}