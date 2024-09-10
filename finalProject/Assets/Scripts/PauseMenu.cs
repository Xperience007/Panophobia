using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pause;
    public static bool GameIsPaused = false;

    public void Update() {
        if(Input.GetButtonDown("Cancel")) {
            if(GameIsPaused) {
                Resume();
            }
            else {
                Pause();
            }
        }
    }
    
    public void Pause() {
        pause.SetActive(true);
        Time.timeScale = 0;
        GameIsPaused = true;
    }

    public void Resume() {
        pause.SetActive(false);
        Time.timeScale = 1;
        GameIsPaused = false;
    }

    public void Restart() {
        GameIsPaused = false;
        Time.timeScale = 1;
        SceneManager.LoadScene("Start");
    }

    public void Tutorial() {
        GameIsPaused = false;
        Time.timeScale = 1;
        SceneManager.LoadScene("Tutorial");
    }

    public void Quit() {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else  
            Application.Quit();
        #endif
    }
}
