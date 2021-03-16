using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SeaManager : MonoBehaviour
{
    public float lightIntensity;
    public float rotationAmplitude;
    public float rotationFrequency;
    [Range(0,1)]
    public float cameraFade;
    public Light directionalLight;
    public Image fadePlane;
    public Transform flexParent;

    [Header("LOD")]
    public GameObject flexLow;
    public GameObject flexMid;
    public GameObject flexHigh;


    Klak.Motion.BrownianMotion m_motion;
    private int m_actualResolution = 0;
    private GameObject m_actualFlexActor;

    private void Start()
    {
        m_motion = GetComponent<Klak.Motion.BrownianMotion>();

        m_actualResolution = PlayerPrefs.GetInt("Sea_Resolution", 0);
        SetResolution(m_actualResolution);
    }

    // Update is called once per frame
    void Update()
    {
        directionalLight.intensity = lightIntensity;
        m_motion.rotationAmount.z = rotationAmplitude;
        m_motion.frequency = rotationFrequency;

        fadePlane.color = new Color(fadePlane.color.r, fadePlane.color.g, fadePlane.color.b, cameraFade);
    }

    void SetResolution(int res)
    {
        // Don't load the already loaded LOD
        if (res == m_actualResolution && m_actualFlexActor != null)
            return;

        // Destroy actual prefab if already here
        if (m_actualFlexActor != null)
            Destroy(m_actualFlexActor);

        GameObject f = null;

        if (res == 0)
        {
            f = flexLow;
        }
        else if (res == 1)
        {
            f = flexMid;
        }
        else if (res == 2)
        {
            f = flexHigh;
        }

        if(f != null)
        {
            m_actualFlexActor = Instantiate(f, flexParent);
            m_actualResolution = res;
        }
    }

    
    #region OSC Method
    public void HighResolution()
    {
        SetResolution(2);
    }

    public void MidResolution()
    {
        SetResolution(1);
    }

    public void LowResolution()
    {
        SetResolution(0);
    }

    public void ActivateSimulation()
    {
        if (m_actualFlexActor != null)
            m_actualFlexActor.SetActive(true);
    }

    public void DeactivateSimulation()
    {
        if (m_actualFlexActor != null)
            m_actualFlexActor.SetActive(false);
    }

    #endregion

    void OnDestroy()
    {
        PlayerPrefs.SetInt("Sea_Resolution", m_actualResolution);
    }
}
