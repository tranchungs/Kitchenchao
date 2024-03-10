using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour,IKitchenObjectParten
{
    private KitchenObject kitchenObj;
    [SerializeField] private GameObject clearCounterTopPoint;
    public virtual void Interact(Player player)
    {
        
    }
    public virtual void InteractAlternate(Player player)
    {

    }
    public Transform GetKitchenObjectFollowTransform()
    {
        return clearCounterTopPoint.transform;
    }
    public KitchenObject GetKitchenObject()
    {
        return this.kitchenObj;
    }
    public void SetKitchenObject(KitchenObject kitchenObj)
    {
        this.kitchenObj = kitchenObj;
    }
    public void ClearKitchenObject()
    {
        this.kitchenObj = null;
    }
    public bool HasKitchenObject()
    {
        return this.kitchenObj != null;
    }
}
