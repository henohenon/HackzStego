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

    public ushort GetAvgScore()
    {
        var sum = _brightnessLevel.Value + _temperatureLevel.Value + _volumeLevel.Value;

        if (sum >= 7)
        {
            return 3;
        }
        else if (sum <= 6 && sum >= 4)
        {
            return 2;
        }
        else
        {
            return 1;
        }
    }
    
    // 明るさのレベルを計算するメソッド
    private ushort CalcBrightnessLevel(float brightness)
    {
        // Brightnessのレベルを5段階に分ける
        if (brightness >= 50f && brightness < 250f)
        {
            return 3;
        }
        else if (brightness >= 250f && brightness < 500f)
        {
            return 2;
        }
        else
        {
            return 1;
        }
    }
    
    // 色温度のレベルを計算するメソッド
    private ushort CalcTemperatureLevel(float temperature)
    {
        
        // ColorTemperatureのレベルを5段階に分ける
        if (temperature >= 1500 && temperature < 4000)
        {
            return 3;
        }
        else if (temperature >= 4000 && temperature < 5000)
        {
            return 2;
        }
        else
        {
            return 1;
        }
    }
    
    private ushort CalcVolume(float volumeRate)
    {
        // ColorTemperatureのレベルを5段階に分ける
        if (volumeRate >= 0.0001 && volumeRate < 0.002)
        {
            return 3;
        }
        else if (volumeRate >= 0.002 && volumeRate < 0.003)
        {
            return 2;
        }
        else
        {
            return 1;
        }
    }
}
