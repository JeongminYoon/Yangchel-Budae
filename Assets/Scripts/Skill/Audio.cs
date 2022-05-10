using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioSource ads;

    void Start()
    {

        

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isGameEnd == true)
        {
            Destroy(gameObject);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {

            ads.volume += 0.1f;


        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {


            ads.volume -= 0.1f;
        }
    }
}
