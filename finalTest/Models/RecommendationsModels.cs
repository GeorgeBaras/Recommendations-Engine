using MyMediaLite.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace finalTest.Models
{
    public class Recommendations
    {
        
        private List<int> mylist = new List<int>();

        //empty constructor
        public Recommendations() {
        }

        //*********** ItemKNN Recommender - most similar items
        public List<int> getMostSimilarItems(string dataset,int itemid, int recs){
            var mydata = RatingData.Read(dataset);
            //Create the recommender
            var recommenderItemKNN = new MyMediaLite.RatingPrediction.ItemKNN();
            //Give it the dataset
            recommenderItemKNN.Ratings = mydata;
            //Train it
            recommenderItemKNN.Train();
            /////////////

            var item_recs = recommenderItemKNN.GetMostSimilarItems(itemid, (uint)recs);

            // Print similar items
            foreach (var i in item_recs)
            {
                mylist.Add(i);
            }

            return mylist;
        }

        //*********** ItemKNN Recommender
        public List<int> getBestItemsKNN(string dataset, int userid, int recs)
        {
            var mydata = RatingData.Read(dataset);
            //Create the recommender
            var recommenderItemKNN = new MyMediaLite.RatingPrediction.ItemKNN();
            //Give it the dataset
            recommenderItemKNN.Ratings = mydata;
            //Train it
            recommenderItemKNN.Train();
            /////////////

            // Make the predictions
            var user_recs = recommenderItemKNN.Recommend(userid, recs);

            // get the recommendations
            foreach (var i in user_recs)
            {
                mylist.Add(i.Item1);
            }

            return mylist;
        }

        //*********** CoClustering Recommender
        public List<int> getBestItemsCoClustering(string dataset, int userid, int recs)
        {
            var mydata = RatingData.Read(dataset);
            //Create the recommender
            var recommenderCoClustering = new MyMediaLite.RatingPrediction.CoClustering();
            //Give it the dataset
            recommenderCoClustering.Ratings = mydata;
            //Train it
            recommenderCoClustering.Train();
            /////////////

            // Make the predictions
            var user_recs = recommenderCoClustering.Recommend(userid, recs);

            // get the recommendations
            foreach (var i in user_recs)
            {
                mylist.Add(i.Item1);
            }

            return mylist;
        }

        //*********** GlobalAverage Recommender
        public List<int> getBestItemsGlobalAverage(string dataset, int userid, int recs)
        {
            var mydata = RatingData.Read(dataset);
            //Create the recommender
            var recommenderGlobalAverage = new MyMediaLite.RatingPrediction.GlobalAverage();
            //Give it the dataset
            recommenderGlobalAverage.Ratings = mydata;
            //Train it
            recommenderGlobalAverage.Train();
            /////////////

            // Make the predictions
            var user_recs = recommenderGlobalAverage.Recommend(userid, recs);

            // get the recommendations
            foreach (var i in user_recs)
            {
                mylist.Add(i.Item1);
            }

            return mylist;
        }

        //*********** Random Recommender
        public List<int> getBestItemsRandom(string dataset, int userid, int recs)
        {
            var mydata = RatingData.Read(dataset);
            //Create the recommender
            var recommenderRandom = new MyMediaLite.RatingPrediction.Random();
            //Give it the dataset
            recommenderRandom.Ratings = mydata;
            //Train it
            recommenderRandom.Train();
            /////////////

            // Make the predictions
            var user_recs = recommenderRandom.Recommend(userid, recs);

            // get the recommendations
            foreach (var i in user_recs)
            {
                mylist.Add(i.Item1);
            }

            return mylist;
        }

        //*********** ItemAverage Recommender
        public List<int> getBestItemsItemAverage(string dataset, int userid, int recs)
        {
            var mydata = RatingData.Read(dataset);
            //Create the recommender
            var recommenderItemAverage = new MyMediaLite.RatingPrediction.ItemAverage();
            //Give it the dataset
            recommenderItemAverage.Ratings = mydata;
            //Train it
            recommenderItemAverage.Train();
            /////////////

            // Make the predictions
            var user_recs = recommenderItemAverage.Recommend(userid, recs);

            // get the recommendations
            foreach (var i in user_recs)
            {
                mylist.Add(i.Item1);
            }

            return mylist;
        }




        //*********** My OWN getTopRatedItems
        public List<int> getTopRatedItems(string dataset, int recs)
        {

            ArrayList uniqueItemHolder = new ArrayList();
            List<ItemRating> myItemList = new List<ItemRating>();

            double rating = 0.0;
            double occurencies = 0;
            string[] helper;

            //put the dataset in an array of lines
            string[] mytext = File.ReadAllLines(dataset);

            if (File.Exists(dataset))
            {
                for (int i = 0; i < mytext.Length; i++)
                {
                    helper = mytext[i].Split('\t');
                    if (!uniqueItemHolder.Contains(Convert.ToInt32(helper[1])))
                    {
                        uniqueItemHolder.Add(Convert.ToInt32(helper[1]));

                    }//end if
                }//end for

                // Now for each unique item calculate the average rating
                foreach (var item in uniqueItemHolder)
                {
                    for (int i = 0; i < mytext.Length; i++)
                    {
                        helper = mytext[i].Split('\t');
                        if ((int)item == Convert.ToInt32(helper[1]))
                        {
                            rating += Convert.ToInt32(helper[2]);
                            occurencies += 1.0;
                        }
                    }//end for
                    //calculate the average rating and reset for the next item
                    /// ADD EVERYTHING TO THE FINAL LIST
                    myItemList.Add(new ItemRating((int)item, (rating / occurencies)));
                    //Reset
                    rating = 0.0;
                    occurencies = 0.0;
                }//end foreach

            }//end if exists

            //Sort the List in ascending order
            List<ItemRating> SortedList = myItemList.OrderBy(o => o.Item_rating).ToList();

            for (int j = SortedList.Count - 1; j >= SortedList.Count - (recs); j--){
                mylist.Add(SortedList[j].Item_id);
            }

            return mylist;
        }


        public void writeToFile(string dataset) {
            //50 items should be enough
            StreamWriter sw = File.CreateText(Path.GetDirectoryName(dataset)+"\\"+Path.GetFileNameWithoutExtension(dataset)+"TopRated.txt");
            
            foreach (var i in this.getTopRatedItems(dataset, 50)){
                sw.WriteLine(i.ToString());
            }
            sw.Close();
        }//endWriteToFile

        public List<int> readTopFromFile(string dataset, int recs){
            if (File.Exists(dataset)){
              String[] mytext = File.ReadAllLines(dataset);
              for (int i = 0; i < recs; i++)
                {
                    mylist.Add(Convert.ToInt16(mytext[i]));
                }
            }
            return mylist;
        }



        class ItemRating
        {
            private int item_id;
            private double item_rating;

            public ItemRating(int id, double rating)
            {
                this.item_id = id;
                this.item_rating = rating;
            }

            public int Item_id
            {
                get { return this.item_id; }
                set { this.item_id = value; }
            }

            public double Item_rating
            {
                get { return this.item_rating;}
                set { this.item_rating = value; }

            }

        }//end class

        //////////////
       


    }

    



}