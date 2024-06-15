using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public float VolumeRate { get; private set; }
    
    private AudioClip microphoneClip;
    private string microphone;
    private int sampleWindow = 128; // 音量を測定するサンプルウィンドウサイズ

    void Start()
    {
        if (Microphone.devices.Length > 0)
        {
            microphone = Microphone.devices[0];
            microphoneClip = Microphone.Start(microphone, true, 10, 44100);
        }
    }

    void Update()
    {
        if (Microphone.IsRecording(microphone))
        {
            VolumeRate = GetAverageVolume();
        }
    }

    float GetAverageVolume()
    {
        float[] data = new float[sampleWindow];
        int position = Microphone.GetPosition(microphone) - sampleWindow + 1;
        if (position < 0) return 0;

        microphoneClip.GetData(data, position);
        float sum = 0;
        for (int i = 0; i < sampleWindow; i++)
        {
            sum += data[i] * data[i];
        }
        return Mathf.Sqrt(sum / sampleWindow);
    }
}
