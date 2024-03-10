using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private GameObject paticalStove;
    [SerializeField] private GameObject fryringFire;
    private void Start()
    {
        stoveCounter.OnCooking += StoveCounter_OnCooking;
    }

    private void StoveCounter_OnCooking(object sender, StoveCounter.OnCookingEventArgs e)
    {
        SetState(e.state);
    }
    private void SetState(bool state)
    {
        paticalStove.SetActive(state);
        fryringFire.SetActive(state);

    }
}
