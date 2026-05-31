using System;
using System.Collections.Generic;
using Helteix.Singletons.SceneServices;
using Naussilus.Core.Sounds;
using UnityEngine;

namespace Naussilus.Gameplay
{
    public class SoundDesignManager : MonoBehaviour
    {
        public static SoundDesignManager instance;
        [SerializeField] private SoundLibraryData libraryData;
        [SerializeField] private MusicsManager musicsManager;
        [SerializeField] private float defaultVolume;
        [SerializeField] private float managmentMusicVolume;
        [SerializeField] private float visualNovelMusicVolume;
        [SerializeField] private GameObject sourceOriginal;
        
        private List<AudioSource> audioSources = new List<AudioSource>();
        private bool isSoundsMuted = false;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        public void VisualNovelMusic(bool launch)
        {
            if (musicsManager != null)
            {
                if (launch)
                {
                    musicsManager.PauseMusic();
                    musicsManager.PlayMusic(libraryData.visualNovelMusic, visualNovelMusicVolume);
                }
                else
                {
                    musicsManager.PauseMusic();
                }
            }
        }

        public void ManagmentMusic(bool launch)
        {
            if (musicsManager != null)
            {
                if (launch)
                {
                    musicsManager.PauseMusic();
                    musicsManager.PlayMusic(libraryData.managmentMusic,managmentMusicVolume);
                }
                else
                {
                    musicsManager.PauseMusic();
                }
            }
        }

        public void CutMusic(bool mute)
        {
            if (musicsManager != null)
            {
                if (mute == true)
                {
                    musicsManager.MuteMusic(true);
                }
                else
                {
                    musicsManager.MuteMusic(false);
                }
            }
        }

        public void CutSounds(bool mute)
        {
            if (mute)
            {
                isSoundsMuted = true;
                foreach (AudioSource source in audioSources)
                {
                    source.volume = 0;
                }
            }
            else
            {
                isSoundsMuted = false;
                foreach (AudioSource source in audioSources)
                {
                    source.volume = defaultVolume;
                }
            }
        }
        
        public void PlaySound(SoundsEnum sound, float volume = 1.0f)
        {
            AudioClip clipToPlay = FindLCip(sound);
            if (clipToPlay != null)
            {
                AudioSource foundSource = FindSource();
                foundSource.clip = clipToPlay;
                foundSource.volume = volume;
                FindSource().Play();
            }
        }

        public void Test()
        {
            PlaySound(SoundsEnum.Clic1);
        }

        private AudioClip FindLCip(SoundsEnum sound)
        {
            foreach (SoundClipData soundClipData in libraryData.clipsList)
            {
                if (soundClipData.soundTag == sound)
                {
                    return soundClipData.clip;
                }
            }
            return null;
        }

        private AudioSource FindSource()
        {
            if (audioSources.Count == 0)
            {
                return CreateNewAudioSource().GetComponent<AudioSource>();
            }
            foreach (AudioSource audioSource in audioSources)
            {
                if (audioSource.isPlaying == false)
                {
                    return audioSource;
                }
            }
            return CreateNewAudioSource().GetComponent<AudioSource>();
        }

        private GameObject CreateNewAudioSource()
        {
            GameObject newAudioSource = Instantiate(sourceOriginal, Vector3.zero, Quaternion.identity);
            newAudioSource.name = "AudioSource" + (audioSources.Count).ToString();
            audioSources.Add(newAudioSource.GetComponent<AudioSource>());
            newAudioSource.transform.parent = transform;
            if (isSoundsMuted == false)
            {
                newAudioSource.GetComponent<AudioSource>().volume = defaultVolume;
            }
            else
            {
                newAudioSource.GetComponent<AudioSource>().volume = 0;
            }
            audioSources.Add(newAudioSource.GetComponent<AudioSource>());
            return newAudioSource;
        }
    }
}