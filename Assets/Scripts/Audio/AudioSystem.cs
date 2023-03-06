using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

namespace EG.Tower.Audio
{
    public class AudioSystem : MonoBehaviour, IAudioSystem
    {
        public Dictionary<AudioMixerGroupType, float> InitialVolumes { get; private set; }

        [SerializeField] private AudioTracksData _tracks;
        [SerializeField] private BackgroundAudioTrack _backgroundTrackPrefab;
        [SerializeField] private SfxAudioTrack _sfxPrefab;
        [SerializeField] private AudioMixer _masterMixer;
        [SerializeField] private AudioMixerConfig[] _mixerConfigs;

        private Dictionary<AudioMixerGroupType, AudioMixerGroup> _mixers;

        public IBackgroundAudioTrack MusicTrack { get; private set; }
        public IBackgroundAudioTrack AmbienceTrack { get; private set; }

        public ISfxAudioTrack CheckResultTrack { get; private set; }
        public ISfxAudioTrack ButtonClickTrack { get; private set; }

        private void Awake()
        {
            _mixers = _mixerConfigs.ToDictionary(m => m.Type, m => m.Group);
            LoadInitialVolumes();

            MusicTrack = CreateBackgroundTrack(_tracks.Music, AudioMixerGroupType.Music);
            AmbienceTrack = CreateBackgroundTrack(_tracks.Ambience, AudioMixerGroupType.Music);
            CheckResultTrack = CreateSfxTrack(_tracks.CheckResult, AudioMixerGroupType.Sfx);
            ButtonClickTrack = CreateSfxTrack(_tracks.ButtonClick, AudioMixerGroupType.Sfx);
        }

        private void LoadInitialVolumes()
        {
            InitialVolumes = new Dictionary<AudioMixerGroupType, float>();

            foreach (var mixer in _mixerConfigs)
            {
                var paramName = GetVolumeParamName(mixer.Type);
                float volume = PlayerPrefs.GetFloat(paramName, 1f);
                InitialVolumes.Add(mixer.Type, volume);
            }
        }

        private void Start()
        {
            if (_tracks == null)
            {
                Debug.LogError("Tracks data is empty!", this);
            }

            InitMixerSettings();
        }

        private IBackgroundAudioTrack CreateBackgroundTrack(AudioTrackItem trackItem, AudioMixerGroupType mixerType)
        {
            var found = _mixers.TryGetValue(mixerType, out var mixerGroup);
            var track = Instantiate(_backgroundTrackPrefab, transform);
            track.Init(trackItem, mixerGroup);

            return track;
        }

        private ISfxAudioTrack CreateSfxTrack(AudioTrackItem trackItem, AudioMixerGroupType mixerType)
        {
            var found = _mixers.TryGetValue(mixerType, out var mixerGroup);
            var track = Instantiate(_sfxPrefab, transform);
            track.Init(trackItem, mixerGroup);

            return track;
        }

        public void SaveMixerSettingsToPlayerPrefs(AudioMixerGroupType type, float volume)
        {
            if (_mixers.TryGetValue(type, out var group))
            {
                string paramName = GetVolumeParamName(type);
                if (_masterMixer.SetFloat(paramName, LinearToDecibel(volume)))
                {
                    PlayerPrefs.SetFloat(GetVolumeParamName(type), volume);
                    PlayerPrefs.Save();
                }
            }
        }

        private void InitMixerSettings()
        {
            foreach (var mixerVolume in InitialVolumes)
            {
                var paramName = GetVolumeParamName(mixerVolume.Key);
                float volume = PlayerPrefs.GetFloat(paramName, 0.8f);
                if (!_masterMixer.SetFloat(paramName, LinearToDecibel(volume)))
                {
                    Debug.LogWarning($"Mixer volume not set. Param: {paramName}");
                }
            }
        }

        private float LinearToDecibel(float linear)
        {
            float db = linear != 0f ? (20.0f * Mathf.Log10(linear)) : -144.0f;

            return db;
        }

        private float DecibelToLinear(float db)
        {
            float linear = Mathf.Pow(10.0f, db / 20.0f);

            return linear;
        }

        private string GetVolumeParamName(AudioMixerGroupType type) => $"{type}Volume";
    }

    [Serializable]
    public class AudioMixerConfig
    {
        public AudioMixerGroupType Type;
        public AudioMixerGroup Group;
    }
}
