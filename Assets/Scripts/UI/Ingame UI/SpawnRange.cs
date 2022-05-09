using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRange : MonoBehaviour
{
    #region singletone
    /// <singletone>
    static public SpawnRange instance = null;
    /// <singletone>

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion

    public GameObject[] ranges = new GameObject[5]; //0:near tower , 1:left bridge , 2: right bridge ,  3:Enemy left bridge , 4: Enemy right bridge
    Vector3[] rangePos = new Vector3[5];
    public Material material;

    void Start()
    {
        this.gameObject.SetActive(false);
        for (int i = 0; i >= ranges.Length; i++)
        {
            rangePos[i] = ranges[i].transform.position;
        }

        int temp = 1;
        for (int i = 0; i < (int)Enums.Team.End; ++i)
        {
            for (int k = 0; k < 2; ++k)
            {
                rangeList[i, k] = ranges[temp];

                //1. ���� �츮�� �г�
                //2. ������ �츮�� �г�
                //3. ���� ���� �г�
                //4. ������ �����г�
                ++temp;
            }
        
        }
       
    }
    
    // Update is called once per frame
    void Update()
    {
        //RangeSetting();
        SetRangeList();
        material.color = new Color(material.color.r, material.color.g, material.color.b, 0.15f);
        iTween.ColorTo(gameObject, iTween.Hash("a", 0.3f, "time", 1f, "loopType", "pingPong", "easeType", "easeOutSine"));
    }
    public void ShowSpawnRangeEffect()
    {
        this.gameObject.SetActive(true);
    }
    public void HideSpawnRangeEffect()
    {
        this.gameObject.SetActive(false);
    }

    public GameObject[,] rangeList = new GameObject[2,2];

    void SetRangeList()
    {
        for (int i = 0; i < 2; i++)
        {
            for (int k = 0; k < 2; k++)
            {
                if (TowerManager.instance.towerList[i, k] == null)
                {
                    rangeList[i, k].SetActive(false);
                }
            }
        }

        for (int i = 0; i < 2; i++)
        {
            if (TowerManager.instance.towerList[0,i] == null && TowerManager.instance.towerList[1, i] == null)
            {
                rangeList[0, i].SetActive(false);
                rangeList[1, i].SetActive(true);
            }
        }
    }
}
