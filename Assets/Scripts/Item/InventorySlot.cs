using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image iconImage;
    public bool isAlwaysVisible = false;

    void Start()
    {
        if (!isAlwaysVisible)
        {
            gameObject.SetActive(false);
        }

        else
        {
            iconImage.enabled = true;
        }
    }

    public void AcivateSlot(Sprite icon)
    {
        gameObject.SetActive(true);
        iconImage.sprite = icon;
        iconImage.enabled = true;
    }
}
