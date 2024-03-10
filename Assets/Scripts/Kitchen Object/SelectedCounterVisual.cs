using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{

    [SerializeField] private BaseCounter BaseCounter;
    [SerializeField] private GameObject[] BaseCounterVisual;
    private void Start()
    {
        Player.Instance.OnSelectedCounterChange += Instance_OnSelectedCounterChange;
    }

    private void Instance_OnSelectedCounterChange(object sender, Player.OnSelectedCounterChangeEventArgs e)
    {
       
       if(e.selectedCounter == BaseCounter)
        {
          
            Show();
        }
        else
        {
   
            Hidden();
        }

           

    }
    private void Show()
    {
        foreach (GameObject item in BaseCounterVisual)
        {
            item.SetActive(true);
        }
       
    }
    private void Hidden()
    {
        foreach (GameObject item in BaseCounterVisual)
        {
            item.SetActive(false);
        }

    }
}
