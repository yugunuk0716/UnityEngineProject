using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropArea : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler
{
    public static List<DropArea> dropAreas;

    public delegate void ObjectLiftEvent(DropArea area, GameObject gameObject);
    public event ObjectLiftEvent onLifted;

    public delegate void ObjectDropEvent(DropArea area, GameObject gameObject);
    public event ObjectDropEvent onDropped;

    public delegate void ObjectHoverEnterEvent(DropArea area, GameObject gameObject);
    public event ObjectHoverEnterEvent onHoverEnter;

    public delegate void ObjectHoverExitEvent(DropArea area, GameObject gameObject);
    public event ObjectHoverExitEvent onHoverExit;

    private void Awake()
    {
        dropAreas = dropAreas ?? new List<DropArea>();
        dropAreas.Add(this);
        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        onLifted += LiftTest;
        onDropped += DropTest;
        onHoverEnter += HoverEnterTest;
        onHoverExit += HoverExitTest;

    }
    private void OnDisable()
    {
        onLifted -= LiftTest;
        onDropped -= DropTest;
        onHoverEnter -= HoverEnterTest;
        onHoverExit += HoverExitTest;
    }

    public void LiftTest(DropArea area, GameObject gameObject)
    {
        Debug.Log(this.gameObject.name + "Object Lifted : " + gameObject.name);
    }
    public void DropTest(DropArea area, GameObject gameObject)
    {
        Debug.Log(this.gameObject.name + "Object Dropped : " + gameObject.name);
    }
    public void HoverEnterTest (DropArea area, GameObject gameObject)
    {
        Debug.Log(this.gameObject.name + "Object Hover Enter : " + gameObject.name);
    }
    public void HoverExitTest(DropArea area, GameObject gameObject)
    {
        Debug.Log(this.gameObject.name + "Object Hover Exit : " + gameObject.name);
    }

    public void TriggerOnLift(DropItem item) 
    {
        onLifted(this, item.gameObject);
    }
    public void TriggerOnDrop(DropItem item) 
    {
        item.SetDroppedArea(this);
        onDropped(this, item.gameObject);
    }
    public void TriggerHoverEnter(GameObject item)
    {
        onHoverEnter(this, item.gameObject);
    }
    public void TriggerHoverExit(GameObject item)
    {
        onHoverExit(this, item.gameObject);
    }






    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        var gameObject = eventData.pointerDrag;
        if (gameObject == null) return;

        TriggerHoverEnter(gameObject);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        var gameObject = eventData.pointerDrag;
        if (gameObject == null) return;

        TriggerHoverExit(gameObject);
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject gameObject = eventData.selectedObject;
        if (gameObject == null) return;

        var draggable = gameObject.GetComponent<DropItem>();
        if (draggable == null) return;

        Debug.Log("item dropped : " + draggable.gameObject.name);
        TriggerOnDrop(draggable);
    }

    public static void SetDropArea(bool enable) 
    {
        foreach (var area in dropAreas)
        {
            area.gameObject.SetActive(enable);
        }
    }
}
