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
       /*  Button[,] tiles = new Button[15, 15]; //Create buttons for the target
        int[,] solid = new int[15, 15]; //Array to store locations of overworld objects */

        Random randon = new Random();

        //int choice;
        Opponent foeHolder;
        Player playeer = new Player();
        SpeechRecognitionEngine recEngine = new SpeechRecognitionEngine();
        bool gameState = true;

        private void Game_Load(object sender, EventArgs e)
        {
            Choices cmds = new Choices();
            cmds.Add(new string[] { "say hello", "do nothing", "fire", "nuclear", "Lightning", "heal", "im in your mind", "yeast brew"});
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
            Random ramdomEnemy = new Random();
            /*if (randon.Next(0, 3) == 0) {return new lol(); }
            else { if (randon.Next(0, 2) == 0) { return new lol(); } else {return new lol(); } }*/
            switch (randon.Next(0,9))
            {
                case 0:
                    return new BigFIgWasp();
                    break;
                case 1:
                    return new PeopleVulture();
                    break;
                case 2:
                    return new FlyingMicrotonalBananah();
                    break;
                case 3:
                    return new Rattlesnake();
                    break;
                case 4:
                    return new EvilMan();
                    break;
                case 5:
                    return new RoadTrain();
                    break;
                case 6:
                    return new InvisibleFace();
                    break;
                case 7:
                    return new Vegemite();
                    break;
                case 8:
                    return new lol();
                    break;
                case 9:
                    return new MysteryJack();
                    break;
                default:
                    return null;
                    break;
            }
            
        }

        #region combatEngine

        public void combat(Opponent oposition, object player, int effects)
        {
            //SET UP SCREEN FOR WOMBAT
            gameState = false;
            foeBox.Image = oposition.icon;
            pictureBox2.Visible = true;
            foeBox.Visible = true;
            foeHb.Visible = true;
            playHb.Visible = true;
            button1.Enabled = false;
            //Done hiding objects
            foeHolder = oposition;
            updateHealth();
            MessageBox.Show("Test 3");
            try
            {
                recEngine.RecognizeAsync(RecognizeMode.Single);
            } catch (InvalidOperationException)
            {
                MessageBox.Show("Excessively pressing the start button does nothing");
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
            MessageBox.Show("You have done nothing");
            Spells newSpl;
            switch (e.Result.Text)
            {

                case "do nothing":
                    MessageBox.Show("You have done nothing");
                    recEngine.RecognizeAsyncStop();
                    foeTurn();
                    break;
                case "fire":
                    newSpl = new fire();
                    recEngine.RecognizeAsyncStop();
                    newSpl.setUp();
                    spellEffect(newSpl);
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
                case "im in your mind":
                    newSpl = new MindCtrl();
                    recEngine.RecognizeAsyncStop();
                    newSpl.setUp();
                    spellEffect(newSpl);
                    break;
                case "yeast brew":
                    newSpl = new yeast();
                    recEngine.RecognizeAsyncStop();
                    newSpl.setUp();
                    spellEffect(newSpl);
                    break;

            }
        }

        public void spellEffect(Spells cast)
        {
            MessageBox.Show("Cast :"+cast.getName());
            bool kill = false;
            if (cast.getEffect() == 0)
            {
                //Damaging attac
                int dBonus = 0;
                if (cast.getElement().Equals("Lightning") && foeHolder.getElement().Equals("Robot")) { dBonus = 4; }
                if (cast.getElement().Equals("Fire") && foeHolder.getElement().Equals("Lightning")) { dBonus = 16; }
                kill = foeHolder.HealthVary(cast.getValue()+dBonus);
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
                playeer.heal(cast.getValue() * -1);
            }

            if (kill == true)
            {
                playeer.levelUp(foeHolder.exp);
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
            if (deed == true) { MessageBox.Show("You are dead"); Application.Exit();  }
            else {MessageBox.Show("Cast your spell"); nextTurn();  }

        }

        public bool foeAttack()
        {
            bool deed = false;
            //Select attack
            //If random attack is usable, then perfomr it, else call foeAttack Again
            int moveChoice = randon.Next(0, 4);
            if ((foeHolder.moves[0].checkUse() == false) && (foeHolder.moves[1].checkUse() == false) && (foeHolder.moves[2].checkUse() == false) && (foeHolder.moves[3].checkUse() == false)) { MessageBox.Show("THE "+foeHolder.getName()+" USED STRUGGLE"); deed = playeer.damage(1); }
                if (foeHolder.moves[moveChoice].checkUse() == true  )
            {
                //WE DO THE MOOOOVE
                //DECLARE THE MOOOOVE
                foeHolder.moves[moveChoice].timer = foeHolder.moves[moveChoice].cooldown;
                MessageBox.Show("The "+ foeHolder.getName() + " used " + foeHolder.moves[moveChoice].name);
                //WE WORK OUT IT's EFFECT
                if (foeHolder.moves[moveChoice].effect == 0)
                {
                    //GENERIC ATTACK
                    deed = playeer.damage(foeHolder.moves[moveChoice].value+foeHolder.Attack());
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
                if (foeHolder.moves[moveChoice].effect == 4)
                {
                    //HEAL SATAN
                    foeHolder.attackC(foeHolder.moves[moveChoice].value);
                    deed = playeer.damage(foeHolder.moves[moveChoice].value + foeHolder.Attack());
                }
            }
            else { deed = foeAttack(); }
            return deed;
        }



        public void endCombat()
        {
            MessageBox.Show("Combat over");
            pictureBox2.Visible = false;
            foeBox.Visible = false;
            foeHb.Visible = false;
            playHb.Visible = false;
            button1.Enabled = true;
            gameState = true;
        }


        #endregion

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }

    #region objects

    //Objects

    //Defines player object
    public class Player
    {

        //Set health value
        int health = 20;
        int mxHp = 20;
        //Returns health value
        int x; int y;
        int level = 1;
        int xp = 0;
        int defence = 6;
        public int getHp()
        {
            return health;
        }
        //Calculates damange

        public bool damage(int damage)
        {
            if ((health - damage + defence) < health)
            {
                health = (health - damage + defence);
            }
            else { health--; }
            if (health > mxHp) { health = mxHp; }
            if (health < 1) { return true; }
            else
            { return false; }
        }

        public void heal(int damage)
        {

            health= health-damage;
            if (health > mxHp) { health = mxHp; }
        }
        public void levelUp(int exp)
        {
            xp = xp+exp;
            if (xp > ((6/5)^level))
            MessageBox.Show("" + xp);

            {
                //Level Up
                MessageBox.Show("Level Up!");
                level++;
                mxHp = mxHp + 4;
                defence = defence + 2;
                //Increase attack and defence and health
            }
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
        public int exp;
        public Image icon;

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
            int newDam = damage - defense;
            if (newDam < 1) newDam = 1;
            healthPoints = healthPoints - newDam;
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
        public string getElement()
        {
            return element;
        }
    }



    public class PeopleVulture : Opponent
    {
        int exp = 3;
        public override void setUp()
        {
            moves[0] = new cry(); moves[1] = new generic(); moves[2] = new generic(); moves[3] = new laser();
            //Set my stats
            icon = Quackathon2017.Properties.Resources.peopleVulture1;
            SetStats(5, 1, 3, 0, "vulture", "People Vulture");
        }
    }
    public class BigFIgWasp : Opponent
    {
        int exp = 4;
        public override void setUp()
        {
            moves[0] = new cry(); moves[1] = new generic(); moves[2] = new generic(); moves[3] = new cry();
            //Set my stats
            icon = Quackathon2017.Properties.Resources.BigFigWasp;
            SetStats(3, 2, 3, 0, "Robot", "Big FIg Wasp");
        }
    }
    public class FlyingMicrotonalBananah : Opponent
    {
        int exp = 2;
        public override void setUp()
        {
            moves[0] = new cry(); moves[1] = new Microtone(); moves[2] = new Microtone(); moves[3] = new cry();
            //Set my stats
            icon = Quackathon2017.Properties.Resources.Flying;
            SetStats(2, 4, 1, 0, "Robot", "Flying Microtonal Bananah");
        }
    }
    public class Rattlesnake : Opponent
    {
        int exp = 4;
        public override void setUp()
        {
            moves[0] = new Bite(); moves[1] = new rattle(); moves[2] = new Microtone(); moves[3] = new rattle();
            //Set my stats
            icon = Quackathon2017.Properties.Resources.Rattlesnake;
            SetStats(7, 2, 5, 0, "Beast", "Rattlesnake");
        }
    }
    public class EvilMan : Opponent
    {
        int exp = 5;
        public override void setUp()
        {
            moves[0] = new generic(); moves[1] = new EvilFace(); moves[2] = new FireEn(); moves[3] = new FireEn();
            //Set my stats
            icon = Quackathon2017.Properties.Resources.EVILman;
            SetStats(3, 6, 2, 0, "Fire", "Evil Man");
        }
    }
    public class RoadTrain : Opponent
    {
        int exp = 3;
        public override void setUp()
        {
            moves[0] = new Plough(); moves[1] = new FullSteamAhead(); moves[2] = new Horn(); moves[3] = new generic();
            icon = Quackathon2017.Properties.Resources.Road_Train2;
            SetStats(8, 7, 2, 0, "robot", "ROAD TRAIN");
        }
    }
    public class InvisibleFace : Opponent
    {
        int exp = 6;
        public override void setUp()
        {
            moves[0] = new EvilFace(); moves[1] = new Bite(); moves[2] = new EvilFace(); moves[3] = new YeastBrew();
            icon = Quackathon2017.Properties.Resources.InvisbleFace;
            SetStats(10, 5, 12, 0, "Vulture", "InvisibleFace");
        }
    }

    public class MysteryJack : Opponent
    {
        int exp = 8;
        public override void setUp()
        {
            moves[0] = new EvilFace(); moves[1] = new Bite(); moves[2] = new EvilFace(); moves[3] = new laser();
            icon = Quackathon2017.Properties.Resources.MysteryJack;
            SetStats(10, 7, 10, 0, "Robot", "Mystery Jack");
        }
    }

    public class Vegemite : Opponent
    {
        int exp = 5;
        public override void setUp()
        {
            //Change moves
            moves[0] = new LidSpin(); moves[1] = new VegemiteToss(); moves[2] = new YeastBrew(); moves[3] = new generic();
            icon = Quackathon2017.Properties.Resources.vegemite;
            SetStats(9, 6, 3, 0, "Beast", "Vegemite");
        }
    }


    public class lol : Opponent
    {
        int exp = 10;
        public override void setUp()
        {
            moves[0] = new Lightning(); moves[1] = new ThunderClouds(); moves[2] = new Catotails(); moves[3] = new Lightning();
            icon = Quackathon2017.Properties.Resources.lordOfLightning;
            SetStats(20, 15, 7, 0, "Lightning", "Lord of lightning");
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

    public class ThunderClouds : Moves
    {
        public ThunderClouds()
        {
            active = true;
            name = "ThunderClouds";
            effect = 1;
            element = "Lightning";
            value = 4;
            cooldown = 3;
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
    public class Lightning : Moves
    {
        public Lightning()
        {
            active = true;
            name = "Lightning";
            effect = 0;
            element = "Lightning";
            value = 4;
            cooldown = 1;
        }
    }
    public class Catotails : Moves
    {
        public Catotails()
        {
            active = true;
            name = "Cat o tails";
            effect = 0;
            element = "Lightning";
            value = 3;
            cooldown = 4;
        }
    }

    public class EvilFace : Moves
    {
        public EvilFace()
        {
            active = true;
            name = "Evil Face";
            effect = 1;
            element = "Robot";
            value = 2;
            cooldown = 1;
        }
    }
    public class rattle : Moves
    {
        public rattle()
        {
            active = true;
            name = "Rattle";
            effect = 2;
            element = "Beast";
            value = 1;
            cooldown = 1;
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

    public class FireEn : Moves
    {
        public FireEn()
        {
            active = true;
            name = "Fire";
            effect = 0;
            element = "Fire";
            value = 4;
            cooldown = 1;
        }
    }

    public class Bite : Moves
    {
        public Bite()
        {
            active = true;
            name = "Bite";
            effect = 4;
            element = "Beast";
            value = 4;
            cooldown = 1;
        }
    }
    public class laser : Moves
    {
        public laser()
        {
            active = true;
            name = "Laser";
            effect = 0;
            element = "Vulture";
            value = 9;
            cooldown = 5;
        }
    }
    public class Microtone : Moves
    {
        public Microtone()
        {
            active = true;
            name = "Microtone";
            effect = 0;
            element = "Robot";
            value = 4;
            cooldown = 5;
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
            element = "Robot";

        }
    }

    public class LidSpin : Moves
    {
        public LidSpin()
        {
            active = true;
            name = "Lid Spin";
            effect = 1;
            value = 2;
            cooldown = 2;
        }
    }

    public class VegemiteToss : Moves
    {
        public VegemiteToss()
        {
            active = true;
            name = "Vegemite Toss";
            effect = 0;
            value = 5;
            cooldown = 3;
        }
    }


    public class YeastBrew : Moves
    {
        public YeastBrew()
        {
            //Heals vegemite an amount of health
            active = true;
            name = "Yeast Brew";
            effect = 3;
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
            setVals("Fireball", 0, "Fire", 6);
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
            setVals("Heak", 3, "N/A", 3);
        }
    }

    public class yeast : Spells
    {
        public override void setUp()
        {
            setVals("yeast brew", 3, "N/A", 10);
        }
    }

    public class MindCtrl : Spells
    {
        public override void setUp()
        {
            setVals("Mind Cntrl", 0, "Vulture", 20);
        }
    }



    #endregion



    #endregion









}
