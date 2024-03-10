using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconTemplateUI : MonoBehaviour
{
    [SerializeField] private Plate plate;
    [SerializeField] private GameObject prefabIcontemple;
    void Start()
    {
        plate.OnAddItem += Plate_OnAddItem;

    }

    private void Plate_OnAddItem(object sender, Plate.OnAddItemArgs e)
    {
        IconTemplate icontemple = prefabIcontemple.GetComponent<IconTemplate>();
        icontemple.SetIconUI(e.kitchenObjectSO.sprite);
        Instantiate(prefabIcontemple, this.transform);
    }
}
