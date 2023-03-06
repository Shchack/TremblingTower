using UnityEngine.Audio;

namespace EG.Tower.Audio
{
    public interface ISfxAudioTrack
    {
        public void Init(AudioTrackItem track, AudioMixerGroup mixer);

        public void PlayContiniousSound();

        public void StopContiniousSound();

        public void PlayOneShot();
    }
}