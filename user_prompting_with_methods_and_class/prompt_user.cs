using System;
using System.Diagnostics.Eventing.Reader;

namespace user_prompting_with_methods_and_class
{//start of namespace
    public class prompt_user
    {//start of class

        //global variable devlaration, string datatype
        //and variable name called name
        //must be private
        private string name = string.Empty;

        //void method to prompt the use for name
        //start with accsess modifier public
        //then type of method void,then name of the
        //method called asking_name(
        public void asking_name() { //start of method

            //asking for name

            //AI
            Console.WriteLine(" chatbot: Hi Please enter you name. ");

            //do while loop to check and re-propmt the user
            do
            {//start of do while loop

                //user entering name
                Console.Write("user: ");
                name = Console.ReadLine();

            } while (!check_name() ); 

            }//end of method

        //boolean method to chech if the user has entered a name
        //start by the access modifier must be private
        //then the type of method boolean then name of it
        //check name()
        private Boolean check_name()
        {//start of boolean method

            //check if the name is entered using if statement
            if ( name == "" && name!=string.Empty){

                //show error message 
                Console.WriteLine("chatbot: can you please enter your name.");

                //return
                return false;

            }
            else
            {//start of else statement

                //show success message
                Console.WriteLine("chatbot: Hi " + name + " nice to meet you");
                //return
                return true;
            }

        }

    }//end of class
}//end of namespace