using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="KitchenSO",menuName = "Scriptable Object/Kitchen Object")]
public class KitchenObjectSO : ScriptableObject
{
    public Transform prefab;
    public Sprite sprite;
    public string objectName;
    public KitchenObjectType type;
}

public enum KitchenObjectType
{
    Cut,
    Cook,
    None
}