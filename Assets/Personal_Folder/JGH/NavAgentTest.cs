using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavAgentTest : MonoBehaviour
{
    //냅매쉬에이전트랑 캐릭타콘트롤러 같이 쓰는거 테스트 

    public NavMeshAgent         agent;
    public CharacterController cc;

    public Vector3 desVelocity;

    public GameObject targetObj = null;


    public GameObject leftTower;
    public GameObject rightTower;

    
    public void SearchTargetTower()
	{
        if (transform.position.x > 0)
        {
            targetObj = rightTower;
        }
        else if (transform.position.x < 0)
        {
            targetObj = leftTower;
        }

	}
	private void Awake()
	{
        agent = GetComponent<NavMeshAgent>();
		cc = GetComponent<CharacterController>();
      
	}

	// Start is called before the first frame update
	void Start()
    {
        SearchTargetTower();
    }

    // Update is called once per frame
    void Update()
    {
        if (targetObj != null)
        { agent.SetDestination(targetObj.transform.position); }

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
            SearchTargetTower();
        //}
    }
}
