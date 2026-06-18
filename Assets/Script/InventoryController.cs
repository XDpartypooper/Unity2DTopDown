using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private ItemCatalog ItemCatalog;

    [SerializeField] private GameObject invetoryPanel;
    [SerializeField] private SlotsScript[] EQPanels;
    [SerializeField] private GameObject SlotPrefab;
    [SerializeField] private int SlotsCount; //18 is half , 36 is full
    [SerializeField] private GameObject[] itemPrefabs;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ItemCatalog = FindAnyObjectByType<ItemCatalog>();
    }

    public bool AddItem(GameObject addeditem)
    {
        foreach (Transform slotTransform in invetoryPanel.transform)
        {
            SlotsScript slot = slotTransform.GetComponent<SlotsScript>();
            if (slot != null && slot.CurrentItem == null)
            {
                GameObject newitem = Instantiate(addeditem, slotTransform);
                newitem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                slot.CurrentItem = newitem;
                return true;
            }
        }
        Debug.Log("Inventory is full");
        return false;
    }

    public List<InventorySaveData> GetInventory()
    {
        List<InventorySaveData> ISaveData = new List<InventorySaveData>();
        foreach (Transform slotTransform in invetoryPanel.transform)
        {
            SlotsScript slot = slotTransform.GetComponent<SlotsScript>();
            if (slot.CurrentItem != null)
            {
                Item item = slot.CurrentItem.GetComponent<Item>();
                ISaveData.Add(new InventorySaveData { itemID = item.ID, SlotIndex = slotTransform.GetSiblingIndex() });
            }
        }

        return ISaveData;
    }
    public List<EQSaveData> GetEQ()
    {
        List<EQSaveData> EQSaveData = new List<EQSaveData>();


        for (int i = 0; i < 3; i++)
        {
            SlotsScript slot = EQPanels[i].GetComponent<SlotsScript>();
            if (slot.CurrentItem != null)
            {
                Item item = slot.CurrentItem.GetComponent<Item>();
                EQSaveData.Add(new EQSaveData { itemID = item.ID, SlotIndex = i });
            }
        }

        return EQSaveData;
    }


    public void SetInventoryItem(List<InventorySaveData> ISaveData)
    {
        foreach (Transform child in invetoryPanel.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < SlotsCount; i++)
        {
            Instantiate(SlotPrefab, invetoryPanel.transform);
        }

        foreach (InventorySaveData data in ISaveData)
        {
            if (data.SlotIndex < SlotsCount)
            {
                SlotsScript slot = invetoryPanel.transform.GetChild(data.SlotIndex).GetComponent<SlotsScript>();
                GameObject itemprefab = ItemCatalog.GetItemPrefab(data.itemID);

                if (itemprefab != null)
                {
                    GameObject item = Instantiate(itemprefab, slot.transform);
                    slot.CurrentItem = item;
                    item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                }
            }
        }
    }
    public void SetInventoryItem()//default
    {
        foreach (Transform child in invetoryPanel.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < SlotsCount; i++)
        {
            Instantiate(SlotPrefab, invetoryPanel.transform);
        }

    }

    public void SetEQItem(List<EQSaveData> EQSaveData)
    {

        for (int i = 0; i < EQSaveData.Count; i++)
        {
            SlotsScript slot = EQPanels[i].GetComponent<SlotsScript>();
            GameObject itemprefab = ItemCatalog.GetItemPrefab(EQSaveData[i].itemID);

            if (itemprefab != null)
            {
                GameObject item = Instantiate(itemprefab, slot.transform);
                slot.CurrentItem = item;
                item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;        
            }
        }  
    }
}
