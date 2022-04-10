using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{


    /// <singletone>
    static public GameManager instance = null;
    /// <singletone>

    static public List<UnitStatus> MyHandsList = new List<UnitStatus>();


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))   //Save(�� Ż���Ҷ� ���� ���빰 �����ų��)
        {
            print(MyHandsList.Count);
        }
    }
}
