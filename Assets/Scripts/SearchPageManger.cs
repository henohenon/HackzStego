using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using UnityEngine.UI;

public class SearchPageManger : MonoBehaviour
{
    private Subject<Unit> _onSearch = new Subject<Unit>();
    public Observable<Unit> OnSearch => _onSearch;
    
    [SerializeField]
    private Button searchButton;
    
    
    // Start is called before the first frame update
    void Start()
    {
        searchButton.onClick.AddListener(async () =>
        {
            _onSearch.OnNext(Unit.Default);
        });
    }
}
