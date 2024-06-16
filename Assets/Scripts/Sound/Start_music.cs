using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;

public class Start_music : MonoBehaviour
{
    [SerializeField] private AudioSource[] AudioSources;
    

    public async Task start_music()
    {

        int number = Random.Range(0, AudioSources.Length - 1);
        AudioSources[number].volume = 0;
        AudioSources[number].DOFade(endValue: 1f, duration: 1f);
        await Task.Delay(1000);
        AudioSources[number].Play();

        
    }

    public async Task stop_music()
    {
        for(var n = 0; n < 6; n++)
        {
            
            AudioSources[n].DOFade(endValue: 0f, duration: 1f);
        }
        await Task.Delay(1000);

        for(var i = 0; i < 6; i++)
        {
            AudioSources[i].Stop();
        }
    }
}
