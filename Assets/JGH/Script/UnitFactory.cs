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


    public void SpawnMeleeUnit(Vector3 spawnPos)
    {
        Instantiate(meleePrefab,spawnPos,Quaternion.identity);
    }

    public void SpawnRangeUnit(Vector3 spawnPos)
    {
        Instantiate(rangePrefab, spawnPos, Quaternion.identity);
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
