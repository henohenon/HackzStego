using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class ResultByLevelManager : MonoBehaviour
{
    [SerializeField] private GameObject[] particles;

    [SerializeField] private GameObject[] fog_particles;

    [SerializeField] private GameObject[] filters;
    [SerializeField] private GameObject[] stars;
    [SerializeField] private GameObject end_button;    
    [SerializeField] private GameObject start_music;
    [SerializeField] private GameObject[] waitStars;


    private void DisableAll()
    {
        end_button.SetActive(false);
        start_music.SetActive(false);
        for (var i = 0; i < particles.Length; i++)
        {
            particles[i].SetActive(false); 
            fog_particles[i].SetActive(false);
            stars[i].SetActive(false);
            filters[i].SetActive(false);  
        }

        foreach (var wait in waitStars)
        {
            wait.SetActive(false);
        }

    }
    
    public async UniTask ChangeLevel(int level)
    {
        DisableAll();
        
        particles[level].SetActive(true);
        fog_particles[level].SetActive(true);
        filters[level].SetActive(true);

        for(var i = 0; i < level+1; i++)
        {
            stars[i].SetActive(true);
            await UniTask.Delay(TimeSpan.FromSeconds(0.24f));
        }
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f));

        foreach (var wait in waitStars)
        {
            wait.SetActive(true);
        }
        
        if(level == 2)
        {
            start_music.SetActive(true);
        }
        else
        {
            end_button.SetActive(true);
        }

    }

    private void OnDisable()
    {
        DisableAll();
    }
}
