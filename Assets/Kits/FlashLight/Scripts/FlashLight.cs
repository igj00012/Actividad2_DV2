using System;
using TMPro;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class FlashLight : InteractableItem
{
    [Header("References")]
    [SerializeField] Light spotLight;
    [SerializeField] Transform hand;
    [SerializeField] InputManagerSO inputManager;
    [SerializeField] TextMeshProUGUI uiText;

    [Header(("Parameters"))]
    [SerializeField] float damage = 0.00001f;
    float raycastLength;
    [SerializeField] float focusIntensity = 5;
    float lightIntensity;
    [SerializeField] Color focusColor = Color.yellow;
    Color lightColor;
    [SerializeField] float battery = 100f;
    public float currentBattery;

    AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();

        raycastLength = spotLight.range;
        lightColor = spotLight.color;
        lightIntensity = spotLight.intensity;

        currentBattery = battery;

        uiText.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        inputManager.OnToogleFlashLight += ToogleFlashLight;
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
        inputManager.OnToogleFlashLight -= ToogleFlashLight;
        inputManager.OnStartFocus -= StartFocus;
        inputManager.OnEndFocus -= StopFocus;
    }

    private void ToogleFlashLight()
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

            uiText.SetText((int)currentBattery + "%");
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

    bool pickedUp = false;
    public override void Interact(PlayerCheckInteraction interactor)
    {
        HideTextMessage();

        transform.SetParent(hand);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        pickedUp = true;

        uiText.gameObject.SetActive(true);
        uiText.SetText(currentBattery + "%");
    }

}
