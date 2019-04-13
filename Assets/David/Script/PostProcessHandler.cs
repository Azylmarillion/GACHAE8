using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessHandler : MonoBehaviour
{

    [SerializeField]
    private PostProcessProfile profile;
    [SerializeField]
    private float vignettePulseSpeed;

    public bool pingPongPulseOrGradual;

    protected float currentIntensity;

    protected float targetIntensity;

    protected ColorGrading colorGrading = null;

    protected Vignette vignette = null;

    // Start is called before the first frame update
    void Start()
    {
        profile.TryGetSettings<ColorGrading>(out colorGrading);
        profile.TryGetSettings<Vignette>(out vignette);
    }

    // Update is called once per frame
    void Update()
    {
        vignettePulseSpeed = Random.Range(0.1f, 0.5f);
        if(colorGrading.saturation.value > -99)
            colorGrading.saturation.value -= 1;

        if(pingPongPulseOrGradual)
            vignette.intensity.value = Mathf.PingPong(Time.time, 0.1f) + 0.4f;

        if (!pingPongPulseOrGradual)
        {
            currentIntensity = Mathf.MoveTowards(vignette.intensity.value, targetIntensity, Time.deltaTime * vignettePulseSpeed);
            if(currentIntensity>= 0.5f)
            {
                currentIntensity = 0.5f;
                targetIntensity = 0.3f;
            }else if(currentIntensity <= 0.3f)
            {
                currentIntensity = 0.3f;
                targetIntensity = 0.5f;
            }
            vignette.intensity.value = currentIntensity;
        } 
    }
}
