using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    
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
            float volume = GetAverageVolume();
            text.text = volume.ToString();
            Debug.Log("Current Volume: " + volume);
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
