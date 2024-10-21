using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class Menu : MonoBehaviour
{
    private GameObject player;
    private CameraMovement cameraOffset;
    [Header("Iluminacion: ")]
    [SerializeField] private Slider brightSlider;
    [Header("Audio: ")]
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private AudioMixer audioMixer;
    [Header("Par�metros de camara: ")]
    [SerializeField] private Slider tunnelingAlphaSlider;
    [SerializeField] private Slider tunnelingRadiusSlider;
    [SerializeField] private TMP_Dropdown cameraMovement;
    [SerializeField] private Slider cameraOffsetSlider;
    private TunnelingVignetteController tunnelingController;
    [Header("Tipo de giro: ")]
    [SerializeField] private TMP_Dropdown turnType;
    [SerializeField] private Slider snapAmountSlider;
    [SerializeField] private Slider continuousSpeedSlider;
    

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        cameraOffset = GameObject.FindObjectOfType<CameraMovement>();
        tunnelingController = GameObject.FindObjectOfType<TunnelingVignetteController>();
    }

    void Start()
    {
        //Sliders:
        brightSlider.onValueChanged.AddListener((v) => ChangeIllumination(v));
        volumeSlider.onValueChanged.AddListener((v) => audioMixer.SetFloat("Volume", Mathf.Log10(v) * 20));
        tunnelingAlphaSlider.onValueChanged.AddListener((v) => tunnelingController.defaultParameters.vignetteColor = new Color(0.0f, 0.0f, 0.0f, v));
        tunnelingRadiusSlider.onValueChanged.AddListener((v) => tunnelingController.defaultParameters.apertureSize = v);
        snapAmountSlider.onValueChanged.AddListener((v) => player.GetComponent<ActionBasedSnapTurnProvider>().turnAmount = v);
        continuousSpeedSlider.onValueChanged.AddListener((v) => player.GetComponent<ActionBasedContinuousTurnProvider>().turnSpeed = v);
        cameraOffsetSlider.onValueChanged.AddListener((v) => { cameraOffset.SetOffset(v); cameraOffset.ResetPosition(); });
        //Dropdowns:
        cameraMovement.onValueChanged.AddListener((v) => ToggleCameraMovement(v));
        turnType.onValueChanged.AddListener((v) => ChangeTurnType());
    }

    private void ChangeIllumination(float value)
    {
        Light[] lights = GameObject.FindObjectsOfType<Light>();
        foreach (Light light in lights)
        {
            light.intensity = value;
        }
    }

    private void ToggleCameraMovement(int value)
    {
        if (value == 0)
        {
            cameraOffset.SetActive(true);
            cameraOffsetSlider.transform.parent.gameObject.SetActive(true);
        }
        else
        {
            cameraOffset.SetActive(false);
            cameraOffset.ResetPosition();
            cameraOffsetSlider.transform.parent.gameObject.SetActive(false);
        }
    }

    private void ChangeTurnType()
    {
        player.GetComponent<ActionBasedContinuousTurnProvider>().enabled = !player.GetComponent<ActionBasedContinuousTurnProvider>().enabled;
        player.GetComponent<ActionBasedSnapTurnProvider>().enabled = !player.GetComponent<ActionBasedSnapTurnProvider>().enabled;
        snapAmountSlider.transform.parent.gameObject.SetActive(!snapAmountSlider.transform.parent.gameObject.activeSelf);
        continuousSpeedSlider.transform.parent.gameObject.SetActive(!continuousSpeedSlider.transform.parent.gameObject.activeSelf);
    }
}

