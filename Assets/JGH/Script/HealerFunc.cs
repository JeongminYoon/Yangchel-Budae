using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealerFunc : Units
{
	public override bool Attack(GameObject _target)
	{
		return base.Attack(_target);
	}

	public override void Death(HandlerDeath handler)
	{
		base.Death(handler);
	}

	public override void SearchUnit()
	{
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
	}


}
