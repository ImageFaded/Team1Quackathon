namespace Quackathon2017
{
    partial class Game
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Game));
            this.button1 = new System.Windows.Forms.Button();
            this.foeHb = new System.Windows.Forms.Button();
            this.playHb = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.foeBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.foeBox)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.Location = new System.Drawing.Point(386, 305);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(98, 168);
            this.button1.TabIndex = 2;
            this.button1.Text = "Test Combat";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // foeHb
            // 
            this.foeHb.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.foeHb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.foeHb.Enabled = false;
            this.foeHb.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.foeHb.Location = new System.Drawing.Point(13, 22);
            this.foeHb.Name = "foeHb";
            this.foeHb.Size = new System.Drawing.Size(160, 23);
            this.foeHb.TabIndex = 3;
            this.foeHb.UseVisualStyleBackColor = false;
            this.foeHb.Visible = false;
            // 
            // playHb
            // 
            this.playHb.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.playHb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.playHb.Enabled = false;
            this.playHb.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.playHb.Location = new System.Drawing.Point(13, 276);
            this.playHb.Name = "playHb";
            this.playHb.Size = new System.Drawing.Size(160, 23);
            this.playHb.TabIndex = 4;
            this.playHb.UseVisualStyleBackColor = false;
            this.playHb.Visible = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Quackathon2017.Properties.Resources.theGizardWizard;
            this.pictureBox2.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.InitialImage")));
            this.pictureBox2.Location = new System.Drawing.Point(0, 305);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(216, 168);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Visible = false;
            // 
            // foeBox
            // 
            this.foeBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.foeBox.Image = global::Quackathon2017.Properties.Resources.EVILman;
            this.foeBox.InitialImage = ((System.Drawing.Image)(resources.GetObject("foeBox.InitialImage")));
            this.foeBox.Location = new System.Drawing.Point(188, 2);
            this.foeBox.Name = "foeBox";
            this.foeBox.Size = new System.Drawing.Size(305, 234);
            this.foeBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.foeBox.TabIndex = 0;
            this.foeBox.TabStop = false;
            this.foeBox.Visible = false;
            // 
            // Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 473);
            this.Controls.Add(this.playHb);
            this.Controls.Add(this.foeHb);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.foeBox);
            this.Name = "Game";
            this.Text = "Game";
            this.Load += new System.EventHandler(this.Game_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.foeBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox foeBox;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button foeHb;
        private System.Windows.Forms.Button playHb;
    }
}

