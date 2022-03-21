using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeFunc : Units
{
    protected override void Awake()
    {
        base.Awake();

    }

    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();

    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();

        Walk();

        if (Input.GetKey(KeyCode.Space))
        {
            SearchUnit();
        }

        //if (targetObj != null)
        //{ targetDist = Vector3.Magnitude(this.gameObject.transform.position - targetObj.transform.position); }


        Attack(targetObj);
    }

   
}
