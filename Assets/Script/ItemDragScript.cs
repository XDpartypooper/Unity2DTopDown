using System.Linq;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDragScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Transform OriginalParent;
    [SerializeField] CanvasGroup CG;

    private float MinDropDistance = 2f;
    private float MaxDropDistance = 3f;

    void Start()
    {
        CG = this.GetComponentInParent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        OriginalParent = transform.parent;
        transform.SetParent(transform.root);
        CG.blocksRaycasts = false;
        CG.alpha = 0.6f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;//follow mouse
    }

    [SerializeField] SlotsScript originalSlot;
    public void OnEndDrag(PointerEventData eventData)
    {
        CG.blocksRaycasts = true;
        CG.alpha = 1f;

        SlotsScript Dropslot = eventData.pointerEnter?.GetComponent<SlotsScript>();
        originalSlot = OriginalParent.GetComponent<SlotsScript>();
        GameObject dropitem = eventData.pointerEnter;


        if (Dropslot == null)// if the slot is empty
        {
            dropitem = eventData.pointerEnter;
            if (dropitem != null)
            {
                Dropslot = dropitem.GetComponentInParent<SlotsScript>();
            }

        }

        if (Dropslot != null)// if SLot are detected
        {
            if (Dropslot.WeaponSlotType == 0)
            {
                if (Dropslot.CurrentItem != null)// if current slot has an item
                {
                    Dropslot.CurrentItem.transform.SetParent(OriginalParent);
                    originalSlot.CurrentItem = Dropslot.CurrentItem;
                    Dropslot.CurrentItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                }
                else
                {
                    originalSlot.CurrentItem = null;
                }

                transform.SetParent(Dropslot.transform);
                Dropslot.CurrentItem = this.gameObject;
            }
            else if (Dropslot.WeaponSlotType == this.GetComponent<Item>().EquipmentSlotType)
            {

                if (Dropslot.CurrentItem != null)// if current slot has an item
                {
                    Dropslot.CurrentItem.transform.SetParent(OriginalParent);
                    originalSlot.CurrentItem = Dropslot.CurrentItem;
                    Dropslot.CurrentItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                }
                else
                {
                    originalSlot.CurrentItem = null;
                }

                transform.SetParent(Dropslot.transform);
                Dropslot.CurrentItem = this.gameObject;
            }
            else
            {
                transform.SetParent(originalSlot.transform);
            }

        }
        else
        {
            if (!isinInventory(eventData.position))// if outside drop item
            {
                DropItem(originalSlot);
            }
            else
            {
                transform.SetParent(originalSlot.transform);
            }

        }
        GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }

    bool isinInventory(Vector2 MousePos)
    {
        RectTransform inventoryRect = OriginalParent.parent.GetComponent<RectTransform>();
        return RectTransformUtility.RectangleContainsScreenPoint(inventoryRect, MousePos);
    }

    void DropItem(SlotsScript originalSlot)
    {
        originalSlot.CurrentItem = null;

        Transform Player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (Player == null)
        {
            Debug.LogError("Missing Player");
            return;
        }

        Vector2 dropoffSet = Random.insideUnitCircle.normalized * Random.Range(MinDropDistance, MaxDropDistance);
        Vector2 DropPos = (Vector2)Player.position + dropoffSet;

        Instantiate(gameObject, DropPos, Quaternion.identity);

        Destroy(gameObject);
    }
}






