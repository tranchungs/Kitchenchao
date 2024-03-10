using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;
public class Plate : KitchenObject
{
    [SerializeField] private List<KitchenObjectSO> ValidateKitchenObjectSO;
    List<KitchenObjectSO> listKitchenObjectSO;
    public event EventHandler<OnAddItemArgs> OnAddItem;
    public class OnAddItemArgs : EventArgs
    {
        public KitchenObjectSO kitchenObjectSO;
    }

  
    private void Awake()
    {
        listKitchenObjectSO = new List<KitchenObjectSO>();
     
    }

  

    public bool TryAddItem(KitchenObject kitchenObject)
    {
        if (ValidateKitchenObjectSO.Contains(kitchenObject.GetKitchenObjectSO()))
        {
            if (listKitchenObjectSO.Contains(kitchenObject.GetKitchenObjectSO()))
            {
                return false;
            }
            else
            {
                listKitchenObjectSO.Add(kitchenObject.GetKitchenObjectSO());
                OnAddItem?.Invoke(this, new OnAddItemArgs { kitchenObjectSO = kitchenObject.GetKitchenObjectSO() });
                return true;
            }
           
            
        }
        else
        {
            return false;
        }
        
      
    }
    public List<KitchenObjectSO> GetKitchenObjectSOList()
    {
        return listKitchenObjectSO;
    }
}
