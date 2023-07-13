using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralStationFramework : MonoBehaviour
{
    //Serialized Variables
    [SerializeField] private GameObject overview;
    [SerializeField] private GameObject explanation;
    [SerializeField] private GameObject exercise;
    [SerializeField] private GameObject buttonRepeatExplanation;
    [SerializeField] private GameObject buttonAbort;
    [SerializeField] private GameObject buttonCompletion;
    [SerializeField] private AudioSource explanationBoxyAudioSource;
    [SerializeField] private GameObject globalBoxy;

    //Public Variables
    public int state = 1;

    //Private Variables
    private bool contributedToGlobalProgression = false;

    //Public functions so they can be called from the Poke Interaction 3D UI buttons

    //Button 1# - Continue Button
    public void ProgressState()
    {
        //Switch statement to activate/deactivate different state object depending on state
        switch (state)
        {
            case 1:
                overview.SetActive(false);
                explanation.SetActive(true);
                buttonAbort.SetActive(true);
                buttonRepeatExplanation.SetActive(true);
                //If the explanation requires some sort of animation or other cue, trigger it here. Also Audio!!
                explanationBoxyAudioSource.Play();
                explanation.GetComponent<IdleAnimActivator>().QueueIdleAnim();

                //Disable Global Boxy
                if (GlobalProgression.Instance.studentSpecterState != 3)
                {
                    globalBoxy.SetActive(false);
                }

                state += 1;
                break;
            case 2:
                explanation.SetActive(false);
                buttonRepeatExplanation.SetActive(false);
                exercise.SetActive(true);
                state += 1;

                //Invoke function to enable completion button after 5 secs
                Invoke("ShowCompletionButton", 5.0f);

                break;
        }

        //Debug.Log state for transparency
        Debug.Log("Current state: " + state);
    }

    //Button 2# - Repeat Explanation Button
    public void RepeatExplanation()
    {
        if (state == 2)
        {
            explanation.SetActive(true);
            //If the explanation requires some sort of animation or other cue, trigger it here. Also Audio!!
            explanationBoxyAudioSource.Play();
            state = 2;
        }

        //Debug.Log state for transparency
        Debug.Log("Current state: " + state);
    }

    //Button 3# - Abort Button
    public void ResetStation()
    {
        if (explanation.activeSelf == true || exercise.activeSelf == true)
        {
            explanation.SetActive(false);
            exercise.SetActive(false);
        }

        buttonRepeatExplanation.SetActive(false);
        buttonAbort.SetActive(false);
        buttonCompletion.SetActive(false);
        overview.SetActive(true);
        state = 1;

        //Debug.Log state for transparency
        Debug.Log("Current state: " + state);
    }

    private void ShowCompletionButton()
    {
        buttonCompletion.SetActive(true);
    }

    public void CompleteStation()
    {
        //Check if this Station has already contributed to global progression
        if (contributedToGlobalProgression == false)
        {
            //Invoke the progression delayed so the fade to black can happen first
            Invoke("DelayedProgression", 2.0f);

            Debug.Log(gameObject.name + " just contributed to global progression!");

            //Fade to black
            FadeTransitions.Instance.Fade(true);
        }
        else if (contributedToGlobalProgression == true)
        {
            Debug.Log("This station has already contributed to global progression. " + gameObject.name);
        }

        buttonCompletion.SetActive(false);
    }

    private void DelayedProgression()
    {
        //Call ProgressToNextGlobalState() from GlobalProgression Singleton Instance
        GlobalProgression.Instance.ProgressToNextGlobalState();

        //Fade from black
        FadeTransitions.Instance.Fade(false);
    }
}
