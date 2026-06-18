using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject MenuUI;
    [SerializeField] private bool isPaused = false;

    void Start()
    { 
        GameObject.Find("UI").SetActive(true);// in case UI is disabled in editor
        //GameObject.Find("PlayerNameMenu").GetComponent<TextMeshProUGUI>().text = "Name:" + SaveController.LoadPlayerName;
        MenuUI.SetActive(false);

    }



    public void OpenMenu(InputAction.CallbackContext context)
    {
        Debug.Log("button pressed :"+ context);
        MenuUI.SetActive(!MenuUI.activeSelf);

        isPaused = !isPaused;

        if (isPaused)
        {           
            Time.timeScale = 0f;
        }
        else
        {        
            Time.timeScale = 1f;
        }
    }
}
