using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Godmode : MonoBehaviour {
    public TextMeshProUGUI godmodeText;
    public static bool godmode = false;

    void Update(){ 
        if(godmode){
            godmodeText.text = "ON";
        } else {
            godmodeText.text = "OFF";
        }
    }

    public void godmodeClicker(){
        if(godmodeText.text == "ON"){
            godmode = false;
        } else {
            godmode = true;
        }
        
    }
}
