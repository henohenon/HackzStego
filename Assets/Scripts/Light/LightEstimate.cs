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
    }

    private void OnDisable()
    {
        cameraManager.frameReceived -= OnCameraFrameReceived;
    }
}
