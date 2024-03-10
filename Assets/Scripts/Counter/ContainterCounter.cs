using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainterCounter : BaseCounter, IKitchenObjectParten
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    public event EventHandler OnSelectedBaseCounter;
    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            OnSelectedBaseCounter?.Invoke(this, EventArgs.Empty);
            Transform kitchenObjectTranform = Instantiate(kitchenObjectSO.prefab);
            kitchenObjectTranform.GetComponent<KitchenObject>().SetKitchenObjectParten(player);
        }
    }
    

}
