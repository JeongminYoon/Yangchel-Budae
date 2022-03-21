using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Units : MonoBehaviour
{
    [SerializeField]
    protected ScriptableObject_Test scObj;

    protected bool isEnemy;
    public bool IsEnemy
    {
        get { return isEnemy; }
        set { isEnemy = value; }
    }
    protected bool isDead = false;

    /// <target>
    protected GameObject targetObj;
    public List<GameObject> listEnemy;
    public float targetDist;
    public Vector3 targetDir;
    public float targetDegAngle;
    /// <target>

    private float atkTime = 0f;

    protected void CalcToObj(GameObject obj)
    {
            Vector3 toTargetVector = obj.transform.position - this.transform.position;

            targetDist = toTargetVector.magnitude;
            targetDir = toTargetVector.normalized;
            float dotProduct = Vector3.Dot(obj.transform.position, this.transform.position);
            //dot, 즉 내적의 결과값 => Cos @ 값 (-1~1값 /0보다 크면 내 앞, 작으면 내 뒤)
            float RadAngle = Mathf.Acos(dotProduct);
                //역 코사인 걸면 라디안 값 
            targetDegAngle = RadAngle * Mathf.Deg2Rad;
               //이것이 ㄹㅇ 진 각도
    }

    protected void Death()
    {
        Destroy(gameObject);
        //이 유닛 참조하고 있는 다른 놈들에 대해서도 예외처리 필요.
        //일단 사용 ㄴㄴㄴ
    }


    protected void Walk(GameObject _target)
    {
        if (_target != null)
        {
            //Vector3 dir = Vector3.Normalize(_target.transform.position - gameObject.transform.position);
            CalcToObj(targetObj);
            if (targetDist > scObj.atkRagne)
            {
                transform.position += targetDir * scObj.moveSpd * Time.deltaTime;
                Debug.Log(scObj.moveSpd + "로 걷고 있습니다.");
            }
        }
        else 
        {
            transform.position += transform.forward * scObj.moveSpd * Time.deltaTime;    
        }

    }

    protected void Attack(GameObject _target)
    {
        if(_target != null && targetDist <= scObj.atkRagne)
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

    protected void SearchUnit()
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
        {
            targetObj = null;
        }
        else
        {
            float lowDist = -1f;

            for (int i = 0; i < tempEnemyList.Count; ++i)
            {
                float dist = Vector3.Magnitude(tempEnemyList[i].transform.position - this.transform.position);

                if (dist <= scObj.sightRange)
                {
                    if (lowDist > dist || lowDist < 0f)
                    {
                        lowDist = dist;
                        targetObj = tempEnemyList[i];
                    }
                }
            }

        }

    }

    protected void SearchTower()
    {
    
    
    }

    public void Hit(int _dmg)
    {
        float temp = scObj.hp;
        scObj.hp -= _dmg;
        Debug.Log(_dmg + "의 데미지를 받아\n" + temp + "에서" + scObj.hp + "가 되었습니다");
    }

    protected virtual void Awake()
    { 
    
    
    }

    protected virtual void Start()
    { 
    


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
