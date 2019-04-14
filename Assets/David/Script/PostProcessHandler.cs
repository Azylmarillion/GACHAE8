using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessHandler : MonoBehaviour
{
    //
    [SerializeField, Range(.1f, 1)]
    float randMin = .1f;
    [SerializeField,Range(.1f,1)]
    float randMax = .3f;
    //
    [SerializeField]
    private PostProcessProfile profile;
    [SerializeField]
    private float vignettePulseSpeed;

    public bool pingPongPulseOrGradual;

    protected bool vignetteOn = true;

    protected float currentIntensity;

    protected float targetIntensity;

    protected ColorGrading colorGrading = null;

    protected Vignette vignette = null;

    void Init(bool _doIt)
    {
        if (!_doIt) return;
        GameManager.m_JumpSpeedPower = false;
        colorGrading.saturation.value = -100;
    }


    private void Awake()
    {
        XboxControllerInputManagerWindows.OnRightBumperDownInputPress += Init;
    }
    // Start is called before the first frame update
    void Start()
    {
        profile.TryGetSettings<ColorGrading>(out colorGrading);
        profile.TryGetSettings<Vignette>(out vignette);
        if (profile == null)
        {
            if (GetComponent<PostProcessProfile>())
            {
                profile = GetComponent<PostProcessProfile>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        vignettePulseSpeed = Random.Range(randMin, randMax);

        if (GameManager.m_JumpSpeedPower && colorGrading.saturation.value < 0)
        {
            colorGrading.saturation.value += 1;
        }

        if (vignetteOn)
        {
            //if (pingPongPulseOrGradual)
            //    vignette.intensity.value = Mathf.PingPong(Time.time, 0.1f) + 0.4f;

            if (!pingPongPulseOrGradual)
            {
                currentIntensity = Mathf.MoveTowards(vignette.intensity.value, targetIntensity, Time.deltaTime * vignettePulseSpeed);
                if (currentIntensity >= 0.5f)
                {
                    currentIntensity = 0.5f;
                    targetIntensity = 0.3f;
                }
                else if (currentIntensity <= 0.3f)
                {
                    currentIntensity = 0.3f;
                    targetIntensity = 0.5f;
                }
                vignette.intensity.value = currentIntensity;
            }
        }
    }
}
