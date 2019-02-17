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
            this.snowButton = new System.Windows.Forms.Button();
            this.foliageButton = new System.Windows.Forms.Button();
            this.grayButton = new System.Windows.Forms.Button();
            this.blueButton = new System.Windows.Forms.Button();
            this.brownButton = new System.Windows.Forms.Button();
            this.greenButton = new System.Windows.Forms.Button();
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
            this.colorBox.Controls.Add(this.snowButton);
            this.colorBox.Controls.Add(this.foliageButton);
            this.colorBox.Controls.Add(this.grayButton);
            this.colorBox.Controls.Add(this.blueButton);
            this.colorBox.Controls.Add(this.brownButton);
            this.colorBox.Controls.Add(this.greenButton);
            this.colorBox.Location = new System.Drawing.Point(15, 20);
            this.colorBox.Name = "colorBox";
            this.colorBox.Size = new System.Drawing.Size(229, 331);
            this.colorBox.TabIndex = 0;
            this.colorBox.TabStop = false;
            this.colorBox.Text = "Tile Selector";
            // 
            // snowButton
            // 
            this.snowButton.BackColor = System.Drawing.Color.White;
            this.snowButton.Location = new System.Drawing.Point(16, 223);
            this.snowButton.Name = "snowButton";
            this.snowButton.Size = new System.Drawing.Size(95, 93);
            this.snowButton.TabIndex = 5;
            this.snowButton.UseVisualStyleBackColor = false;
            this.snowButton.Click += new System.EventHandler(this.colorButton_Click);
            // 
            // foliageButton
            // 
            this.foliageButton.BackColor = System.Drawing.Color.ForestGreen;
            this.foliageButton.Location = new System.Drawing.Point(117, 25);
            this.foliageButton.Name = "foliageButton";
            this.foliageButton.Size = new System.Drawing.Size(95, 93);
            this.foliageButton.TabIndex = 4;
            this.foliageButton.UseVisualStyleBackColor = false;
            this.foliageButton.Click += new System.EventHandler(this.colorButton_Click);
            // 
            // grayButton
            // 
            this.grayButton.BackColor = System.Drawing.Color.Silver;
            this.grayButton.Location = new System.Drawing.Point(117, 124);
            this.grayButton.Name = "grayButton";
            this.grayButton.Size = new System.Drawing.Size(95, 93);
            this.grayButton.TabIndex = 3;
            this.grayButton.UseVisualStyleBackColor = false;
            this.grayButton.Click += new System.EventHandler(this.colorButton_Click);
            // 
            // blueButton
            // 
            this.blueButton.BackColor = System.Drawing.Color.Blue;
            this.blueButton.Location = new System.Drawing.Point(16, 124);
            this.blueButton.Name = "blueButton";
            this.blueButton.Size = new System.Drawing.Size(95, 93);
            this.blueButton.TabIndex = 2;
            this.blueButton.UseVisualStyleBackColor = false;
            this.blueButton.Click += new System.EventHandler(this.colorButton_Click);
            // 
            // brownButton
            // 
            this.brownButton.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.brownButton.Location = new System.Drawing.Point(117, 223);
            this.brownButton.Name = "brownButton";
            this.brownButton.Size = new System.Drawing.Size(95, 93);
            this.brownButton.TabIndex = 1;
            this.brownButton.UseVisualStyleBackColor = false;
            this.brownButton.Click += new System.EventHandler(this.colorButton_Click);
            // 
            // greenButton
            // 
            this.greenButton.BackColor = System.Drawing.Color.Lime;
            this.greenButton.Location = new System.Drawing.Point(16, 25);
            this.greenButton.Name = "greenButton";
            this.greenButton.Size = new System.Drawing.Size(95, 93);
            this.greenButton.TabIndex = 0;
            this.greenButton.UseVisualStyleBackColor = false;
            this.greenButton.Click += new System.EventHandler(this.colorButton_Click);
            // 
            // currentBox
            // 
            this.currentBox.Controls.Add(this.currentTilePicture);
            this.currentBox.Location = new System.Drawing.Point(15, 358);
            this.currentBox.Name = "currentBox";
            this.currentBox.Size = new System.Drawing.Size(229, 256);
            this.currentBox.TabIndex = 1;
            this.currentBox.TabStop = false;
            this.currentBox.Text = "Current Tile";
            // 
            // currentTilePicture
            // 
            this.currentTilePicture.BackColor = System.Drawing.Color.Blue;
            this.currentTilePicture.Location = new System.Drawing.Point(55, 73);
            this.currentTilePicture.Name = "currentTilePicture";
            this.currentTilePicture.Size = new System.Drawing.Size(110, 110);
            this.currentTilePicture.TabIndex = 0;
            this.currentTilePicture.TabStop = false;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(31, 636);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(196, 182);
            this.saveButton.TabIndex = 2;
            this.saveButton.Text = "Save File";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // loadButton
            // 
            this.loadButton.Location = new System.Drawing.Point(31, 834);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(196, 182);
            this.loadButton.TabIndex = 3;
            this.loadButton.Text = "Load File";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // mapBox
            // 
            this.mapBox.Location = new System.Drawing.Point(288, 20);
            this.mapBox.Name = "mapBox";
            this.mapBox.Size = new System.Drawing.Size(1061, 996);
            this.mapBox.TabIndex = 4;
            this.mapBox.TabStop = false;
            this.mapBox.Text = "Map";
            // 
            // LevelEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1543, 1028);
            this.Controls.Add(this.mapBox);
            this.Controls.Add(this.loadButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.currentBox);
            this.Controls.Add(this.colorBox);
            this.Name = "LevelEditor";
            this.Text = "Level Editor";
            this.colorBox.ResumeLayout(false);
            this.currentBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.currentTilePicture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox colorBox;
        private System.Windows.Forms.Button greenButton;
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
    }
}