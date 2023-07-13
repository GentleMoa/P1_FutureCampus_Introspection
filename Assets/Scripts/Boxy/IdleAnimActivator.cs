using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAnimActivator : MonoBehaviour
{
    //Serialized Variables
    [SerializeField] private int idleAnimActivationDelay;
    [SerializeField] private Animator boxyAnimator;

    private void OnEnable()
    {
        //DON'T FORGET: customize idleAnimActivationDelay to the length of the respective boxy voice-over audio clip!!!
        Invoke("QueueIdleAnim", idleAnimActivationDelay);
    }

    public void QueueIdleAnim()
    {
        boxyAnimator.SetTrigger("Idling");
    }
}
