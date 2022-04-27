using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour{

    [SerializeField]
    private TextMeshProUGUI promtText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UpdateText(string promtMessage) {
        if (promtText) promtText.text = promtMessage;
    }
}
