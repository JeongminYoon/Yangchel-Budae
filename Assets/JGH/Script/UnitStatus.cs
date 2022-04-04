using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "UnitStatus", menuName = "Scriptable Object/UnitStatus", order= int.MaxValue)]
public class UnitStatus : ScriptableObject
{
    public void DeepCopy(UnitStatus origin)
	{
        unitName = String.Copy(origin.unitName);
        unitNum = origin.unitNum;

        fullHp = origin.fullHp;
<<<<<<< HEAD
        curHp = origin.curHp;
       //hp = origin.hp;

=======
        curHp = fullHp;
        //hp = origin.hp;
>>>>>>> f68ff8f9cdadd206e6ebe145f926d56366f45b64

        dmg = origin.dmg;
        atkSpd = origin.atkSpd;
        moveSpd = origin.moveSpd;
        atkRange = origin.atkRange;
        sightRange = origin.sightRange;
        cost = origin.cost;
        spawnTime = origin.spawnTime;

        unitColScale = origin.unitColScale;

        isSplashAtk = origin.isSplashAtk;
        isDead = origin.isDead;
    }


    //[SerializeField] //접근지정자가 private라도 인스펙터 창에 뜨게 하는거
    public string   unitName;
    
    public int      unitNum;//생성등에서 Enum형으로 편하게 쓸용도
    public int      unitTier;

    public float fullHp;
    public float curHp;
<<<<<<< HEAD
//public float    hp;
=======
    //public float    hp;
>>>>>>> f68ff8f9cdadd206e6ebe145f926d56366f45b64

    public float    dmg;
    public float    atkSpd;
    public float    moveSpd;
    public float    atkRange;
    public float    sightRange;
    public int      cost;
    public float    spawnTime;

    public Vector3    unitColScale;

    public bool     isSplashAtk;
    public bool     isDead;
   


    

}
