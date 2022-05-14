using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Eragonia_Demo_Day_One
{
    public class MusicManager
    {
        Song[] songs = new Song[5];
        public World world;
        public int timer = 0;
        public int nextIndex = -1;
        public Boolean doTransistion1 = false;
        public Boolean doTransistion3 = false;
        public Boolean doTransistion2 = false;
        public void LoadSongs()
        {
            songs[0] = world.Content.Load<Song>("Sounds/Music/mainTheme");
            songs[1] = world.Content.Load<Song>("Sounds/Music/playTheme");
            songs[2] = world.Content.Load<Song>("Sounds/Music/bossIntro");
            songs[3] = world.Content.Load<Song>("Sounds/Music/finalBoss");
            songs[4] = world.Content.Load<Song>("Sounds/Music/victoryTheme");
        }

        public void PlaySong(int index)
        {


            MediaPlayer.IsRepeating = true;
            //nextIndex = index;
            MediaPlayer.Play(songs[index]);
        }
        public void PlaySong(int index, Boolean repeat) {
            MediaPlayer.IsRepeating = repeat;
            MediaPlayer.Play(songs[index]);
        }
        public void TransistionSong(int index, Boolean repeat)
        {
            MediaPlayer.IsRepeating = repeat;
            nextIndex = index;
            doTransistion1 = true;
        }
       

        public void Update()
        {
            if (doTransistion1)
            {
                MediaPlayer.Volume -= 0.01f;
                if (MediaPlayer.Volume - 0.01f <= 0)
                {
                    MediaPlayer.Play(songs[nextIndex]);
                    doTransistion1 = false;
                    doTransistion2 = true;
                }
            }
            else if (doTransistion2)
            {
                MediaPlayer.Volume += 0.01f;
                if (MediaPlayer.Volume + 0.01f >= world.maxVolume)
                {
                    doTransistion2 = false;
                }
            }
            else if(doTransistion3) {
                MediaPlayer.Volume -= 0.01f;
                if (MediaPlayer.Volume - 0.01f <= 0)
                {
                    doTransistion3 = false;
                    MediaPlayer.Stop();
                    MediaPlayer.Volume = world.maxVolume;
                }
            }

        }


    }
}
