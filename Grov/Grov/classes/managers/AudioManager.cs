using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System.Windows.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;


namespace Grov
{
    class AudioManager
    {
        #region Fields
        // ************* Fields ************* //

        Dictionary<string, SoundEffect> songs;
        private static AudioManager instance;
        private SoundEffect currentSong;
        private double totalMilliseconds;
        #endregion

        #region Properties
        // ************* Properties ************* //

        public static AudioManager Instance { get => instance; }

        #endregion

        #region Constructors
        // ************* Constructor ************* //

        private AudioManager()
        {
            this.LoadSongs();
        }

        public static void Initialize()
        {
            if(instance == null)
            {
                instance = new AudioManager();
            }
        }

        #endregion

        #region Methods
        // ************* Methods ************* //

        private void LoadSongs()
        {
            songs = new Dictionary<string, SoundEffect>();
            totalMilliseconds = 0;

            songs.Add("TitleMusicIntro", DisplayManager.ContentManager.Load<SoundEffect>("Audio/Music/TitleMusicIntro"));
            songs.Add("Audio/Music/TitleMusicLoop", DisplayManager.ContentManager.Load<SoundEffect>("Audio/Music/TitleMusicLoop"));

            this.PlaySong("TitleMusicIntro");
        }

        public void PlaySong(string song)
        {
            Console.WriteLine(song);
            if (songs.ContainsKey(song))
            {
                currentSong = songs[song];
                currentSong.CreateInstance().Play();
            }
        }

        //Testing purposes
        public void Update(GameTime gameTime)
        {
            totalMilliseconds += gameTime.ElapsedGameTime.Milliseconds;
            Console.WriteLine(totalMilliseconds + "||\t" + currentSong.Duration.TotalMilliseconds);
            if (totalMilliseconds >= currentSong.Duration.TotalMilliseconds - 600 && currentSong.Name.Contains("Intro"))
            {
                this.PlaySong(currentSong.Name.Substring(0, currentSong.Name.Length - 5) + "Loop");
                totalMilliseconds = 0;
            }
            else if (currentSong.Name.Contains("Loop") && totalMilliseconds >= currentSong.Duration.TotalMilliseconds - 1775)
            {
                this.PlaySong(currentSong.Name);
                totalMilliseconds = 0;
            }
            else if(totalMilliseconds >= currentSong.Duration.TotalMilliseconds)
            {
                this.PlaySong(currentSong.Name);
                totalMilliseconds = 0;
            }
        }
        #endregion

    }
}
