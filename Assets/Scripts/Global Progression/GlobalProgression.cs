using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalProgression : MonoBehaviour
{
    //Public Variables
    public int globalProgressionState;
    public int studentSpecterState;
    public List<GameObject> environments = new List<GameObject>();
    public List<Material> skyboxes = new List<Material>();
    public List<GameObject> studentSpecters = new List<GameObject>();

    //Serialized Variables
    [SerializeField] private GameObject globalBoxy;

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

    private void AddStudentSpecters()
    {
        studentSpecters[studentSpecterState - 1].SetActive(true);
    }
}
