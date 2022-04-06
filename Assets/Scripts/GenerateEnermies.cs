using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnermies : MonoBehaviour
{

    public GameObject theEnemy;
    public int xPos;
    public int zPos;
    public int enemyCount;
    public int maxEnemies;
    // Start is called before the first frame update
    void Start()
    {
       StartCoroutine(EnemyDrop()); 
    }
     IEnumerator EnemyDrop()
        {
            while(enemyCount < maxEnemies)
            {
                xPos = Random.Range(-13,9);
                zPos = Random.Range(13,90);
                Instantiate(theEnemy,new Vector3(xPos,-1.02f,zPos),Quaternion.identity);
                yield return new WaitForSeconds(0.1f);
                enemyCount += 1;
            }
        }

    // Update is called once per frame
    void Update()
    {
        
    }
}
