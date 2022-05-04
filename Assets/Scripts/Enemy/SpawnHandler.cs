using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnHandler : MonoBehaviour
{

public GameObject enemy;

public int numberOfEnemies;

public GameObject terrain;
private BoxCollider col;
private int activeEnermies;


    // Start is called before the first frame update
    void Start()
    {
       GameObject tmp = Instantiate(enemy);
       tmp.transform.position = new Vector3(0.0f, tmp.transform.position.y, 0.0f);
       col = terrain.GetComponent<BoxCollider>();
       

        
    }

    // Update is called once per frame
    void Update()
    {
        if(activeEnermies<numberOfEnemies)
        {
            GenerateObject(enemy);
        }
    }

    void GenerateObject(GameObject go)
    {
        if (go == null) return;

        
            GameObject tmp = Instantiate(go);
        
            Vector3 randomPoint = GetRandomPoint();
            tmp.gameObject.transform.position = new Vector3(randomPoint.x, 
            tmp.transform.position.y, randomPoint.z);
            activeEnermies++;

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
