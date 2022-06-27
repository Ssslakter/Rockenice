using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using TMPro;

public class MenuContoller : MonoBehaviour
{
    public string sceneWhereToGo;
    public Slider volumeSlider;
    public GameObject[] screenTypes = new GameObject[3];
    protected SaveData saveData;
    public bool isFinite = true;
    public TMP_Dropdown dropdown;
    public Button loadButton;

    private void Awake()
    {
        isFinite = true;
        LocalizationSettings.InitializationOperation.WaitForCompletion();
        saveData = SaveDataManager.LoadJsonData();
        loadButton.interactable = saveData.isSavedExists;
        volumeSlider.value = saveData.volume;
        AudioListener.volume = saveData.volume;
        ChangeFullscreen(saveData.fullScreenMode);
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[saveData.currentLanguage];
    }

    public void NewGame()
    {
        saveData.playerPosition = SaveData.Default.playerPosition;
        saveData.maxScore = SaveData.Default.maxScore;
        saveData.hp = SaveData.Default.hp;
        saveData.isFiniteWorld = isFinite;
        SaveDataManager.SaveJsonData(saveData);
        SceneManager.LoadScene(sceneWhereToGo);
    }
    public void LoadGame()
    {
        SaveDataManager.SaveJsonData(saveData);
        if (saveData.isFiniteWorld != isFinite)
        {
            ChangeWorldMode();
        }
        SceneManager.LoadScene(sceneWhereToGo);
    }

    public void Exit()
    {
        SaveDataManager.SaveJsonData(saveData);
        Application.Quit();
    }

    public void VolumeChanged()
    {
        saveData.volume = volumeSlider.value;
        AudioListener.volume = saveData.volume;
    }

    public void ChangeFullscreen(int type)
    {
        Screen.fullScreenMode = Global.screenModes[type];
        for (int i = 0; i < screenTypes.Length; i++)
        {
            screenTypes[i].SetActive(false);
        }
        screenTypes[type].SetActive(true);
        saveData.fullScreenMode = type;
    }

    public virtual void ChangeLanguage()
    {
        int nextLanguage = (saveData.currentLanguage + 1) % LocalizationSettings.AvailableLocales.Locales.Count;
        LocaleIdentifier localeCode = LocalizationSettings.AvailableLocales.Locales[nextLanguage].Identifier;
        for (int i = 0; i < LocalizationSettings.AvailableLocales.Locales.Count; i++)
        {
            Locale aLocale = LocalizationSettings.AvailableLocales.Locales[i];
            if (aLocale.Identifier == localeCode)
            {
                saveData.currentLanguage = nextLanguage;
                LocalizationSettings.SelectedLocale = aLocale;
                break;
            }
        }
    }

    public void ChangeResOnDrop()
    {
        saveData.resolution = dropdown.value;
        ChangeResolution(saveData.resolution);
    }

    private void ChangeResolution(int type)
    {
        if (type == 0)
        {
            Screen.SetResolution(1920, 1080, saveData.fullScreenMode != 0);
        }
        else if (type == 1)
        {
            Screen.SetResolution(1080, 720, saveData.fullScreenMode != 0);
        }
        else if (type == 2)
        {
            Screen.SetResolution(854, 480, saveData.fullScreenMode != 0);
        }
    }

    public void ResetProgress()
    {
        saveData.playerPosition = SaveData.Default.playerPosition;
        saveData.maxScore = SaveData.Default.maxScore;
        saveData.hp = SaveData.Default.hp;
        loadButton.interactable = false;
        saveData.isSavedExists = false;
    }


    public void ChangeWorldMode()
    {
        isFinite = !isFinite;
        if (isFinite)
        {
            isFinite = false;
            sceneWhereToGo = "SampleScene";
        }
        else
        {
            isFinite = true;
            sceneWhereToGo = "DemoVersion";
        }
    }
}
