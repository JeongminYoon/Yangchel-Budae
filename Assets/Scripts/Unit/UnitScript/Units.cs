using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;

abstract public class Units : MonoBehaviour
{

    public float DotTestAngle;
    public float DotTestAngle2;
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

    public float targetDist;
    public Vector3 targetDir;
    public float targetDegAngle;
    public float targetColSize;
    /// <target>

    public GameObject hpBar = null;

    public GameObject center = null;

    public GameObject weapon = null;
    public Weapon       weaponScript;

    public Collider unitCol;

    private float atkCurTime = 0f;

    public float searchTime = 0.5f;
    public float searchCurTime = 0f;



    public NavMeshAgent navAgent;
    public Animator animController;
    //public CharacterController charContoller;


    public delegate void HandlerDeath(GameObject unit);
    public HandlerDeath handlerDeath;


    //간단한 FSM를 위한 용도 -> 나중에 시간남으면 찐 FSM으로 바꿔줄 예정
    public Enums.UnitState preState = Enums.UnitState.End;
    public Enums.UnitState curState = Enums.UnitState.Walk;
    public void SetState(Enums.UnitState state)
    {
        if (curState != state )
        {
            preState = curState;
            curState = state;
        }
    }
    public bool CompareState(Enums.UnitState state)
    {
        return curState == state ? true : false;
    }

    protected void CalcToObj(GameObject obj)
    {
        //float targetColSize = obj.GetComponent<Units>().unitStatus.unitColScale.x;
        //float myColSize = this.gameObject.GetComponent<Units>().unitStatus.unitColScale.x;

        Units temp = obj.GetComponent<Units>();

        if (temp != null)
        {
            targetColSize = temp.unitStatus.unitColScale.x;
        }
        else 
        {
            targetColSize = 0f;
        }


        Vector3 targetPos = obj.transform.position;
        targetPos.y = this.gameObject.transform.position.y;

        Vector3 toTargetVector = targetPos - this.transform.position;
        //Vector3 toTargetVector = obj.transform.position - this.transform.position;

        targetDist = toTargetVector.magnitude;
        //targetDist -= (targetColSize/2f + myColSize/2f);

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

        #region 차후 수정 요망
        //if (obj.GetComponent<Units>() != null)
        //{
        //    float targetColSize = obj.GetComponent<Units>().unitStatus.unitColScale.x;
        //    float myColSize = this.gameObject.GetComponent<Units>().unitStatus.unitColScale.x;

        //    Vector3 targetPos = obj.transform.position;
        //    targetPos.y = this.gameObject.transform.position.y;

        //    Vector3 toTargetVector = targetPos - this.transform.position;
        //    //Vector3 toTargetVector = obj.transform.position - this.transform.position;

        //    targetDist = toTargetVector.magnitude;
        //    targetDist -= (targetColSize / 2f + myColSize / 2f);

        //    targetDir = toTargetVector.normalized;
        //    float dotProduct = Vector3.Dot(obj.transform.position, this.transform.position);
        //    //dot, 즉 내적의 결과값 => Cos @ 값 (-1~1값 /0보다 크면 내 앞, 작으면 내 뒤)
        //    float RadAngle = Mathf.Acos(dotProduct);
        //    //역 코사인 걸면 라디안 값 
        //    targetDegAngle = RadAngle * Mathf.Deg2Rad;
        //    //이것이 ㄹㅇ 찐 각도

        //    //포워드 벡터와 사이벡터 내적 결과값 (cos @)
        //    //0-> 수직
        //    //1-> 평행
        //    //음수 -> 뒤
        //    //양수 -> 앞
        //    // 시야각/2 보다 크면 시야내에 있음
        //}
        #endregion
    }

    public virtual void Death(HandlerDeath handler)
    {
        if (!unitStatus.isDead && unitStatus.curHp <= 0f)
        {
            unitStatus.isDead = true;

            //유닛 콜라이더 끄기 
            if (unitCol != null)
            {
                unitCol.enabled = false;
            }

            //네비매쉬 끄기 
            if (navAgent != null)
            {
                navAgent.enabled = false;
            }

            //UnitManager에서 제외하기
            handler(this.gameObject);

            //자기 타겟, 무기 타겟, (원거리면) 총알 타겟 릴리즈
            Release();

            //DeathAnimation 재생하기
        }
        //Destroy(gameObject);
        //이 유닛 참조하고 있는 다른 놈들에 대해서도 예외처리 필요. => 0324 Unit Manager로 처리 완료
        //또 이거 쓰면 그 머다냐 메모리 릭 생긴다는 얘기도 있음.
            //=> 가비지 컬렉터 구조상 디스트로이 전에 참조하고 있는 애들 부분에서 없애줘야함.
    }


    public virtual void Walk()
    {
        if (!unitStatus.isDead)
        {
            //지금은 그냥 타겟 있을때만 그쪽으로 걸어가는 방식.

            if (targetObj != null)
            {
                CalcToObj(targetObj);

                if (targetObj.CompareTag("Tower") || targetObj.CompareTag("Nexus"))
                {
                    if (targetDist > unitStatus.atkRange + targetColSize)
                    {
                        if (!unitStatus.isDead)
                        { navAgent.isStopped = false; }
                    }
                    else
                    {
                        if (!unitStatus.isDead)
                        { navAgent.isStopped = true; }
                    }
                }
                else
                {
                    if (targetDist > unitStatus.atkRange)
                    {
                        if (!unitStatus.isDead)
                        { navAgent.isStopped = false; }

                    }
                    else
                    {
                        if (!unitStatus.isDead)
                        { navAgent.isStopped = true; }
                    }
                }
            }
            else
            {
                SearchUnit();
            }
        }
        else 
        {
            int a = 0;
        }
    }

	public virtual bool Attack(GameObject _target)
    {
        if (!unitStatus.isDead)
        {
            //지금 타워처럼 스케일이 1 이상인 애들도 
            //공격 범위만큼 가까이 가야 때릴 수 있음.
            // 추후 콜리더 범위로 수정 해야함.
            //if (_target != null && targetDist <= unitStatus.atkRange + targetColSize + unitStatus.unitColScale.x)

            if (_target != null)
            {
                Units unitScript = _target.GetComponent<Units>();

                if (unitScript.unitStatus.isDead)
                {
                    return false;
                }

                if (_target.CompareTag("Tower") || _target.CompareTag("Nexus"))
                {
                    if (targetDist <= unitStatus.atkRange + targetColSize)
                    {
                        atkCurTime += Time.deltaTime;

                        if (atkCurTime >= unitStatus.atkSpd)
                        {
                            atkCurTime = 0f;
                            return true;
                        }
                    }
                }
                else
                {
                    if (targetDist <= unitStatus.atkRange)
                    {
                        atkCurTime += Time.deltaTime;

                        if (atkCurTime >= unitStatus.atkSpd)
                        {
                            atkCurTime = 0f;
                            return true;
                        }
                    }
                }


            }
        }
        return false;
    }

    public virtual void SearchUnit()
    {
        if (!unitStatus.isDead)
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

            //if (unitStatus.unitName == "Medic")
            //{//메딕 본인도 가져와버림.
            //    listTarget = UnitManager.instance.unitList[Funcs.B2I(isEnemy)].ToList<GameObject>();

            //    listTarget.Remove(this.gameObject);
            //}
            //else 
            //{
            listTarget = UnitManager.instance.unitList[Funcs.B2I(!isEnemy)].ToList<GameObject>();
            //}

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

                            //NavTest
                            navAgent.SetDestination(targetObj.transform.position);
                        }
                    }
                }

                if (lowDist == -1f)
                {
                    SearchTower();
                }
            }
        }
    }

    protected void SearchTower()
    {
        if (unitStatus.isDead)
        { return; }

        //나중에 스킬2 완성되면 스킬2 타워도 찾는걸로 바꾸기

        //For only unit
        //가까운 라인 파악하고 타워 확인하는거.
        if (transform.position.x > 0)
        {
            targetObj = TowerManager.instance.towerList[Funcs.B2I(!isEnemy), Defines.right];
        }

        if(transform.position.x <= 0)
        {
            targetObj = TowerManager.instance.towerList[Funcs.B2I(!isEnemy), Defines.left] ;
        }

        if (targetObj == null)
        {
            if (transform.position.x > 0)
            { targetObj = TowerManager.instance.towerList[Funcs.B2I(!isEnemy), Defines.left]; }
            else
            { targetObj = TowerManager.instance.towerList[Funcs.B2I(!isEnemy), Defines.right]; }

            
            if (targetObj == null)
            {
                //220427 이제 스킬2는 공격 안하기로 함.
				//if (SkillManager.instance.isSkill2Live)
				//{
				//	Skill2 tempSkill2 = SkillManager.instance.skill2.GetComponent<Skill2>();

				//	if (tempSkill2 != null)
				//	{ targetObj = tempSkill2.tower; }
				//}
				//else
				//{
					targetObj = TowerManager.instance.nexusList[Funcs.B2I(!isEnemy)];
				//}

			}
        }

        //NavTest
        navAgent.SetDestination(targetObj.transform.position);
    }

    public void DeadTargetException(GameObject isDeadTarget)
    {
        //UnitManager에서 유닛 한마리 죽으면 자동으로 모든 애들중 죽은 애를 타겟으로 잡고있는 래~끼한테 실행.
        //본인 예외 처리가 아니라 죽은 본인을 타겟으로 잡고있는 오브젝트들한테 예외 처리를 해줄 부분.

            //밀리 => 웨펀한테만 알려주고 공격 애니메이션 취소
            //레윈쥐 -> 웨펀한테 알려주고 웨펀은 불ㅡㅡ릿들한테 말해주기 / 공격 애니메이션 취소
            //메ㅡ딕 -> 웨펀한테 알려주고 웨펀은 불ㅡㅡ릿들한테 말해주기
                // 메디슨들한테 알려주기 / 공격,힐 애니메이션 취소
            //탱커 -> 웨펀한테 알려주고 공격 애니메이션 취소.

        //이거 실행되고 바로 리서치 유닛 들어감.


        //220502 14:40 => 총알과 약품은 알아서 isDead 확인해서 target = null 해주기.


        if (targetObj == isDeadTarget)
        {

            targetObj = null; //타겟 없애기

            //공격중이라면 공격 애니메이션 취소하기
            
            if (weaponScript != null) //무기에서 타겟 없애기
            {
                //weaponScript.targetObj = null; 
                weaponScript.DeadTargetException(isDeadTarget);
            }

            //if (unitStatus.unitNum == (int)Enums.UnitClass.medic)
            //{
            //    //메딕이면 약품에서도 처리해주기.
            //}

        }
    }

    public void Hit(int _dmg)
    {
        if (!unitStatus.isDead)
        {
            float temp = unitStatus.curHp;
            unitStatus.curHp -= _dmg;
            Debug.Log(_dmg + "의 데미지를 받아\n" + temp + "에서" + unitStatus.curHp + "가 되었습니다");
        }

        //DamageUIManager.instance.PlayHpEffect(_dmg, this.gameObject.transform.position + new Vector3(0f, 3.2f, 0f));
    }

    public void Cure(int _healAmount)
    {
        if (!unitStatus.isDead)
        {
            float temp = unitStatus.curHp;
            unitStatus.curHp += _healAmount;
            Debug.Log(_healAmount + "의 힐링을 받아\n" + temp + "에서" + unitStatus.curHp + "가 되었습니다");

            if (unitStatus.curHp > unitStatus.fullHp)
            {

                unitStatus.curHp = unitStatus.fullHp;
            }
        }

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
        //Unit 말고 걍 깡통 옵줵 두 캡슐 콜라이더, 박스 콜라이더 가지고 있는 애들로 테스트 해보자.
        unitCol = this.gameObject.GetComponent<Collider>();

        Vector3 tempSize = new Vector3();

        if (unitCol != null)
        {
            if (unitCol as CapsuleCollider != null)
            {
                tempSize.x = (unitCol as CapsuleCollider).radius/2f;
                tempSize.x *= transform.localScale.x;
                
                tempSize.z = (unitCol as CapsuleCollider).radius / 2f;
                tempSize.z *= transform.localScale.z;
                
                tempSize.y = (unitCol as CapsuleCollider).height;
                tempSize.y *= transform.localScale.y;
            }
            else if (unitCol as BoxCollider != null)
            {
                tempSize.x = (unitCol as BoxCollider).size.x * this.gameObject.transform.localScale.x;
                tempSize.y = (unitCol as BoxCollider).size.y * this.gameObject.transform.localScale.y;
                tempSize.x = (unitCol as BoxCollider).size.z * this.gameObject.transform.localScale.z;
            }
        }

        unitStatus.unitColScale = tempSize;

    }
    public void WeaponSetting()
    {
        if (weapon == null)
        {
            weapon = Funcs.FindGameObjectInChildrenByTag(this.gameObject, "Weapon");
        }

        if (weapon != null)
        {
            weaponScript = weapon.GetComponent<Weapon>();

            weaponScript.dmg = unitStatus.dmg;
            weaponScript.isEnemy = isEnemy;
        }
	}

		public void CenterSetting()
    {
        if (center == null)
        {
            center = Funcs.FindGameObjectInChildrenByName(this.gameObject, "Center");
        }
    }

    public float MuzzleToTarget()
    {
        //지금 사용 안됨 ㄴㄴㄴ
        if(weaponScript.muzzle != null) 
        {
			Vector3 muzzleForward = weaponScript.muzzle.transform.forward.normalized;
            Vector3 dir = (targetObj.transform.position - weaponScript.muzzle.transform.position).normalized;
			
			float muzzleToTargetAngle = Vector3.Dot(muzzleForward, dir);

            muzzleToTargetAngle = Mathf.Acos(muzzleToTargetAngle);
            muzzleToTargetAngle *= Mathf.Rad2Deg;
            
            DotTestAngle = muzzleToTargetAngle;


            Vector3 dir2 = targetObj.transform.position - transform.position;
            DotTestAngle2 = Vector3.SignedAngle(transform.up, weaponScript.muzzle.transform.position.normalized, dir.normalized);


            return muzzleToTargetAngle;
        }

        return 0f;
    }


    protected virtual void Awake()
    {
        ScriptableObj_DeepCopy(); //깊은 복사
    }

    protected virtual void Start()
    {
        DeathEventSetting();

        //unitStatus = unitStatus_Origin; 얕은 복사 Shallow
        //ScriptableObj_DeepCopy(); //깊은 복사

        WeaponSetting();
        CenterSetting();
        animController = this.gameObject.GetComponent<Animator>();
        //charContoller = this.gameObject.GetComponent<CharacterController>();
       //navMeshTest
        navAgent = this.gameObject.GetComponent<NavMeshAgent>();
        navAgent.speed = unitStatus.moveSpd;

        atkCurTime = unitStatus.atkSpd;

        SearchUnit();

        if (targetObj != null)
        { transform.LookAt(targetObj.transform); }

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

        Death(handlerDeath);

        //MuzzleToTarget();
    }

    public virtual void Release()
    {
        targetObj = null;

        if (weapon != null)
        {
            Weapon weaponScript = weapon.GetComponent<Weapon>();

            if (weaponScript != null)
            {
                weaponScript.Release();
            }
        }

        listTarget.Clear();

        //hp바는 어디에서 할까 
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


