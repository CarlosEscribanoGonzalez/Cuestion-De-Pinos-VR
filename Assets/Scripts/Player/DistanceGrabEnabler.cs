using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class DistanceGrabEnabler : MonoBehaviour
{
    private InputAction myAction;
    [Space][SerializeField] private InputActionAsset myActionsAsset;
    private GameObject distanceGrab;
    private bool isEnabled = false;
    private bool grabbed = false;

    private void Awake()
    {
        distanceGrab = GameObject.Find("Distance Grab");
        distanceGrab.SetActive(false);
    }

    void Start()
    {
        myAction = myActionsAsset.FindAction("XRI RightHand/DistanceGrab");
        myAction.performed += OnMyAction;
    }

    private void OnMyAction(InputAction.CallbackContext context)
    {
        isEnabled = !isEnabled;
        if (grabbed)
        {
            return;
        }
        distanceGrab.SetActive(!distanceGrab.activeSelf);
    }

    public void ObjectGrabbed()
    {
        grabbed = true;
    }

    public void Deactivate()
    {
        if (!isEnabled) distanceGrab.SetActive(false);
        grabbed = false;
    }
}
