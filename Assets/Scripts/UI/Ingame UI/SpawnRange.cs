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

    public GameObject[] ranges = new GameObject[3]; //0:near tower 1:left bridge 2:right bridge
    public Material rangeEffectMat;
    public Sprite[] sprites = new Sprite[3];

    void Start()
    {
        this.gameObject.SetActive(false);
        rangeEffectMat.SetTexture("_MainTex", sprites[0].texture);
    }

    // Update is called once per frame
    void Update()
    {
        if (TowerManager.instance.towerList[0, 0] == null)
        {
            Destroy(ranges[1]);
            rangeEffectMat.SetTexture("_MainTex", sprites[2].texture);
        }
        if (TowerManager.instance.towerList[0, 1] == null)
        {
            Destroy(ranges[2]);
            rangeEffectMat.SetTexture("_MainTex", sprites[1].texture);
        }
        //Material[] mats = rangeEffect.GetComponent<Renderer>().materials;
        //mats[0].SetTexture(0,sprites[0].texture);
        //rangeEffect.GetComponent<Renderer>().materials = mats;
    }
    public void ShowSpawnRangeEffect()
    {
        this.gameObject.SetActive(true);
    }
    public void HideSpawnRangeEffect()
    {
        this.gameObject.SetActive(false);
    }
}
