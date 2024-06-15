using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using R3;
using TMPro;

public class DebugPageManager : MonoBehaviour
{
    [SerializeField] private LightEstimate _lightEstimate;
    [SerializeField] private SoundManager _soundManager;
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

    private void Update()
    {
        brightValue.text = _lightEstimate.Brightness.ToString();
        temperatureValue.text = _lightEstimate.ColorTemperature.ToString();
        volumeValue.text = _soundManager.VolumeRate.ToString();
    }
}
