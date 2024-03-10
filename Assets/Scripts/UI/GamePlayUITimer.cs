using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayUITimer : MonoBehaviour
{
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject TimeSpin;
    private float TimeSpinFill;
    // Start is called before the first frame update
    void Start()
    {
        TimeSpinFill = 1f;
        Hide();
        KitchenGameManager.Instance.OnGamePlaying += Instance_OnGamePlaying;
    }

    private void Instance_OnGamePlaying(object sender, KitchenGameManager.OnCountdownToStartArgs e)
    {
        Show();
        TimeSpinFill = e.TimeCountDown;
    }

    // Update is called once per frame
    void Update()
    {
        Image image = TimeSpin.GetComponent<Image>();
        image.fillAmount = TimeSpinFill;
        if (Math.Round(TimeSpinFill) == 1)
        {
            Hide();
        }
    }
    private void Show()
    {
        background.SetActive(true);
        TimeSpin.SetActive(true);
    }
    private void Hide()
    {
        background.SetActive(false);
        TimeSpin.SetActive(false);
    }
}
