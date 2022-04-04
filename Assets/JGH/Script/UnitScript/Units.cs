using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
abstract public class Units : MonoBehaviour
{
    //C#에서 가상함수(virtual 키워드)는
    //딱히 자식 클래스에서 재정의(override) 안해도
    //자동적으로 불러와짐
        //abstract 함수로 선언하면 무조건 적으로 자식 클래스에서 override 해줘야함
                //=> 애초에 abstract 함수 선언하면 본문 정의 불가능.
                //=> 그리고 무조건 override키워드로 재정의 해야함
                        //=>virtual 함수는 new 키워드로 재정의 가능.

    public UnitStatus   unitStatus_Origin; //스크립터블 오브젝트 원본

    //[SerializeField]
    [HideInInspector]
    public UnitStatus unitStatus; //스크립터블 오브젝트 원본을 복사한, 코드내에서 실제로 변경될 스텟

   
    public bool isEnemy;
    public bool IsEnemy
    {
        get { return isEnemy; }
        set { isEnemy = value; }
    }
    //protected bool isDead = false;

    /// <target>
    public GameObject targetObj;
    public List<GameObject> listTarget = new List<GameObject>();
    //public List<GameObject> listTower;
    //public GameObject nexus;
    //public GameObject[] arrTowers = new GameObject[2]; //0 -> Left, 1 -> Right

    public float targetDist;
    public Vector3 targetDir;
    public float targetDegAngle;
    /// <target>


    public Collider unitCol;

    private float atkCurTime = 0f;

    public float searchTime = 0.5f;
    public float searchCurTime = 0f;

    public delegate void HandlerDeath(GameObject unit);
    public HandlerDeath handlerDeath;
    protected void CalcToObj(GameObject obj)
    {
        Vector3 targetPos = obj.transform.position;
        targetPos.y = this.gameObject.transform.position.y;

        Vector3 toTargetVector = targetPos - this.transform.position;
        //Vector3 toTargetVector = obj.transform.position - this.transform.position;

        targetDist = toTargetVector.magnitude;
		targetDir = toTargetVector.normalized;
		float dotProduct = Vector3.Dot(obj.transform.position, this.transform.position);
		//dot, 즉 내적의 결과값 => Cos @ 값 (-1~1값 /0보다 크면 내 앞, 작으면 내 뒤)
		float RadAngle = Mathf.Acos(dotProduct);
		//역 코사인 걸면 라디안 값 
		targetDegAngle = RadAngle * Mathf.Deg2Rad;
		//이것이 ㄹㅇ 찐 각도

        //포워드 벡터와 사이벡터 내적 결과값 (cos @)
        //0-> 수직
        //1-> 평행
        //음수 -> 뒤
        //양수 -> 앞
        // 시야각/2 보다 크면 시야내에 있음

	}

	public virtual void Death(HandlerDeath handler)
    {
        if (unitStatus.curHp <= 0f)
        {
            unitStatus.isDead = true;
            handler(this.gameObject);
            Destroy(this.gameObject);
        }

        //Destroy(gameObject);
        //이 유닛 참조하고 있는 다른 놈들에 대해서도 예외처리 필요. => 0324 Unit Manager로 처리 완료
        //또 이거 쓰면 그 머다냐 메모리 릭 생긴다는 얘기도 있음.
    }


    public virtual void Walk()
	{
        //지금은 그냥 타겟 있을때만 그쪽으로 걸어가는 방식.

        //Vector3 dir = Vector3.Normalize(_target.transform.position - gameObject.transform.position);
        if (targetObj != null)
        {
            CalcToObj(targetObj);

            if (targetDist > unitStatus.atkRange)
            {
                //transform.Translate(targetDir * unitStatus.moveSpd * Time.deltaTime);
                transform.position += targetDir * unitStatus.moveSpd * Time.deltaTime;
                //Debug.Log(unitStatus.moveSpd + "로 걷고 있습니다.");
            }
        }
        else 
        {
            SearchUnit();
        }
	}

	public virtual bool Attack(GameObject _target)
    {
        //지금 타워처럼 스케일이 1 이상인 애들도 
        //공격 범위만큼 가까이 가야 때릴 수 있음.
        // 추후 콜리더 범위로 수정 해야함.
        if (_target != null && targetDist <= unitStatus.atkRange)
        {
            atkCurTime += Time.deltaTime;

            //Units temp = _target.GetComponent<Units>(); //=> null 나옴. 근데 그게 맞지 ㅋㅋ 안넣었으니까 ㅋㅋ

            if (atkCurTime >= unitStatus.atkSpd)
            {
                //추후 각 유닛의 무기에 따라서 콜리더 판정으로 넘기기
                //Debug.Log(unitStatus.atkRange + "의 범위로\n" + unitStatus.moveSpd + "의 데미지를 줍니다.");
               // temp.Hit((int)unitStatus.dmg);
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

    public virtual void SearchUnit()
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

        if (unitStatus.unitName == "Medic")
        {//메딕 본인도 가져와버림.
            listTarget = UnitManager.instance.unitList[Funcs.B2I(isEnemy)].ToList<GameObject>();

            listTarget.Remove(this.gameObject);
        }
        else 
        {
            listTarget = UnitManager.instance.unitList[Funcs.B2I(!isEnemy)];
        }



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
    {
        //나중에 스킬2 완성되면 스킬2 타워도 찾는걸로 바꾸기

        //For only unit
        //가까운 라인 파악하고 타워 확인하는거.
        if (transform.position.x > 0)
        {
            //int temp = Funcs.B2I(!isEnemy);
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

        if (targetObj == null)
        {
            targetObj = TowerManager.instance.nexusList[Funcs.B2I(!isEnemy)];
        }
    }

    public void Hit(int _dmg)
    {
        float temp = unitStatus.curHp;
        unitStatus.curHp -= _dmg;
        Debug.Log(_dmg + "의 데미지를 받아\n" + temp + "에서" + unitStatus.curHp + "가 되었습니다");
    }

    public void ScriptableObj_DeepCopy()
    {
        //unitStatus = unitStatus_Origin; //shallow copy -> 얕은 복사
        unitStatus = ScriptableObject.CreateInstance<UnitStatus>();
        unitStatus.DeepCopy(unitStatus_Origin);
    }

    public virtual void DeathEventSetting()
    {
        handlerDeath = new HandlerDeath(UnitManager.instance.RemoveDeadUnit);
        handlerDeath += UnitManager.instance.ResearchTarget_AllUnit;
    }

    public void ColliderSetting()
    {
        unitCol = this.gameObject.GetComponent<Collider>();

        Vector3 tempSize = new Vector3();

        if (unitCol != null)
        {
            if (unitCol as CapsuleCollider != null)
            {
                tempSize.x = (unitCol as CapsuleCollider).radius;
                tempSize.z = tempSize.x;

                tempSize.y = (unitCol as CapsuleCollider).height;
            }
            else if (unitCol as BoxCollider != null)
            {
                tempSize = (unitCol as BoxCollider).size;
            }
        }

        unitStatus.unitColScale = tempSize;

    }

    protected virtual void Awake()
    {
        //SearchUnit();

        ScriptableObj_DeepCopy(); //깊은 복사

    }

    protected virtual void Start()
    {
        DeathEventSetting();

        //unitStatus = unitStatus_Origin; 얕은 복사 Shallow
        //ScriptableObj_DeepCopy(); //깊은 복사

        SearchUnit();

        ColliderSetting();
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
        
        Death(handlerDeath);
    }

	void OnDrawGizmos()
    {
        if (targetObj != null)
        {
            if (this.gameObject.tag == "Tower")
            {
                Gizmos.color = Color.blue;
            }
            else if (this.gameObject.tag == "Enemy")
            { 
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, targetObj.transform.position);
            }
            else
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(transform.position, targetObj.transform.position);
            }

            
        }
    }


}


