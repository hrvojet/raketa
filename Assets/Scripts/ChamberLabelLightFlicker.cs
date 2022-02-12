using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ChamberLabelLightFlicker : MonoBehaviour
{
    private Light _light;
    
    // Start is called before the first frame update
    void Start()
    {
        _light = GetComponent<Light>();
        TurnLightOff();
        
        // InvokeRepeating("ToggleLight", 0.5f, 0.5f);
        StartCoroutine(PlayShort());

    }

    private void ToggleLight()
    {
        _light.enabled = !_light.enabled;
    }

    private void TurnLightOn()
    {
        _light.enabled = true;
    }

    private void TurnLightOff()
    {
        _light.enabled = false;
    }

    /*
     * U = .._ (short, short, long)
     * P = .__. (short, long, long, short)
     * Pause between letters = _
     */
    
    // Odvratan hardkodiran moreseov kod za UP
    IEnumerator PlayShort()
    {
        // start with light turned off
        yield return new WaitForSeconds(1.5f);
        
        while (true)
        {
            TurnLightOn();
            yield return new WaitForSeconds(0.5f);
            TurnLightOff();
            yield return new WaitForSeconds(0.5f);
            TurnLightOn();
            yield return new WaitForSeconds(0.5f);
            TurnLightOff();
            yield return new WaitForSeconds(0.5f);
            TurnLightOn();
            yield return new WaitForSeconds(1.5f);
            
            TurnLightOff();
            yield return new WaitForSeconds(1.5f);

            TurnLightOn();
            yield return new WaitForSeconds(0.5f);
            TurnLightOff();
            yield return new WaitForSeconds(0.5f);
            TurnLightOn();
            yield return new WaitForSeconds(1.5f);
            TurnLightOff();
            yield return new WaitForSeconds(0.5f);
            TurnLightOn();
            yield return new WaitForSeconds(1.5f);
            TurnLightOff();
            yield return new WaitForSeconds(0.5f);
            TurnLightOn();
            yield return new WaitForSeconds(0.5f);
            TurnLightOff();
            yield return new WaitForSeconds(1.5f);
        }
    }
}
