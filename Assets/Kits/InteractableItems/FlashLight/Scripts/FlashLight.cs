using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using static Unity.VisualScripting.Member;

public class FlashLight : InteractableItem
{
    [Header("References")]
    [SerializeField] Light spotLight;
    [SerializeField] Transform hand;
    [SerializeField] InputManagerSO inputManager;
    [SerializeField] InputActionReference toggleLight;
    [SerializeField] InputActionReference focusLight;
    [SerializeField] GameplayUIManager instance;

    [Header(("Parameters"))]
    [SerializeField] float damage = 0.00001f;
    float raycastLength;
    [SerializeField] float focusIntensity = 5;
    float lightIntensity;
    [SerializeField] Color focusColor = Color.yellow;
    Color lightColor;
    [SerializeField] public float maxBattery = 100f;
    public float currentBattery;

    AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();

        raycastLength = spotLight.range;
        lightColor = spotLight.color;
        lightIntensity = spotLight.intensity;

        currentBattery = maxBattery;

        instance.SetBatteryVisibility(false);
    }

    private void OnEnable()
    {
        inputManager.OnToggleFlashLight += ToggleFlashLight;
        inputManager.OnStartFocus += StartFocus;
        inputManager.OnEndFocus += StopFocus;
    }

    protected override void Update()
    {
        base.Update();

        if (isFocusing)
        {
            Focusing();
        }
    }

    private void OnDisable()
    {
        inputManager.OnToggleFlashLight -= ToggleFlashLight;
        inputManager.OnStartFocus -= StartFocus;
        inputManager.OnEndFocus -= StopFocus;
    }

    private void ToggleFlashLight()
    {
        if (pickedUp)
        {
            source.PlayOneShot(source.clip);
            spotLight.enabled = !spotLight.enabled;
        }
    }

    bool isFocusing = false;
    void StartFocus()
    {
        if (spotLight.enabled && pickedUp && currentBattery > 0)
        {
            isFocusing = true;

            ChangeSpotLight();
        }
    }

    private void Focusing()
    {
        if (currentBattery > 0)
        {
            Ray ray = new Ray(transform.position, cam.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, raycastLength))
            {
                if (!hit.collider.CompareTag("Player"))
                {
                    Life life = hit.collider.GetComponent<Life>();
                    if (life != null)
                    {
                        life.TakeDamage(damage);
                    }
                }
            }

            currentBattery -= 10f * Time.deltaTime;
            if (currentBattery <= 0)
            {
                currentBattery = 0;
                isFocusing = false;
                spotLight.enabled = false;
            }

            instance.ChangeBattery((int)currentBattery);

        }
    }

    private void ChangeSpotLight()
    {
        if (isFocusing) {
            spotLight.intensity = focusIntensity;
            spotLight.color = focusColor;
        }
        else
        {
            spotLight.intensity = lightIntensity;
            spotLight.color = lightColor;
        }
    }

    void StopFocus()
    {
        isFocusing = false;

        ChangeSpotLight();
    }

    public bool pickedUp = false;
    public override void Interact(PlayerCheckInteraction interactor)
    {
        HideTextMessage();

        transform.SetParent(hand);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        pickedUp = true;

        instance.SetBatteryVisibility(true);
        instance.ChangeBattery((int)currentBattery);
        instance.ShowTutorial("Use " + toggleLight.action.GetBindingDisplayString() + " to " + toggleLight.action.name + " the light");
        StartCoroutine(WaitForNextText());
    }

    float waitTimer = 10;
    IEnumerator WaitForNextText()
    {
        yield return new WaitForSeconds(waitTimer);

        instance.ShowTutorial("Use " + focusLight.action.GetBindingDisplayString() + " to " + focusLight.action.name + " the light");
    }

    public void RecoverBattery(float batteryToRecover)
    {
        currentBattery = Mathf.Min(currentBattery + batteryToRecover, maxBattery);
        instance.ChangeBattery((int)currentBattery);
    }
}
