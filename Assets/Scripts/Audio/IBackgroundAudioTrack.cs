using UnityEngine.Audio;

namespace EG.Tower.Audio
{
    public interface IBackgroundAudioTrack
    {
        public void Init(AudioTrackItem track, AudioMixerGroup mixer);

        public void Play();

        public void Stop(bool immediately = false);
    }
}
