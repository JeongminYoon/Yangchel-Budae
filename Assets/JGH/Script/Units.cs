using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Units : MonoBehaviour
{
    [SerializeField]
    protected ScriptableObject_Test scObj;


    protected void Walk()
    {
        Debug.Log(scObj.moveSpd + "로 걷고 있습니다.");
    }

    protected void Attack()
    {
        Debug.Log(scObj.atkRagne + "의 범위로\n" + scObj.moveSpd + "의 데미지를 줍니다.");
    }

    protected void Search()
    {
        Debug.Log(scObj.sightRange + "의 범위로 적을 찾고 있습니다.");
    }

    protected void Hit(int _dmg)
    {
        float temp = scObj.hp;
        scObj.hp -= _dmg;
        Debug.Log(_dmg + "의 데미지를 받아\n" + temp + "에서" + scObj.hp + "가 되었습니다");
    }









}
