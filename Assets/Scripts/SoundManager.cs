using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioClipRefsSO audioClipRefsSO;

    void Start()
    {
        DeliveryManager.Instance.OnRecipeCompleted += OnRecipeCompleted;
        DeliveryManager.Instance.OnRecipeFailed += OnRecipeFailed;
    }
    void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }

    void PlaySound(AudioClip[] audioClipArr, Vector3 position, float volume = 1f)
    {
        AudioClip toPlay = audioClipArr[UnityEngine.Random.Range(0, audioClipArr.Length)];
        AudioSource.PlayClipAtPoint(toPlay, position, volume);
    }

    void OnRecipeCompleted(object s, EventArgs e)
    {
        PlaySound(audioClipRefsSO.deliverySuccess, DeliveryCounter.Instance.transform.position);
    }
    void OnRecipeFailed(object s, EventArgs e)
    {
        PlaySound(audioClipRefsSO.deliveryFail, DeliveryCounter.Instance.transform.position);
    }
}
