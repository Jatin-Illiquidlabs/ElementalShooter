using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip gemsAudio;

    private void OnEnable()
    {
        EventsManager.Instance.OnPickupCoin += CollectGems;
    }

    private void OnDisable()
    {
        EventsManager.Instance.OnPickupCoin -= CollectGems;
    }

    private void CollectGems(object sender, int _gems)
    {
        audioSource.PlayOneShot(gemsAudio);
    }
}
