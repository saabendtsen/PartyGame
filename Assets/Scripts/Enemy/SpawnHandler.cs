using System.Collections;
using System.Collections.Generic;
using Random=UnityEngine.Random;
using UnityEngine;
using UnityEngine.UI;

using TMPro;
using System;

public class SpawnHandler : MonoBehaviour{

public TextMeshProUGUI counterText;
public TextMeshProUGUI aliveText;

int kills;

public EnemyAi enemyAi;

public GameObject enemy;

public int numberOfEnemies;

public GameObject terrain;
private BoxCollider col;
private int activeEnermies;

    // Start is called before the first frame update
    void Start()
    {
       col = terrain.GetComponent<BoxCollider>();

       for(int i= 0; i < numberOfEnemies; i++)
        {
            GenerateObject(enemy);
            activeEnermies++;
        }
    }



    // Update is called once per frame
    void Update()
    {
        aliveText.SetText(activeEnermies.ToString());
    }

    void NextWave()
    {
        numberOfEnemies = Convert.ToInt32(numberOfEnemies*1.3);
        for(int i= 0; i < numberOfEnemies; i++)
        {
            GenerateObject(enemy);
            activeEnermies++;
        }
    }

    void GenerateObject(GameObject go)
    {
        if (go == null) return;  
        GameObject tmp = Instantiate(go);      

        
            Vector3 randomPoint = GetRandomPoint();
            tmp.gameObject.transform.position = new Vector3(randomPoint.x, 
            tmp.transform.position.y, randomPoint.z);
            
    }

    public void EnemyKilled()
    {
        activeEnermies--;
        kills++;
        showKills();

        if (activeEnermies == 0)
        {
            Invoke("NextWave",1f);
        }
        
    }

    public void showKills()
    {
        counterText.SetText(kills.ToString());
        if(kills > int.Parse(PlayerPrefs.GetString("HighScore", "0"))) {   
            PlayerPrefs.SetString("HighScore", kills.ToString());
        }
    }

     Vector3 GetRandomPoint()
    {
        int xRandom = 0;
        int zRandom = 0;
        
        
        xRandom = (int)Random.Range(col.bounds.min.x, col.bounds.max.x);
        zRandom = (int)Random.Range(col.bounds.min.z, col.bounds.max.z);

        return new Vector3(xRandom, 0.0f, zRandom);
}
}