using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Units
{
	//Units 클래스는 기본적으로 ㄹㅇ 유닛들한테 맞춰서 설계해놓은거라서
	//타워클래스는 앵간하면 부모함수(base.Func()) 콜 하지말고
	//함수 재정의 (override)해서 쓰거나 함수 새로 만들어서 쓸 예정.
			//그대로 쓰기엔 조금 다른 부분들 유
	//외부에서 타워 함수 부를때도 GetComponent<Units>() as Tower으로 불러서 함수 호출하삼

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
			bullet.GetComponent<Bullet>().dmg = (int)unitStatus.dmg;

			return true;
		}

		return false;
	}

	//기본 3종 Cycle 함수도 부모꺼 호출 금지
	protected override void Awake()
	{

	}

	protected override void Start()
	{
		ScriptableObj_DeepCopy();
	}

	protected override void Update()
	{

	}

}
