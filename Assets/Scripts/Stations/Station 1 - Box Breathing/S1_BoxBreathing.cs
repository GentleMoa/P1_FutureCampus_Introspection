using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S1_BoxBreathing : MonoBehaviour
{
    //Serialized Variables
    [SerializeField] private GameObject textBreatheIn;
    [SerializeField] private GameObject textBreatheOut;
    [SerializeField] private GameObject textHold;

    public void BreatheIn()
    {
        textHold.SetActive(false);
        textBreatheOut.SetActive(false);
        textBreatheIn.SetActive(true);
    }

    public void BreatheOut()
    {
        textHold.SetActive(false);
        textBreatheOut.SetActive(true);
        textBreatheIn.SetActive(false);
    }

    public void Hold()
    {
        textHold.SetActive(true);
        textBreatheOut.SetActive(false);
        textBreatheIn.SetActive(false);
    }
}
