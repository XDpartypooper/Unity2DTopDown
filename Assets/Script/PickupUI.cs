using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PickupUI : MonoBehaviour
{
    public static PickupUI Instance { get; private set; }

    public GameObject popupPrefab;
    public int maxPopup = 5;
    public float popupDuration = 3f;

    private readonly Queue<GameObject> activepopups = new();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log(" too much UI appearing");
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    public void ShowItemPickup(string itemName, Sprite itemIcon)
    {
        GameObject newPopup = Instantiate(popupPrefab, transform);
        newPopup.GetComponentInChildren<TextMeshProUGUI>().text = itemName;

        Image itemImage = newPopup.transform.Find("ItemIcon")?.GetComponent<Image>();
        if (itemImage)
        {
            itemImage.sprite = itemIcon;
        }

        activepopups.Enqueue(newPopup);
        if (activepopups.Count > maxPopup)
        {
            Destroy(activepopups.Dequeue());
        }

        StartCoroutine(FadeOutAndDestroy(newPopup));
    }

    private IEnumerator FadeOutAndDestroy(GameObject Popup)
    {
        yield return new WaitForSeconds(popupDuration);
        if(Popup==null)yield break;

        CanvasGroup CG = Popup.GetComponent<CanvasGroup>();

        for (float timepassed = 0f; timepassed < 1f; timepassed += Time.deltaTime)
        {
            if (Popup == null) yield break;
            CG.alpha = 1f - timepassed;
            yield return null;
        }
        Destroy(Popup);
    }
}
