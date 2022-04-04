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
     protected override void Start()
    {
        base.Start();

        
    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();

        Walk();

        //if (targetObj != null)
        //{ targetDist = Vector3.Magnitude(this.gameObject.transform.position - targetObj.transform.position); }


        Attack(targetObj);
    }

    


}
