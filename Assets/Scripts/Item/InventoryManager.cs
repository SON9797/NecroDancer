using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public ItemDatabase itemDatabase;

    [Header("슬롯 연결")]
    [SerializeField] InventorySlot shovelSlot;
    [SerializeField] InventorySlot attackSlot;
    [SerializeField] InventorySlot bodySlot;
    [SerializeField] InventorySlot feetSlot;
    [SerializeField] InventorySlot torchSlot;
    [SerializeField] InventorySlot headSlot;
    [SerializeField] InventorySlot keySlot;

    void Start()
    {
        if (itemDatabase != null && itemDatabase.itemList.Count > 0)
        {
            Debug.Log("123");
            EquipItem("S1001");
            EquipItem("W1006");
        }

        else
        {
            Debug.LogError("데이터베이스가 비어있다.");
        }
    }


    void Update()
    {
        
    }

    public void EquipItem(string itemId)
    {
        ItemData item = itemDatabase.GetItemById(itemId);

        if (item == null)
        {
            return;            
        }

        string fileName = item.Icon.Split('_')[0] + "_" + item.Icon.Split('_')[1];

        Sprite[] sprites = Resources.LoadAll<Sprite>("Icon/" + fileName);

        Sprite icon = null;
        foreach (var s in sprites)
        {
            if (s.name == item.Icon)
            {
                icon = s;
                break;
            }
        }

        if (icon == null)
        {
            Debug.LogError("로드실패");
            return;
        }

        switch (item.Type)
        {
            case "Shovel":
                shovelSlot.AcivateSlot(icon);
                break;

            case "Weapon":
                attackSlot.AcivateSlot(icon);
                break;

            case "Body":
                bodySlot.AcivateSlot(icon);
                break;

            case "Feet":
                feetSlot.AcivateSlot(icon);
                break;

            case "Torch":
                torchSlot.AcivateSlot(icon);
                break;

            case "Head":
                headSlot.AcivateSlot(icon); 
                break;

            case "Key":
                keySlot.AcivateSlot(icon);
                break;
        }
    }
}
