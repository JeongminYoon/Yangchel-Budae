using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public static class Funcs
{
    public static int B2I(bool boolean)
    {
        return Convert.ToInt32(boolean);
    }

	public static Structs.RayResult RayToWorld()
	{
		Structs.RayResult rayResult = new Structs.RayResult();

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit castHit;

		if (Physics.Raycast(ray, out castHit))
		{
			rayResult.hitPosition = castHit.point;
			rayResult.hitPosition.y = 1f;
			rayResult.isHit = true;
		}
		else
		{
			rayResult.isHit = false;
		}

		return rayResult;
	}
}

public static class Defines
{
	public const int right = 1;
	public const int left = 0;

	public const int ally = 0;
	public const int enemy = 1;
}

namespace Enums
{ 
	public enum UnitClass
	{
		melee,
		range,
		tanker,
		healer,
		skill,
		End
	}

	public enum Team
	{ 
		ally,
		enemy,
		End
	}
}

namespace Structs
{
	public struct RayResult
	{
		public bool isHit;
		public Vector3 hitPosition;
	}
}

