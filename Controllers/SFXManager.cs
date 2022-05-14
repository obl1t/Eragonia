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
    public class SFXManager
    {
        public World world;
        Dictionary<String, SoundEffect> sounds = new Dictionary<String, SoundEffect>();
        public SoundEffectInstance currentSound;
        public void Initialize() {
            sounds.Add("click", world.Content.Load<SoundEffect>("Sounds/SFX/click"));
            sounds.Add("loss", world.Content.Load<SoundEffect>("Sounds/SFX/lossTune"));
            sounds.Add("artifact1", world.Content.Load<SoundEffect>("Sounds/SFX/artifact1"));
            sounds.Add("artifact2", world.Content.Load<SoundEffect>("Sounds/SFX/artifact2"));
            sounds.Add("artifact3", world.Content.Load<SoundEffect>("Sounds/SFX/artifact3"));
            sounds.Add("waveStart", world.Content.Load<SoundEffect>("Sounds/SFX/waveStart"));
            sounds.Add("towerPlace", world.Content.Load<SoundEffect>("Sounds/SFX/placeTower"));
            sounds.Add("tierUp", world.Content.Load<SoundEffect>("Sounds/SFX/tierUp"));
            sounds.Add("swoosh", world.Content.Load<SoundEffect>("Sounds/SFX/swoosh"));
            sounds.Add("arrow", world.Content.Load<SoundEffect>("Sounds/SFX/arrow"));
            sounds.Add("ballista", world.Content.Load<SoundEffect>("Sounds/SFX/ballista"));
            sounds.Add("upgrade", world.Content.Load<SoundEffect>("Sounds/SFX/upgrade"));
            sounds.Add("blipHigh", world.Content.Load<SoundEffect>("Sounds/SFX/blipHigh"));
            sounds.Add("blipLow", world.Content.Load<SoundEffect>("Sounds/SFX/blipLow"));
            sounds.Add("thunder", world.Content.Load<SoundEffect>("Sounds/SFX/thunder"));
            sounds.Add("mace", world.Content.Load<SoundEffect>("Sounds/SFX/mace"));
            sounds.Add("sword", world.Content.Load<SoundEffect>("Sounds/SFX/sword"));
            sounds.Add("hellPillar", world.Content.Load<SoundEffect>("Sounds/SFX/hellPillar"));
            sounds.Add("break", world.Content.Load<SoundEffect>("Sounds/SFX/break"));
            sounds.Add("beep", world.Content.Load<SoundEffect>("Sounds/SFX/beep"));
        }
        public void PlaySound(String name) {
            if(sounds[name] != null) {
                
                currentSound = sounds[name].CreateInstance();
                currentSound.Volume = world.maxSFX;
                currentSound.Play();
            }
        }
        public void PlaySound(String name, float volumeMult) {
            if (sounds[name] != null)
            {

                currentSound = sounds[name].CreateInstance();
                currentSound.Volume = world.maxSFX * volumeMult;
                currentSound.Play();
            }
        }
        public void PlaySoundQuietly(String name) {
            if (sounds[name] != null)
            {

                currentSound = sounds[name].CreateInstance();
                currentSound.Volume = world.maxSFX / 10f;
                currentSound.Play();
            }
        }
        public void PlaySoundQuickly(String name) {
            if (sounds[name] != null)
            {

                currentSound = sounds[name].CreateInstance();
                currentSound.Volume = world.maxSFX;
                currentSound.Pan = 0.0f;
                currentSound.Pitch = 0.7f;
                currentSound.Play();
            }
        }
        public void PlaySoundWithStop(String name) {
            currentSound.Stop();
            
            currentSound.Dispose();
            if (sounds[name] != null)
            {
        
                currentSound = sounds[name].CreateInstance();
                currentSound.Volume = world.maxSFX;
                currentSound.Play();
            }
        }

        public void PlaySoundPitchUp(String name) {
            if (sounds[name] != null)
            {

                currentSound = sounds[name].CreateInstance();
                currentSound.Volume = world.maxSFX;
                currentSound.Pitch = 0.5f;
                currentSound.Pan = 0.25f;
                currentSound.Play();
            }
        }
        
    }
}
