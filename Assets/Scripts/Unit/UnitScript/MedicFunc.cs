using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class MedicFunc : Units
{

	public GameObject medicinePrefab;
	public GameObject medicLeftHand = null;

	public GameObject healTarget;
	private float healFulltime = 2f;
	private float healCurTime = 2f;
	private float healDistance;
	

	public override bool Attack(GameObject _target)
	{
		//1. 힐이 우선적(아군 유닛 개수부터 파악하기)
		//1-1. 체력 비율이 제일 적은...?
		//=> 이러면 탱커한테 달려갈텐데...
		//거리안의 애들중에서 가장 비율이 적은 친구로
		//2. 아군 유닛이 없다면 공격
		//3. 아군 유닛들이 모두 체력이 꽉 차있다면 힐x 공격하기.
		//둘다 타겟으로 잡아놓고 공격할때 우선순위 지정하기

		//1. 아군 유닛이 있을 경우 
		//=>힐 타겟 있을경우
		//=> 그냥 타겟은 타워도 잡히기 때문에 예외 처리 안됨.
		if (healTarget != null)
		{//아군 힐 타겟 있으면 힐하기

			if (healDistance <= unitStatus.atkRange)
			{
				healCurTime += Time.deltaTime;

				if (healCurTime >= healFulltime)
				{
					healCurTime = 0f;

					if (animController.GetCurrentAnimatorStateInfo(0).IsName("Medic_Throw"))
					{
						animController.Play("Medic_Throw", -1, 0f);
					}
					else
					{
						animController.SetTrigger("tHeal");
					}
				}
			}



		}
		else
		{//아군 없으면 공격
			if (base.Attack(targetObj))
			{

				//if (isLookTarget)
				//{
				//	gameObject.transform.Rotate(new Vector3(0f, MuzzleToTarget(), 0f));
				//}

				//isLookTarget = false;


				if (animController.GetCurrentAnimatorStateInfo(0).IsName("Medic_Attack"))
				{
					animController.Play("Medic_Attack", -1, 0f);
				}
				else
				{
					animController.SetTrigger("tAttack");
				}

				weaponScript.targetObj = _target;
			}

		}


		//if (base.Attack(_target))
		//{
		//	Units temp = _target.GetComponent<Units>();

		//	if (temp != null)
		//	{
		//		if (temp.isEnemy)
		//		{

		//			if (animController.GetCurrentAnimatorStateInfo(0).IsName("Medic_Attack"))
		//			{
		//				animController.Play("Medic_Attack", -1, 0f);
		//			}
		//			else
		//			{
		//				animController.SetTrigger("tAttack");
		//			}
		//		}
		//		else
		//		{
		//			if (animController.GetCurrentAnimatorStateInfo(0).IsName("Medic_Throw"))
		//			{
		//				animController.Play("Medic_Throw", -1, 0f);
		//			}
		//			else
		//			{
		//				animController.SetTrigger("tHeal");
		//			}
		//		}
		//	}
		//}
		////if (base.Attack(_target))
		////{
		////	GameObject medicine = Instantiate(medicinePrefab,transform.position, Quaternion.identity);
		////	Medicine temp = medicine.GetComponent<Medicine>();
		////	if (temp != null)
		////	{
		////		temp.healAmount = (int)unitStatus.dmg;
		////		temp.targetObj = _target;
		////		temp.medic = this.gameObject;
		////		return true; 
		////	}
		////	return false;
		////}

		return false;
	}

	public void Fire()
	{
		if (weapon != null)
		{
			weaponScript.Fire(this.gameObject.transform.rotation);
		}
	}


	public void Heal()
	{
		GameObject medicine = Instantiate(medicinePrefab, medicLeftHand.transform.position, Quaternion.identity);
		Medicine temp = medicine.GetComponent<Medicine>();

		if (temp != null)
		{
			temp.healAmount = (int)(unitStatus.dmg / 2f);
			temp.targetObj = healTarget;
			temp.medic = this.gameObject;
		}
	}

	public override void SearchUnit()
	{
		//아군 유닛만 타겟으로 잡도록.
		//타워 히트는 없는걸루...?
		//있으면 그냥 units의 SearchUnit에서 예외처리만 해주면 되고
		//==> 걍 아예 새로 만들어야할듯
		// 적 타워한테 어그로 끌리는건 아군들 다 없어야함
		//base.SearchUnit();

		//1. 힐이 우선적(아군 유닛 개수부터 파악하기)
		//1-1. 체력 비율이 제일 적은...?
		//=> 이러면 탱커한테 달려갈텐데...
		//거리안의 애들중에서 가장 비율이 적은 친구로
		//2. 아군 유닛이 없다면 공격
		//3. 아군 유닛들이 모두 체력이 꽉 차있다면 힐x 공격하기.
		//둘다 타겟으로 잡아놓고 공격할때 우선순위 지정하기

		base.SearchUnit();

		List<GameObject> listAlly = UnitManager.instance.GetUnitList_Val(Funcs.B2I(isEnemy));
		listAlly.Remove(this.gameObject);

		if (listAlly.Count == 0)
		{
			healTarget = null;
		}
		else
		{
			listAlly = Funcs.FindUnitsInSightRange(this.gameObject, listAlly);
			//여기서 제일 체력 비율이? 낮은 친구
			//GameObject damagedUnit = null;
			float leastHpRatio = 1.1f;
			foreach (GameObject obj in listAlly)
			{
				Units temp = obj.GetComponent<Units>();
				float hpRatio = temp.unitStatus.curHp / temp.unitStatus.fullHp;

				if (temp != null)
				{
					if (hpRatio < 1f && hpRatio < leastHpRatio)
					{
						leastHpRatio = hpRatio;
						healTarget = obj;
						healDistance = Vector3.Magnitude(obj.transform.position - transform.position);
					}
				}
			}
			//= damagedUnit;
		}
	}

	public void HandSetting()
	{
		if (medicLeftHand == null)
		{
			medicLeftHand = Funcs.FindGameObjectInChildrenByName(this.gameObject, "Hand_L");
		}
	}

	//public void LookState(int lookState)
	//{
	//	isLookTarget = Funcs.I2B(lookState);
	//}


	protected override void Awake()
	{
		base.Awake();
	}

	protected override void Start()
	{
		base.Start();
		if (weapon != null)
		{
			weaponScript.FindMuzzle();
		}
		HandSetting();
		SearchUnit();
	}

	protected override void Update()
	{
		//base.Update();
		searchCurTime += Time.deltaTime;

		if (searchCurTime >= searchTime)
		{
			SearchUnit();
			searchCurTime = 0f;
		}

		//if (isLookTarget)
		//{
		if (healTarget != null)
		{
			transform.LookAt(healTarget.transform);
		}
		else if (targetObj != null)
		{
			transform.LookAt(targetObj.transform);
		}

		//}


		Death(handlerDeath);
		Walk();
		Attack(targetObj);
	}


}
