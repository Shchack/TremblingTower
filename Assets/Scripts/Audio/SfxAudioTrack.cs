using UnityEngine;
using UnityEngine.Audio;

namespace EG.Tower.Audio
{
    public class SfxAudioTrack : MonoBehaviour, ISfxAudioTrack
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
            _audioSource.loop = true;
            _audioSource.outputAudioMixerGroup = mixer;
            _audioSource.clip = _track.Clip;
            _audioSource.volume = _track.Volume;
            name = track.Clip != null ? track.Clip.name : "No_Clip";
        }

        public void PlayContiniousSound()
        {
            _audioSource.volume = _track.Volume;

            if (!_audioSource.isPlaying)
            {
                _audioSource.Play();
            }
        }

        public void StopContiniousSound()
        {
            if (_audioSource.isPlaying)
            {
                StartCoroutine(_audioSource.Fade(_audioSource.volume, 0f, 0.5f));
            }
        }

        public void PlayOneShot()
        {
            _audioSource.PlayOneShot(_track.Clip, _track.Volume);
        }
    }
}
