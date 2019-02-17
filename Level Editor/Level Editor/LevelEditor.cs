using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Level_Editor
{
    public partial class LevelEditor : Form
    {
        private int width;
        private int height;
        private int boxSize;
        private Color currentColor;
        private PictureBox[,] mapArray;

        private bool fileNameTitle;
        private bool unsavedChanges;
        private string fileName;

        //This constructor is for making a new file
        public LevelEditor(int height, int width)
        {
            this.width = width;
            this.height = height;

            fileNameTitle = false;
            unsavedChanges = false;

            //Default Color is blue
            currentColor = Color.Blue;

            //Close event
            this.FormClosing += CloseEvent;

            InitializeComponent();

            CreateMap();
        }
        //This constructor is for loading files
        public LevelEditor(string fileName)
        {
            InitializeComponent();

            //Close event
            this.FormClosing += CloseEvent;

            this.fileName = fileName;
            LoadMap(fileName);
        }



        /// <summary>
        /// Change the color of the brush
        /// </summary>
        private void colorButton_Click(object sender, EventArgs e)
        {
            currentColor = ((Button)sender).BackColor;
            currentTilePicture.BackColor = currentColor;
        }
        /// <summary>
        /// Change the color of the selected map tile
        /// </summary>
        private void MapSquare_Click(object sender, EventArgs e)
        {
            //Un-capture the mouse
            ((PictureBox)sender).Capture = false;

            if (MouseButtons == MouseButtons.Left)
            {
                //Change the color
                ((PictureBox)sender).BackColor = currentColor;

                //There are now unsaved changes
                if (fileNameTitle)
                {
                    this.Text = string.Format("Level Editor - {0} *", fileName);
                }
                else
                {
                    this.Text = "Level Editor *";
                }
                unsavedChanges = true;
            }
        }



        //Save the map
        private void saveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Title = "Save a Level File";
            saveDialog.Filter = "Level Files (*.level)|*.level";
            saveDialog.FileName = "MyLevel";
            DialogResult result = saveDialog.ShowDialog();
            if(result == DialogResult.OK)
            {
                BinaryWriter writer = null;

                try
                {
                    Stream streamOut = File.OpenWrite(saveDialog.FileName);
                    writer = new BinaryWriter(streamOut);

                    //Write the height, then width
                    writer.Write(height);
                    writer.Write(width);

                    for(int i = 0; i < height; i++)
                    {
                        for(int ii = 0; ii < width; ii++)
                        {
                            writer.Write((mapArray[i, ii].BackColor).ToArgb());
                        }
                    }

                    MessageBox.Show("File Saved Successfully", "File Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //Mark the file as saved
                    fileNameTitle = true;
                    unsavedChanges = false;
                    this.Text = string.Format("Level Editor - {0}", saveDialog.FileName);
                    fileName = saveDialog.FileName;
                }
                catch (Exception exception)
                {
                    MessageBox.Show(string.Format("Error writing to file: ", exception.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (writer != null)
                    {
                        writer.Close();
                    }
                }
            }
        }
        //Load a map
        private void loadButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Title = "Open a Level File";
            openFile.Filter = "Level Files (*.level)|*.level";
            DialogResult result = openFile.ShowDialog();
            if(result == DialogResult.OK)
            {
                LoadMap(openFile.FileName);
            }
        }


        /// <summary>
        /// Generates a grid from the given height and width
        /// </summary>
        private void CreateMap()
        {
            //Create a 2D array the size of the height
            mapBox.Controls.Clear();
            mapArray = new PictureBox[height, width];
            //Create the size of the box; equal to the height of the map / the number of map slots
            boxSize = (mapBox.Height - 27) / height;

            //Create a bunch of picture boxes, and assign all their values
            for (int i = 0; i < height; i++)
            {
                for (int ii = 0; ii < width; ii++)
                {
                    //i = height ii = width

                    mapArray[i, ii] = new PictureBox();
                    //Add it to the Map Box
                    mapBox.Controls.Add(mapArray[i, ii]);
                    //Default Color will be Blue
                    mapArray[i, ii].BackColor = Color.Blue;
                    //Set the size of the box
                    mapArray[i, ii].Height = boxSize;
                    mapArray[i, ii].Width = boxSize;
                    //Set the location
                    mapArray[i, ii].Location = new Point(10 + ii * boxSize, 18 + i * boxSize);

                    //Give it a click event
                    mapArray[i, ii].MouseDown += MapSquare_Click;
                    //Also give it a mouseenter event
                    mapArray[i, ii].MouseEnter += MapSquare_Click;
                }
            }

            //fix the size of the map box and window
            mapBox.Width = (boxSize * width) + 20;
            this.Width = (boxSize * width) + 268;
        }






        //The actual process for loading a map
        private void LoadMap(string openFile)
        {
            BinaryReader reader = null;

            try
            {
                Stream streamIn = File.OpenRead(openFile);
                reader = new BinaryReader(streamIn);

                //Read the height, then the width
                height = reader.ReadInt32();
                width = reader.ReadInt32();

                //Create a new map with the data
                CreateMap();

                //Write all the color data
                for (int i = 0; i < height; i++)
                {
                    for (int ii = 0; ii < width; ii++)
                    {
                        mapArray[i, ii].BackColor = Color.FromArgb(reader.ReadInt32());
                    }
                }

                MessageBox.Show("File Loaded Successfully", "File Loaded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Mark the file as saved
                fileNameTitle = true;
                unsavedChanges = false;
                this.Text = string.Format("Level Editor - {0}", openFile);
                fileName = openFile;
            }
            catch (Exception exception)
            {
                MessageBox.Show(string.Format("Error writing to file: ", exception.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }



        //Form closing event
        private void CloseEvent(object sender, FormClosingEventArgs e)
        {
            if(unsavedChanges)
            {
                DialogResult result = MessageBox.Show("You have unsaved changes! Are you sure you want to quit?", "Unsaved Changes", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if(result != DialogResult.Yes)
                {
                    e.Cancel = true;
                }
            }
        }

    }
}
