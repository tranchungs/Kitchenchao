using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            if (player.GetKitchenObject().TryGetPlateKitchenObject(out Plate plate))
            {
                DeliveryManager.Instance.DeliverRecipe(plate);
                player.GetKitchenObject().DestroySelf();
            }
            else
            {
                player.GetKitchenObject().DestroySelf();
            }
        }
       
    }
}
