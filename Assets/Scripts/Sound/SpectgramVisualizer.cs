using UnityEngine;

public class SpectrogramVisualizer : MonoBehaviour
{
    [SerializeField]
    private int lowFrequency = 2000;  // 低い周波数範囲
    [SerializeField]
    private int highFrequency = 5000; // 高い周波数範囲
    [SerializeField]
    private float amplitudeThreshold = 0.1f; // 振幅の閾値

    
    private AudioSource audioSource;
    private int sampleWindow = 1024;
    private float[] spectrumData;

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
        }
    }

    void OnDrawGizmos()
    {
        if (spectrumData == null) return;

        Gizmos.color = Color.green;
        int lowIndex = Mathf.FloorToInt(lowFrequency * sampleWindow / 44100);
        int highIndex = Mathf.CeilToInt(highFrequency * sampleWindow / 44100);

        float oldy = 0;
        float xRate = 0.01f;
        for (int i = 0; i < spectrumData.Length; i++)
        {
            float y = spectrumData[i] * 1000; // 縦軸の振幅をスケールアップ
            
            if (i >= lowIndex && i <= highIndex)
            {
                Gizmos.color = Color.red; // 指定した音域を赤色で表示
            }
            else
            {
                Gizmos.color = Color.green; // その他の音域を緑色で表示
            }
            Gizmos.DrawLine(new Vector3((i - 1) * xRate, oldy, 0), new Vector3(i * xRate, y, 0));
            oldy = y;
        }
        // 振幅の閾値を示す水平線を描画
        Gizmos.color = Color.blue;
        float thresholdY = amplitudeThreshold * 1000;
        Gizmos.DrawLine(new Vector3(0, thresholdY, 0), new Vector3(spectrumData.Length * xRate, thresholdY, 0));

    }
}