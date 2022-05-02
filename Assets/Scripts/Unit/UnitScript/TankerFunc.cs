﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TankerFunc : Units
{//탱커는 유닛 공격 안하고 기냥 무적권 타워 향해서 돌격 해야함.
	public override bool Attack(GameObject _target)
	{
		if (base.Attack(_target))
		{
			animController.SetTrigger("tAttack");
			weaponScript.targetObj = _target;
			
			return true;
		}
		return false;
	}
	
	public void Slash(int colState)
	{
		if (weapon != null && targetObj != null)
		{
			transform.LookAt(targetObj.transform);
			weaponScript.WeaponColState(colState);
		}
	}

	public override void Walk()
	{
		if (targetObj != null)
		{
			CalcToObj(targetObj);

			if (targetDist > unitStatus.atkRange)
			{
				//transform.position += targetDir * unitStatus.moveSpd * Time.deltaTime;
				if (!unitStatus.isDead)
				{ 
					navAgent.isStopped = false;
					animController.SetBool("bWalk", true);
				}

				//transform.LookAt(targetObj.transform);
			}
			else
			{
				if (!unitStatus.isDead)
				{ 
					navAgent.isStopped = true;
					animController.SetBool("bWalk", false);
				}
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

		WeaponSetting();
		CenterSetting();

		animController = this.gameObject.GetComponent<Animator>();
		//charContoller = this.gameObject.GetComponent<CharacterController>();
		navAgent = this.gameObject.GetComponent<NavMeshAgent>();
		navAgent.speed = unitStatus.moveSpd;


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

		//if (targetObj != null)
		//{
		//	transform.LookAt(targetObj.transform);
		//}

		Walk();
		Attack(targetObj);

		Death(handlerDeath);
		Delete();
	}


}
