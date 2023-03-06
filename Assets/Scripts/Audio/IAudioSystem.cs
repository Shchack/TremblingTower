using System.Collections.Generic;

namespace EG.Tower.Audio
{
    public interface IAudioSystem
    {
        public Dictionary<AudioMixerGroupType, float> InitialVolumes { get; }

        public IBackgroundAudioTrack MusicTrack { get; }

        public IBackgroundAudioTrack AmbienceTrack { get; }

        public ISfxAudioTrack CheckResultTrack { get; }

        public ISfxAudioTrack ButtonClickTrack { get; }

        public void SaveMixerSettingsToPlayerPrefs(AudioMixerGroupType type, float volume);
    }
}
