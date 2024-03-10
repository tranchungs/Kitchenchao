using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
           
            // Quầy ko có Item
            if (player.HasKitchenObject())
            {

                // if PLayer has kitchen objectt, move object player to ClearCounter
                player.GetKitchenObject().SetKitchenObjectParten(this);

            }
            
        }
        else
        {
            //Quầy có Item
            if (player.HasKitchenObject())
            {
                if (player.GetKitchenObject().TryGetPlateKitchenObject(out Plate plate))
                {
                    if (plate.TryAddItem(GetKitchenObject()))
                    {
                        GetKitchenObject().DestroySelf();
                    }

                }
                else
                {
                    Plate plateCurrent =(Plate) GetKitchenObject();
                    if (plateCurrent.TryAddItem(player.GetKitchenObject()))
                    {
                        player.GetKitchenObject().DestroySelf();
                    }
                }

            }
            else
            {
                this.GetKitchenObject().SetKitchenObjectParten(player);
            }


        }
    }
  
   
}
