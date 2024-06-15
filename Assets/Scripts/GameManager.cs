using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using R3;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private SearchPageManger searchPageManger;
    [SerializeField] private GameObject searchingObj;
    [SerializeField]
    private ResultPageManager resultPageManager;
    
    
    private void Start()
    {
        searchPageManger.gameObject.SetActive(true);
        searchingObj.SetActive(false);
        resultPageManager.gameObject.SetActive(false);

        searchPageManger.OnSearch.Subscribe(async (_) =>
        {
            searchingObj.SetActive(true);
            await UniTask.Delay(TimeSpan.FromSeconds(3));
            searchingObj.SetActive(false);
            resultPageManager.gameObject.SetActive(true);
            searchPageManger.gameObject.SetActive(false);
        });
        
        resultPageManager.OnReset.Subscribe(async (_) =>
        {
            resultPageManager.gameObject.SetActive(false);
            searchPageManger.gameObject.SetActive(true);
        });
    }
}
