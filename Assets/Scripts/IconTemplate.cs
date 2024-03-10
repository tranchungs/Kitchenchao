using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconTemplate : MonoBehaviour
{
    [SerializeField] private Image sprite;
    public void SetIconUI(Sprite newsprite)
    {
        sprite.sprite = newsprite;
    }
}
