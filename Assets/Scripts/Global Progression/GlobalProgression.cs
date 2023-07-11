using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalProgression : MonoBehaviour
{
    //Public Variables
    public int globalProgressionState;
    public List<GameObject> environments = new List<GameObject>();
    public List<Material> skyboxes = new List<Material>();


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

    void Start()
    {
        
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
        }
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
}
