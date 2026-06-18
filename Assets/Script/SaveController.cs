using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveController : MonoBehaviour
{
    private string SaveLocation;
    private string SaveFileName = "SaveData.jason";
    private InventoryController inventoryController;

    private string DefaultName = "?????";
    public static string LoadPlayerName = "You aint suppose to see this";

    [SerializeField] private string StartMapBound = "Graveyard";
    [SerializeField] private GameObject StartPlayerPos;

    void Start()
    {
        SaveLocation = Path.Combine(Application.persistentDataPath, SaveFileName);
        inventoryController =FindAnyObjectByType<InventoryController>();
        LoadGame();
    }

    public void SaveGame()
    {
        Debug.Log("saving?");

        SaveData SaveData = new SaveData
        {
            PlayerPos = GameObject.FindGameObjectWithTag("Player").transform.position,
            MapBound = FindObjectsByType<CinemachineConfiner2D>(FindObjectsSortMode.None)[0].BoundingShape2D.name,
            InventorySaveData = inventoryController.GetInventory(),
            EQSaveData = inventoryController.GetEQ()
        };

        File.WriteAllText(SaveLocation, JsonUtility.ToJson(SaveData));

    }

    public void LoadGame()
    {
        if (File.Exists(SaveLocation) && STARTScreenController.NewSaveFile == false)
        {

            SaveData SaveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(SaveLocation));

            //LoadPlayerName = SaveData.PlayerName;
            Vector3 LoadPlayerpos = SaveData.PlayerPos;
            string LoadMapBound = SaveData.MapBound;
            List<InventorySaveData> LoadInvetory = SaveData.InventorySaveData;
            List<EQSaveData> LoadEQ = SaveData.EQSaveData;

            GameObject.FindGameObjectWithTag("Player").transform.position = LoadPlayerpos;
            FindObjectsByType<CinemachineConfiner2D>(FindObjectsSortMode.None)[0].BoundingShape2D = GameObject.Find(LoadMapBound).GetComponent<PolygonCollider2D>();
            LoadPlayerName = DefaultName;//Delete after adding load from file          
            inventoryController.SetInventoryItem(LoadInvetory);
            inventoryController.SetEQItem(LoadEQ);

        }
        else // if no save data or newsavefile is false
        {
            //SaveGame();
            LoadPlayerName = DefaultName; // "?????"
     
            Vector3 LoadPlayerpos = StartPlayerPos.transform.position;
            string LoadMapBound = StartMapBound;

            GameObject.FindGameObjectWithTag("Player").transform.position = LoadPlayerpos;
            FindObjectsByType<CinemachineConfiner2D>(FindObjectsSortMode.None)[0].BoundingShape2D = GameObject.Find(LoadMapBound).GetComponent<PolygonCollider2D>();
            inventoryController.SetInventoryItem();

        }
    }

    public void ReturnToTitle()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartScreen");
    }
}
