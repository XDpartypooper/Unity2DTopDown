using System.IO;
using Unity.Cinemachine;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class STARTScreenController : MonoBehaviour
{
    private string SaveLocation;
    private string SaveFileName = "SaveData.jason";
    public static bool NewSaveFile = false;

    [SerializeField] private GameObject LoadGameButton;
    [SerializeField] private bool FileExsist = false;

    void Start()
    {
        SaveLocation = Path.Combine(Application.persistentDataPath, SaveFileName);
        FileExsist = File.Exists(SaveLocation);

        if (FileExsist)
        {
            LoadGameButton.SetActive(true);
        }
        else
        {
            LoadGameButton.SetActive(false);
        }
    }

    public void StartGame()//new game
    {
        NewSaveFile = true;
        SceneManager.LoadScene("GameScene");
        //loads new state
    }
    public void LoadGame()//load game
    {
        NewSaveFile = false;
        SceneManager.LoadScene("GameScene");
        //load from save file
    }
}
