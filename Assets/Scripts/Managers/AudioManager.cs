using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoSingleton<AudioManager>
{
    [Header("Audio")]
    private AudioSource _audioSource = null;
    [SerializeField] private AudioClip _selectedClip = null;
    [SerializeField] private AudioClip _noSelectionClip = null;
    [SerializeField] private AudioClip _deletedClip = null;
    [SerializeField] private AudioClip _swapClip = null;
    [SerializeField] private AudioClip _randomItemsClip = null;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Play an audio clip based on the type = selectedItem, noSelection, delete, swapItems, randomItems
    /// </summary>
    /// <param name="type"></param>
    public void PlaySFX(string type)
    {
        switch(type)
        {
            case "selectedItem":
                _audioSource.PlayOneShot(_selectedClip);
                break;
            case "noSelection":
                _audioSource.PlayOneShot(_noSelectionClip);
                break;
            case "delete":
                _audioSource.PlayOneShot(_deletedClip);
                break;
            case "swapItems":
                _audioSource.PlayOneShot(_swapClip);
                break;
            case "randomItems":
                _audioSource.PlayOneShot(_randomItemsClip);
                break;
            default:
                break;
        }
    }
}
