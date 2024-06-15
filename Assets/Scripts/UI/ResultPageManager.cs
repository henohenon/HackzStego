using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using R3;

public class ResultPageManager : MonoBehaviour
{
    private Subject<Unit> _restart = new Subject<Unit>();
    public Observable<Unit> Restart => _restart;
    
    private string[] _results = new string[5]
    {
        "うーんwやめといた方がいいかも...",
        "あとちょっと頑張れるはず！",
        "五分五分。まぁ悪くはないかな",
        "いい感じ！このまま行こう!",
        "絶好調！今しかない！",
    };
    
    [SerializeField]
    private ScoringManager _scoringManager;
    [SerializeField] private Text perText;
    [SerializeField] private Text detailText;
    [SerializeField] private Toggle brightnessToggle;
    [SerializeField] private Toggle temperatureToggle;
    [SerializeField] private Toggle volumeToggle;
    [SerializeField] private Button backButton;
    
    
    // Start is called before the first frame update
    void Awake()
    {
        backButton.onClick.AddListener(() =>
        {
            _restart.OnNext(Unit.Default); 
        });
        _scoringManager.BrightnessLevel.Subscribe(level =>
        {
            brightnessToggle.isOn = level > 3;
        });
        _scoringManager.TemperatureLevel.Subscribe(level =>
        {
            temperatureToggle.isOn = level > 3;
        });
        _scoringManager.VolumeLevel.Subscribe(level =>
        {
            volumeToggle.isOn = level > 3;
        });
    }


    private void OnEnable()
    {
        var avgScore = (int)Mathf.Round(_scoringManager.GetAvgScore());
        Debug.Log("avgScore:" + avgScore);
        perText.text = "レベル：" + avgScore;
        detailText.text = _results[avgScore-1];
    }
}
