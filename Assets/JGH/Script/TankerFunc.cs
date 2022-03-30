using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankerFunc : Units
{
	public override bool Attack(GameObject _target)
	{
		return base.Attack(_target);
	}

	public override void Death(HandlerDeath handler)
	{
		base.Death(handler);
	}

	protected override void Walk()
	{
		base.Walk();

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
