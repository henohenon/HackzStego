using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using R3;

[RequireComponent(typeof(ResultByLevelManager))]
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
    [SerializeField] private Start_music _startMusic;
    [SerializeField] private Text detailText;
    [SerializeField] private Toggle brightnessToggle;
    [SerializeField] private Toggle temperatureToggle;
    [SerializeField] private Toggle volumeToggle;
    [SerializeField] private Button backButton;
    [SerializeField] private Button start_music;
    [SerializeField] private Button end_music;
    [SerializeField] private GameObject start_music_object;
    [SerializeField] private GameObject end_music_object;
    
    private ResultByLevelManager _resultByLevel;
    
    // Start is called before the first frame update
    void Awake()
    {
        end_music_object.SetActive(false);
        backButton.onClick.AddListener(() =>
        {
            _restart.OnNext(Unit.Default); 
        });
        start_music.onClick.AddListener(() =>
        {
            _startMusic.start_music();
            start_music_object.SetActive(false);
            end_music_object.SetActive(true);
        });
        end_music.onClick.AddListener(() =>
        {
            _startMusic.stop_music();
            end_music_object.SetActive(false);
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
        
        _resultByLevel = GetComponent<ResultByLevelManager>();
    }


    private void OnEnable()
    {
        var avgScore = (int)Mathf.Round(_scoringManager.GetAvgScore());
        Debug.Log("avgScore:" + avgScore);
        detailText.text = _results[avgScore-1];

        _resultByLevel.ChangeLevel(avgScore-1);
    }
    
}
