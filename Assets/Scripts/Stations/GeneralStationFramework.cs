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
                buttonRepeatExplanation.SetActive(true);
                buttonAbort.SetActive(true);
                //If the explanation requires some sort of animation or other cue, trigger it here. Also Audio!!
                state += 1;
                break;
            case 2:
                explanation.SetActive(false);
                exercise.SetActive(true);
                state += 1;

                //Invoke function to enable completion button after 5 secs
                Invoke("ShowCompletionButton", 5.0f);

                ////If this station has not contributed to global progression yet, enable completion button after a short delay
                //if (contributedToGlobalProgression == false)
                //{
                //    //Invoke function to enable completion button after 5 secs
                //    Invoke("ShowCompletionButton", 5.0f);
                //
                //    //Set contributedToGlobalProgression flag
                //    contributedToGlobalProgression = true;
                //}
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
            //Call ProgressToNextGlobalState() from GlobalProgression Singleton Instance
            GlobalProgression.Instance.ProgressToNextGlobalState();

            Debug.Log(gameObject.name + " just contributed to global progression!");
        }
        else if (contributedToGlobalProgression == true)
        {
            Debug.Log("This station has already contributed to global progression. " + gameObject.name);
        }

        buttonCompletion.SetActive(false);
    }
}
