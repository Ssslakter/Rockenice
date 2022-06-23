using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuContoller : MonoBehaviour
{
    public string sceneWhereToGo;
    private float volume;
    private FullScreenMode fullScreenMode;
    public Slider volumeSlider;
    private void Awake()
    {

        SaveData saveData = SaveDataManager.LoadJsonData();
        volume = saveData.volume;
        volumeSlider.value = volume;
    }
    public void Play()
    {
        SceneManager.LoadScene(sceneWhereToGo);
    }

    public void Exit()
    {
        SaveData saveData = new SaveData();
        saveData.volume = volume;
        SaveDataManager.SaveJsonData(saveData);
        Application.Quit();
    }

    public void VolumeChanged()
    {
        volume = volumeSlider.value;
    }

}
