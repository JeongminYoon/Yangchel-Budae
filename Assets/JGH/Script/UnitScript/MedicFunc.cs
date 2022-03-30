using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicFunc : Units
{
	public override bool Attack(GameObject _target)
	{

		return true;
	}


	public override void SearchUnit()
	{//아군 유닛만 타겟으로 잡도록.
		
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
