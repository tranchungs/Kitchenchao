using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StoveCounter : BaseCounter,IHasProgress
{
    public event EventHandler<OnCookingSoundEventArgs> OnCookingSound;
    public class OnCookingSoundEventArgs : EventArgs
    {
        public StoveState state;
    }
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    private FryingRecipeSO fryingRecipeSOcurrent;
    private StoveState State;
    public event EventHandler<IHasProgress.OnProcessChangedEventArg> OnProcessChanged;

    public event EventHandler<OnCookingEventArgs> OnCooking;
    public class OnCookingEventArgs : EventArgs
    {
        public bool state;
    }
    public enum StoveState
    {
        Idle,
        Frying,
        Burning,
        Burned
    }
    private float timeCooking;
    private float timeBurring;
    private void Start()
    {
        timeBurring = 0;
        timeCooking = 0;
        State = StoveState.Idle;
    }
    private void Update()
    {
        if (HasKitchenObject())
        {
         
            switch (State)
            {
                case StoveState.Idle:
                    OnCookingSound?.Invoke(this, new OnCookingSoundEventArgs { state = StoveState.Idle});
                    break;
                case StoveState.Frying:
                    timeCooking += Time.deltaTime;
                    OnCookingSound?.Invoke(this, new OnCookingSoundEventArgs { state = StoveState.Frying });
                    OnProcessChanged?.Invoke(this, new IHasProgress.OnProcessChangedEventArg { ProcessNomalized = (float)timeCooking / fryingRecipeSOcurrent.timefryingMax });
                    if (timeCooking > fryingRecipeSOcurrent.timefryingMax)
                    {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(fryingRecipeSOcurrent.outputkitchenObjectSO, this);
                        State = StoveState.Burning;
                        timeCooking = 0;
                    }
                    break;
                case StoveState.Burning:  
                    timeBurring += Time.deltaTime;
                    OnCookingSound?.Invoke(this, new OnCookingSoundEventArgs { state = StoveState.Burning });
                    OnProcessChanged?.Invoke(this, new IHasProgress.OnProcessChangedEventArg { ProcessNomalized = (float)timeBurring / fryingRecipeSOcurrent.timefryingBurned });
                    if (timeBurring > fryingRecipeSOcurrent.timefryingBurned)
                    {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(fryingRecipeSOcurrent.outputkitchenObjectSOBurned, this);
                        State = StoveState.Burned;
                        timeBurring = 0;
                    }

                    break;
                case StoveState.Burned:
                    OnCookingSound?.Invoke(this, new OnCookingSoundEventArgs { state = StoveState.Burned });
                    OnProcessChanged?.Invoke(this, new IHasProgress.OnProcessChangedEventArg { ProcessNomalized = (float)0 / fryingRecipeSOcurrent.timefryingBurned });
                    break;
            }
        }

    }
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // Quầy ko có Item
            if (player.HasKitchenObject() && HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
            {
                OnCooking?.Invoke(this, new OnCookingEventArgs { state = true});
                player.GetKitchenObject().SetKitchenObjectParten(this);
               fryingRecipeSOcurrent = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                State = StoveState.Frying;

            }
        }
        else
        {
            //Quầy có Item
            if (!player.HasKitchenObject())
            {
                OnCooking?.Invoke(this, new OnCookingEventArgs { state = false });
                this.GetKitchenObject().SetKitchenObjectParten(player);
                State = StoveState.Idle;
                OnProcessChanged?.Invoke(this, new IHasProgress.OnProcessChangedEventArg { ProcessNomalized = 0 });
            }
            else
            {
                if (player.GetKitchenObject().TryGetPlateKitchenObject(out Plate plate))
                {
                    if (plate.TryAddItem(GetKitchenObject()))
                    {
                        OnCooking?.Invoke(this, new OnCookingEventArgs { state = false });
                        GetKitchenObject().DestroySelf();
                        State = StoveState.Idle;
                        OnProcessChanged?.Invoke(this, new IHasProgress.OnProcessChangedEventArg { ProcessNomalized = 0 });

                    }

                }

            }

        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO cuttingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        return cuttingRecipeSO != null;
    }
    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
     
        FryingRecipeSO fryingRecipeSO = null;
        if(inputKitchenObjectSO.type == KitchenObjectType.Cook)
        {
            foreach (FryingRecipeSO item in fryingRecipeSOArray)
            {
                if (item.inputkitchenObjectSO == inputKitchenObjectSO)
                {
                    fryingRecipeSO = item;

                }
            }
            return fryingRecipeSO;
        }
        else
        {
            return fryingRecipeSO;
        }
        
        
    }
    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        if (fryingRecipeSO != null)
        {
            return fryingRecipeSO.outputkitchenObjectSO;
        }
        else
        {
            return null;
        }
    }
}
