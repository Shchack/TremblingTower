using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace EG.Tower.Audio
{
    public class NullAudioSystem : IAudioSystem
    {
        private IBackgroundAudioTrack _backTrackStub;
        private ISfxAudioTrack _sfxTrackStub;

        public NullAudioSystem()
        {
            _backTrackStub = new NullBackgroundAudioTrack();
            _sfxTrackStub = new NullSfxAudioTrack();
        }

        public Dictionary<AudioMixerGroupType, float> InitialVolumes { get; private set; }

        public IBackgroundAudioTrack MusicTrack => NotifyNullBackAudioPlay();

        public IBackgroundAudioTrack AmbienceTrack => NotifyNullBackAudioPlay();

        public ISfxAudioTrack CheckResultTrack => NotifyNullSfxAudioPlay();

        public ISfxAudioTrack ButtonClickTrack => NotifyNullSfxAudioPlay();

        public void SaveMixerSettingsToPlayerPrefs(AudioMixerGroupType type, float volume)
        {
        }

        private IBackgroundAudioTrack NotifyNullBackAudioPlay()
        {
            Debug.LogWarning("Audio track not found. Using null stub!");

            return _backTrackStub;
        }

        private ISfxAudioTrack NotifyNullSfxAudioPlay()
        {
            Debug.LogWarning("Audio track not found. Using null stub!");

            return _sfxTrackStub;
        }
    }

    public class NullBackgroundAudioTrack : IBackgroundAudioTrack
    {
        public void Init(AudioTrackItem track, AudioMixerGroup mixer)
        {
        }

        public void Play()
        {
        }

        public void Stop(bool immediately = false)
        {
        }
    }

    public class NullSfxAudioTrack : ISfxAudioTrack
    {
        public void Init(AudioTrackItem track, AudioMixerGroup mixer)
        {
        }

        public void PlayContiniousSound()
        {
        }

        public void StopContiniousSound()
        {
        }

        public void PlayOneShot()
        {
        }
    }
}
