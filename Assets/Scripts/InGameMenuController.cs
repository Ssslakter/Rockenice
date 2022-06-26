using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class InGameMenuController : MenuContoller
{
    public static bool gameIsPaused;
    public GameObject settingMenu;
    public GameObject crosshair;
    public Inventory inventory;


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
        Global.player.transform.position = saveData.playerPosition;
        ScoreController.score = saveData.maxScore;
        HealthBar.hp = saveData.hp;
        gameIsPaused = false;
        PauseGame();
    }
    public void StopPlaying()
    {
        SaveProgress();
        SceneManager.LoadScene(sceneWhereToGo);
    }
    void PauseGame()
    {
        settingMenu.SetActive(gameIsPaused);
        crosshair.SetActive(!gameIsPaused);
        AudioListener.pause = gameIsPaused;

        if (gameIsPaused)
        {
            if (inventory.gameObject.activeSelf == true)
            {
                inventory.InventoryCloses();
            }
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;
            crosshair.transform.GetChild(0).GetComponent<TMP_Text>().text = "";
        }

    }

    public void SaveProgress()
    {
        saveData.playerPosition = Global.player.transform.position;
        saveData.maxScore = ScoreController.score;
        SaveDataManager.SaveJsonData(saveData);
    }

    public override void ChangeLanguage()
    {
        base.ChangeLanguage();
        crosshair.transform.GetChild(0).GetComponent<TMP_Text>().text = "";
    }
}