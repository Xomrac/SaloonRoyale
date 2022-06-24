using UnityEngine;

namespace Audio
{
    [CreateAssetMenu(fileName="clip_",menuName="Audio System/Sounds/Sound")]
    public class AudioTrack : ScriptableObject
    {
        [SerializeField] private AudioClip audioClip;

        public AudioClip GetClip()
        {
            return audioClip;
        }
    }
}
