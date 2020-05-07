using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LanternLight : MonoBehaviour
{
    [SerializeField] private Light2D _light2D;

    // Start is called before the first frame update

    public void IncreaseLightOuterRadius(float increment)
    {
        _light2D.pointLightOuterRadius += increment;
    }
    
}
