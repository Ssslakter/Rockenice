using UnityEngine;
using UnityEngine.SceneManagement;


public class InGameMenuController : MenuContoller
{
    public static bool gameIsPaused;
    public GameObject settingMenu;
    public GameObject crosshair;

    public void Unpause()
    {
        gameIsPaused = false;
        PauseGame();
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            gameIsPaused = !gameIsPaused;
            PauseGame();
        }
    }
    private void Start()
    {
        gameIsPaused = false;
        PauseGame();
    }
    public void StopPlaying()
    {
        SaveDataManager.SaveJsonData(saveData);
        SceneManager.LoadScene(sceneWhereToGo);
    }
    void PauseGame()
    {
        if (gameIsPaused)
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;
        }
        settingMenu.SetActive(gameIsPaused);
        crosshair.SetActive(!gameIsPaused);
        AudioListener.pause = gameIsPaused;
    }
}