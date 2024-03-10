using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "KitchenSO", menuName = "Scriptable Object/Frying Recipe")]

public class FryingRecipeSO : ScriptableObject
{
    public KitchenObjectSO inputkitchenObjectSO;
    public KitchenObjectSO outputkitchenObjectSO;
    public KitchenObjectSO outputkitchenObjectSOBurned;
    public float timefryingMax;
    public float timefryingBurned;
    public StateMeat state;
}
public enum StateMeat
{
    Uncook,
    Cooked,
    Burned
}