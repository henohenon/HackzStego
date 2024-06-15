using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityScreenNavigator.Runtime.Core.Page;

[RequireComponent(typeof(Page))]
public class TitlePageManager : MonoBehaviour
{
    private PageContainer _container;
    
    [SerializeField]
    private Button startButton;
    
    // Start is called before the first frame update
    private void Start()
    {
        _container = FindObjectOfType<PageContainer>();
        startButton.onClick.AddListener(() =>
        {
            Debug.Log(_container);
            _container.Push("SearchPage", true);
        });
    }
}
