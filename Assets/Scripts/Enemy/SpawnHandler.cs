using System.Collections;
using System.Collections.Generic;
using Random=UnityEngine.Random;
using UnityEngine;
using UnityEngine.UI;
using System;



public class SpawnHandler : MonoBehaviour{

public Text counterText;

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
       if(activeEnermies==0)
        {
            NextWave();
        }
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
        numberOfEnemies++;
        kills++;
        showKills();
        
    }

    public void showKills()
    {
        counterText.text = kills.ToString();
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
