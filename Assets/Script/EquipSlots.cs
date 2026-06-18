using Unity.VisualScripting;
using UnityEngine;

public class EquipSlots : MonoBehaviour
{
    public SlotsScript EQslot;
    public Item EQItem;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EQslot = GetComponent<SlotsScript>();
        if (EQslot.CurrentItem != null)//if current item in slot
        {
            EQItem = GetComponentInChildren<Item>();
        }
       
    }

    private void OnTransformChildrenChanged()//When the slot add or removed
    {
            PlayerStats playerStats = FindFirstObjectByType<PlayerStats>();

        if (transform.childCount > 0)// if it becomes filled
        {
            EQItem = GetComponentInChildren<Item>();
            playerStats.RecalculateStats();
        }
        else // if it becomes emty
        {
            EQItem = null;
            playerStats.RecalculateStats();
        }
        
    }
}
