using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeFunc : Units
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();

        Walk(target);

        if (Input.GetMouseButton(0))
        {
            Search();
        }

        if (target != null)
        { targetDistance = Vector3.Magnitude(this.gameObject.transform.position - target.transform.position); }


        Attack(target);
    }
}
