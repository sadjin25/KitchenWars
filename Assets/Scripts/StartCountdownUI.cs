using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class StartCountdownUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI countdownText;

    void Start()
    {
        GameManager.Instance.OnStateChanged += OnStateChanged;
    }

    void Update()
    {
        countdownText.text = Mathf.Ceil(GameManager.Instance.GetCountdownTimer()).ToString();
    }

    void OnStateChanged(object s, EventArgs e)
    {
        if (GameManager.Instance.IsCountdown())
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
