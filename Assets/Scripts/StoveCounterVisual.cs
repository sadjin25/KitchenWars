using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] StoveCounter stoveCounter;
    [SerializeField] GameObject stoveCounterObject;
    [SerializeField] GameObject particleObject;

    void Start()
    {
        stoveCounter.OnStateChanged += OnStateChanged;
    }

    void OnStateChanged(object s, StoveCounter.OnStateChangedEventArgs e)
    {
        bool isVisible = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
        stoveCounterObject.SetActive(isVisible);
        particleObject.SetActive(isVisible);
    }
}
