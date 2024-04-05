using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class GameClockUI : MonoBehaviour
{
    [SerializeField] private GameObject uiParent;
    [SerializeField] private Image progressImage;
    [SerializeField] private TextMeshProUGUI timeText;
    void Start()
    {
        Hide();
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
    }
    private void Update()
    {
        if (GameManager.Instance.IsGamePlayingState())
        {
            progressImage.fillAmount = GameManager.Instance.GetGamePlayingTimerNormalized();
            timeText.text = Mathf.CeilToInt(GameManager.Instance.GetGamePlayingTimer()).ToString();
        }
    }
    private void GameManager_OnStateChanged(object sender, EventArgs e)
    {
        if (GameManager.Instance.IsGamePlayingState())
        {
            Show();
        }
    }
    private void OnDestroy() {
        GameManager.Instance.OnStateChanged -= GameManager_OnStateChanged;
    }
    private void Show()
    {
        uiParent.SetActive(true);
    }
    private void Hide()
    {
        uiParent.SetActive(false);
    }
}
