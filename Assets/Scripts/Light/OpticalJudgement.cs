using UnityEngine;
using TMPro;

public class OpticalJudgement : MonoBehaviour
{
    public float Brightness { get; private set; }
    public float ColorTemperature { get; private set; }
    public int BrightnessLevel { get; private set; }
    public int ColorTemperatureLevel { get; private set; }

    [SerializeField] private TextMeshProUGUI brightnessLevelText; // 明るさレベルを表示するTextMeshPro
    [SerializeField] private TextMeshProUGUI colorTemperatureLevelText; // 色温度レベルを表示するTextMeshPro

    // 光の推定値をセットするメソッド
    public void SetLightEstimation(float brightness, float colorTemperature)
    {
        Brightness = brightness;
        ColorTemperature = colorTemperature;
        CalculateLevels();
        UpdateUIText();
    }

    // 明るさと色温度のレベルを計算するメソッド
    private void CalculateLevels()
    {
        // Brightnessのレベルを5段階に分ける
        if (Brightness <= 0.2f)
        {
            BrightnessLevel = 1;
        }
        else if (Brightness <= 0.4f)
        {
            BrightnessLevel = 2;
        }
        else if (Brightness <= 0.6f)
        {
            BrightnessLevel = 3;
        }
        else if (Brightness <= 0.8f)
        {
            BrightnessLevel = 4;
        }
        else
        {
            BrightnessLevel = 5;
        }

        // ColorTemperatureのレベルを5段階に分ける
        if (ColorTemperature <= 2500f)
        {
            ColorTemperatureLevel = 1;
        }
        else if (ColorTemperature <= 4500f)
        {
            ColorTemperatureLevel = 2;
        }
        else if (ColorTemperature <= 6500f)
        {
            ColorTemperatureLevel = 3;
        }
        else if (ColorTemperature <= 8500f)
        {
            ColorTemperatureLevel = 4;
        }
        else
        {
            ColorTemperatureLevel = 5;
        }
    }

    // UIにテキストを更新するメソッド
    private void UpdateUIText()
    {
        if (brightnessLevelText != null)
        {
            brightnessLevelText.text = "Brightness Level: " + BrightnessLevel.ToString();
        }

        if (colorTemperatureLevelText != null)
        {
            colorTemperatureLevelText.text = "Color Temperature Level: " + ColorTemperatureLevel.ToString();
        }
    }
}
