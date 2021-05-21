using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuHandler : MonoBehaviour
{
    public GameObject pauseUI;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space)) PauseGame();
    }

    public void ResumeGame()
    {
        StateHandler.instance.gamePaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseUI.SetActive(false);
    }

    public void PauseGame()
    {
        AudioManager.instance.StopAllSounds();
        StateHandler.instance.gamePaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pauseUI.SetActive(true);
    }

    public void SaveAndQuit()
    {
        GameHandler.instance.UploadData();
        StateHandler.instance.anticipatedState = StateHandler.STATE.HOME;
        SceneManager.LoadScene("Menu");
    }

    public void Tutorial()
    {
        GameHandler.instance.UploadData();
        StateHandler.instance.anticipatedState = StateHandler.STATE.TUTORIAL;
        SceneManager.LoadScene("Menu");
    }

}
