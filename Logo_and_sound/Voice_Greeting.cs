using System;
using System.Media;

namespace Logo_and_sound
{//start of namespace
    public class Voice_Greeting
    {//start of class

        //void method to play sound
        public void Greet() 
        {//start of method

            //replace the \bin\Debug\net10.0-windows\ from the path with Greet.wave
            string full_path =AppDomain.CurrentDomain.BaseDirectory.Replace(@"\bin\Debug\net10.0-windows\", @"\Greet.wav");

            SoundPlayer player = new SoundPlayer(full_path);

            player.Load();
            player.Play();


        }//end of method

    }//end of class
}//end of namespace