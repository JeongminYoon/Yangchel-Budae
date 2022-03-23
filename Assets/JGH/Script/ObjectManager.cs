using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    /// <singletone>
    public static ObjectManager instance = null;
    /// <singletone>

    public List<GameObject> enemyList;
    public List<GameObject> allyList;


    void Awake()
	{
        if (instance == null)
        {
            instance = this;
        }
	}

	void Start()
    {
        
    }

    void Update()
    {
        
    }
}
