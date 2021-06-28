using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public DropArea topArea;
    public DropArea bottomArea;

    public RectTransform topRectParent;
    public RectTransform bottomRectParent;
    public RectTransform hoverRectParent;
    private void Awake()
    {
        topArea.onLifted += LiftedFromTop;
        topArea.onDropped += DroppedFromTop;

        bottomArea.onLifted += LiftedFromBottom;
        bottomArea.onDropped += DroppedFromBottom;
    }



    private void LiftedFromTop(DropArea area, GameObject gameObject) 
    {
        gameObject.transform.SetParent(hoverRectParent, true);
    }
    private void DroppedFromTop(DropArea area, GameObject gameObject)
    {
        gameObject.transform.SetParent(topRectParent, true);
    }
    private void LiftedFromBottom(DropArea area, GameObject gameObject)
    {
        gameObject.transform.SetParent(hoverRectParent, true);
    }
    private void DroppedFromBottom(DropArea area, GameObject gameObject)
    {
        gameObject.transform.SetParent(bottomRectParent, true);
    }
    private void SetDropArea(bool active) 
    {
        topArea.gameObject.SetActive(active);
        bottomArea.gameObject.SetActive(active);
    }
}
