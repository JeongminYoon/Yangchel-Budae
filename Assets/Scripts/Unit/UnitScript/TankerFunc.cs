using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankerFunc : Units
{//탱커는 유닛 공격 안하고 기냥 무적권 타워 향해서 돌격 해야함.
	public override bool Attack(GameObject _target)
	{
		return base.Attack(_target);
	}

	public override void Walk()
	{
		if (targetObj != null)
		{
			CalcToObj(targetObj);

			if (targetDist > unitStatus.atkRange)
			{
				transform.position += targetDir * unitStatus.moveSpd * Time.deltaTime;
				//Debug.Log(unitStatus.moveSpd + "로 걷고 있습니다.");
			}
		}
		else
		{
			SearchTower();
		}
	}

	protected override void Awake()
	{
		base.Awake();
	}

	protected override void Start()
	{

		DeathEventSetting();

		ScriptableObj_DeepCopy(); //깊은 복사

		SearchTower();
	}

	protected override void Update()
	{
		//base.Update();

		if (targetObj == null)
		{
			searchCurTime += Time.deltaTime;

			if (searchCurTime >= searchTime)
			{

				SearchTower();
				searchCurTime = 0f;
			}
		}
		Walk();
		Death(handlerDeath);
	}


}
