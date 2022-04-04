using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Units
{
	//Units 클래스는 기본적으로 ㄹㅇ 유닛들한테 맞춰서 설계해놓은거라서
	//타워클래스는 앵간하면 부모함수(base.Func()) 콜 하지말고
	//부모 가상 함수 재정의(override)해서 쓰거나 함수 새로 만들어서 쓸 예정.
			//그대로 쓰기엔 조금 다른 부분들 유
	//외부에서 타워 함수 호출 할 일있으면 GetComponent<Units>() as Tower으로 불러서 함수 호출 해주삼

	public GameObject bulletPrefab;

	public override bool Attack(GameObject _target)
	{
		//피격판정은 콜리더로 하기
		if (base.Attack(_target)) //실제 Unit쪽에서 공격 성공하고 나서 총알 생성
		{
			Transform gunTr = transform.GetChild(0).transform;
			Vector3 gunPos = gunTr.position;
			Quaternion gunRot = gunTr.rotation;
			GameObject bullet = Instantiate(bulletPrefab, gunPos, gunRot);
			
			////for Test
			bullet.transform.LookAt(targetObj.transform);
			////for Test
			////추후에는 타워용 총알 따로 놔둘꺼고 되게 빠르게 할꺼임 
			////총알 자체도 손좀 많이 봐야하고

			bullet.GetComponent<Bullet>().dmg = (int)unitStatus.dmg;

			return true;
		}

		return false;
	}

	public override void SearchUnit()
	{
		listTarget = UnitManager.instance.unitList[Funcs.B2I(!isEnemy)];


		if (listTarget.Count == 0)
		{
			targetObj = null;
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
				targetObj = null;
			}
		}
	}

	public override void DeathEventSetting()
	{
		//base.DeathEvent();

		handlerDeath = new HandlerDeath(TowerManager.instance.RemoveDeadTower);
		//handlerDeath += UnitManager.instance.ResearchTarget_AllUnit;

	}

	public override void Death(HandlerDeath handler)
	{
		if (unitStatus.curHp <= 0f)
		{
			unitStatus.isDead = true;
			handler(this.gameObject);
			Destroy(this.gameObject);
		}
	}

	//기본 3종 Cycle 함수도 부모꺼 호출 금지
	protected override void Awake()
	{
		base.Awake();
	}

	protected override void Start()
	{
		DeathEventSetting();
		ColliderSetting();
		//ScriptableObj_DeepCopy();

		searchTime = 0.25f;

	}

	protected override void Update()
	{

		if (UnitManager.instance.unitList[Funcs.B2I(!isEnemy)].Count != 0)
		{
			searchCurTime +=	Time.deltaTime;

			if (searchCurTime >= searchTime)
			{
				SearchUnit();
				searchCurTime = 0f;
			}
		}

		if (targetObj != null)
		{ Attack(targetObj); }


		Death(handlerDeath);
	}

}
