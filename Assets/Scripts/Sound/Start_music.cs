using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start_music : MonoBehaviour
{
    [SerializeField] private AudioSource[] AudioSources;
    

    void start_music()
    {
        int number = Random.Range(0, AudioSources.Length - 1);
        AudioSources[number].Play();

        
    }
}
