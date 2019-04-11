﻿namespace Level_Editor
{
    partial class LevelEditor
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
            this.colorBox = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.bossDoorButton = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.flowerButton = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.bridgeButton = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.snowButton = new System.Windows.Forms.Button();
            this.foliageButton = new System.Windows.Forms.Button();
            this.grayButton = new System.Windows.Forms.Button();
            this.blueButton = new System.Windows.Forms.Button();
            this.brownButton = new System.Windows.Forms.Button();
            this.grassButton = new System.Windows.Forms.Button();
            this.currentBox = new System.Windows.Forms.GroupBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.loadButton = new System.Windows.Forms.Button();
            this.mapBox = new System.Windows.Forms.GroupBox();
            this.enemyBox = new System.Windows.Forms.GroupBox();
            this.palletteButton = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();
            this.enemy1 = new System.Windows.Forms.PictureBox();
            this.eraseEnemy = new System.Windows.Forms.PictureBox();
            this.enemy0 = new System.Windows.Forms.PictureBox();
            this.currentTilePicture = new System.Windows.Forms.PictureBox();
            this.colorBox.SuspendLayout();
            this.currentBox.SuspendLayout();
            this.enemyBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.enemy1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eraseEnemy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.enemy0)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.currentTilePicture)).BeginInit();
            this.SuspendLayout();
            // 
            // colorBox
            // 
            this.colorBox.Controls.Add(this.label9);
            this.colorBox.Controls.Add(this.bossDoorButton);
            this.colorBox.Controls.Add(this.label8);
            this.colorBox.Controls.Add(this.flowerButton);
            this.colorBox.Controls.Add(this.label7);
            this.colorBox.Controls.Add(this.bridgeButton);
            this.colorBox.Controls.Add(this.label6);
            this.colorBox.Controls.Add(this.label5);
            this.colorBox.Controls.Add(this.label4);
            this.colorBox.Controls.Add(this.label3);
            this.colorBox.Controls.Add(this.label2);
            this.colorBox.Controls.Add(this.label1);
            this.colorBox.Controls.Add(this.snowButton);
            this.colorBox.Controls.Add(this.foliageButton);
            this.colorBox.Controls.Add(this.grayButton);
            this.colorBox.Controls.Add(this.blueButton);
            this.colorBox.Controls.Add(this.brownButton);
            this.colorBox.Controls.Add(this.grassButton);
            this.colorBox.Location = new System.Drawing.Point(15, 29);
            this.colorBox.Name = "colorBox";
            this.colorBox.Size = new System.Drawing.Size(230, 689);
            this.colorBox.TabIndex = 0;
            this.colorBox.TabStop = false;
            this.colorBox.Text = "Tile Selector";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(22, 658);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(84, 20);
            this.label9.TabIndex = 17;
            this.label9.Text = "Boss Door";
            // 
            // bossDoorButton
            // 
            this.bossDoorButton.BackColor = System.Drawing.Color.Red;
            this.bossDoorButton.Location = new System.Drawing.Point(16, 563);
            this.bossDoorButton.Name = "bossDoorButton";
            this.bossDoorButton.Size = new System.Drawing.Size(94, 92);
            this.bossDoorButton.TabIndex = 16;
            this.bossDoorButton.UseVisualStyleBackColor = false;
            this.bossDoorButton.Click += new System.EventHandler(this.colorButton_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(123, 540);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 20);
            this.label8.TabIndex = 15;
            this.label8.Text = "Flower";
            // 
            // flowerButton
            // 
            this.flowerButton.BackColor = System.Drawing.Color.Fuchsia;
            this.flowerButton.Location = new System.Drawing.Point(122, 438);
            this.flowerButton.Name = "flowerButton";
            this.flowerButton.Size = new System.Drawing.Size(94, 92);
            this.flowerButton.TabIndex = 14;
            this.flowerButton.UseVisualStyleBackColor = false;
            this.flowerButton.Click += new System.EventHandler(this.colorButton_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(18, 540);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 20);
            this.label7.TabIndex = 13;
            this.label7.Text = "Bridge";
            // 
            // bridgeButton
            // 
            this.bridgeButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.bridgeButton.Location = new System.Drawing.Point(16, 438);
            this.bridgeButton.Name = "bridgeButton";
            this.bridgeButton.Size = new System.Drawing.Size(94, 92);
            this.bridgeButton.TabIndex = 12;
            this.bridgeButton.UseVisualStyleBackColor = false;
            this.bridgeButton.Click += new System.EventHandler(this.colorButton_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(117, 400);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 20);
            this.label6.TabIndex = 11;
            this.label6.Text = "Log/Stump";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 402);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 20);
            this.label5.TabIndex = 10;
            this.label5.Text = "Door";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(123, 263);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 20);
            this.label4.TabIndex = 9;
            this.label4.Text = "Rocks";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 265);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "Water";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(118, 125);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "Border Wall";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 126);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Floor";
            // 
            // snowButton
            // 
            this.snowButton.BackColor = System.Drawing.Color.Purple;
            this.snowButton.Location = new System.Drawing.Point(18, 300);
            this.snowButton.Name = "snowButton";
            this.snowButton.Size = new System.Drawing.Size(94, 92);
            this.snowButton.TabIndex = 5;
            this.snowButton.UseVisualStyleBackColor = false;
            this.snowButton.Click += new System.EventHandler(this.colorButton_Click);
            // 
            // foliageButton
            // 
            this.foliageButton.BackColor = System.Drawing.Color.ForestGreen;
            this.foliageButton.Location = new System.Drawing.Point(117, 25);
            this.foliageButton.Name = "foliageButton";
            this.foliageButton.Size = new System.Drawing.Size(94, 92);
            this.foliageButton.TabIndex = 4;
            this.foliageButton.UseVisualStyleBackColor = false;
            this.foliageButton.Click += new System.EventHandler(this.colorButton_Click);
            // 
            // grayButton
            // 
            this.grayButton.BackColor = System.Drawing.Color.Silver;
            this.grayButton.Location = new System.Drawing.Point(118, 163);
            this.grayButton.Name = "grayButton";
            this.grayButton.Size = new System.Drawing.Size(94, 92);
            this.grayButton.TabIndex = 3;
            this.grayButton.UseVisualStyleBackColor = false;
            this.grayButton.Click += new System.EventHandler(this.colorButton_Click);
            // 
            // blueButton
            // 
            this.blueButton.BackColor = System.Drawing.Color.Blue;
            this.blueButton.Location = new System.Drawing.Point(18, 163);
            this.blueButton.Name = "blueButton";
            this.blueButton.Size = new System.Drawing.Size(94, 92);
            this.blueButton.TabIndex = 2;
            this.blueButton.UseVisualStyleBackColor = false;
            this.blueButton.Click += new System.EventHandler(this.colorButton_Click);
            // 
            // brownButton
            // 
            this.brownButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(70)))), ((int)(((byte)(60)))));
            this.brownButton.Location = new System.Drawing.Point(118, 300);
            this.brownButton.Name = "brownButton";
            this.brownButton.Size = new System.Drawing.Size(94, 92);
            this.brownButton.TabIndex = 1;
            this.brownButton.UseVisualStyleBackColor = false;
            this.brownButton.Click += new System.EventHandler(this.colorButton_Click);
            // 
            // grassButton
            // 
            this.grassButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(213)))), ((int)(((byte)(100)))));
            this.grassButton.Location = new System.Drawing.Point(16, 25);
            this.grassButton.Name = "grassButton";
            this.grassButton.Size = new System.Drawing.Size(94, 92);
            this.grassButton.TabIndex = 0;
            this.grassButton.UseVisualStyleBackColor = false;
            this.grassButton.Click += new System.EventHandler(this.colorButton_Click);
            // 
            // currentBox
            // 
            this.currentBox.Controls.Add(this.currentTilePicture);
            this.currentBox.Location = new System.Drawing.Point(16, 725);
            this.currentBox.Name = "currentBox";
            this.currentBox.Size = new System.Drawing.Size(159, 160);
            this.currentBox.TabIndex = 1;
            this.currentBox.TabStop = false;
            this.currentBox.Text = "Current Tile";
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(34, 891);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(194, 65);
            this.saveButton.TabIndex = 2;
            this.saveButton.Text = "Save File";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // loadButton
            // 
            this.loadButton.Location = new System.Drawing.Point(32, 962);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(106, 65);
            this.loadButton.TabIndex = 3;
            this.loadButton.Text = "Load File";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // mapBox
            // 
            this.mapBox.Location = new System.Drawing.Point(288, 20);
            this.mapBox.Name = "mapBox";
            this.mapBox.Size = new System.Drawing.Size(1060, 995);
            this.mapBox.TabIndex = 4;
            this.mapBox.TabStop = false;
            this.mapBox.Text = "Map";
            // 
            // enemyBox
            // 
            this.enemyBox.Controls.Add(this.enemy1);
            this.enemyBox.Controls.Add(this.eraseEnemy);
            this.enemyBox.Controls.Add(this.enemy0);
            this.enemyBox.Location = new System.Drawing.Point(15, 29);
            this.enemyBox.Name = "enemyBox";
            this.enemyBox.Size = new System.Drawing.Size(230, 689);
            this.enemyBox.TabIndex = 6;
            this.enemyBox.TabStop = false;
            this.enemyBox.Text = "Enemies";
            this.enemyBox.Visible = false;
            // 
            // palletteButton
            // 
            this.palletteButton.Location = new System.Drawing.Point(134, 2);
            this.palletteButton.Name = "palletteButton";
            this.palletteButton.Size = new System.Drawing.Size(98, 34);
            this.palletteButton.TabIndex = 5;
            this.palletteButton.Text = "Next Page";
            this.palletteButton.UseVisualStyleBackColor = true;
            this.palletteButton.Click += new System.EventHandler(this.palletteButton_Click);
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(146, 962);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(81, 63);
            this.clearButton.TabIndex = 7;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // enemy1
            // 
            this.enemy1.Image = global::Level_Editor.Properties.Resources.Fire;
            this.enemy1.Location = new System.Drawing.Point(1, 141);
            this.enemy1.Name = "enemy1";
            this.enemy1.Size = new System.Drawing.Size(100, 98);
            this.enemy1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.enemy1.TabIndex = 2;
            this.enemy1.TabStop = false;
            this.enemy1.Click += new System.EventHandler(this.enemy_Click);
            // 
            // eraseEnemy
            // 
            this.eraseEnemy.Image = global::Level_Editor.Properties.Resources.eraser;
            this.eraseEnemy.Location = new System.Drawing.Point(4, 37);
            this.eraseEnemy.Name = "eraseEnemy";
            this.eraseEnemy.Size = new System.Drawing.Size(100, 98);
            this.eraseEnemy.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.eraseEnemy.TabIndex = 1;
            this.eraseEnemy.TabStop = false;
            this.eraseEnemy.Click += new System.EventHandler(this.enemy_Click);
            // 
            // enemy0
            // 
            this.enemy0.Image = global::Level_Editor.Properties.Resources.EnemyholderSprite;
            this.enemy0.Location = new System.Drawing.Point(111, 37);
            this.enemy0.Name = "enemy0";
            this.enemy0.Size = new System.Drawing.Size(100, 98);
            this.enemy0.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.enemy0.TabIndex = 0;
            this.enemy0.TabStop = false;
            this.enemy0.Click += new System.EventHandler(this.enemy_Click);
            // 
            // currentTilePicture
            // 
            this.currentTilePicture.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(213)))), ((int)(((byte)(100)))));
            this.currentTilePicture.Location = new System.Drawing.Point(26, 26);
            this.currentTilePicture.Name = "currentTilePicture";
            this.currentTilePicture.Size = new System.Drawing.Size(110, 109);
            this.currentTilePicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.currentTilePicture.TabIndex = 0;
            this.currentTilePicture.TabStop = false;
            // 
            // LevelEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1544, 1042);
            this.Controls.Add(this.palletteButton);
            this.Controls.Add(this.enemyBox);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.mapBox);
            this.Controls.Add(this.loadButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.currentBox);
            this.Controls.Add(this.colorBox);
            this.Name = "LevelEditor";
            this.Text = "Level Editor";
            this.colorBox.ResumeLayout(false);
            this.colorBox.PerformLayout();
            this.currentBox.ResumeLayout(false);
            this.enemyBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.enemy1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eraseEnemy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.enemy0)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.currentTilePicture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox colorBox;
        private System.Windows.Forms.Button grassButton;
        private System.Windows.Forms.Button brownButton;
        private System.Windows.Forms.Button snowButton;
        private System.Windows.Forms.Button foliageButton;
        private System.Windows.Forms.Button grayButton;
        private System.Windows.Forms.Button blueButton;
        private System.Windows.Forms.GroupBox currentBox;
        private System.Windows.Forms.PictureBox currentTilePicture;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.GroupBox mapBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button bridgeButton;
        private System.Windows.Forms.Button palletteButton;
        private System.Windows.Forms.GroupBox enemyBox;
        private System.Windows.Forms.PictureBox enemy0;
        private System.Windows.Forms.PictureBox eraseEnemy;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button flowerButton;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button bossDoorButton;
        private System.Windows.Forms.PictureBox enemy1;
    }
}