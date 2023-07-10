using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsManager : MonoBehaviour
{
    private static EventsManager instance;
    public static EventsManager Instance { get { return instance; } }

    public event EventHandler<int> OnPickupCoin;

    public event EventHandler<bool> OnGameOver;

    private void Awake()
    {
        if (instance != null)
            Destroy(this);

        instance = this;

        DontDestroyOnLoad(this);
    }

    public void PickupCoin(int _gems)
    {
        OnPickupCoin?.Invoke(this, _gems);
    }

    public void GameOver(bool _hasFinishedLevel)
    {
        OnGameOver?.Invoke(this, _hasFinishedLevel);
    }
}
