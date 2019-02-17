using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Level_Editor
{
    public partial class Form1 : Form
    {
        int width;
        int height;

        public Form1()
        {
            InitializeComponent();
        }

        //Load a File
        private void loadButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Title = "Open a Level File";
            openFile.Filter = "Level Files (*.grovlev)|*.grovlev";
            DialogResult result = openFile.ShowDialog();
            if(result == DialogResult.OK)
            {
                LevelEditor levelEditor = new LevelEditor(openFile.FileName);
                levelEditor.ShowDialog();
            }
        }

        //Create a new File
        private void createButton_Click(object sender, EventArgs e)
        {
            //Create a temp string array that will hold every error the user causes
            List<string> errors = new List<string>();

            //Interpret width as int
            if(!int.TryParse(widthBox.Text, out width))
            {
                errors.Add("-The Width is not a number.");
            }
            //Make sure it's greater than/equal to 10
            else if (width < 10)
            {
                errors.Add("-Width is too small. Minimum is 10");
            }
            //Make sure it's smaller than/equal to 30
            else if (width > 30)
            {
                errors.Add("-Width too large. Maximum is 30");
            }

            //Interpret height as int
            if (!int.TryParse(heightBox.Text, out height))
            {
                errors.Add("-The Height is not a number.");
            }
            //Make sure it's greater than/equal to 10
            else if (height < 10)
            {
                errors.Add("-Height is too small. Minimum is 10");
            }
            //Make sure it's smaller than/equal to 30
            else if (height > 30)
            {
                errors.Add("-Height too large. Maximum is 30");
            }

            //Display an error message if there was anything wrong
            if(errors.Count != 0)
            {
                string errorList = "";
                foreach(string message in errors)
                {
                    errorList += message;
                    errorList += "\n";
                }
                MessageBox.Show(errorList, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //Otherwise it's ok to make a file
            else
            {
                LevelEditor levelEditor = new LevelEditor(height, width);
                levelEditor.ShowDialog();
            }
        }
    }
}
