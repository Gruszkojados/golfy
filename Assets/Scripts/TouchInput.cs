using System;
using Cinemachine;
using UnityEngine;

namespace golf {
    public class TouchInput : MonoBehaviour
    {
        public static event Action OnTouch = () => { };
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
                    if(Input.touchCount == 1) {
                        lastTouch = Input.GetTouch(0).position;
                        Touch();
                        Rotate();
                        isDragging = true;
                    } else if (Input.touchCount == 2) {
                        // TODO
                        // CameraZoom(); <--- zoom na dwa palce
                    } else {
                        isDragging = false;
                        return;
                    }
            #endif
        }

        void Touch() {

            if (isDragging)
            {
                return;
            }
            
            OnTouch.Invoke();
        }

        void CameraZoom() {
            var touch = Input.GetTouch(1).position;

            if (Math.Abs(Vector3.Distance(touch, lastTouch)) >= lastDistance) {
                if(cinemachineCamera.m_Lens.OrthographicSize <= 200f) {
                    lastDistance = Vector3.Distance(touch, lastTouch);
                    cinemachineCamera.m_Lens.OrthographicSize = lastDistance;
                }
            } else {
                if(cinemachineCamera.m_Lens.OrthographicSize >= 40f) {
                    lastDistance = Vector3.Distance(touch, lastTouch);
                    cinemachineCamera.m_Lens.OrthographicSize = lastDistance;
                }
            } 
        }

        public void OnValueChanged(float newValue) {
            if(isDragging) {
                isDragging = false;
            }
            cinemachineCamera.m_Lens.OrthographicSize = newValue;
        }

        void Rotate() {   
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

        void Click() {
            Debug.Log("Click TouchInput");
            OnTouch.Invoke();
        }

        void MouseDrag() {
            Debug.Log("MouseDrag TouchInput");
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