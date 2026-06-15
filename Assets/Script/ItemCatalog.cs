using System.Collections.Generic;
using UnityEngine;

public class ItemCatalog : MonoBehaviour
{
    public List<Item> itemPrefabs;
    private Dictionary<int, GameObject> itemCatalog;

    private void Awake()
    {
        itemCatalog = new Dictionary<int, GameObject>();

        for (int i = 0; i < itemPrefabs.Count; i++)
        {
            if (itemPrefabs[i] != null)
            {
                itemPrefabs[i].ID = i + 1;
            }

        }

        foreach (Item item in itemPrefabs)
        {
            itemCatalog[item.ID] = item.gameObject;
        }
    }

    public GameObject GetItemPrefab(int itemID)
    {
        itemCatalog.TryGetValue(itemID, out GameObject prefab);

        if (prefab == null)
        {
            Debug.LogWarning($"No such ID : {itemID}");
        }
        return prefab;
    }
}
