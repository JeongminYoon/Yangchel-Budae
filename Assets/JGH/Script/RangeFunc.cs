using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeFunc : Units
{
	protected override void Attack(GameObject _target)
	{
		base.Attack(_target);


	}

	protected override void Awake()
	{
		base.Awake();

	}

	// Start is called before the first frame update
	protected override void Start()
    {
        base.Start();
        
    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
        Walk();

        
    }
}
