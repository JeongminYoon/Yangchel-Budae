using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager_test : MonoBehaviour
{
    public static CardManager Inst { get; private set; }
    //private void Awake() => Inst = this;

    [SerializeField] ScriptableObject_YJMtest itemSO;

    List<Item> itemBuffer;

    public Item PopItem()
    {
    if (itemBuffer.Count == 0)
            SetupItemBuffer();

        int Rnd = (Random.Range(0, 8));
        print(Rnd);
        itemBuffer.RemoveAt(Rnd);
        Item item = itemBuffer[Rnd];


        return item;

    }

    void SetupItemBuffer()
    {
        itemBuffer = new List<Item>();
        for (int i = 0; i < 8f; i++)
        {
            Item item = itemSO.items[i];
            itemBuffer.Add(item);
        }

        for(int i = 0;i < itemBuffer.Count;i++)
        {
            Item temp = itemBuffer[i];
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SetupItemBuffer();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            print(PopItem().name);
    }
}
