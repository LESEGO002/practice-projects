namespace string_manipulation_and_encapsulation_practice
{//start of namespace
    public class call
    {//start of class

        //varibles
        private int calls = 12;
        private string caller = "Steven segall";
        private int cellno = 0782570715;
        private double mintues = 200.5;

        //method
        public string return_caller_info() 
        {//start of method

            //returning caller information
            string info = "caller: " + caller + "\n" + "cell no: " + cellno + "\n" +
                    "number of calls: " + calls + "\n" + "minutes: " + mintues;

           return info;

        }//end of method

    }//end of class
}//end of namespace