using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string name;
    public float hp;
    public float dmg;
    public float atkSpd;
    public float moveSpd;
    public float atkRange;
    public float sightRange;
    public int cost;
    public float spawnTime;
}
[CreateAssetMenu(fileName = "UnitStatus-1", menuName = "Scriptable Object/UnitStatus-1")]

public class ScriptableObject_YJMtest : ScriptableObject
{
    public Item[] items;
}
