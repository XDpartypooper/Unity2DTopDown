using UnityEngine;
using UnityEngine.UI;

public class TabController : MonoBehaviour
{
    public Image[] TabImages;
    public GameObject[] page;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ActivateTab(0);
    }

    // Update is called once per frame
    public void ActivateTab(int TabNumber)
    {
        for (int i = 0; i < page.Length; i++)
        {
            page[i].SetActive(false);
            TabImages[i].color = Color.gray;
        }
        page[TabNumber].SetActive(true);
        TabImages[TabNumber].color = Color.white;
    }
}
