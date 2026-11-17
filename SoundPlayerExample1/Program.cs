using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;//must be imported to use soundplayer class

namespace SoundPlayerExample1
{//start of namespace

    //how to use soundplayer class to play sounds in c#
    //sound player class only supports .wav file
    public class Program
    {//start of class
        static void Main(string[] args)
        {//start of main method

            //instance for soundplayer class
            SoundPlayer playsound = new SoundPlayer(@"Greeting.wav");
            playsound.Load();//load the wave file
            playsound.Play();//will play sound


            Console.WriteLine("Playing sound...");
            Console.ReadLine();



        }//end of method
    }//end of class
}//end of namespace
