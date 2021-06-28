using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropItem : MonoBehaviour,IPointerDownHandler,IPointerUpHandler,IDragHandler, IEndDragHandler//,IBeginDragHandler
{
    public delegate void DropItemMoveStartEvent(DropItem item);
    public event DropItemMoveStartEvent onMoveStart;

    public delegate void DropItemMoveEndEvent(DropItem item);
    public event DropItemMoveEndEvent onMoveEnd;

    public delegate void DropItemNoEvent(DropItem item);
    public event DropItemNoEvent onNothing;

    private RectTransform rectTransform;
    private RectTransform clampRectTransform;

    private Vector3 origingalWorldPos;
    private Vector3 originalRectWorldPos;

    private Vector3 minWorldPosition;
    private Vector3 maxWorldPosition;

    public DropArea droppedArea;
    public DropArea prevDropArea;

 
   

    public float moveSpeed =5;


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        clampRectTransform = rectTransform.root.GetComponent<RectTransform>();
    }

    public void SetDroppedArea(DropArea area)
    {
        this.droppedArea = area;

    }

    public void OnPointerDown(PointerEventData eventData) 
    {
        originalRectWorldPos = rectTransform.position;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(
            clampRectTransform, eventData.position, eventData.pressEventCamera, out origingalWorldPos);
    }
    public void OnPointerUp(PointerEventData eventData) 
    {
        
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (onMoveStart != null) onMoveStart(this);
        DropArea.SetDropArea(true);

        if (droppedArea != null) droppedArea.TriggerOnLift(this);
        prevDropArea = droppedArea;
        droppedArea = null;

        Rect clamp = new Rect(Vector2.zero, clampRectTransform.rect.size);
        Vector3 minPosition = clamp.min - rectTransform.rect.min;
        Vector3 maxPosition = clamp.max - rectTransform.rect.max;

        RectTransformUtility.ScreenPointToWorldPointInRectangle(clampRectTransform, minPosition, eventData.pressEventCamera, out minWorldPosition);
        RectTransformUtility.ScreenPointToWorldPointInRectangle(clampRectTransform, maxPosition, eventData.pressEventCamera, out maxWorldPosition);

        Debug.Log(minWorldPosition + "/" + maxWorldPosition);

    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 worldPointerPos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(clampRectTransform, eventData.position, eventData.pressEventCamera, out worldPointerPos)) 
        {
            Vector3 offsetToOriginal = worldPointerPos - origingalWorldPos;
            rectTransform.position = origingalWorldPos + offsetToOriginal;
        }

        Vector3 worldPos = rectTransform.position;
        worldPos.x = Mathf.Clamp(rectTransform.position.x, minWorldPosition.x, maxWorldPosition.x);
        worldPos.y = Mathf.Clamp(rectTransform.position.y, minWorldPosition.y, maxWorldPosition.y);
        rectTransform.position = worldPos;
    }

    public void OnEndDrag(PointerEventData eventData) 
    {
        DropArea.SetDropArea(false);
        if (onMoveEnd != null) onMoveEnd(this);

        bool noEvent = true;
        foreach (var go in eventData.hovered)
        {
            var dropArea = go.GetComponent<DropArea>();
            if (dropArea != null) 
            {
                noEvent = false;
                break;
            }
        }
        if (noEvent) 
        {
            if (onNothing != null) onNothing(this);
        }

    }


    private void Update()
    {
        
    }

}
