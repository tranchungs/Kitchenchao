using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKitchenObjectParten {
    public Transform GetKitchenObjectFollowTransform();

    public KitchenObject GetKitchenObject();

    public void SetKitchenObject(KitchenObject kitchenObj);


    public void ClearKitchenObject();


    public bool HasKitchenObject();
    
}
