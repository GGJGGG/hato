using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioSourceExtension
{
    public static IEnumerator Fade(this AudioSource audio, float to, float duration)
    {
        var time = 0.0f;

        var from = audio.volume;

        if (from == 0)
        {
            audio.Play();
        }

        while(true)
        {
            time += Time.deltaTime;
            var t = Mathf.Min(1, time / duration);
            var volume = Mathf.Lerp(from, to, t);
            audio.volume = volume;
            if (t == 1.0f)
            {
                yield break;
            }
            yield return null;
        }

        if (audio.volume == 0)
        {
            audio.Stop();
        }
    }
}
