using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragScript : MonoBehaviour,IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]  private Transform OriginalParent;
    [SerializeField]  CanvasGroup CG;

    void Start()
    {
        CG=this.GetComponentInParent<CanvasGroup>();
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
       transform.position= eventData.position;//follow mouse
    }

    
    public void OnEndDrag(PointerEventData eventData)
    {
        CG.blocksRaycasts = true;
        CG.alpha = 1f;
        bool isEQ = false;

        SlotsScript Dropslot = eventData.pointerEnter?.GetComponent<SlotsScript>();
        SlotsScript originalSlot = OriginalParent.GetComponent<SlotsScript>();

        EQSlotScript OrignalEQDropslot = OriginalParent.GetComponent<EQSlotScript>();
        EQSlotScript EQDropslot = eventData.pointerEnter?.GetComponent<EQSlotScript>();

        var originalParent = OrignalEQDropslot?.transform ?? originalSlot?.transform;

        if (OrignalEQDropslot != null)
        {
            isEQ = OrignalEQDropslot.CurrentEquipment.GetComponent<Item>().IsEquipment;
        }
        else if (originalSlot != null)
        {
            isEQ = originalSlot.CurrentItem.GetComponent<Item>().IsEquipment;
        }

      
        if (Dropslot == null)// if the slot is empty
        {
            GameObject dropitem = eventData.pointerEnter;
            if (dropitem != null)
            {
                Dropslot = dropitem.GetComponentInParent<SlotsScript>();
            }
        }
        else if (EQDropslot != null)
        {
            GameObject dropitem = eventData.pointerEnter;
            if (originalSlot.CurrentItem.GetComponent<Item>().IsEquipment)//if DRAGGED object is equipment
            {
                EQDropslot = dropitem.GetComponentInParent<EQSlotScript>();
            }
        }

        if (Dropslot != null)//main slots
        {
            //Dropslot.CurrentItem.transform.SetParent(originalParent);       
            if (Dropslot.CurrentItem != null)
            {
                if (originalSlot != null)
                {
                    originalSlot.CurrentItem = Dropslot.CurrentItem;
                }
                else
                {
                    EQDropslot.CurrentEquipment = Dropslot.CurrentItem;
                }
                Dropslot.CurrentItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            }
            else
            {
                if (originalSlot != null)
                {
                    originalSlot.CurrentItem = null;
                }
                else if(EQDropslot!= null)
                {
                    EQDropslot.CurrentEquipment = null;
                }
               
            }

            transform.SetParent(Dropslot.transform);
            Dropslot.CurrentItem = this.gameObject;
        }
        else if (EQDropslot != null && isEQ)//eq
        {
            //originalParent
            if (EQDropslot.CurrentEquipment != null)
            {
                if (originalSlot != null)
                {                  
                    EQDropslot.CurrentEquipment.transform.SetParent(originalParent);
                    originalSlot.CurrentItem = EQDropslot.CurrentEquipment;
                    EQDropslot.CurrentEquipment.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                }
                else if(EQDropslot != null)
                {
                    EQDropslot.CurrentEquipment.transform.SetParent(originalParent);
                    OrignalEQDropslot.CurrentEquipment = EQDropslot.CurrentEquipment;
                    EQDropslot.CurrentEquipment.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                }             
            }
            else
            {
                if (originalSlot != null)
                {
                    originalSlot.CurrentItem = null;
                }
                else if (EQDropslot != null)
                {
                    EQDropslot.CurrentEquipment = null;
                }
            }

            transform.SetParent(EQDropslot.transform);
            EQDropslot.CurrentEquipment = this.gameObject;
        }
        else
        {
            transform.SetParent(OriginalParent);
        }
       
        GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }
}




