using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour
{
    [SerializeField] private GameObject textTime;
    private float TimeCoundown;
    private void Start()
    {
        Hide();
        KitchenGameManager.Instance.OnCountdownToStart += Instance_OnCountdownToStart;
    }
    private void Update()
    {
        if (Math.Floor(TimeCoundown) <= 0)
        {
            Hide();
        }
        else
        {
            TextMeshProUGUI textTimeString = textTime.GetComponent<TextMeshProUGUI>();
            textTimeString.SetText(TimeCoundown.ToString());
        }
       
    }
    private void Instance_OnCountdownToStart(object sender, KitchenGameManager.OnCountdownToStartArgs e)
    {
        Show();

        TimeCoundown = e.TimeCountDown;


    }
    private void Show()
    {
        textTime.SetActive(true);
    }
    private void Hide()
    {
        textTime.SetActive(false);
    }
}
