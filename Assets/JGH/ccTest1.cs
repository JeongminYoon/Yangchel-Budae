using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ccTest1 : MonoBehaviour
{

    public   CharacterController cc;
    //캐릭터 컨트롤러 -> 리지드바디와 캡슐콜라이더를 합춰놓은거.
    //움직일때 Move함수를 이용하면 된다.
    //그렇게 안하고 그냥 transform으로 밀어버리면 충돌 물리 연산이 안일어남.


    public float spd = 1.5f;

    public float gravity = -9.8f;
    //CC의 move함수 사용하기 위해 중력가속도 지정
        
    public float yVelocity = 0f;
    //중력처럼 사용할 속도값


    void Start()
    {
        cc = this.gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = -transform.forward;
        //Character Controller의 움직이는 함수 Move 와 simple Move
        //Move(방향 * 속도 * DeltaTime) => 중력값을 따로 계산 안함.
        //=> 따로 우리가 중력값을 곱해줘야함.
        //=> 점프처리 따로 하기 위해서
        //cc.Move(-(transform.forward.normalized) * spd * Time.deltaTime);
        //=>중력값을 고려하지 않고 그냥 앞으로 가는거


        //중력값을 방향에다가 넣어줘야함.
        //yVelocity += gravity * Time.deltaTime;
        //dir.y = yVelocity;
        //cc.Move(dir * spd * Time.deltaTime);
        //하지만 이건 중력가속도가 계속 늘어나기 때문에
        //한번 떨어지면 계속 떨어질꺼임 ㅋㅋ


        if (cc.isGrounded)
        {
            //isGrounded => cc.collisionFlags의 값을 확인하는거임.
            //isGrounded == CollisionFlags.Below
            //Above, Bellow 등 여러 값이있는데 비트 마스크로 계산 가능.
            //Above가 땅위, 즉 공중에 있다는 뜻이 아니라
            //대가리 쪽에 충돌판정이 => 천장이랑 충돌 되고 있다는 얘기임 ㅋㅋ
            //None이 공중에 있다는거
            //Sides는 옆쪽, 벽과 충돌하는거


            //땅 밟고있으면 중력가속도값을 초기화 해주고
            yVelocity = 0f;
            Debug.Log("땅위");

            if (Input.GetKeyDown(KeyCode.Space))
            {
                yVelocity = 150f;
            }
        }
        else if (cc.collisionFlags == CollisionFlags.None)
        {
            yVelocity = -9.8f;
            Debug.Log("공중이야~");
        }

        if( cc.collisionFlags == CollisionFlags.Above)
        { //공중에 있을 때만 중력가속도 값을 더해주면 된다.
            //yVelocity = -9.8f;
            Debug.Log("천장에 대가리 닿음");
        }

        if ((cc.collisionFlags & CollisionFlags.Sides) != 0)
        {//비트연산 먹히기때문에 이런식으로 가능.
            Debug.Log("벽에 닿았어~");
        }
        yVelocity += gravity * Time.deltaTime;
        dir.y = yVelocity;
        cc.Move(dir * spd * Time.deltaTime);




        //SimpleMove(방향 * 속도) => 중력값을 알아서 계산해줌.
        //=> 점프처리 따로 하기에 어려움.
        //cc.SimpleMove(-(transform.forward.normalized) * spd);

    }

	private void OnControllerColliderHit(ControllerColliderHit hit)
	{//캐릭터 컨트롤러가 move 함수로 인해 움직일 때 
        //충돌을 확인하는거.
            //Debug.Log("캐릭터 컨트롤러 이동중 충돌확인");

        //그외 충돌은
        //어차피 트리거충돌은 
        //조건 부합시 충돌된 오브젝트들에 콜백으로 들어오니까 ㄱㅊ
        //쓰던대로 쓰면댄다.. 이말이야

        //그러면 OncollisionEnter은...?
        //아쉽지만 호출안됨 ㅋㅋ~~
        //OnControllerColliderHit에서 bool값 넣어서 직접 OnCollision~~~ 기능 써야함.
	}

	private void OnCollisionEnter(Collision collision)
	{
        Debug.Log("콜라이젼 엔터 확인");
	}

	private void OnTriggerEnter(Collider other)
	{
        Debug.Log("트리거 설정된 콜라이더와 충돌 확인");
	}
}
