using UnityEngine;

public class CrapSoundManager : MonoBehaviour
{ 
    private AudioClip microphoneClip;
    private string microphone;
    private int sampleWindow = 1024;
    private float[] sampleData;
    private float[] spectrumData;
    private float previousVolume = 0f;
    private float clapThreshold = 0.2f; // 拍手を検知するための閾値
    private float frequencyThreshold = 0.00001f; // 周波数成分の閾値
    private int lowFrequency = 1000; // 低い周波数範囲
    private int highFrequency = 1500; // 高い周波数範囲

    void Start()
    {
        if (Microphone.devices.Length > 0)
        {
            microphone = Microphone.devices[0];
            microphoneClip = Microphone.Start(microphone, true, 10, 44100);
            sampleData = new float[sampleWindow];
            spectrumData = new float[sampleWindow];
        }
    }

    void Update()
    {
        if (Microphone.IsRecording(microphone))
        {
            float currentVolume = GetCurrentVolume();
            float dominantFrequencyAmplitude = GetDominantFrequencyAmplitude();
            Debug.Log(dominantFrequencyAmplitude);

            if (dominantFrequencyAmplitude != 0)
            {
                Debug.Log("Clap detected!");
                // 拍手が検知されたときの処理をここに追加
            }

            previousVolume = currentVolume;
        }
    }

    float GetCurrentVolume()
    {
        int position = Microphone.GetPosition(microphone) - sampleWindow + 1;
        if (position < 0) return 0;

        microphoneClip.GetData(sampleData, position);
        float sum = 0;
        for (int i = 0; i < sampleWindow; i++)
        {
            sum += sampleData[i] * sampleData[i];
        }
        return Mathf.Sqrt(sum / sampleWindow);
    }
    float GetDominantFrequencyAmplitude()
    {
        int position = Microphone.GetPosition(microphone) - sampleWindow + 1;
        if (position < 0) return 0;

        microphoneClip.GetData(sampleData, position);
        AudioListener.GetSpectrumData(spectrumData, 0, FFTWindow.BlackmanHarris);

        int lowIndex = Mathf.FloorToInt(lowFrequency * sampleWindow / 44100);
        int highIndex = Mathf.CeilToInt(highFrequency * sampleWindow / 44100);

        for (int i = lowIndex; i <= highIndex; i++)
        {
            Debug.Log(spectrumData[i]);
            if (spectrumData[i] > frequencyThreshold)
            {
                return spectrumData[i]; // 閾値を超えた振幅を返す
            }
        }

        return 0f; // 閾値を超える振幅がない場合
    }
}