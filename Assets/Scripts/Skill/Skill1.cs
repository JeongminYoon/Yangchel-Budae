using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill1 : MonoBehaviour
{
    public UnitStatus skillStatus_Origin;
    private UnitStatus skillStatus;



    public GameObject airplanPrefab;
   
  
    // Start is called before the first frame update
    void Start()
    {
     //Scriptable object Deepcopy//
        skillStatus = ScriptableObject.CreateInstance<UnitStatus>();
        skillStatus.DeepCopy(skillStatus_Origin);
        //Scriptable object Deepcopy//

        //Instantiate(airplanPrefab);
        Vector3 airpos = new Vector3(0, 20, -33);
        Instantiate(airplanPrefab, airpos, Quaternion.identity);
        CameraShake.instance.Shake(12f,0.05f,0.05f);
    }

    // Update is called once per frame
    void Update()
    {



    }
}
