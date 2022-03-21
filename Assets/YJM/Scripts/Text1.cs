using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Text1 : MonoBehaviour
{
    GameObject UItext;
    int a;
    int b;
    int c; 
    int d;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        a = CardManager.a;
        b = CardManager.b;
        c = CardManager.c;
        d = CardManager.d;
        GetComponent<Text>().text = (a+"and "+b+"and "+c+"and "+d).ToString();

    }
}
