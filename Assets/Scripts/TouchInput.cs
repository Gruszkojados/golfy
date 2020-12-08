using System;
using Cinemachine;
using UnityEngine;

namespace golf {
    public class TouchInput : MonoBehaviour
    {
        public static event Action<float> OnDrag = (_) => { };
        [SerializeField] float scrollDistance = 1f;
        [SerializeField] float dragForce = .1f;
        CinemachineVirtualCamera cinemachineCamera;
        public GameObject cameraGameObject;
        bool isDragging;
        float lastFingerX;
        float lastDistance;
        Vector2 lastTouch;
        public float myDragForce = 2f;

        private void Awake() {
            lastTouch = new Vector2(0f, 0f);
            cinemachineCamera = cameraGameObject.GetComponent<CinemachineVirtualCamera>();
            cinemachineCamera.m_Lens.OrthographicSize = 60f;    
        }
        void Update() {
            #if UNITY_EDITOR // Controll for development on PC
                    if (Input.GetMouseButtonDown(0))
                    {
                        isDragging = true;
                    }

                    if (Input.GetMouseButtonUp(0))
                    {
                        isDragging = false;
                        return;
                    }
                    MouseDrag(Input.mousePosition.x);
                    
            #else // Controll for mobile
                    if(Input.touchCount == 1) {
                        MouseDrag(Input.GetTouch(0).position.x);
                        isDragging = true;
                    } else {
                        isDragging = false;
                        return;
                    }
            #endif
        }
        public void OnValueChanged(float newValue) { // Slider for camera zoom
            if(isDragging) {
                isDragging = false;
            }
            cinemachineCamera.m_Lens.OrthographicSize = newValue;
        }
        void MouseDrag(float positionX) {
            var touchX = positionX;
            
            if (!isDragging)
            {
                lastFingerX = touchX;
                return;
            }
            
            var dragAmount = lastFingerX - touchX;
            
            if (Mathf.Abs(dragAmount) >= scrollDistance)
            {
                // Sscrolling
                lastFingerX = touchX;
                OnDrag.Invoke(dragAmount * dragForce * myDragForce);
            }
        }
    }
}

        

