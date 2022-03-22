using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "UnitStatus", menuName = "Scriptable Object/UnitStatus", order= int.MaxValue)]
public class UnitStatus : ScriptableObject
{
    //[SerializeField] //접근지정자가 private라도 인스펙터 창에 뜨게 하는거
    public string   unitName;
    public float    hp;
    public float    dmg;
    public float    atkSpd;
    public float    moveSpd;
    public float    atkRagne;
    public float    sightRange;
    public int      cost;
    public float    spawnTime;

    public float    unitScale;

    public bool     isSplashAtk;
    public bool     isDead;
   


    

}
