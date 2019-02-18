namespace Level_Editor
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
            this.label7 = new System.Windows.Forms.Label();
            this.button7 = new System.Windows.Forms.Button();
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
            this.currentTilePicture = new System.Windows.Forms.PictureBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.loadButton = new System.Windows.Forms.Button();
            this.mapBox = new System.Windows.Forms.GroupBox();
            this.colorBox.SuspendLayout();
            this.currentBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.currentTilePicture)).BeginInit();
            this.SuspendLayout();
            // 
            // colorBox
            // 
            this.colorBox.Controls.Add(this.label7);
            this.colorBox.Controls.Add(this.button7);
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
            this.colorBox.Location = new System.Drawing.Point(10, 13);
            this.colorBox.Margin = new System.Windows.Forms.Padding(2);
            this.colorBox.Name = "colorBox";
            this.colorBox.Padding = new System.Windows.Forms.Padding(2);
            this.colorBox.Size = new System.Drawing.Size(153, 387);
            this.colorBox.TabIndex = 0;
            this.colorBox.TabStop = false;
            this.colorBox.Text = "Tile Selector";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 351);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Bridge";
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.button7.Location = new System.Drawing.Point(11, 285);
            this.button7.Margin = new System.Windows.Forms.Padding(2);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(63, 60);
            this.button7.TabIndex = 12;
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.colorButton_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(78, 260);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Log/Stump";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 261);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Door";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(82, 171);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Rocks";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 172);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Water";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(79, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Border Wall";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Floor";
            // 
            // snowButton
            // 
            this.snowButton.BackColor = System.Drawing.Color.Purple;
            this.snowButton.Location = new System.Drawing.Point(12, 195);
            this.snowButton.Margin = new System.Windows.Forms.Padding(2);
            this.snowButton.Name = "snowButton";
            this.snowButton.Size = new System.Drawing.Size(63, 60);
            this.snowButton.TabIndex = 5;
            this.snowButton.UseVisualStyleBackColor = false;
            this.snowButton.Click += new System.EventHandler(this.colorButton_Click);
            // 
            // foliageButton
            // 
            this.foliageButton.BackColor = System.Drawing.Color.ForestGreen;
            this.foliageButton.Location = new System.Drawing.Point(78, 16);
            this.foliageButton.Margin = new System.Windows.Forms.Padding(2);
            this.foliageButton.Name = "foliageButton";
            this.foliageButton.Size = new System.Drawing.Size(63, 60);
            this.foliageButton.TabIndex = 4;
            this.foliageButton.UseVisualStyleBackColor = false;
            this.foliageButton.Click += new System.EventHandler(this.colorButton_Click);
            // 
            // grayButton
            // 
            this.grayButton.BackColor = System.Drawing.Color.Silver;
            this.grayButton.Location = new System.Drawing.Point(79, 106);
            this.grayButton.Margin = new System.Windows.Forms.Padding(2);
            this.grayButton.Name = "grayButton";
            this.grayButton.Size = new System.Drawing.Size(63, 60);
            this.grayButton.TabIndex = 3;
            this.grayButton.UseVisualStyleBackColor = false;
            this.grayButton.Click += new System.EventHandler(this.colorButton_Click);
            // 
            // blueButton
            // 
            this.blueButton.BackColor = System.Drawing.Color.Blue;
            this.blueButton.Location = new System.Drawing.Point(12, 106);
            this.blueButton.Margin = new System.Windows.Forms.Padding(2);
            this.blueButton.Name = "blueButton";
            this.blueButton.Size = new System.Drawing.Size(63, 60);
            this.blueButton.TabIndex = 2;
            this.blueButton.UseVisualStyleBackColor = false;
            this.blueButton.Click += new System.EventHandler(this.colorButton_Click);
            // 
            // brownButton
            // 
            this.brownButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(70)))), ((int)(((byte)(60)))));
            this.brownButton.Location = new System.Drawing.Point(79, 195);
            this.brownButton.Margin = new System.Windows.Forms.Padding(2);
            this.brownButton.Name = "brownButton";
            this.brownButton.Size = new System.Drawing.Size(63, 60);
            this.brownButton.TabIndex = 1;
            this.brownButton.UseVisualStyleBackColor = false;
            this.brownButton.Click += new System.EventHandler(this.colorButton_Click);
            // 
            // grassButton
            // 
            this.grassButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(213)))), ((int)(((byte)(100)))));
            this.grassButton.Location = new System.Drawing.Point(11, 16);
            this.grassButton.Margin = new System.Windows.Forms.Padding(2);
            this.grassButton.Name = "grassButton";
            this.grassButton.Size = new System.Drawing.Size(63, 60);
            this.grassButton.TabIndex = 0;
            this.grassButton.UseVisualStyleBackColor = false;
            this.grassButton.Click += new System.EventHandler(this.colorButton_Click);
            // 
            // currentBox
            // 
            this.currentBox.Controls.Add(this.currentTilePicture);
            this.currentBox.Location = new System.Drawing.Point(11, 404);
            this.currentBox.Margin = new System.Windows.Forms.Padding(2);
            this.currentBox.Name = "currentBox";
            this.currentBox.Padding = new System.Windows.Forms.Padding(2);
            this.currentBox.Size = new System.Drawing.Size(153, 118);
            this.currentBox.TabIndex = 1;
            this.currentBox.TabStop = false;
            this.currentBox.Text = "Current Tile";
            // 
            // currentTilePicture
            // 
            this.currentTilePicture.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(213)))), ((int)(((byte)(100)))));
            this.currentTilePicture.Location = new System.Drawing.Point(37, 29);
            this.currentTilePicture.Margin = new System.Windows.Forms.Padding(2);
            this.currentTilePicture.Name = "currentTilePicture";
            this.currentTilePicture.Size = new System.Drawing.Size(73, 71);
            this.currentTilePicture.TabIndex = 0;
            this.currentTilePicture.TabStop = false;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(21, 526);
            this.saveButton.Margin = new System.Windows.Forms.Padding(2);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(131, 58);
            this.saveButton.TabIndex = 2;
            this.saveButton.Text = "Save File";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // loadButton
            // 
            this.loadButton.Location = new System.Drawing.Point(21, 588);
            this.loadButton.Margin = new System.Windows.Forms.Padding(2);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(131, 72);
            this.loadButton.TabIndex = 3;
            this.loadButton.Text = "Load File";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // mapBox
            // 
            this.mapBox.Location = new System.Drawing.Point(192, 13);
            this.mapBox.Margin = new System.Windows.Forms.Padding(2);
            this.mapBox.Name = "mapBox";
            this.mapBox.Padding = new System.Windows.Forms.Padding(2);
            this.mapBox.Size = new System.Drawing.Size(707, 647);
            this.mapBox.TabIndex = 4;
            this.mapBox.TabStop = false;
            this.mapBox.Text = "Map";
            // 
            // LevelEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1029, 668);
            this.Controls.Add(this.mapBox);
            this.Controls.Add(this.loadButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.currentBox);
            this.Controls.Add(this.colorBox);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "LevelEditor";
            this.Text = "Level Editor";
            this.colorBox.ResumeLayout(false);
            this.colorBox.PerformLayout();
            this.currentBox.ResumeLayout(false);
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
        private System.Windows.Forms.Button button7;
    }
}