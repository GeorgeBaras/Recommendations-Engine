using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
// for the recommendations
using finalTest.Models;


namespace finalTest.Models
{
    public class FileController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Delete()
        {
            return View();
        }

        

        [HttpPost]
        public ActionResult Delete(string file_name)
        {
            //Delete the file passed by the form
            System.IO.File.Delete(Server.MapPath("~/App_Data/uploads/" + User.Identity.GetUserName() +"/"+ file_name));
            return View();
        }

        //
        // GET: /File/
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(HttpPostedFileBase file, string file_name, string file_type, string NoR)
        {
            //Convert the NoR to int
            int NoRecs = Convert.ToInt16(NoR);
            //add the .txt extension if it is not there
            string myfilename = file_name+".txt";
            //do nothing for the file type
            
            //If the folder for the user does not exist,create it
            DirectoryInfo di = Directory.CreateDirectory(Server.MapPath("~/App_Data/uploads/" + User.Identity.GetUserName()));

            //check that the file is of valid 
            if (file.ContentLength > 0)
            {
                //save the file given
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/App_Data/uploads/" + User.Identity.GetUserName()), myfilename);
                file.SaveAs(path);

                ///////create a file with the top rated
                //create a recommendations object
                Recommendations topRated = new Recommendations();
                topRated.writeToFile(path);
            }




            return RedirectToAction("Index");
            
        }

       


    }
}