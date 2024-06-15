using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using R3;
using TMPro;

public class DebugPageManager : MonoBehaviour
{
    [SerializeField] private ScoringManager _scoringManager;
    [SerializeField] private TMP_Text brightLevel;
    [SerializeField] private TMP_Text temperatureLevel;
    [SerializeField] private TMP_Text volumeLevel;
    [SerializeField] private TMP_Text brightValue;
    [SerializeField] private TMP_Text temperatureValue;
    [SerializeField] private TMP_Text volumeValue;

    
    private void Start()
    {
        _scoringManager.BrightnessLevel.Subscribe(level =>
        {
            brightLevel.text = level.ToString();
        });
        _scoringManager.TemperatureLevel.Subscribe(level =>
        {
            temperatureLevel.text = level.ToString();
        });
        _scoringManager.VolumeLevel.Subscribe(level =>
        {
            volumeLevel.text = level.ToString();
        });
    }

    private void LateUpdate()
    {
        brightValue.text = _scoringManager.Info.Brightness.ToString();
        temperatureValue.text = _scoringManager.Info.ColorTemperature.ToString();
        volumeValue.text = _scoringManager.Info.VolumeRate.ToString();
    }
}
