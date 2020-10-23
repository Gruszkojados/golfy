using System;
using UnityEngine;

namespace golf {
public class TouchInput : MonoBehaviour
{
    public static event Action OnTouch = () => { };
    public static event Action<float> OnDrag = (_) => { };

    [SerializeField] float scrollDistance = 1f;
    [SerializeField] float dragForce = .1f;
    
    bool isDragging;
    float lastFingerX;

    public float myDragForce = 2f;
    
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            Click();
            isDragging = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            return;
        }
        
        MouseDrag();
#else
        if (Input.touchCount != 1)
        {
            isDragging = false;
            return;
        }

        Touch();
        Rotate();

        isDragging = true;
#endif
    }

    void Touch()
    {

        if (isDragging)
        {
            return;
        }
        
        OnTouch.Invoke();
    }

    void Rotate()
    {   
        Debug.Log("Rotate TouchInput");
        var touch = Input.GetTouch(0);
        var touchX = touch.position.x;
        
        if (!isDragging)
        {
            lastFingerX = touchX;
            return;
        }

        var dragAmount = lastFingerX - touchX;
        
        if (Mathf.Abs(dragAmount) >= scrollDistance)
        {
            // scrolling!
            lastFingerX = touchX;

            OnDrag.Invoke(dragAmount * dragForce * myDragForce);
        }
    }

    void Click()
    {
        Debug.Log("Click");
        OnTouch.Invoke();
    }

    void MouseDrag()
    {
        var touchX = Input.mousePosition.x;
        
        if (!isDragging)
        {
            lastFingerX = touchX;
            return;
        }
        
        var dragAmount = lastFingerX - touchX;
        
        if (Mathf.Abs(dragAmount) >= scrollDistance)
        {
            // scrolling!
            lastFingerX = touchX;

            OnDrag.Invoke(dragAmount * dragForce * myDragForce);
        }
    }
}
}