using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class MenuContoller : MonoBehaviour
{
    public string sceneWhereToGo;
    public Slider volumeSlider;
    public GameObject[] screenTypes = new GameObject[3];
    protected SaveData saveData;


    private void Awake()
    {
        LocalizationSettings.InitializationOperation.WaitForCompletion();
        try
        {
            saveData = SaveDataManager.LoadJsonData();
            volumeSlider.value = saveData.volume;
            AudioListener.volume = saveData.volume;
            ChangeFullscreen(saveData.fullScreenMode);
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[saveData.currentLanguage];
        }
        catch (System.Exception)
        {
            saveData = SaveData.Default();
            volumeSlider.value = saveData.volume;
            AudioListener.volume = saveData.volume;
            ChangeFullscreen(saveData.fullScreenMode);
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[saveData.currentLanguage];
        }

    }

    public void Play()
    {
        SaveDataManager.SaveJsonData(saveData);
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

    public void ChangeLanguage()
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
}
