using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalProgression : MonoBehaviour
{
    //Public Variables
    public int globalProgressionState;
    public int studentSpecterState;
    public int boxyVoiceOverSection;
    public int tutorialSpriteState;
    public List<GameObject> environments = new List<GameObject>();
    public List<Material> skyboxes = new List<Material>();
    public List<GameObject> studentSpecters = new List<GameObject>();
    public List<AudioClip> ambientAudio = new List<AudioClip>();
    public List<AudioClip> globalBoxyVoiceOver = new List<AudioClip>();
    public List<GameObject> tutorialSprites = new List<GameObject>();

    //Serialized Variables
    [SerializeField] private GameObject globalBoxy;
    [SerializeField] private AudioSource globalBoxyAudioSource;
    [SerializeField] private AudioSource ambientAudioSource;

    #region Singleton
    //Singleton
    public static GlobalProgression Instance { set; get; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion

    private void Start()
    {
        //Wait for Intro to finish to play: Tutorial - Snap Turn
        Invoke("TriggerBoxyVoiceoverSections", 54.0f);
        Invoke("UpdateTutorialSprites", 54.0f);

        //Wait for Tutorial - Snap Turn to finish to play: Tutorial - Teleportation
        Invoke("TriggerBoxyVoiceoverSections", 75.0f);
        Invoke("UpdateTutorialSprites", 75.0f);

        //Wait for Tutorial - Teleportation to finish to play: Tutorial - Button Interaction
        Invoke("TriggerBoxyVoiceoverSections", 96.0f);
        Invoke("UpdateTutorialSprites", 96.0f);

        //Wait for Tutorial - Button Interaction to finish to play: Disclaimer
        Invoke("TriggerBoxyVoiceoverSections", 109.0f);
        //Disable all tutorialSprites
        Invoke("UpdateTutorialSprites", 109.0f);
    }

    // ! ! !
    //This function has to be called each time a station is completed (Don't forget to make sure each station can only contribute to global progression once!)
    // ! ! !
    public void ProgressToNextGlobalState()
    {
        //Ensuring the globalProgressionState int does not go out of bounds of the lists
        if (globalProgressionState < environments.Count - 1)
        {
            //Increment globalProgressionState by +1
            globalProgressionState += 1;

            UpdateEnvironmentAndSkybox();
            UpdateAmbientAudio();
        }

        //Progressively enabling more and more internation fake student specters from other EuT+ countries
        if (studentSpecterState < studentSpecters.Count)
        {
            studentSpecterState += 1;
            AddStudentSpecters();
        }

        //Enabling global Boxy in the final global progression state
        if (studentSpecterState == 3)
        {
            globalBoxy.SetActive(true);

            //Play Global Boxy Outro here
            globalBoxyAudioSource.clip = globalBoxyVoiceOver[globalBoxyVoiceOver.Count - 1];
            globalBoxyAudioSource.Play();
        }
    }

    public void TriggerBoxyVoiceoverSections()
    {
        //Increment boxyVoiceOverSection by +1
        boxyVoiceOverSection += 1;

        //Play the correct global Boxy VoiceOver Audio Clip
        globalBoxyAudioSource.clip = globalBoxyVoiceOver[boxyVoiceOverSection - 1];
        globalBoxyAudioSource.Play();
    }

    private void UpdateEnvironmentAndSkybox()
    {
        //Update Environment
        //Disable all environments
        for (int i = 0; i < environments.Count; i++)
        {
            environments[i].SetActive(false);
        }

        //Enable only the correct one fitting the current globalProgressionState
        environments[globalProgressionState].SetActive(true);

        //Update Skybox
        RenderSettings.skybox = skyboxes[globalProgressionState];
    }

    private void AddStudentSpecters()
    {
        studentSpecters[studentSpecterState - 1].SetActive(true);
    }

    private void UpdateAmbientAudio()
    {
        ambientAudioSource.clip = ambientAudio[globalProgressionState];
        ambientAudioSource.Play();
    }

    private void UpdateTutorialSprites()
    {
        if (tutorialSpriteState < 3)
        {
            //Disable all tutorialSprites
            for (int i = 0; i < tutorialSprites.Count; i++)
            {
                tutorialSprites[i].SetActive(false);
            }

            //Enable only the correct one
            tutorialSprites[tutorialSpriteState].SetActive(true);

            //Increment tutorialSpriteState by +1
            tutorialSpriteState += 1;
        }
        else
        {
            //Disable all tutorialSprites
            for (int i = 0; i < tutorialSprites.Count; i++)
            {
                tutorialSprites[i].SetActive(false);
            }
        }
    }
}
