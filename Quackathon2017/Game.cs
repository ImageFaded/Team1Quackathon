using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Recognition;

namespace Quackathon2017
{

    public partial class Game : Form
    {
        SpeechRecognitionEngine recEngine = new SpeechRecognitionEngine();
        bool gameState = true;
        public Game()
        {
            MaximizeBox = false;
            PictureBox test = new PictureBox();
            Size = new Size(500, 500);
            test.Size = new Size(50, 50);
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {

            Choices commands = new Choices();                                           //the choices object contains the words that the Speech recognistion software will be interpreting
            commands.Add(new String[] { "forward", "backward", "left", "right" });      //adds "say hello" and "print my name" as commands the computer will understand
            GrammarBuilder gb = new GrammarBuilder();                                   //Grammerbuilder object is used to pass the commands to the Grammer object
            gb.Append(commands);                                                        //add commands to gb object
            Grammar grammer = new Grammar(gb);                                          // Grammer object is passed to the speech recognition engine 


            recEngine.LoadGrammarAsync(grammer);                            //tells the speech recognition engine what to scan for while the engine is running
            try                                                             //stops an error ocurring if no microphone is connected
            {
                recEngine.SetInputToDefaultAudioDevice();                   //use the current default audio input device
                recEngine.SpeechRecognized += controlCharacter;   //attempt to recognise user speech

            }
            catch (Exception)
            {
                MessageBox.Show("No Microphone Detected!");                 //tell the user if no microphone is connected
            }


        }

        public void controlCharacter(object sender, SpeechRecognizedEventArgs e)
        {
            ///Danial black lee les
            ///Block lesss
            //Maybe more
            while (true)
            {
                switch (e.Result.Text)
                {
                    case "forward":
                        break;
                    case "backwards":
                        break;
                    case "left":
                        break;
                    case "right":
                        break;
                }
            }
        }
        public int combat(object foe, object player, int conditions)
        {
            //Clear screen, show combat icons
            //Show starting effects from conditions
            //Do player action
            
            //Do enemy action
            //Enemy action


            //Others?


        }

        public int playerAction()
        {


            return 0;
        }
    }
}
