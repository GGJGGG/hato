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

    public int LowFreq  = 150;
    public int HighFreq = 800;
    public float ThresholdVolume = 1;

    [SerializeField] Text debugText;
    [SerializeField] InputPowerView view;
    [SerializeField] Player player;

    AudioSource audioSource;

    void Start()
    {
        StartCoroutine(InputStart());
    }

    // https://qiita.com/niusounds/items/b8858a2b043676185a54
    IEnumerator InputStart()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = Microphone.Start(null, true, 10, 44100);  // マイク名、ループするかどうか、AudioClipの秒数、サンプリングレート を指定する
        audioSource.loop = true;
        while (!(Microphone.GetPosition("") > 0)){ yield return null; }             // マイクが取れるまで待つ。空文字でデフォルトのマイクを探してくれる
        audioSource.Play();                                           // 再生する

        const int windowSize = 256; // 解像度高くしたかったので256から1024に変更
        float[] spectrum = new float[windowSize];

        float prevVolume = 0;
        var prevFreq = 0;

        while (true)
        {
            audioSource.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);

            var maxIndex = 0;
            var maxValue = 0.0f;
            for (int i = 0; i < spectrum.Length; i++)
            {
                var val = spectrum[i];
                if (val > maxValue)
                {
                    maxValue = val;
                    maxIndex = i;
                }
            }

            var freq = maxIndex * AudioSettings.outputSampleRate / 2 / spectrum.Length;
            maxValue = maxValue / audioSource.volume < 0.0513f ? 0 : maxValue; // 無音のときにも0.0512が入る
            var volume = maxValue / audioSource.volume;
            volume = Mathf.Lerp(prevVolume, volume, 0.5f);
            freq = (int)Mathf.Lerp(prevFreq, freq, 0.5f);

            debugText.text = string.Format("周波数{0} volume{1:###0.0000}", freq, volume);

            var rate = (volume / audioSource.volume) < ThresholdVolume ? 0 : Mathf.InverseLerp(LowFreq, HighFreq, freq); // 小さい音を無視

            //var rate = (volume / audioSource.volume) < 1 ? 0 : Mathf.InverseLerp(150, 2000, freq); // 小さい音を無視
            //var rate = Mathf.InverseLerp(150, 2000, freq); // 小さい音を無視
            view.SetPower(rate);
            player.Boost(rate);

            prevVolume = volume;
            prevFreq = freq;

            yield return null;
        }
    }
}
