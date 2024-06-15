using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityScreenNavigator.Runtime.Core.Page;

[RequireComponent(typeof(Page))]
public class ResultPageManager : MonoBehaviour
{
    private string[,] _results = new string[3, 2]
    {
        {"100%", "フィーバー¥nカーニバル"},
        {"50%", "至って平穏"},
        {"10%", "ボロボロ"}
    };
    
    [SerializeField] private Text perText;
    [SerializeField] private Text detailText;
    [SerializeField] private Button backButton;

    private PageContainer _container;
    
    
    // Start is called before the first frame update
    async void Start()
    {
        _container = FindObjectOfType<PageContainer>();
        
        // ランダムに結果を表示
        var result = Random.Range(0, 3);
        perText.text = _results[result, 0];
        detailText.text = _results[result, 1];
        
        backButton.onClick.AddListener(() =>
        {
            _container.Pop(true);
        });
    }
    
}
