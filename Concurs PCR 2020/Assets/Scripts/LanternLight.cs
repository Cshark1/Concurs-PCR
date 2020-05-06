using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LanternLight : MonoBehaviour
{
    private Light2D _light2D;

    // Start is called before the first frame update
    void Start()
    {
        _light2D = this.gameObject.GetComponent<Light2D>();

        if (_light2D == null)
        {
            Debug.LogError("_light2D is NULL");
        }
    }

    public void IncreaseLightOuterRadius(float increment)
    {
        _light2D.pointLightOuterRadius += increment;
    }
    
}
