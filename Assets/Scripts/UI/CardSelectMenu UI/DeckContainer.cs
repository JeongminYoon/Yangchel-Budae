using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Card/DeckContainer")]
public class DeckContainer : ScriptableObject
{
    public List<UnitStatus> deckCardList;
}

