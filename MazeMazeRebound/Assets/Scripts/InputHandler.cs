using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.EnhancedTouch;

public class InputHandler : MonoBehaviour
{
    public static InputHandler Instance { get; private set; }

    public event EventHandler OnFingerUp;
    public event EventHandler OnBallTouchOff;
    public event EventHandler OnObjectDeleted;
    public event EventHandler OnSlingshotTeleported;
    public event EventHandler OnStartShoot;

    [SerializeField]
    private GameObject slingshot;

    private bool isFingerOnScreen;
    private bool isBallTouch;

    public Vector3 startFingerPos;

    private GameObject draggedObject;  
    private Vector3 offset; 

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    private void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }

    void Update()
    {
        if (UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches.Count > 0)
        {
            if (!EventSystem.current.IsPointerOverGameObject() && Time.timeScale != 0)
            {
                if (UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches[0].phase == UnityEngine.InputSystem.TouchPhase.Began)
                {
                    startFingerPos = Camera.main.ScreenToWorldPoint(UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches[0].screenPosition);

                    RaycastHit2D hit = Physics2D.Raycast(startFingerPos, Vector2.zero);

                    if (hit.collider != null)
                    {
                        if (hit.collider.GetComponent<Slingshot>() && !GameManager.Instance.GetIsFingerActive() && !GameManager.Instance.GetIsHammerActive() && !GameManager.Instance.GetIsTeleportActive())
                        {
                            OnStartShoot?.Invoke(this, EventArgs.Empty);
                            isFingerOnScreen = true;
                        }
                        else if (hit.collider.GetComponent<Ball>())
                        {
                            isBallTouch = true;
                        }
                        else if (hit.collider.GetComponent<DraggableObject>() && GameManager.Instance.GetIsFingerActive())
                        {
                            draggedObject = hit.collider.gameObject;
                            offset = draggedObject.transform.position - Camera.main.ScreenToWorldPoint(UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches[0].screenPosition);
                        }
                        else if (hit.collider.GetComponent<DraggableObject>() && GameManager.Instance.GetIsHammerActive())
                        {
                            Destroy(hit.collider.gameObject);

                            OnObjectDeleted?.Invoke(this, EventArgs.Empty);
                        }

                    }
                    else if(GameManager.Instance.GetIsTeleportActive())
                    {

                        Vector3 newPosition = Camera.main.ScreenToWorldPoint(UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches[0].screenPosition);
                        slingshot.transform.position = new Vector3(newPosition.x, newPosition.y, slingshot.transform.position.z);
                        OnSlingshotTeleported?.Invoke(this, EventArgs.Empty);
                    }
                }
                else if (UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches[0].phase == UnityEngine.InputSystem.TouchPhase.Moved)
                {
                    if (draggedObject != null)
                    {
                        Vector3 newPosition = Camera.main.ScreenToWorldPoint(UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches[0].screenPosition) + offset;
                        draggedObject.transform.position = new Vector3(newPosition.x, newPosition.y, draggedObject.transform.position.z);
                    }
                }
                else if (UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches[0].phase == UnityEngine.InputSystem.TouchPhase.Ended)
                {
                    if (isFingerOnScreen)
                        OnFingerUp?.Invoke(this, EventArgs.Empty);
                    if (isBallTouch)
                        OnBallTouchOff?.Invoke(this, EventArgs.Empty);

                    isFingerOnScreen = false;
                    isBallTouch = false;
                    draggedObject = null;
                    GameManager.Instance.SetTeleport(false);
                    GameManager.Instance.SetHammer(false);
                    GameManager.Instance.SetFinger(false);
                }
            }
        }
    }

    public bool GetIsFingerOnScreen() { return isFingerOnScreen; }

    public bool GetIsBallTouch() { return isBallTouch; }

    public UnityEngine.InputSystem.EnhancedTouch.Touch GetFinger() { return UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches[0]; }
}
