using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nexus : Tower
{

	//public override void DeathEventSetting()
	//{
	//	handlerDeath = new HandlerDeath(TowerManager.instance.RemoveDeadTower);
	//}

	public override void Death(HandlerDeath handler = null)
	{
		if (unitStatus.curHp <= 0f)
		{
			unitStatus.isDead = true;

			GameManager.instance.InGameResultCheck(isEnemy);

			//handler(this.gameObject);
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
		 
		ColliderSetting();
	}

	// Update is called once per frame
	protected override void Update()
	{
		//base.Update();

		Death(handlerDeath);
	}
}
