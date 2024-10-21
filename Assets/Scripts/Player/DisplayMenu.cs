using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DisplayMenu : MonoBehaviour
{
    private InputAction myAction;
    [Space][SerializeField] private InputActionAsset myActionsAsset;
    private GameObject menu;
    private GameObject menuInteractor;
    private static GameObject Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            Instance = this.gameObject;
        }
        else Destroy(this.gameObject);
        menu = GameObject.Find("UI");
        menuInteractor = GameObject.Find("UI Interactor");
        menu.SetActive(false);
        menuInteractor.SetActive(false);
    }

    void Start()
    {
        myAction = myActionsAsset.FindAction("XRI LeftHand/Pause");
        myAction.performed += OnMyAction;
        Light[] lights = GameObject.FindObjectsOfType<Light>();
    }

    private void OnMyAction(InputAction.CallbackContext context)
    {
        menu.SetActive(!menu.activeSelf);
        menuInteractor.SetActive(!menuInteractor.activeSelf);
    }
}