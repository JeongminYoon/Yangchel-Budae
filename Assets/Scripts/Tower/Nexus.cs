using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nexus : Tower
{

	public override void DamagedEventSetting()
	{
		base.DamagedEventSetting();
	}

	public override void DamagedFire()
	{
		base.DamagedFire();
	}

	public override void InstantiateFire(int i)
	{
		base.InstantiateFire(i);
	}

	public override void DestoryFires()
	{
		base.DestoryFires();
	}

	public override void Death(HandlerDeath handler = null)
	{
		if (!unitStatus.isDead && unitStatus.curHp <= 0f)
		{
			unitStatus.isDead = true;

			GameManager.instance.InGameResultCheck(isEnemy);

			DestoryFires();

			GameObject boomFx = Instantiate(boomFxPrefab, transform.position, transform.rotation);

			Destroy(this.gameObject);
		}
	}

	protected override void Awake()
	{
		base.Awake();
	}

	protected override void Start()
	{
		//DeathEventSetting();
		DamagedEventSetting();
		ColliderSetting();
	}

	// Update is called once per frame
	protected override void Update()
	{
		//base.Update();

		DamagedFire();
		Death(handlerDeath);
	}
}
