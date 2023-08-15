using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class GamePlayingClockUI : MonoBehaviour
{
    [SerializeField] Image clockGauge;

    void Start()
    {
        GameManager.Instance.OnStateChanged += OnStateChanged;
        gameObject.SetActive(false);
    }

    void Update()
    {
        clockGauge.fillAmount = GameManager.Instance.GetGamePlayingTimeRatio();
    }

    void OnStateChanged(object s, EventArgs e)
    {
        if (GameManager.Instance.IsGamePlaying())
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    void Show()
    {
        gameObject.SetActive(true);
    }

    void Hide()
    {
        gameObject.SetActive(false);
    }
}
