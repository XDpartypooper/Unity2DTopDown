using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    //player location
    public Vector3 PlayerPos;
    public string MapBound;//mapboundary
    public List<InventorySaveData> InventorySaveData; //Inventory
    public List<EQSaveData> EQSaveData; //Inventory


}
