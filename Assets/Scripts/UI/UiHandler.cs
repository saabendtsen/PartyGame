using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiHandler : MonoBehaviour {

    public void PlayGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Gameover(){
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void Reload(){
        SceneManager.LoadScene(1);
    }

    public void Menu(){
        SceneManager.LoadScene(0);
    }
    
    public void ExitGame() {
        Debug.Log("Exit Game!");
        Application.Quit();
    }
}
