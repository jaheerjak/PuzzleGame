using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MergePiece : MonoBehaviour,IDragHandler,IBeginDragHandler,IEndDragHandler
{
    [SerializeField] Image sprite;
    [SerializeField] Transform dragObj;
    public int id;
    [SerializeField] RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private Vector3 startPosition;

    private void Awake()
    {
       //rectTransform = GetComponent<RectTransform>();
       canvas = GetComponentInParent<Canvas>();
       // canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = rectTransform.transform.position;
        //canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //canvasGroup.blocksRaycasts = true;
        // You can implement dropping logic here, such as snapping the object back to its initial position if not dropped onto a valid target
        rectTransform.transform.position = startPosition;
    }
}
