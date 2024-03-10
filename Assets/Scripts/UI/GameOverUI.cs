using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameObject Container;
    [SerializeField] private GameObject TextPoint;
    void Start()
    {
        Container.SetActive(false);
        KitchenGameManager.Instance.OnGameOver += Instance_OnGameOver;

    }
    private void Update()
    {
        TextMeshProUGUI textTimeString = TextPoint.GetComponent<TextMeshProUGUI>();
        textTimeString.SetText(DeliveryManager.Instance.successfulRecipesAmount.ToString());
    }
    private void Instance_OnGameOver(object sender, System.EventArgs e)
    {
        Container.SetActive(true);
    }


    
}
