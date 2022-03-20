using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{

    int iTemp = 0;
    // Start is called before the first frame update
    void Start()
    {
        //iTemp = Units.instance.GetSetInt;
        //Debug.Log(iTemp);

        //Units.instance.GetSetInt = 1;
        //Debug.Log(Units.instance.GetSetInt);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UnitFactory.instance.SpawnMeleeUnit(new Vector3(0f, 1f, 0f));
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            UnitFactory.instance.SpawnRangeUnit(new Vector3(0f, 1f, 0f));
        }  

    }
}
