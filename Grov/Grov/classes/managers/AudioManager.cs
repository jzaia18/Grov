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
using CSCore;
using CSCore.CoreAudioAPI;
using CSCore.DirectSound;
using CSCore.Codecs.MP3;
using CSCore.Codecs.WAV;
using CSCore.SoundOut;
using CSCore.Streams;


namespace Grov
{
    class AudioManager
    {
        #region Fields
        // ************* Fields ************* //

        WasapiOut audioStream;

        Dictionary<string, string> songs;
        Dictionary<string, SoundEffect> effects;
        BufferSource currentSong;
        private static AudioManager instance;
        private double totalMilliseconds;
        private int count; //test for frame count
        #endregion

        #region Properties
        // ************* Properties ************* //

        public static AudioManager Instance { get => instance; }

        #endregion

        #region Constructors
        // ************* Constructor ************* //

        private AudioManager()
        {
            audioStream = new WasapiOut();
            this.LoadSongs();
            this.LoadEffects();
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

        private void LoadEffects()
        {
            effects = new Dictionary<string, SoundEffect>();

            effects.Add("FireShot", DisplayManager.ContentManager.Load<SoundEffect>("Audio/SFX/FireShot"));
        }

        private void LoadSongs()
        {
            songs = new Dictionary<string, string>();
            totalMilliseconds = 0;

            songs.Add("TitleMusicIntro", "Audio/music/TitleMusicIntro.mp3");
            songs.Add("TitleMusicLoop", "Audio/music/TitleMusicLoop.mp3");
            this.PlaySong("TitleMusicIntro");
        }

        public void PlaySong(string song)
        {
            
            if (songs.ContainsKey(song))
            {
                audioStream.Initialize()
                audioStream.Play();
                count = 0;
            }
        }

        public void PlayEffect(string effect)
        {
            if (effects.ContainsKey(effect))
            {
                effects[effect].CreateInstance().Play();
            }
        }

        //Testing purposes
        public void Update(GameTime gameTime)
        {
            
        }
        #endregion

    }
}
