using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "KitchenSO", menuName = "Scriptable Object/Cutting Recipe")]
public class CuttingRecipeSO : ScriptableObject
{
    public KitchenObjectSO inputkitchenObjectSO;
    public KitchenObjectSO outputkitchenObjectSO;
    public int cuttingProgressMax;
}
