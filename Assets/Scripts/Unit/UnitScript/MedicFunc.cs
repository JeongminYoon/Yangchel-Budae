using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicFunc : Units
{
	public GameObject medicinePrefab;

	public override bool Attack(GameObject _target)
	{//야는 힐 하는거
		//풀체인놈 한테 안하고 가장 체력 비율 적은 애한테?
		//이건 상의하고 나서 ㄱㄱ

		if (base.Attack(_target))
		{
			GameObject medicine = Instantiate(medicinePrefab,transform.position, Quaternion.identity);
			Medicine temp = medicine.GetComponent<Medicine>();
			if (temp != null)
			{
				temp.healAmount = (int)unitStatus.dmg;
				temp.targetObj = _target;
				temp.medic = this.gameObject;

				return true; 
			}

			return false;
		}

		return false;
	}


	public override void SearchUnit()
	{//아군 유닛만 타겟으로 잡도록.
	 //타워 히트는 없는걸루...?
	 //있으면 그냥 units의 SearchUnit에서 예외처리만 해주면 되고
			//==> 걍 아예 새로 만들어야할듯
			// 적 타워한테 어그로 끌리는건 아군들 다 없어야함
		base.SearchUnit();
	
	}

	protected override void Awake()
	{
		base.Awake();
	}

	protected override void Start()
	{
		base.Start();
	}

	protected override void Update()
	{
		base.Update();

		Walk();

		Attack(targetObj);
	}


}
