using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
    public float health;
    private float lerpTimer;
    public float maxHealth = 100f;
    public float chipSpeed = 2f;

    public UiHandler uihandler;

    public Image frontHealthBar;
    public Image backHealthBar;

    void Start() {
        health = maxHealth;
    }

    void Update() {
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthUI();   
    }

    public void UpdateHealthUI(){
        //debug health amount
        //Debug.Log(health);
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = health / maxHealth;
        if (fillB > hFraction){
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }

        if (fillF < hFraction){
            backHealthBar.color = Color.green;
            backHealthBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, backHealthBar.fillAmount, percentComplete);
        }
        if(health == 0f){
            uihandler.GetComponent<UiHandler>().Gameover();
        }
    }

    public void ApplyDamage(float damage) {
        //Debug.Log("Player take Damage");
        health -= damage;
        lerpTimer = 0f;
        if(health <= 0) {
            // KILL player object
        //Debug.Log("YOU DIED! NOOB");
        }
    }

    public void RestoreHealth(float healAmount){
        health += healAmount;
        lerpTimer = 0f;
    }

}
