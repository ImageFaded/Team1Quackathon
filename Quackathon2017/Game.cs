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
    //Set up classes
    public partial class Game : Form
    {
        int choice;

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
            commands.Add(new String[] { "fire" });      //adds "say hello" and "print my name" as commands the computer will understand
            GrammarBuilder gb = new GrammarBuilder();                                   //Grammerbuilder object is used to pass the commands to the Grammer object
            gb.Append(commands);                                                        //add commands to gb object
            Grammar grammer = new Grammar(gb);                                          // Grammer object is passed to the speech recognition engine 


            recEngine.LoadGrammarAsync(grammer);                            //tells the speech recognition engine what to scan for while the engine is running
            try                                                             //stops an error ocurring if no microphone is connected
            {
                recEngine.SetInputToDefaultAudioDevice();                   //use the current default audio input device
                                                                            //attempt to recognise user speech

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
                    case "Fire":
                        //I am wizard, i am going to set this on fire
                        //Damage enemy
                        choice = 1;
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
        public int Combat(PeopleVulture foe, Player player, int conditions)
        {
            //Clear screen, show combat icons
            //Show starting effects from conditions
            //Do player action
            playerAction(foe);
            //Do enemy action
            //Enemy action


            //Others?

            return 0;
        }

        public void playerAction(PeopleVulture foe)
        {
            recEngine.SpeechRecognized += controlCharacter;
            switch (choice)
            {
                case 1:
                    bool vic = foe.HealthVary(4);
                    MessageBox.Show("DAMAGE DONE");
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
            }
            if (choice == 1)
            {
                //Do damage


            }
        }
        public void foeAction()
        {
            //Do option
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Instatiate player object
            //Instatiate people vulture
            PeopleVulture nonagon = new PeopleVulture();
            nonagon.SetStats(5, 1, 3, 0, 2);
            nonagon.displayStats();
            Player playeer = new Player();
            Combat(nonagon, playeer, 0);

            //Show em both

        }
    }

    //Defines player object
    public class Player
    {
        //Set health value
        int health = 10;
        //Returns health value
        public int getHp()
        {
            return health;
        }
        //Calculates damange
   
        public bool damage(int damage)
        {
            health = health - damage;
            if (health < 1) { return true; }
            else
            { return false; }
        }
    }

    public abstract class Opponent
    {
        int healthPoints;
        int attack;
        int defense;
        int special;
        int element;
        int temp;

        public void SetStats(int healthPoints, int attack, int defense, int special, int element)
        {
            this.healthPoints = healthPoints;
            this.attack = attack;
            this.defense = defense;
            this.special = special;
            this.element = element;
        }

        public void displayStats()
        {
            MessageBox.Show("Health : " + healthPoints + " Attack  : " + attack + " Defense : " + defense + " Special : " + special + " Element : " + element);
        }

        public int Attack()
        {
            return attack;
        }
        public bool HealthVary(int damage)
        {
            if (defense > damage)
            {
                temp = defense;
                defense = defense - 1;
            }
            healthPoints = healthPoints - (damage - defense);
            defense = temp;
            if (healthPoints < 1)
            {
                return true;
            }
            else
            {
                return false;

            }
        }

      
    }

    public class PeopleVulture : Opponent
    {

    }


    
}
