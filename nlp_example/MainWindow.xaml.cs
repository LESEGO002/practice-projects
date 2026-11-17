using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
//using ml imports 
using Microsoft.ML;
using Microsoft.ML.Data;


namespace nlp_example
{//start of namespace
    

    
    public partial class MainWindow : Window
    {//start of class

        //ML.Net context to work with data it contains,an instance with an object name
        private readonly MLContext mLContext;

        //class to detect sentiment / class to get status of the answer if related to what the
        //user asked

        //the clas will be working with the list
        //create an instance for the list,to hold the traning data
        //with object nemae traningData
        private List<SentimentData> trainingData;

        //crating an instance for the class predictionEngine
        //with an object name PredEngine(predictionEngine)
        private PredictionEngine<SentimentData, SentimentPrediction> predEngine;



        public MainWindow()
        {//start of constructor

            InitializeComponent();

            //initializing all the ML componets
            mLContext = new MLContext();

            trainData();

            //call the trainmodel method
            TrainModel();



        }//end of constructor






        //method to do the pipelines and also insert data from trainData() method, and more
        //method wiill also re-train the ai
        private void TrainModel() 
        {//start of the method train model

            //using var,var stans for variable
            //anything you nassign to nthe var such as object also the varialbe var will be
            //an object

            //load the info of the data the LoadEnumarable function that is in the training data
            var trainDataView = mLContext.Data.LoadFromEnumerable(trainingData);

            //then add or transformit to pipe line
            var pipeline = mLContext.Transforms.Text.FeaturizeText("Features",nameof(SentimentData.Text)).
            Append(mLContext.BinaryClassification.Trainers.SdcaLogisticRegression(labelColumnName: "Label", featureColumnName: "Features"));


            //fill the pipelines with the data usin the Fit() fuction or method
            var model = pipeline.Fit(trainDataView);

            //add all the trainign data to the engine which is predictionEngine
            //from the pipeline
            predEngine = mLContext.Model.CreatePredictionEngine<SentimentData, SentimentPrediction>(model);

            
        
        
        }//end of train model





        //methid to auto train the ai model when the project runs
        private void trainData() 
        {
            //initialize with base training data
            trainingData = new List<SentimentData>
            {//start of adding training data

                new SentimentData{Text="I am happy" , Label=true },
                new SentimentData{Text="I am worried",Label=false },
                new SentimentData{Text="I am nervous",Label=false},
                new SentimentData{Text="I am shocked",Label=true },
                new SentimentData{Text="I am disappointed",Label=false },
                 new SentimentData{Text="I am stunned",Label=false },


            };//end of trainig data



        }//end of train data



        //event handelr to train the ai with emotions
        private void training_ai(object sender, RoutedEventArgs e ) 
        {//start of event handler

            //collect what the use inputse
            string input = emotions.Text.ToString();

            //check if the use enterd something
            if (string.IsNullOrEmpty(input)) 
            {//start of if

                MessageBox.Show("Please enter something");

                //stop the app
                return;
            
            }//end of if

            //get the prediction
            var prediction = predEngine.Predict(new SentimentData {Text=input });

            //get the confidence percentages, or related pre
            float positiveScore = prediction.Probability * 100;
            float negativeScore =  100 - positiveScore;
            string emotionType = prediction.Prediction.ToString();


            //collect or build messate stause
            string message_feedback = $"{emotions} emotion or answer related\n" 
                + $"positive {positiveScore}\n"
                + $"negative {negativeScore}";

            show_emotions_detected.Text = message_feedback;

            //retrain the ai
            trainingData.Add( new SentimentData { Text=input, Label=prediction.Prediction });
            TrainModel();


        }//end of event handler





    }//end of class
}//end of namespace
