using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TrashCounter : BaseCounter
{
    public event EventHandler OnTrashKitchentObject;
    public static TrashCounter Instance { get; private set; }
    public void Awake()
    {
        Instance = this;
    }
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            OnTrashKitchentObject?.Invoke(this, EventArgs.Empty);
            player.GetKitchenObject().DestroySelf();
        }
      
        
    }
}
