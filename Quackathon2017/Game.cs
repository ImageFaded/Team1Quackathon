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
        Random randon = new Random();

        //int choice;
        Opponent foeHolder;
        Player playeer = new Player();
        SpeechRecognitionEngine recEngine = new SpeechRecognitionEngine();
        bool gameState = true;

        private void Game_Load(object sender, EventArgs e)
        {
            Choices cmds = new Choices();
            cmds.Add(new string[] { "say hello", "regret existing", "fire", "nuclear", "Lightning", "heal", "consider suicide"});
            GrammarBuilder gBuild = new GrammarBuilder();
            gBuild.Append(cmds);
            Grammar gram = new Grammar(gBuild);

            recEngine.LoadGrammarAsync(gram);
            recEngine.SetInputToDefaultAudioDevice();
            recEngine.SpeechRecognized += reco;
        }

        public Game()
        {
            MaximizeBox = false;
            PictureBox test = new PictureBox();
            Size = new Size(500, 500);
            test.Size = new Size(50, 50);
            InitializeComponent();
        }

        #region overworld

        public void setUpGrid()
        {

        }

#endregion





        private void button1_Click(object sender, EventArgs e)
        {

            //Instatiate player object
            //Instatiate people vulture
            Opponent nonagon = getRanFoe();
            nonagon.setUp();
            nonagon.displayStats();
            combat(nonagon, playeer, 0);

            //Show em both

        }

        public Opponent getRanFoe()
        {
            if (randon.Next(0, 3) == 0) {return new RoadTrain(); }
            else { return new PeopleVulture(); }
        }

        #region combatEngine

        public void combat(Opponent oposition, object player, int effects)
        {
            //SET UP SCREEN FOR WOMBAT
            gameState = false;
            pictureBox2.Visible = true;
            foeBox.Visible = true;
            foeHb.Visible = true;
            playHb.Visible = true;
            button1.Enabled = false;
            //Done hiding objects
            foeHolder = oposition;
            MessageBox.Show("Test1");
            updateHealth();
            try
            {
                recEngine.RecognizeAsync(RecognizeMode.Single);
            } catch (InvalidOperationException)
            {
                MessageBox.Show("Stop hitting the start button so much!");
            }
        }

        public void updateHealth()
        {
            //Set health bar to ten times the health of the player
            playHb.Width = 10 * playeer.getHp();
            foeHb.Width = 10 * foeHolder.getHp();
        }

        public void nextTurn()
        {
            updateHealth();
            recEngine.RecognizeAsync(RecognizeMode.Single);
        }

        private void reco(object sender, SpeechRecognizedEventArgs e)
        {
            MessageBox.Show("TEST 3");
            Spells newSpl;
            switch (e.Result.Text)
            {
                case "say hello":
                    MessageBox.Show("GENERAL KANOBI");
                    recEngine.RecognizeAsyncStop();
                    break;
                case "regret existing":
                    MessageBox.Show("KILL YR SELF");
                    recEngine.RecognizeAsyncStop();
                    foeTurn();
                    break;
                case "fire":
                    newSpl = new fire();
                    recEngine.RecognizeAsyncStop();
                    newSpl.setUp();
                    spellEffect(newSpl);
                    break;
                case "nuclear":
                    MessageBox.Show("Nuclear");
                    //Auto win round
                    recEngine.RecognizeAsyncStop();
                    break;
                case "Lightning":
                    newSpl = new lightningBolt();
                    recEngine.RecognizeAsyncStop();
                    newSpl.setUp();
                    spellEffect(newSpl);
                    break;
                case "heal":
                    newSpl = new Heal();
                    recEngine.RecognizeAsyncStop();
                    newSpl.setUp();
                    spellEffect(newSpl);
                    break;
                case "consider suicide":
                    newSpl = new MindCtrl();
                    recEngine.RecognizeAsyncStop();
                    newSpl.setUp();
                    spellEffect(newSpl);
                    break;

            }
        }

        public void spellEffect(Spells cast)
        {
            MessageBox.Show("CAST :"+cast.getName());
            bool kill = false;
            if (cast.getEffect() == 0)
            {
                //Damaging attac
                kill = foeHolder.HealthVary(cast.getValue());
            }
            if (cast.getEffect() == 1)
            {
                //Boost Atac
               //Does nothig
            }
            if (cast.getEffect() == 2)
            {
                //Bost defence
                //does nothing
            }
            if (cast.getEffect() == 3)
            {
                //Heal
                playeer.damage(cast.getValue() * -1);
            }

            if (kill == true)
            {
                endCombat();
            }
            else
            {
                foeTurn();
            }
        }

        public void foeTurn()
        {
            updateHealth();
            //D
            bool deed = foeAttack();
            for (int i = 0; i < 4; i++)
            { foeHolder.moves[i].cool(); }
            if (deed == true) { MessageBox.Show("YOU ARE DEED"); endCombat(); }
            else {MessageBox.Show("CASE YOUR SP'EL"); nextTurn();  }

        }

        public bool foeAttack()
        {
            bool deed = false;
            //Select attack
            //If random attack is usable, then perfomr it, else call foeAttack Again
            int moveChoice = randon.Next(0, 4);
            if ((foeHolder.moves[0].checkUse() == false) && (foeHolder.moves[1].checkUse() == false) && (foeHolder.moves[2].checkUse() == false) && (foeHolder.moves[3].checkUse() == false)) { MessageBox.Show("THE PEOPLE VULTURE USED STRUGGLE"); deed = playeer.damage(1); }
                if (foeHolder.moves[moveChoice].checkUse() == true  )
            {
                //WE DO THE MOOOOVE
                //DECLARE THE MOOOOVE
                foeHolder.moves[moveChoice].timer = foeHolder.moves[moveChoice].cooldown;
                MessageBox.Show("THE "+ foeHolder.getName() + " USED " + foeHolder.moves[moveChoice].name);
                //WE WORK OUT IT's EFFECT
                if (foeHolder.moves[moveChoice].effect == 0)
                {
                    //GENERIC ATTACK
                    deed = playeer.damage(foeHolder.moves[moveChoice].value);
                }
                if (foeHolder.moves[moveChoice].effect == 1)
                {
                    //INCREASE ATTACK
                    foeHolder.attackC(foeHolder.moves[moveChoice].value);
                }
                if (foeHolder.moves[moveChoice].effect == 2)
                {
                    //INCREASE DEFENCE
                    foeHolder.defenceC(foeHolder.moves[moveChoice].value);
                }
                if (foeHolder.moves[moveChoice].effect == 3)
                {
                    //HEAL SATAN
                    foeHolder.HealthVary(-1*(foeHolder.moves[moveChoice].value));
                }
            }
            else { MessageBox.Show("ARE WE STUCK FOREVER?"); deed = foeAttack(); }
            return deed;
        }



        public void endCombat()
        {
            MessageBox.Show("COMBAT OVER");
            pictureBox2.Visible = false;
            foeBox.Visible = false;
            foeHb.Visible = false;
            playHb.Visible = false;
            button1.Enabled = true;
            gameState = true;
        }

        #endregion


    }

    #region objects

    //Objects

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

    #region enemy Classes

    public abstract class Opponent
    {
        public Moves[] moves = new Moves[4];
        int healthPoints;
        string name;
        int attack;
        int defense;
        int special;
        string element;
        int temp;

        public virtual void setUp()
        {

        }

        public void SetStats(int healthPoints, int attack, int defense, int special, string element, string name)
        {
            this.healthPoints = healthPoints;
            this.attack = attack;
            this.defense = defense;
            this.special = special;
            this.element = element;
            this.name = name;
        }

        public void displayStats()
        {
            MessageBox.Show("Health : " + healthPoints + " Attack  : " + attack + " Defense : " + defense + " Special : " + special + " Element : " + element + " Name : " + name);
        }
        public string getName()
        {
            return name;
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
        public void attackC(int change)
        {
            attack = attack + change;
        }
        public void defenceC(int change)
        {
            defense = defense + change;
        }
        public int getHp()
        {
            return healthPoints;
        }
    }



    public class PeopleVulture : Opponent
    {

        public override void setUp()
        {
            moves[0] = new cry(); moves[1] = new generic(); moves[2] = new generic(); moves[3] = new cry();
            //Set my stats

            SetStats(5, 1, 3, 0, "vulture", "People Vulture");
        }
    }
    public class RoadTrain : Opponent
    {
        public override void setUp()
        {
            moves[0] = new Plough(); moves[1] = new FullSteamAhead(); moves[2] = new Horn(); moves[3] = new generic();

            SetStats(8, 7, 2, 0, "robot", "ROAD TRAIN");
        }
    }

    #endregion

    #region move classes


    public abstract class Moves
    {
        public bool active; //If a move is an attack or a passive effect
        public string name; //The name of the move
        public int effect; //The effect of the move 0. Normal Attack, 1. Buff Attack, 2. Buff Defence, 3. Heal, 4. Attack (Ignores defence)
        public string element; //The element of the move Lightning, Fire, Robot, Vulture, Beast Etc Etc
        public int value; //The numerical value of the move
        public int cooldown; //The cooldown time of the move
        public int timer; //How long the move has left
        public void cool()
        {
            if (timer > 0)
            {
                timer--;
            }
        }
        public bool checkUse()
        {
            if (timer > 0) { return false; }
            else { return true; }
        }

    }

    public class cry : Moves
    {
        public cry()
        {
            active = true;
            name = "Cry";
            effect = 1;
            element = "Vulture";
            value = 2;
            cooldown = 2;
        }
    }
    public class generic : Moves
    {
        public generic()
        {
            active = true;
            name = "Generic";
            effect = 0;
            element = "Vulture";
            value = 4;
            cooldown = 1;
        }
    }

    public class Plough : Moves
    {
        public Plough()
        {
            active = true;
            name = "Plough";
            effect = 0;
            element = "Robot";
            value = 3;
            cooldown = 2;
        }
    }

    public class FullSteamAhead : Moves
    {
        public FullSteamAhead()
        {
            active = true;
            name = "Full Steam Ahead";
            effect = 0;
            element = "Robot";
            value = 7;
            cooldown = 5;
        }
    }

    public class Horn : Moves
    {
        public Horn()
        {
            active = true;
            name = "Horn";
            effect = 1;
            value = 2;
            cooldown = 2;

        }
    }

    #endregion

    #region spells


    public abstract class Spells
    {
        bool active; //If a move is an attack or a passive effect
        string name; //The name of the move
        int effect; //The effect of the move 0. Normal Attack, 1. Buff Attack, 2. Buff Defence, 3. Heal, 4. Attack (Ignores defence)
        string element; //The element of the move Lightning, Fire, Robot, Vulture, Beast Etc Etc
        int value; //The numerical value of the move
        public virtual void setUp()
        {

        }
        public string getName()
        {
            MessageBox.Show("AM I BEING CALLED?");

            return name;
        }
        public int getEffect()
        {
            return effect;
        }
        public string getElement()
        {
            return element;
        }
        public int getValue()
        {
            return value;
        }
        public void setVals(string name, int effect, string element, int value)
        {
            this.name = name; this.effect = effect; this.element = element; this.value = value;
        }

    }

    public class fire : Spells
    {
        public override void setUp()
        {
            setVals("FIREBALL", 0, "FIRE", 5);
        }
    }
    public class lightningBolt : Spells
    {
        public override void setUp()
        {
            setVals("Lightning Bolt", 0, "Lightning", 7);
        }
    }
    public class Heal : Spells
    {
        public override void setUp()
        {
            setVals("HEAL", 3, "N/A", 3);
        }
    }

    public class MindCtrl : Spells
    {
        public override void setUp()
        {
            setVals("Mind Cntrl", 0, "VULTURE", 20);
        }
    }

    #endregion

    #endregion









}
