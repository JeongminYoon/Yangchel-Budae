using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Units : MonoBehaviour
{

    [SerializeField]
    protected ScriptableObject_Test scObj;

    protected bool isEnemy;
    protected bool isDead = false;

    [SerializeField]
    protected List<GameObject> listEnemy;
    [SerializeField]
    protected GameObject target;
    public float targetDistance;

    private float atkTime = 0f;


    protected void Walk(GameObject _target)
    {
        if (_target != null && targetDistance > scObj.atkRagne)
        {
            Vector3 dir = Vector3.Normalize(_target.transform.position - gameObject.transform.position);
            transform.position += dir * scObj.moveSpd * Time.deltaTime;
            Debug.Log(scObj.moveSpd + "로 걷고 있습니다.");
        }
    }

    protected void Attack(GameObject _target)
    {
        if(_target != null && targetDistance <= scObj.atkRagne)
        { 
            Debug.Log(scObj.atkRagne + "의 범위로\n" + scObj.moveSpd + "의 데미지를 줍니다.");
            Units temp =  _target.GetComponent<Units>(); //=> null 나옴. 근데 그게 맞지 ㅋㅋ

            if (atkTime >= scObj.atkSpd)
            {
                temp.Hit((int)scObj.dmg);
                atkTime = 0f;
            }

        }
    }

    protected void Search()
    {
        //매 프레임마다 돌리지는 말구 
        //타겟 유닛이 없을때 몇초마다 돌리기?
        //타겠이 정해졌을때는 안 돌리다가? 타겟이 죽었을 경우 재 탐색?

        //일단 무적권 가까운 타워를 목표로 잡기
        //그 쪽으로 걸어가다가 범위안에 적 있으면 타겟 바꾸기
        Debug.Log(scObj.sightRange + "의 범위로 적을 찾고 있습니다.");

        //GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");

        List<GameObject> tempEnemyList = TestScript.instance.GetEnemyList();

        if (tempEnemyList.Count == 0)
        { target = null; }
        else 
        {
            float distTemp = 0f;
            GameObject tempTarget = null;//거리안의 타겟이 있을 때만 되도록 걸러주기

            for (int i = 0; i < tempEnemyList.Count; ++i)
            {
                float dist = Vector3.Magnitude(this.gameObject.transform.position - tempEnemyList[i].transform.position);

                if (dist <= scObj.sightRange)
                {
                    dist = Mathf.Abs(dist);
                    
                    if (distTemp > dist || i == 0)
                    {
                        distTemp = dist;    
                        target = tempEnemyList[i];
                    }
                }
            }
            
        }
    }

    public void Hit(int _dmg)
    {
        float temp = scObj.hp;
        scObj.hp -= _dmg;
        Debug.Log(_dmg + "의 데미지를 받아\n" + temp + "에서" + scObj.hp + "가 되었습니다");
    }


    protected virtual void Update()
    {//가상함수로 만들면
     //모노비헤이비어가 update 돌릴 때 오버 라이딩된 자식 함수가 돌게되고
     //그때 맨 처음 base.update()로 돌리기
       atkTime += Time.deltaTime;

        if (scObj.hp <= 0)
        {
            isDead = true;
        }
    }



}
