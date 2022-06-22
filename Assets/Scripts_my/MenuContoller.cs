using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuContoller : MonoBehaviour
{
    public string sceneWhereToGo;

    public void Play()
    {
        SceneManager.LoadScene(sceneWhereToGo);
    }

    public void Exit()
    {
        Application.Quit();
    }

}
