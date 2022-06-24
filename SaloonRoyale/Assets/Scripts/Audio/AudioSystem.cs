using UnityEngine;

namespace Audio
{
    public class AudioSystem : MonoBehaviour
    {
        [SerializeField] private AudioSource soundtrackPlayer;
        [SerializeField] private AudioSource sfxPlayer;

        public void PlaySoundtrack(AudioClip clip)
        {
            soundtrackPlayer.clip = clip;
            soundtrackPlayer.Play();
        }

        public void PlaySfx(AudioClip clip)
        {
            sfxPlayer.clip = clip;
            sfxPlayer.PlayOneShot(clip);
        }
    }
}
