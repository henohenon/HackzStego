using System.Collections;
using System.Collections.Generic;
using R3;
using UnityEngine;

public class ScoringManager : MonoBehaviour
{
    private ReactiveProperty<ushort> _brightnessLevel = new ReactiveProperty<ushort>(0);
    private ReactiveProperty<ushort> _temperatureLevel = new ReactiveProperty<ushort>(0);
    private ReactiveProperty<ushort> _volumeLevel = new ReactiveProperty<ushort>(0);
    public ReadOnlyReactiveProperty<ushort> BrightnessLevel => _brightnessLevel;
    public ReadOnlyReactiveProperty<ushort> TemperatureLevel => _temperatureLevel;
    public ReadOnlyReactiveProperty<ushort> VolumeLevel => _volumeLevel;
    
    
    [SerializeField]
    LightEstimate lightEstimate;
    [SerializeField]
    SoundManager soundManager;

    // Update is called once per frame
    void Update()
    {
        _brightnessLevel.OnNext(CalcBrightnessLevel(lightEstimate.Brightness));
        _temperatureLevel.OnNext(CalcTemperatureLevel(lightEstimate.ColorTemperature));
        Debug.Log(soundManager.VolumeRate);
        _volumeLevel.OnNext(CalcVolume(soundManager.VolumeRate));
    }

    public float GetAvgScore()
    {
        return ((float)(_brightnessLevel.Value + _temperatureLevel.Value + _volumeLevel.Value)) / 3;
    }
    
    // 明るさと色温度のレベルを計算するメソッド
    private ushort CalcBrightnessLevel(float brightness)
    {
        // Brightnessのレベルを5段階に分ける
        if (brightness <= 0.2f)
        {
            return 1;
        }
        else if (brightness <= 0.4f)
        {
            return 2;
        }
        else if (brightness <= 0.6f)
        {
            return 3;
        }
        else if (brightness <= 0.8f)
        {
            return 4;
        }
        else
        {
            return 5;
        }
    }

    private ushort CalcTemperatureLevel(float temperature)
    {
        
        // ColorTemperatureのレベルを5段階に分ける
        if (temperature <= 2500f)
        {
            return 1;
        }
        else if (temperature <= 4500f)
        {
            return 2;
        }
        else if (temperature <= 6500f)
        {
            return 3;
        }
        else if (temperature <= 8500f)
        {
            return 4;
        }
        else
        {
            return 5;
        }
    }
    
    private ushort CalcVolume(float volumeRate)
    {
        // ColorTemperatureのレベルを5段階に分ける
        if (volumeRate >= 0.9f)
        {
            return 1;
        }
        else if (volumeRate >= 0.6f)
        {
            return 2;
        }
        else if (volumeRate >= 0.3f)
        {
            return 3;
        }
        else if (volumeRate >= 0.1f)
        {
            return 4;
        }
        else
        {
            return 5;
        }
    }
}
