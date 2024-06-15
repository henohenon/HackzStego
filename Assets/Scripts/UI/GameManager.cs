using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using R3;

public class GameManager : MonoBehaviour
{
    [SerializeField] private SearchPageManger searchPageManger;
    [SerializeField] private GameObject searchingObj;
    [SerializeField] private ResultPageManager resultPageManager;
    
    private void Start()
    {
        ChangePage(Page.Search);
        searchPageManger.Search.Subscribe(async (_) =>
        {
            ChangePage(Page.Searching);
            await UniTask.Delay(TimeSpan.FromSeconds(2));
            ChangePage(Page.Result);
        });
        resultPageManager.Restart.Subscribe(async (_) =>
        {
            ChangePage(Page.Search);
        });
    }

    private void ChangePage(Page page)
    {
        switch (page)
        {
            case Page.Search:
                searchPageManger.gameObject.SetActive(true);
                searchingObj.SetActive(false);
                resultPageManager.gameObject.SetActive(false);
                break;
            case Page.Searching:
                searchPageManger.gameObject.SetActive(false);
                searchingObj.SetActive(true);
                resultPageManager.gameObject.SetActive(false);
                break;
            case Page.Result:
                searchPageManger.gameObject.SetActive(false);
                searchingObj.SetActive(false);
                resultPageManager.gameObject.SetActive(true);
                break;
        }
    }
    
    protected enum Page
    {
        Search,
        Searching,
        Result
    }
}
