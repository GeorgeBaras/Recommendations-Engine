using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyMediaLite.IO;
using System.IO;
using MyMediaLite.Eval;

namespace finalTest.Models
{
    public class EvaluationModels
    {
        private MyMediaLite.Data.IRatings mydata;

        // default constructor
        public EvaluationModels(string dataset) {
           mydata = RatingData.Read(dataset);
        }


        public string EvaluateKNN() { 
        var recommenderKNN = new MyMediaLite.RatingPrediction.ItemKNN();
            recommenderKNN.Ratings = mydata;
            recommenderKNN.Train();

            return recommenderKNN.DoCrossValidation().ToString();
        }

        public string EvaluateCoClustering()
        {
            var recommenderCo = new MyMediaLite.RatingPrediction.CoClustering();
            recommenderCo.Ratings = mydata;
            recommenderCo.Train();

            return recommenderCo.DoCrossValidation().ToString();
        }

        public string EvaluateGlobalAverage()
        {
            var recommenderGA = new MyMediaLite.RatingPrediction.GlobalAverage();
            recommenderGA.Ratings = mydata;
            recommenderGA.Train();

            return recommenderGA.DoCrossValidation().ToString();
        }

        public string EvaluateRandom()
        {
            var recommenderR = new MyMediaLite.RatingPrediction.Random();
            recommenderR.Ratings = mydata;
            recommenderR.Train();

            return recommenderR.DoCrossValidation().ToString();
        }

        public string EvaluateItemAverage()
        {
            var recommenderIA = new MyMediaLite.RatingPrediction.ItemAverage();
            recommenderIA.Ratings = mydata;
            recommenderIA.Train();

            return recommenderIA.DoCrossValidation().ToString();
        }

        // RMSE xxxxx MAE xxxxxx CBD xxxxx

        // Methods to extract the numbers from the full error string
        public string getRMSE(string input) {

            string[] helper = input.Split(' ');
            return helper[1];
        }

        public string getMAE(string input)
        {

            string[] helper = input.Split(' ');
            return helper[3];
        }

        public string getCBD(string input)
        {

            string[] helper = input.Split(' ');
            return helper[5];
        }



    }
}