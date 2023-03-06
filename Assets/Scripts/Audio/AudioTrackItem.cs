using System;
using UnityEngine;

namespace EG.Tower.Audio
{
    [Serializable]
    public class AudioTrackItem
    {
        public AudioClip Clip;
        public float Volume = 1f;
        public float FadeInDuration = 3f;
        public float FadeOutDuration = 2f;
    }
}
