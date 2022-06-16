using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour{
    public Text highscore;

    private void Update() {
        highscore.text = PlayerPrefs.GetString("HighScore", "0");
    }

}
