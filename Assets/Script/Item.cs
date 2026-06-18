using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public int EquipmentSlotType;
    public int ID;

    public string Name;

    [Header("Stats")]
    public int Strength;
    public int Defense;
    public int Health;
    public virtual void pickup()
    {
        Sprite ItemIcon = GetComponent<Image>().sprite;
        if (PickupUI.Instance != null)
        {
            PickupUI.Instance.ShowItemPickup(Name, ItemIcon);
        }
    }


}
