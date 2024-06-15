using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityScreenNavigator.Runtime.Core.Page;

[RequireComponent(typeof(PageContainer))]
public class GameManager : MonoBehaviour
{
    private PageContainer _container;

    private void Awake()
    {
        _container = GetComponent<PageContainer>();
        _container.Push("SearchPage", true);
    }
}
