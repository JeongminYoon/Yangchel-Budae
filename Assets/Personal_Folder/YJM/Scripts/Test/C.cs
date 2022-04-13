using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum CardState { Unit, Magic }


[CreateAssetMenu(menuName ="Data/Card/Card")]
public class C : ScriptableObject
{
    [Header("CardState")]
    public CardState cState;

    public CFunction func;

    [Header("")]
    public int A;
    public float damage;
    public int cost;
    public float hp;
}
