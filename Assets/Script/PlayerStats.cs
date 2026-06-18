using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int BaseStrength = 1;
    public int BaseDefense = 1;
    public int BaseHealth = 10;

    public int CurrStrength ;
    public int CurrDefense;
    public int CurrHealth;
    public int MaxHealth;


    public EquipSlots[] equipmentSlots;

    private void Start()
    {
        
        equipmentSlots = FindObjectsByType<EquipSlots>(FindObjectsSortMode.None);
        RecalculateStats();//update stats when starts
        if (MaxHealth == 0)
        {
            Debug.LogWarning("Failed to Get Recalucate Stats Or MaxHealth is 0 or less");
        }
        CurrHealth = MaxHealth;
    }


    public void RecalculateStats()
    {
        CurrStrength = BaseStrength;
        CurrDefense = BaseDefense;
        //CurrHealth = BaseHealth;// make this so it doesnt just fully heal everytime
        MaxHealth = BaseHealth;
        float healthPercentage = (float)CurrHealth/MaxHealth;

        foreach (EquipSlots slot in equipmentSlots)
        {
            if (slot.EQItem != null)
            {
                CurrStrength += slot.EQItem.Strength;
                CurrDefense += slot.EQItem.Defense;
                MaxHealth += slot.EQItem.Health;
                CurrHealth = Mathf.FloorToInt(MaxHealth * healthPercentage);
                if (CurrHealth < 1)
                {
                    CurrHealth = 1;//make sure player doesnt go to 0 or less
                }
            }
        }
    }

}
