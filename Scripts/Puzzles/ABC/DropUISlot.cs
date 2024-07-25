using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropUISlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,IDropHandler
{
    public int id;
    public bool isDropped;
    public GameObject activeTileObj;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            RectTransform draggedTransform = eventData.pointerDrag.GetComponent<RectTransform>();
            if (draggedTransform != null)
            {
                transform.localScale = new Vector3(1.2f, 1.2f, 1.2f); // Scale the object when it touches the slot
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            RectTransform draggedTransform = eventData.pointerDrag.GetComponent<RectTransform>();
            if (draggedTransform != null)
            {
                transform.localScale = new Vector3(1f, 1f, 1f); // Reset scale when it exits the slot
            }
        }
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (isDropped) return;
        if (eventData.pointerDrag != null)
        {
            KidDragnDrop dropped = eventData.pointerDrag.GetComponent<KidDragnDrop>();
            if (dropped != null)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
                Debug.Log("Dropped");
                if (id == dropped.id)
                {
                    isDropped = true;

                    activeTileObj.SetActive(true);
                    dropped.OnDropTile();
                    KidPuzzleManager.Instance.TilePlaced();
                }
            }
        }
    }
}
