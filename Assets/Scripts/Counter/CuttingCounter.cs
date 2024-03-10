using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CuttingCounter : BaseCounter, IHasProgress
{

    public event EventHandler OnCutting;
    public event EventHandler<IHasProgress.OnProcessChangedEventArg> OnProcessChanged;
    public static event EventHandler OnAnyCut;

    private int cuttingProcess;
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // Quầy ko có Item
            if (player.HasKitchenObject() && HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
            {     
                    // if PLayer has kitchen objectt, move object player to ClearCounter
                    player.GetKitchenObject().SetKitchenObjectParten(this);
                    cuttingProcess = 0;
                    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    OnProcessChanged?.Invoke(this, new IHasProgress.OnProcessChangedEventArg{ ProcessNomalized = (float)cuttingProcess / cuttingRecipeSO.cuttingProgressMax });
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

            }
            else
            {
                this.GetKitchenObject().SetKitchenObjectParten(player);
            }

        }

    }
    public override void InteractAlternate(Player player)
    {

        // xử lý code ở đây nào??????? cắt cà chua => cà chua, cải => cải slice
        if (HasKitchenObject()&& HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            OnCutting?.Invoke(this, EventArgs.Empty);
            OnAnyCut?.Invoke(this, EventArgs.Empty);
            cuttingProcess++;
            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
            OnProcessChanged?.Invoke(this, new IHasProgress.OnProcessChangedEventArg { ProcessNomalized = (float)cuttingProcess / cuttingRecipeSO.cuttingProgressMax });
            if (cuttingProcess>= cuttingRecipeSO.cuttingProgressMax)
            {
                KitchenObjectSO item = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(item, this);
                
            }
           
          
        }
     
    }
    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        return cuttingRecipeSO != null;
    }
    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        if (cuttingRecipeSO != null)
        {
            return cuttingRecipeSO.outputkitchenObjectSO;
        }
        else
        {
            return null;
        }
    }

    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSOreturn = null;
        if (inputKitchenObjectSO.type == KitchenObjectType.Cut)
        {
            foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
            {
                if (cuttingRecipeSO.inputkitchenObjectSO == inputKitchenObjectSO)
                {
                    cuttingRecipeSOreturn = cuttingRecipeSO;
                }
            }
            return cuttingRecipeSOreturn;
        }
        else
        {
            return cuttingRecipeSOreturn;
        }
       
    }

}
