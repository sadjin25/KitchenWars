using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    enum State
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver,
    }

    State state;
    float waitingToStartTimer = 1f;
    float countdownToStartTimer = 3f;
    float gamePlayingTimer = 60f;
    readonly float gamePlayingMaxTime = 60f;

    public event EventHandler OnStateChanged;

    void Awake()
    {
        Instance = this;
        state = State.WaitingToStart;
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }

    void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if (waitingToStartTimer < 0f)
                {
                    waitingToStartTimer = 1f;
                    state = State.CountdownToStart;

                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.CountdownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer < 0f)
                {
                    countdownToStartTimer = 3f;
                    state = State.GamePlaying;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0f)
                {
                    gamePlayingTimer = gamePlayingMaxTime;
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
                break;
        }
    }

    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }

    public bool IsCountdown()
    {
        return state == State.CountdownToStart;
    }

    public bool IsGameOver()
    {
        return state == State.GameOver;
    }

    public float GetCountdownTimer()
    {
        return countdownToStartTimer;
    }

    public float GetGamePlayingTimeRatio()
    {
        return 1 - (gamePlayingTimer / gamePlayingMaxTime);
    }
}
