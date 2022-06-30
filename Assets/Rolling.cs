using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rolling : MonoBehaviour
{
    public int radius = 200;

    Vector3 initPos;

    // Start is called before the first frame update
    void Start()
    {
        initPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        CalPosition();
    }
    void CalPosition()
    {
        int phase =  1;
        float xPos = radius * Mathf.Cos(Time.time * phase);
        float zPos = radius * Mathf.Sin(Time.time * phase);

        Vector3 pos = new Vector3(xPos + initPos.x, initPos.y, zPos + initPos.z);
        gameObject.transform.position = pos;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
}
