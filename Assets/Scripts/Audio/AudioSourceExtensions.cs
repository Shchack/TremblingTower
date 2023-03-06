using System.Collections;
using UnityEngine;

namespace EG.Tower.Audio
{
    public static class AudioSourceExtensions
    {
        private const float STOP_PLAY_VOLUME = 0f;

        public static IEnumerator FadeStop(this AudioSource source, float fadeTime)
        {
            yield return Fade(source, source.volume, STOP_PLAY_VOLUME, fadeTime);
        }

        public static IEnumerator Fade(this AudioSource source, float fromVolume, float toVolume, float fadeTime)
        {
            if (fadeTime != 0)
            {
                float timer = 0;
                while (timer < fadeTime)
                {
                    timer += Time.fixedDeltaTime;

                    source.volume = Mathf.Lerp(fromVolume, toVolume, timer / fadeTime);
                    yield return null;
                }
            }

            if (toVolume == 0)
            {
                source.Stop();
            }
        }
    }
}
