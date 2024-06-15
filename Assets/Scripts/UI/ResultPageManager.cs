using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using R3;
using Random = UnityEngine.Random;

public class ResultPageManager : MonoBehaviour
{
    private Subject<Unit> _restart = new Subject<Unit>();
    public Observable<Unit> Restart => _restart;
    
    private string[,] _results = new string[3, 2]
    {
        {"100%", "フィーバー¥nカーニバル"},
        {"50%", "至って平穏"},
        {"10%", "ボロボロ"}
    };
    
    [SerializeField] private Text perText;
    [SerializeField] private Text detailText;
    [SerializeField] private Button backButton;
    
    
    // Start is called before the first frame update
    void Awake()
    {
        backButton.onClick.AddListener(() =>
        {
            _restart.OnNext(Unit.Default); 
        });
    }


    private void OnEnable()
    {
        // ランダムに結果を表示
        var result = Random.Range(0, 3);
        perText.text = _results[result, 0];
        detailText.text = _results[result, 1];
    }
}
