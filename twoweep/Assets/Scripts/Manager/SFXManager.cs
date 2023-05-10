using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour 
{
    [SerializeField] private AudioSource sfx_dead;
    [SerializeField] private AudioSource sfx_plug;
    [SerializeField] private AudioSource sfx_clear;

    void Awake() 
    {
        
    }
    
    public void PlaySFXDead() 
    {
        sfx_dead.Play();
    }
    
    public void PlaySFXPlug()
    {
        sfx_plug.Play();
    }

    public void PlaySFXClear() 
    {
        sfx_clear.Play();
    }
}
