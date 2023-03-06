using UnityEngine;

namespace EG.Tower.Audio
{
    [CreateAssetMenu(fileName = "AudioTracksData", menuName = "Data/Audio/Tracks", order = 0)]
    public class AudioTracksData : ScriptableObject
    {
        [Header("Music")]
        public AudioTrackItem Music;
        public AudioTrackItem Ambience;

        [Header("SFX")]
        public AudioTrackItem CheckResult;

        [Header("UI")]
        public AudioTrackItem ButtonClick;
    }
}
