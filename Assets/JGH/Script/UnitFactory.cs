using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitFactory : MonoBehaviour
{
    /// <singletone>
    public static UnitFactory instance = null;

    private void Awake()
    {
        if (instance == null)
        { instance = this; }
    }
    /// <singletone>

    public GameObject meleePrefab;
    public GameObject rangePrefab;
    public GameObject enemyPrefab;

    public GameObject  SpawnMeleeUnit(Vector3 spawnPos)
    {
        GameObject tempObj = Instantiate(meleePrefab, spawnPos, Quaternion.identity);
        tempObj.GetComponent<Units>().IsEnemy = false;
        return tempObj;
    }

    public GameObject SpawnRangeUnit(Vector3 spawnPos)
    {
        GameObject tempObj = Instantiate(rangePrefab, spawnPos, Quaternion.identity);
        tempObj.GetComponent<Units>().IsEnemy = false;
        return tempObj;
    }

    public GameObject SpawnEnemy(Vector3 spawnPos)
    {
        GameObject tempObj = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        tempObj.GetComponent<Units>().IsEnemy = true;
        return tempObj;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
        
    }
}
