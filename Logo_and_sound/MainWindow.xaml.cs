using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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

namespace Logo_and_sound
{//satrt of namespace 
   
    public partial class MainWindow : Window
    {//start of class
        public MainWindow()
        {//start of constructor


            InitializeComponent();


            //Crating instance for class Voice greeting
            Voice_Greeting greeter = new Voice_Greeting();
            //Calling method to play sound

            greeter.Greet();

        }//end of constructor


    }//ensd of class
}//end of namespace
