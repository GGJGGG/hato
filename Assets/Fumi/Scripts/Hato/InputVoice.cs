using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputVoice : SingletonMonoBehaviour<InputVoice>
{
    public class NoteNameDetector
    {
        private string[] noteNames = { "ド", "ド♯", "レ", "レ♯", "ミ", "ファ", "ファ♯", "ソ", "ソ♯", "ラ", "ラ♯", "シ" };

        public string GetNoteName(float freq)
        {
            if (freq == 0)
                return "";

            // 周波数からMIDIノートナンバーを計算
            var noteNumber = calculateNoteNumberFromFrequency(freq);
            // 0:C - 11:B に収める
            var note = noteNumber % 12;
            // 0:C～11:Bに該当する音名を返す
            return noteNames[note];
        }

        // See https://en.wikipedia.org/wiki/MIDI_tuning_standard
        private int calculateNoteNumberFromFrequency(float freq)
        {
            return Mathf.FloorToInt(69 + 12 * Mathf.Log(freq / 440, 2));
        }
    }

    public int CenterFreq = 300;
    public int FreqBandNumber = 13; // Freqの幅は13音分
    int LowFreq  = 150;
    int HighFreq = 800;

    public float ThresholdVolume = 1;
    public float PrevEffectRate = 0;

    [SerializeField] Text debugText;

    /// <summary>
    /// Occurs when on voice input update.
    /// 入力があったかどうか 入力音程の相対的な高さ 周波数 音の強さ
    /// </summary>
    public event Action<bool, float, int, float> OnUpdateVoiceInput;

    public event Action<int, int, int> OnUpdateCenterFreq;

    AudioSource audioSource;

    void Start()
    {
        SetCenterFreq(CenterFreq);
        StartCoroutine(InputStart());
    }

    // https://qiita.com/niusounds/items/b8858a2b043676185a54
    // 基音をとるのはこのへん
    // http://ibako-study.hateblo.jp/entry/2014/02/06/031945
    IEnumerator InputStart()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = Microphone.Start(null, true, 10, 44100);  // マイク名、ループするかどうか、AudioClipの秒数、サンプリングレート を指定する
        audioSource.loop = true;
        while (!(Microphone.GetPosition("") > 0)){ yield return null; }             // マイクが取れるまで待つ。空文字でデフォルトのマイクを探してくれる
        audioSource.Play();                                           // 再生する

        const int windowSize = 1024; // 解像度高くしたかったので256から1024に変更
        float[] spectrum = new float[windowSize];

        float prevVolume = 0;
        var prevFreq = 0;
        float threshold = 0.04f * audioSource.volume; //ピッチとして検出する最小の分布

        while (true)
        {
            audioSource.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);

            var maxIndex = 0;
            var maxValue = 0.0f;
            for (int i = 0; i < spectrum.Length; i++)
            {
                var val = spectrum[i];
                if (val > maxValue && val > threshold)
                {
                    maxValue = val;
                    maxIndex = i;
                }
            }

            float freqN = maxIndex;
            if (maxIndex > 0 && maxIndex < spectrum.Length - 1)
            {
                //隣のスペクトルも考慮する
                float dL = spectrum[maxIndex - 1] / spectrum[maxIndex];
                float dR = spectrum[maxIndex + 1] / spectrum[maxIndex];
                freqN += 0.5f * (dR * dR - dL * dL);
            }

            var freq = (int)(freqN * AudioSettings.outputSampleRate / 2 / spectrum.Length);
            maxValue = maxValue / audioSource.volume < 0.0650f ? 0 : maxValue; // 無音のときにもmacで0.0512が入る winで650くらいが安定してた
            var volume = maxValue / audioSource.volume;
            volume = Mathf.Lerp(volume, prevVolume, PrevEffectRate);
            freq = (int)Mathf.Lerp(freq, prevFreq, PrevEffectRate);

            if (debugText != null)
            {
                debugText.text = string.Format("周波数{0} volume{1:###0.0000}", freq, volume);
            }

            var isVoiceMute = (volume / audioSource.volume) < ThresholdVolume;
            var rate = isVoiceMute ? 0 : Mathf.InverseLerp(LowFreq, HighFreq, freq); // 小さい音を無視

            if (OnUpdateVoiceInput != null)
            {
                OnUpdateVoiceInput(isVoiceMute, rate, freq, volume);
            }

            prevVolume = volume;
            prevFreq = freq;

            yield return null;
        }
    }

    /// <summary>
    /// 判定の中央に使う周波数を登録します。
    /// </summary>
    public void SetCenterFreq(int centerFreq)
    {
        var rate = Mathf.Pow(1.059f, (FreqBandNumber - 1) / 2.0f);

        LowFreq = (int)(centerFreq / rate);
        HighFreq = (int)(centerFreq * rate);

        if (OnUpdateCenterFreq != null)
        {
            OnUpdateCenterFreq(LowFreq, centerFreq, HighFreq);
        }
    }
}
