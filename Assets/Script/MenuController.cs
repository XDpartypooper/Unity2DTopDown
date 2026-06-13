using UnityEngine;
using UnityEngine.InputSystem;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject MenuUI;
    void Start()
    {

        MenuUI.SetActive(false);
    }

    public void OpenMenu(InputAction.CallbackContext context)
    {
        Debug.Log("button pressed :"+ context);
        MenuUI.SetActive(!MenuUI.activeSelf);
        //need to stop time? or sumthing
    }
}
