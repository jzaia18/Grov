namespace Level_Editor
{
    partial class Form1
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
            this.loadButton = new System.Windows.Forms.Button();
            this.createBox = new System.Windows.Forms.GroupBox();
            this.createButton = new System.Windows.Forms.Button();
            this.heightBox = new System.Windows.Forms.TextBox();
            this.heightLabel = new System.Windows.Forms.Label();
            this.widthLabel = new System.Windows.Forms.Label();
            this.widthBox = new System.Windows.Forms.TextBox();
            this.createBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // loadButton
            // 
            this.loadButton.Location = new System.Drawing.Point(20, 20);
            this.loadButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(368, 105);
            this.loadButton.TabIndex = 0;
            this.loadButton.Text = "Load Map";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // createBox
            // 
            this.createBox.Controls.Add(this.createButton);
            this.createBox.Controls.Add(this.heightBox);
            this.createBox.Controls.Add(this.heightLabel);
            this.createBox.Controls.Add(this.widthLabel);
            this.createBox.Controls.Add(this.widthBox);
            this.createBox.Location = new System.Drawing.Point(20, 178);
            this.createBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.createBox.Name = "createBox";
            this.createBox.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.createBox.Size = new System.Drawing.Size(368, 272);
            this.createBox.TabIndex = 1;
            this.createBox.TabStop = false;
            this.createBox.Text = "Create New Map";
            // 
            // createButton
            // 
            this.createButton.Location = new System.Drawing.Point(44, 148);
            this.createButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.createButton.Name = "createButton";
            this.createButton.Size = new System.Drawing.Size(280, 109);
            this.createButton.TabIndex = 4;
            this.createButton.Text = "Create Map";
            this.createButton.UseVisualStyleBackColor = true;
            this.createButton.Click += new System.EventHandler(this.createButton_Click);
            // 
            // heightBox
            // 
            this.heightBox.Location = new System.Drawing.Point(174, 94);
            this.heightBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.heightBox.Name = "heightBox";
            this.heightBox.Size = new System.Drawing.Size(148, 26);
            this.heightBox.TabIndex = 3;
            this.heightBox.Text = "10";
            // 
            // heightLabel
            // 
            this.heightLabel.AutoSize = true;
            this.heightLabel.Location = new System.Drawing.Point(39, 98);
            this.heightLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.heightLabel.Name = "heightLabel";
            this.heightLabel.Size = new System.Drawing.Size(120, 20);
            this.heightLabel.TabIndex = 2;
            this.heightLabel.Text = "Height (In Tiles)";
            // 
            // widthLabel
            // 
            this.widthLabel.AutoSize = true;
            this.widthLabel.Location = new System.Drawing.Point(39, 55);
            this.widthLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.widthLabel.Name = "widthLabel";
            this.widthLabel.Size = new System.Drawing.Size(114, 20);
            this.widthLabel.TabIndex = 1;
            this.widthLabel.Text = "Width (In Tiles)";
            // 
            // widthBox
            // 
            this.widthBox.Location = new System.Drawing.Point(174, 51);
            this.widthBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.widthBox.Name = "widthBox";
            this.widthBox.Size = new System.Drawing.Size(148, 26);
            this.widthBox.TabIndex = 0;
            this.widthBox.Text = "10";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(405, 465);
            this.Controls.Add(this.createBox);
            this.Controls.Add(this.loadButton);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.Text = "Level Editor";
            this.createBox.ResumeLayout(false);
            this.createBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.GroupBox createBox;
        private System.Windows.Forms.Button createButton;
        private System.Windows.Forms.TextBox heightBox;
        private System.Windows.Forms.Label heightLabel;
        private System.Windows.Forms.Label widthLabel;
        private System.Windows.Forms.TextBox widthBox;
    }
}

