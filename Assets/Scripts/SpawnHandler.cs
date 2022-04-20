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

    ObjectPool<enemy> _pool;

    public int InactiveCount;

    public int ActiveCount;

    // Start is called before the first frame update
    void Start()
    {
        //GameObject tmp = Instantiate(enemy);
        tmp.transform.position =
            new Vector3(0.0f, tmp.transform.position.y, 0.0f);
        col = terrain.GetComponent<BoxCollider>();

        _pool =
            new ObjectPool<enemy>(GenerateObject,
                OntakeFromPool,
                OnReturnToPool);
    }

    // Update is called once per frame
    void Update()
    {
        var enemy = _pool.Get();
        InactiveCount = _pool.InactiveCount;
        ActiveCount = _pool.ActiveCount;

        if (activeEnermies < numberOfEnemies)
        {
            GenerateObject();
        }
    }

    void GenerateObject()
    {
        GameObject tmp = Instantiate(enemy);

        Vector3 randomPoint = GetRandomPoint();
        tmp.gameObject.transform.position =
            new Vector3(randomPoint.x, tmp.transform.position.y, randomPoint.z);
        activeEnermies++;
        tmp.SetPool (_pool);

        return tmp;
    }

    Vector3 GetRandomPoint()
    {
        int xRandom = 0;
        int zRandom = 0;

        xRandom = (int) Random.Range(col.bounds.min.x, col.bounds.max.x);
        zRandom = (int) Random.Range(col.bounds.min.z, col.bounds.max.z);

        return new Vector3(xRandom, 0.0f, zRandom);
    }

    void OntakeFromPool(enemy enemy)
    {
        enemy.gameObject.setActive(true);
        activeEnermies++;
    }

    void OnReturnToPool(enemy enemy)
    {
        enemy.gameObject.setActive(false);
        activeEnermies--;
    }
}
