using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerAudio : MonoBehaviour
{
   [SerializeField] private AudioClip _step;
   [SerializeField] private AudioClip _conflict;
    private AudioSource _audioSource;

   
    
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

  
    
    
    public void PlayStep()
    {
        if (_audioSource.clip!=_step)
        {
            _audioSource.clip = _step;
        }
       Play();
    }
    
    public void PlayConflict()
    {
        if (_audioSource.clip!=_conflict)
        {
            _audioSource.clip = _conflict;
        }
       Play();
    }

    private void Play()
    {
        _audioSource.pitch = Random.Range(.85f, 1.15f);
        _audioSource.Play();
    }
}
