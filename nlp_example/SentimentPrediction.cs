using Microsoft.ML.Data;

namespace nlp_example
{//start of namesapce
    public class SentimentPrediction
    {//start of class

        [ColumnName("PredictedLabel")]
        public bool Prediction { get; set; }
        public float Probability { get; set; }
        public float Score { get; set; }



    }//end of class


}//end of namespace