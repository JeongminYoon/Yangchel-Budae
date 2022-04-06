using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUI : MonoBehaviour
{
    public float currentTime = 0f;
    public float destroyTime = 1f;
    Vector3 position;

    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = position + new Vector3(0, currentTime * 5, 0);
        currentTime += Time.deltaTime;
        if (currentTime >= destroyTime)
        {
            Destroy(this.gameObject);
        }
    }
}
