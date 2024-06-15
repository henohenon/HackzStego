using System.Collections;
using System.Collections.Generic;
using R3;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    private ReactiveProperty<bool> isCrapping = new ReactiveProperty<bool>(false);
    public ReadOnlyReactiveProperty<bool> IsCrapping => isCrapping;
    public float VolumeRate { get; private set; }
    
    private AudioClip microphoneClip;
    private string microphone;
    private const int sampleWindow = 128; // 音量を測定するサンプルウィンドウサイズ
    
    private AudioSource audioSource;
    
    [SerializeField]
    private int lowFrequency = 10000;  // 低い周波数範囲
    [SerializeField]
    private int highFrequency = 21000; // 高い周波数範囲
    [SerializeField]
    private float amplitudeThreshold = 0.003f; // 振幅の閾値


    void Start()
    {
        audioSource = GetComponent<AudioSource>();        
        if (Microphone.devices.Length > 0)
        {
            microphone = Microphone.devices[0];
            microphoneClip = Microphone.Start(microphone, true, 10, 44100);
            audioSource.clip = microphoneClip;
            audioSource.loop = true;
            audioSource.Play();
        }

    }

    void Update()
    {
        VolumeRate = GetAverageVolume();
        
        audioSource.GetSpectrumData(spectrumData, 0, FFTWindow.BlackmanHarris);
        if (IsClapDetected())
        {
            isCrapping.Value = true;
            Debug.Log("isCrapped");
        }
    }

    float [] sampleData = new float[sampleWindow];
    float GetAverageVolume()
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
    

    float[] spectrumData = new float[sampleWindow];
    bool IsClapDetected()
    {
        int lowIndex = Mathf.FloorToInt(lowFrequency * sampleWindow / 44100);
        int highIndex = Mathf.CeilToInt(highFrequency * sampleWindow / 44100);

        for (int i = lowIndex; i <= highIndex; i++)
        {
            if (spectrumData[i] > amplitudeThreshold)
            {
                return true;
            }
        }
        return false;
    }
}
