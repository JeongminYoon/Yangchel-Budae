//using System.Collections;
//using System.Collections.Generic;
using System;
using System.Collections.Generic;
using UnityEngine;

public static class Funcs
{

	public static void GameManagerHasNotExist()
	{
		if (GameManager.instance == null)
		{
			GameObject tempObj = new GameObject();
			tempObj.AddComponent<GameManager>();
		}
		
	}
    public static int B2I(bool boolean)
    {
        return Convert.ToInt32(boolean);
    }

	public static bool I2B(int integer)
	{
		return Convert.ToBoolean(integer);
	}

	public static Structs.RayResult RayToWorld()
	{
		//이걸 그냥 충돌한 놈이 그라운드 일때만 리턴하게?
		//아니면 소환하는 곳에서 충돌된 놈이 그라운드가 아니면 그 새기 크기 판단해서 옆에 생성되게?

		Structs.RayResult rayResult = new Structs.RayResult();

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit castHit;

		if (Physics.Raycast(ray, out castHit))
		{
			rayResult.hitPosition = castHit.point;
			rayResult.hitPosition.y = 0f;
			rayResult.hitObj = castHit.transform.gameObject;
			rayResult.isHit = true;
		}
		else
		{
			rayResult.isHit = false;
		}

		return rayResult;
	}

	public static void ChangeMesh(GameObject origin, Mesh mesh)
	{
		MeshFilter tempFilter = origin.GetComponent<MeshFilter>();

		if (tempFilter != null)
		{
			tempFilter.mesh = mesh;
		}

	}

	public static GameObject FindGameObjectInChildrenByName(GameObject Parent, string ObjName)
	{
		int childrenCount = Parent.transform.childCount;

		GameObject[] findObjs = new GameObject[childrenCount];

		if (Parent.name == ObjName)
		{
			return Parent;
		}

		if (childrenCount == 0)
		{
			return null;
		}
		else 
		{
			for (int i = 0; i < childrenCount; ++i)
			{
				findObjs[i] = FindGameObjectInChildrenByName(Parent.transform.GetChild(i).gameObject, ObjName);

				if (findObjs[i] != null && findObjs[i].name == ObjName)
				{
					return findObjs[i];
				}
			}

			return null;
		}
	}

	public static GameObject FindGameObjectInChildrenByTag(GameObject Parent, string ObjTag)
	{
		int childrenCount = Parent.transform.childCount;

		GameObject[] findObjs = new GameObject[childrenCount];

		if (Parent.CompareTag(ObjTag))
		{
			return Parent;
		}

		if (childrenCount == 0)
		{
			return null;
		}
		else
		{
			for (int i = 0; i < childrenCount; ++i)
			{
				findObjs[i] = FindGameObjectInChildrenByTag(Parent.transform.GetChild(i).gameObject, ObjTag);
				
				if (findObjs[i]!= null && findObjs[i].CompareTag(ObjTag))
				{
					return findObjs[i];
				}
			}

			return null;
		}
	}

	public static GameObject FindClosestUnit(GameObject me, List<GameObject> objList)
	{
		float lowDist = -1f;
		//float searchRange = me.GetComponent<Units>().unitStatus.sightRange;
		GameObject closestObj = null;

		foreach (GameObject obj in objList)
		{
			float dist = Vector3.Magnitude(obj.transform.position - me.transform.position);

			if (lowDist > dist || lowDist < 0f)
			{
				lowDist = dist;
				closestObj = obj;
			}
		}

		return closestObj;
	}

	public static List<GameObject> FindUnitsInSightRange(GameObject me, List<GameObject> objList)
	{
		List<GameObject> unitsInRange = new List<GameObject>();
		unitsInRange.Clear();

		Units temp = me.GetComponent<Units>();
		float searchRange = 0f;

		if (temp != null)
		{ 
			searchRange = temp.unitStatus.sightRange;
		}

		foreach (GameObject obj in objList)
		{
			float dist = Vector3.Magnitude(obj.transform.position - me.transform.position);

			if (dist <= searchRange)
			{
				unitsInRange.Add(obj);
			}
		}

		return unitsInRange;
	}

}

public static class Defines
{
	public const int right = 1;
	public const int left = 0;

	public const int ally = 0;
	public const int enemy = 1;


	public static Vector3 camPos = new Vector3(0f, 40f, -26f);
	public static Vector3 camRot = new Vector3(60f, 0f, 0f);


	#region towerPos
	public static Vector3 enemyTower_Rot = new Vector3(0f, 180f, 0f);
	public static Vector3 enemyTower_RightPos = new Vector3(5f, 0f, 11.5f);
	public static Vector3 enemyTower_LeftPos = new Vector3(-5f, 0f,	11.5f);
	public static Vector3 enemyNexusPos = new Vector3(0, 0f, 17f);

	public static Vector3 allyTower_RightPos = new Vector3(5f, 0f, -11.5f);
	public static Vector3 allyTower_LeftPos = new Vector3(-5f, 0f, -11.5f);
	public static Vector3 allyNexusPos = new Vector3(0, 0f, -17f);

	public static Vector3[,] towersPos = { { allyTower_LeftPos ,
												allyTower_RightPos,
												allyNexusPos},
											{ enemyTower_LeftPos,
												enemyTower_RightPos,
												enemyNexusPos} };
	#endregion

	public static string[] deathAnimations = { "Units_Death_01",
												"Units_Death_02",
												"Units_Death_03"};
	public static string[] atkAnims = {"Melee_Attack",
										"Range_Attack_01",
										"Range_Attack_02",
										"Medic_Attack",
										"Tanker_Attack"
										};
}

namespace Enums
{
	public enum SceneNum
	{ 
		MainMenu,
		CardSelect,
		InGame,
		Result,
		SceneEnd
	}
	public enum UnitClass
	{
		melee1, //0
		melee2, //1
		range1, //2
		range2, //3
		tanker, //4
		medic, //5
		skill_1,
		Skill_2,
		End
	}

	public enum Team
	{ 
		ally,
		enemy,
		End
	}

	public enum UnitState
	{ 
		Idle,
		Walk,
		Attack,
		Death,
		End
	}

	public enum eUnitFXS
	{
		AttackFXS,
		HitFXS,
		DeathFXS,
		End
	}
}

namespace Structs
{
	public struct RayResult
	{
		public bool isHit;
		public Vector3 hitPosition;
		public GameObject hitObj;
	}



}

