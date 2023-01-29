using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioHandler : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private string _audioMixerName;

    private bool _isOn;

    public bool IsOn => _isOn;

    private void Awake()
    {
        ChangeAudioMixerValue();
    }

    public void ChangeAudioListenerState()
    {
        var value = PlayerPrefs.GetInt(Constants.AudioListenerKey, 1);
        var targetValue = 0;
        if (Convert.ToBoolean(value))
        {
            targetValue = 0;
        }
        else
        {
            targetValue = 1;
        }

        PlayerPrefs.SetInt(Constants.AudioListenerKey, targetValue);
        ChangeAudioMixerValue();
    }

    public void ChangeAudioMixerValue()
    {
        var volume = 0;

        if (Convert.ToBoolean(PlayerPrefs.GetInt(Constants.AudioListenerKey, 1)))
        {
            volume = 0;
            _isOn = true;
        }
        else
        {
            volume = -80;
            _isOn = false;
        }
        _audioMixer.SetFloat(_audioMixerName, volume);
    }

    public void RevardChangeAudioMixerValue()
    {
        var volume = 0;

        if (Convert.ToBoolean(PlayerPrefs.GetInt(Constants.AudioListenerKey, 1)))
        {
            volume = 0;
        }
        else
        {
            volume = -80;
        }
        _audioMixer.SetFloat(_audioMixerName, volume);
    }

    public void TryStopMusic()
    {
        if (_isOn)
        {
            ChangeAudioMixerValue();
        }
    }
}
