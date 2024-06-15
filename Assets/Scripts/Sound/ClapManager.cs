using R3;
using UnityEngine;

public class ClapManager : MonoBehaviour
{
    private ReactiveProperty<bool> isCrapping = new ReactiveProperty<bool>(false);
    public ReadOnlyReactiveProperty<bool> IsCrapping => isCrapping;
    private AudioSource audioSource;
    private int sampleWindow = 1024;
    private float[] spectrumData;

    [SerializeField]
    private int lowFrequency = 10000;  // 低い周波数範囲
    [SerializeField]
    private int highFrequency = 21000; // 高い周波数範囲
    [SerializeField]
    private float amplitudeThreshold = 0.003f; // 振幅の閾値

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        spectrumData = new float[sampleWindow];

        if (Microphone.devices.Length > 0)
        {
            string microphone = Microphone.devices[0];
            audioSource.clip = Microphone.Start(microphone, true, 10, 44100);
            audioSource.loop = true;
            while (!(Microphone.GetPosition(microphone) > 0)) { } // マイクが準備できるまで待機
            audioSource.Play();
        }
    }

    void Update()
    {
        if (audioSource.isPlaying)
        {
            audioSource.GetSpectrumData(spectrumData, 0, FFTWindow.BlackmanHarris);
            if (IsClapDetected())
            {
                isCrapping.Value = true;
                Debug.Log("isCrapped");
            }
        }
    }

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