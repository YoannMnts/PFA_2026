using UnityEngine;

namespace Naussilus.Gameplay
{
    public class MusicsManager : MonoBehaviour
    {
        [SerializeField] private AudioSource musicAudioSource;
        private AudioClip musicClip;

        public void PlayMusic(AudioClip musicToPlay, float volume = 1.0f)
        {
            musicAudioSource.volume = volume;
            musicAudioSource.clip = musicToPlay;
            musicAudioSource.Play();
        }

        public void PauseMusic()
        {
            musicAudioSource.Pause();
        }

        public void MuteMusic(bool mute)
        {
            if (mute)
            {
                musicAudioSource.mute = true;
            }
            else
            {
                musicAudioSource.mute = false;
            }
        }
    }
}
