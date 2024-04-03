using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public event EventHandler OnStateChanged;
    private enum State
    {
        WaitingToStart,
        CountDownToStart,
        GamePlaying,
        GameOver
    }
    [SerializeField] private Player player;

    private State state;
    private float waitingToStartTimer = 1;
    private float countDownToStartTimer = 3;
    private float gamePlayingTimer = 30;

    private bool isGamePause = false;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        TurntoWaitingToStart();
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
    }
    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        ToggleGame();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if (waitingToStartTimer <= 0)
                {
                    TurntoCountDownToStart();
                }
                break;
            case State.CountDownToStart:
                countDownToStartTimer -= Time.deltaTime;
                if (countDownToStartTimer <= 0)
                {
                    TurntoGamePlaying();
                }
                break;
            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer <= 0)
                {
                    TurntoGameOver();
                }
                break;
            case State.GameOver:
                break;
            default:
                break;
        }
    }
    private void TurntoWaitingToStart()
    {
        state = State.WaitingToStart;
        DisablePlayer();
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }
    private void TurntoCountDownToStart()
    {
        state = State.CountDownToStart;
        DisablePlayer();
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }
    private void TurntoGamePlaying()
    {
        state = State.GamePlaying;
        EnablePlayer();
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }
    private void TurntoGameOver()
    {
        state = State.GameOver;
        DisablePlayer();
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }
    private void DisablePlayer()
    {
        player.enabled = false;
    }
    private void EnablePlayer()
    {
        player.enabled = true;
    }
    public bool IsCountDownState()
    {
        return state == State.CountDownToStart;
    }
    public bool IsGamePlayingState()
    {
        return state == State.GamePlaying;
    }
    public bool IsGameOverState()
    {
        return state == State.GameOver;
    }
    public float GetCountDownTimer()
    {
        return countDownToStartTimer;
    }
    private void ToggleGame()
    {
        isGamePause = !isGamePause;
        if (isGamePause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
