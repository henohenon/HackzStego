using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityScreenNavigator.Runtime.Core.Modal;
using UnityScreenNavigator.Runtime.Core.Page;

[RequireComponent(typeof(ModalContainer), typeof(Page))]
public class SearchPageManger : MonoBehaviour
{
    [SerializeField]
    private Button searchButton;
    
    private PageContainer _pageContainer;
    private ModalContainer _modalContainer;
    
    // Start is called before the first frame update
    void Start()
    {
        _pageContainer = FindObjectOfType<PageContainer>();
        _modalContainer = GetComponent<ModalContainer>();
        
        searchButton.onClick.AddListener(async () =>
        {
            await _modalContainer.Push("SearchingModal", true);
            await UniTask.Delay(3000);
            await _modalContainer.Pop(true);
            _pageContainer.Push("ResultPage", true);
        });
    }
}
