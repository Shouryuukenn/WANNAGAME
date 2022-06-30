using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    private Vector3 spherepos;

    // Start is called before the first frame update
    void Start()
    {
        spherepos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // X方向に周期的に動かす
        transform.position = new Vector3(Mathf.Sin(Time.time) * 2.0f + spherepos.x, spherepos.y, spherepos.z);

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
}
