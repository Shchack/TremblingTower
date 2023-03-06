using UnityEngine;
using UnityEngine.Audio;

namespace EG.Tower.Audio
{
    public class BackgroundAudioTrack : MonoBehaviour, IBackgroundAudioTrack
    {
        [SerializeField] private AudioSource _audioSource;

        private AudioTrackItem _track;

        private void Start()
        {
            SetupAudioSource();
        }

        private void SetupAudioSource()
        {
            if (_audioSource == null)
            {
                _audioSource = gameObject.AddComponent<AudioSource>();
            }
        }

        public void Init(AudioTrackItem track, AudioMixerGroup mixer)
        {
            _track = track;
            _audioSource.outputAudioMixerGroup = mixer;
            _audioSource.clip = _track.Clip;
            _audioSource.spatialBlend = 0.5f;
            _audioSource.volume = 0f;
            name = track.Clip != null ? track.Clip.name : "No_Clip";
        }

        public virtual void Play()
        {
            if (_track == null)
            {
                Debug.LogError("Can't play audio track. Track is empty!", this);
                return;
            }

            StopAllCoroutines();
            _audioSource.Play();

            StartCoroutine(_audioSource.Fade(_audioSource.volume, _track.Volume, _track.FadeInDuration));
        }

        public virtual void Stop(bool immediately = false)
        {
            StopAllCoroutines();

            if (_audioSource == null || !_audioSource.isPlaying)
            {
                return;
            }

            if (immediately)
            {
                _audioSource.Stop();
            }
            else
            {
                StartCoroutine(_audioSource.FadeStop(_track.FadeOutDuration));
            }
        }
    }
}
