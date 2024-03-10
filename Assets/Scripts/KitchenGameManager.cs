using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class KitchenGameManager : MonoBehaviour
{
    public event EventHandler OnGameOver;
    public event EventHandler<OnCountdownToStartArgs> OnGamePlaying;

    public event EventHandler<OnCountdownToStartArgs> OnCountdownToStart;
    public class OnCountdownToStartArgs : EventArgs
    {
        public float TimeCountDown;
    }
    public static KitchenGameManager Instance { get; private set; }
    private State state;
    private float WaitToStartTimmer = 1f;
    private float CountdownToStartTimmer = 10f;
    private float GamePlayingTimmer ;
    private float GamePlayingTimmerMax = 120f;
    public enum State
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver
    }
    private void Awake()
    {
        Instance = this;
        state = State.WaitingToStart;
    }
    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                WaitToStartTimmer -= Time.deltaTime;
                if (WaitToStartTimmer <= 0f)
                {
                    state = State.CountdownToStart;
                }
                break;
            case State.CountdownToStart:
                OnCountdownToStart?.Invoke(this, new OnCountdownToStartArgs { TimeCountDown = CountdownToStartTimmer });
                CountdownToStartTimmer -= Time.deltaTime;
                if (CountdownToStartTimmer <= 0f)
                {

                    state = State.GamePlaying;
                    GamePlayingTimmer = GamePlayingTimmerMax;
                }
                break;
            case State.GamePlaying:
                OnGamePlaying?.Invoke(this, new OnCountdownToStartArgs { TimeCountDown = 1-((float)GamePlayingTimmer / GamePlayingTimmerMax) });
                GamePlayingTimmer -= Time.deltaTime;
                if (GamePlayingTimmer <= 0f)
                {

                    state = State.GameOver;
                }
                break;
            case State.GameOver:
                OnGameOver?.Invoke(this, EventArgs.Empty);
                break;
        }

    }
    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }
}
