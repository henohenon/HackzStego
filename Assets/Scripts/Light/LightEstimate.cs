using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.XR.ARFoundation;

public class LightEstimate : MonoBehaviour
{
    [SerializeField] ARCameraManager cameraManager;
    private Light light;

    [SerializeField] private TextMeshProUGUI brightnessText;
    [SerializeField] private TextMeshProUGUI colorTemperatureText;
    [SerializeField] private TextMeshProUGUI brightnessLevelText; // 明るさレベルを表示するTextMeshPro
    [SerializeField] private TextMeshProUGUI colorTemperatureLevelText; // 色温度レベルを表示するTextMeshPro

    public float Brightness { get; private set; }
    public float ColorTemperature { get; private set; }
    public int BrightnessLevel { get; private set; }
    public int ColorTemperatureLevel { get; private set; }

    void Start()
    {
        this.light = GetComponent<Light>();
        cameraManager.frameReceived += OnCameraFrameReceived;
    }

    void OnCameraFrameReceived(ARCameraFrameEventArgs e)
    {
        Color color = Color.white;
        float intensity = 1.0f;
        float? brightness = null;
        float? colorTemperature = null;

        if (e.lightEstimation.averageBrightness.HasValue)
        {
            brightness = e.lightEstimation.averageBrightness.Value;
            intensity = brightness.Value * 2.0f;
            if (intensity > 1) intensity = 1.0f;
        }
        if (e.lightEstimation.averageColorTemperature.HasValue)
        {
            colorTemperature = e.lightEstimation.averageColorTemperature.Value;
            color = Mathf.CorrelatedColorTemperatureToRGB(colorTemperature.Value);
        }

        Color c = color * intensity;
        light.color = c;
        RenderSettings.ambientSkyColor = c;

        // TextMeshProに値を設定
        if (brightnessText != null && brightness.HasValue)
        {
            brightnessText.text = "Brightness: " + brightness.Value.ToString("#.000");
        }

        if (colorTemperatureText != null && colorTemperature.HasValue)
        {
            colorTemperatureText.text = "Color Temperature: " + colorTemperature.Value.ToString("#.000");
        }

        // 光の推定値をセット
        if (brightness.HasValue && colorTemperature.HasValue)
        {
            SetLightEstimation(brightness.Value, colorTemperature.Value);
        }
    }

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

    private void OnDisable()
    {
        cameraManager.frameReceived -= OnCameraFrameReceived;
    }
}
