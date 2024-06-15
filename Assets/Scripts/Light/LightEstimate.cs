using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(Light))]
public class LightEstimate : MonoBehaviour
{
    [SerializeField] ARCameraManager cameraManager;
    private Light light;

   
    public float Brightness { get; private set; }
    public float ColorTemperature { get; private set; }

    void Start()
    {
        light = GetComponent<Light>();
        cameraManager.frameReceived += OnCameraFrameReceived;
    }

    private void OnCameraFrameReceived(ARCameraFrameEventArgs e)
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
        
        Brightness = brightness ?? 0;
        ColorTemperature = colorTemperature ?? 0;
    }

    private void OnDisable()
    {
        cameraManager.frameReceived -= OnCameraFrameReceived;
    }
}
