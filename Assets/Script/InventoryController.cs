using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private GameObject invetoryPanel;
    [SerializeField] private GameObject SlotPrefab;
    [SerializeField] private int SlotsCount;
    [SerializeField] private GameObject[] itemPrefabs;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (SlotsCount == 0)
        {
            SlotsCount = 1;
            Debug.Log("No Slot Count entered");
        }

        for (int i = 0; i < SlotsCount; i++)
        {
            SlotsScript slot = Instantiate(SlotPrefab, invetoryPanel.transform).GetComponent<SlotsScript>();

            if (i < itemPrefabs.Length)
            {
                GameObject item = Instantiate(itemPrefabs[i], slot.transform);
                item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                slot.CurrentItem = item;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
