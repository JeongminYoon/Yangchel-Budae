using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeFunc : Units
{
	public override bool Attack(GameObject _target)
	{
        //if (weapon != null)
        //{
        //    weapon.GetComponent<BoxCollider>().enabled = base.Attack(_target);
        //    if (weapon.GetComponent<BoxCollider>().enabled == true)
        //    {
        //        animController.SetBool("bWalk", false);
        //        animController.SetTrigger("tAttack");
        //        return true;
        //    }
        //}

        if (base.Attack(_target))
        {
            animController.SetTrigger("tAttack");
            weaponScript.targetObj = _target;
            return true;
        }


		return false;
    }

    public void Slash(int colState)
    {
        if (weapon != null)
        {
            weaponScript.WeaponColState(colState);
        }
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

        //if (targetObj != null)
        //{ targetDist = Vector3.Magnitude(this.gameObject.transform.position - targetObj.transform.position); }

        Attack(targetObj);


    }


	private void OnTriggerEnter(Collider other)
	{
        //if (other.CompareTag("Weapon"))
        //{//���� ����
        // //=> ���� ��ü�� ������ ���ӿ�����Ʈ��
        // //Units Ŭ���� �����ͼ�
        // //�Ʊ����� ������ �Ǻ�.(���� ������ isEnemy��)
        // //������ �Ʊ��̸� ������ ���ְ� �����̸� ������ �ֱ�
        // //=> ���� �ѳ����׸� ���� �ϱ� �ؾ��ϴµ�...
        //}
        //else if(other.CompareTag("Bullet"))
        //{//�Ѿ� (�����̴�, Ÿ����)
        // //�Ѿ� �����ϴ� ��� ���� �ڱ� isEnemy ���� �����ְ�
        // //�浹�� ���� �Ҹ��� isEnemy ���� �Ǻ��ؼ�
        // //�Ʊ��̸� ������ ���ְ� �����̸� ������ �ֱ�
        //}
        //Units[] temp = other.transform.GetComponentsInParent<Units>();


        //=> �׳� �������� Ȥ�� �Ѿ� �ʿ���
        //Ÿ�� ������Ʈ�� �������� Ȯ�� �ѵ� ó�� ������!
    }

}
