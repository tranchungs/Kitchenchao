using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            Transform kitchenObjectTranform = Instantiate(kitchenObjectSO.prefab);
            kitchenObjectTranform.GetComponent<KitchenObject>().SetKitchenObjectParten(player);

        }
    }
}
