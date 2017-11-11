using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quackathon2017
{
    public partial class Game : Form
    {
        //Initalises game
        public Game()
        {
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            PictureBox test = new PictureBox();
            Size = new Size(500, 500);
            test.Size = new Size(50, 50);
            InitializeComponent();
        }
        //Controls Character
        public void controlCharacter(object sender, KeyEventArgs e)
        {
            while (true)
            {
                switch (e.KeyCode)
                {
                    case (Keys.Up):
                        break;
                    case (Keys.Down):
                        break;
                    case (Keys.Left):
                        break;
                    case (Keys.Right):
                        break;
                }
            }
        }
    }
}
