using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{

    public void InvokeSceneChange()
    {
        Invoke("ChangeScene", 0.25f);
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene(1);
    }
}
