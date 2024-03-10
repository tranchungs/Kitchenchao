using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private IKitchenObjectParten kitchenObjectParent;
    public KitchenObjectSO GetKitchenObject()
    {
        return kitchenObjectSO;
    }
    public void SetKitchenObjectParten(IKitchenObjectParten kitchenObjectParten)
    {
        if (this.kitchenObjectParent != null)
        {
            this.kitchenObjectParent.ClearKitchenObject();
        }
        this.kitchenObjectParent = kitchenObjectParten;
        if (this.kitchenObjectParent.HasKitchenObject())
        {
            Debug.LogError("Counter already has a Kitchen Object");
        }
        kitchenObjectParten.SetKitchenObject(this);
        transform.parent = kitchenObjectParten.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }
    public IKitchenObjectParten GetClearCounter()
    {
        return this.kitchenObjectParent;
    }
    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }
    public void DestroySelf()
    {
        
        kitchenObjectParent.ClearKitchenObject();

        Destroy(gameObject);

    }
    public bool TryGetPlateKitchenObject(out Plate plate)
    {
        if(this is Plate)
        {
            plate = (Plate)this;
            return true;
        }
        else
        {
            plate = null;
            return false;
        }
    }
    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParten kitchenObjectParent)
    {
   
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);

        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();

        kitchenObject.SetKitchenObjectParten(kitchenObjectParent);

        return kitchenObject;
    }
}
