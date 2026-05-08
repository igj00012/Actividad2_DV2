using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenLight : MonoBehaviour
{
    [SerializeField] Light pointLight;
    [SerializeField] GameObject lightON;
    [SerializeField] float delayTime = 1f;

    float maxIntensity;

    private void Awake()
    {
        maxIntensity = pointLight.intensity;
    }

    private void Start()
    {
        StartCoroutine(ToggleLight());
    }

    IEnumerator ToggleLight()
    {
        while (true)
        {
            pointLight.intensity = 0;
            lightON.SetActive(false);
            yield return new WaitForSeconds(delayTime);

            pointLight.intensity = maxIntensity;
            lightON.SetActive(true);
            yield return new WaitForSeconds(delayTime);
        }
    }
}
