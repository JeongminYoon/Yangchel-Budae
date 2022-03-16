using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "UnitStatus", menuName = "Scriptable Object/UnitStatus", order= int.MaxValue)]
public class ScriptableObject_Test : ScriptableObject
{
    [SerializeField] //접근지정자가 private라도 인스펙터 창에 뜨게 하는거
    private float   hp;
    [SerializeField]
    private float   dmg;
    [SerializeField]
    private float   atkSpd;
    [SerializeField]
    private float   moveSpd;
    [SerializeField]
    private float   atkRagne;
    [SerializeField]
    private float   sightRange;
    [SerializeField]
    private int     cost;
    [SerializeField]
    private float   spawnTime;
    
    

    public float GetSetHp;
 

     public void SetHp(int value)
     {

     }

    //public float GetHP()
    //{ 
    //    return hp;
    //}

    //public void SetHP(float _temp)
    //   {
    //    hp = _temp;
    //}
    public float GetSetDmg
    {
        get { return dmg; }
        set { dmg = value; }
    }


    

}
