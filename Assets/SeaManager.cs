using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeaManager : MonoBehaviour
{
    public float lightIntensity;
    public float rotationAmplitude;
    public float rotationFrequency;
    public bool activateSimulation;
    [Range(0,1)]
    public float cameraFade;

    public Light directionalLight;
    public GameObject flex;
    public Image fadePlane;
    Klak.Motion.BrownianMotion m_motion;

    private void Start()
    {
        m_motion = GetComponent<Klak.Motion.BrownianMotion>();
    }

    // Update is called once per frame
    void Update()
    {
        directionalLight.intensity = lightIntensity;
        m_motion.rotationAmount.z = rotationAmplitude;
        m_motion.frequency = rotationFrequency;
        flex.SetActive(activateSimulation);
        fadePlane.color = new Color(fadePlane.color.r, fadePlane.color.g, fadePlane.color.b, cameraFade);
    }
}
