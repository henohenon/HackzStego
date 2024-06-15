using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultByLevelManager : MonoBehaviour
{
    [SerializeField] private GameObject[] particles;
    [SerializeField] private GameObject[] fog_particles;

    private void DisableAll()
    {
        for (var i = 0; i < particles.Length; i++)
        {
            particles[i].SetActive(false); 
            fog_particles[i].SetActive(false);    
        }
    }
    
    public void ChangeLevel(int level)
    {
        DisableAll();
        
        particles[level].SetActive(true);
        fog_particles[level].SetActive(true);
    }

    private void OnDisable()
    {
        DisableAll();
    }
}
