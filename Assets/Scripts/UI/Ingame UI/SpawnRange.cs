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
        HideRange(1);
        HideRange(2);
    }
    
    // Update is called once per frame
    void Update()
    {
        RangeSetting();
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

    void RangeSetting()
    {
        // 만약에 아군타워,적군타워 둘다 부숴졌을때 부자연스럽게 출력됨. 수정필요
        if (TowerManager.instance.towerList[0, 0] == null)
        {
            ActiveRange(1);
        }
        if (TowerManager.instance.towerList[0, 1] == null)
        {
            ActiveRange(2);
        }
        if (TowerManager.instance.towerList[1, 0] == null)
        {
            HideRange(3);
        }
        if (TowerManager.instance.towerList[1, 1] == null)
        {
            HideRange(4);
        }
    }

    void ActiveRange(int i)
    {
        ranges[i].transform.position = rangePos[i];
    }

    void HideRange(int i)
    {
        ranges[i].transform.position = new Vector3(99f, -99f, 0f);
    }
}
