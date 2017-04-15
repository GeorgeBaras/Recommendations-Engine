using MyMediaLite.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
using System.Web.Http;
// for the recommendations
using finalTest.Models;
using System.IO;

namespace finalTest.Controllers
{
    public class RecsController : ApiController
    {
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

       

        
        //Recs fo certain user or item
        public List<int> Get(string uname, string fname,string option, int id, int recs)
        {
            //set the filename path
            string dataset = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/uploads/" + uname + "/" + fname);
            
            //create the list and an instance for the Recommendations class
            List<int> mylist = new List<int>();
            Recommendations listOfRecs = new Recommendations();

            /*  Available
             * getBestItemsKNN           -- knn_best
             * getBestItemsCoClustering  -- cc_best
             * getBestItemsGlobalAverage -- ga_best
             * getBestItemsRandom        -- ra_best
             * getBestItemsItemAverage   -- ia_best
             * getMostSimilarItems       -- knn_similar
             * getTopRatedItems          -- top
             */
            mylist.Clear();

            switch (option)
            {
                case "knn_best":
                    mylist = listOfRecs.getBestItemsKNN(dataset, id, recs);
                    break;
                case "cc_best":
                    mylist = listOfRecs.getBestItemsCoClustering(dataset, id, recs);
                    break;
                case "ga_best":
                    mylist = listOfRecs.getBestItemsGlobalAverage(dataset, id, recs);
                    break;
                case "ra_best":
                    mylist = listOfRecs.getBestItemsRandom(dataset, id, recs);
                    break;
                case "ia_best":
                    mylist = listOfRecs.getBestItemsItemAverage(dataset, id, recs);
                    break;
                case "knn_similar":
                    mylist = listOfRecs.getMostSimilarItems(dataset, id, recs);
                    break;
                default:  // Top Rated
                    mylist = listOfRecs.readTopFromFile((Path.GetDirectoryName(dataset) + "\\" + Path.GetFileNameWithoutExtension(dataset) + "TopRated.txt"), recs);
                    break;
            }

            return mylist;
        }////END GET

        
        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
