using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Units : MonoBehaviour
{
    [SerializeField]
    protected UnitStatus unitStatus;

    protected bool isEnemy;
    public bool IsEnemy
    {
        get { return isEnemy; }
        set { isEnemy = value; }
    }
    //protected bool isDead = false;

    /// <target>
    protected GameObject targetObj;
    public List<GameObject> listTarget = new List<GameObject>();
    //public List<GameObject> listTower;
    //public GameObject nexus;
    //public GameObject[] arrTowers = new GameObject[2]; //0 -> Left, 1 -> Right

    public float targetDist;
    public Vector3 targetDir;
    public float targetDegAngle;
    /// <target>

    private float atkCurTime = 0f;

    public float searchTime = 0.5f;
    public float searchCurTime = 0f;


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
		//이것이 ㄹㅇ 찐 각도
	}

	protected void Death()
    {
        Destroy(gameObject);
        //이 유닛 참조하고 있는 다른 놈들에 대해서도 예외처리 필요.
        //또 이거 쓰면 그 머다냐 메모리 릭 생긴다는 얘기도 있음.
        //일단 사용 ㄴㄴㄴ
    }


    protected void Walk()
    {
        //지금은 그냥 타겟 있을때만 그쪽으로 걸어가는 방식.

        //Vector3 dir = Vector3.Normalize(_target.transform.position - gameObject.transform.position);
        CalcToObj(targetObj);
        if (targetDist > unitStatus.atkRagne)
        {
            transform.position += transform.forward * unitStatus.moveSpd * Time.deltaTime;
            //Debug.Log(unitStatus.moveSpd + "로 걷고 있습니다.");
        }
    }

    protected virtual bool Attack(GameObject _target)
    {
        //지금 타워처럼 스케일이 1 이상인 애들도 
        //공격 범위만큼 가까이 가야 때릴 수 있음.
        // 추후 콜리더 범위로 수정 해야함.
        if (_target != null && targetDist <= unitStatus.atkRagne)
        {
            atkCurTime += Time.deltaTime;

            Units temp = _target.GetComponent<Units>(); //=> null 나옴. 근데 그게 맞지 ㅋㅋ 안넣었으니까 ㅋㅋ

            if (atkCurTime >= unitStatus.atkSpd)
            {
                //추후 각 유닛의 무기에 따라서 콜리더 판정으로 넘기기
                Debug.Log(unitStatus.atkRagne + "의 범위로\n" + unitStatus.moveSpd + "의 데미지를 줍니다.");
                temp.Hit((int)unitStatus.dmg);
                atkCurTime = 0f;
                return true;
            }
        }
        else if (_target == null && atkCurTime != 0f)
        {
            atkCurTime = 0f;
            return false;
        }

        return false;
    }

    protected void SearchUnit()
    {
        //1. 소환 됐을 때 가까운 라인 파악.
        //2. 가까운 라인의 상대 타워 유무 파악
        //3. 아직 있을 경우 상대 타워 타겟으로 지정.
        //4. 상대 몬스터 생성되면(에너미 리스트에 존재할 경우) 서치 시작
        //=> 매 프레임이 아니라 일정 시간마다
        //5. 가장 가까운 유닛 타겟이 정해지면, 타겟을 바꾼 뒤 그쪽으로 이동
        //6. 사정거리안에 들어오면 공격시작
        //7. 공격 해서 적 몬스터 상태가 isDead == true되면 
        //objectManager에서 리스트에서도 지우고 
        //타겟도 취소
        //8. 다시 1번으로 돌아가기

        //매 프레임마다 돌리지는 말구 
        //타겟 유닛이 없을때 몇초마다 돌리기?
        //타겠이 정해졌을때는 안 돌리다가? 타겟이 죽었을 경우 재 탐색?

        //일단 무적권 가까운 타워를 목표로 잡기
        //그 쪽으로 걸어가다가 범위안에 적 있으면 타겟 바꾸기

        //Debug.Log(unitStatus.sightRange + "의 범위로 적을 찾고 있습니다.");

        listTarget = UnitManager.instance.unitList[Funcs.B2I(!isEnemy)]; 
            

        if (listTarget.Count == 0)
        {
            SearchTower();
        }
        else
        {
            float lowDist = -1f;

            for (int i = 0; i < listTarget.Count; ++i)
            {
                float dist = Vector3.Magnitude(listTarget[i].transform.position - this.transform.position);

                if (dist <= unitStatus.sightRange)
                {
                    if (lowDist > dist || lowDist < 0f)
                    {
                        lowDist = dist;
                        targetObj = listTarget[i];
                    }
                }
            }

            if (lowDist == -1f)
            {
                SearchTower();
            }
        }
    }

    protected void SearchTower()
    {//가까운 라인 파악하고 타워 확인하는거.
        
        
        if (transform.position.x > 0)
        {
            int temp = Funcs.B2I(!isEnemy);
            targetObj = TowerManager.instance.towerList[Funcs.B2I(!isEnemy), Defines.right];
        }
        else 
        {

            targetObj = TowerManager.instance.towerList[Funcs.B2I(!isEnemy), Defines.left] ;

            //if (isEnemy)
            //{
            //    targetObj = TowerManager.instance.towerList[Defines.ally, Defines.left] ;
            //}
            //else { targetObj = TowerManager.instance.towerList[Defines.enemy, Defines.left]; }
        }
    }

    public void Hit(int _dmg)
    {
        float temp = unitStatus.hp;
        unitStatus.hp -= _dmg;
        Debug.Log(_dmg + "의 데미지를 받아\n" + temp + "에서" + unitStatus.hp + "가 되었습니다");
    }

    protected virtual void Awake()
    {
        SearchUnit();



    }

    protected virtual void Start()
    { 
    


    }
    protected virtual void Update()
    {//가상함수로 만들면
     //모노비헤이비어가 update 돌릴 때 오버 라이딩된 자식 함수가 돌게되고
     //그때 맨 처음 base.update()로 돌리기
        //Attack();


        #region Search
        searchCurTime += Time.deltaTime;

        if (searchCurTime >= searchTime)
        { 
            SearchUnit();
            searchCurTime = 0f;
        }
        #endregion

        if (targetObj != null)
        {
            transform.LookAt(targetObj.transform);
        }


        if (unitStatus.hp <= 0f)
        {
            unitStatus.isDead = true;
        }
    }

	void OnDrawGizmos()
    {
        if (targetObj != null)
        {
            Gizmos.color = Color.green;

            Gizmos.DrawLine(transform.position, targetObj.transform.position);
        }
    }


}


