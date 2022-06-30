using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ray : MonoBehaviour
{
    [SerializeField, Tooltip("rayの座標に表示するオブジェクトをここに割り当てる")]
    private GameObject rayobject;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Ray ray = new Ray(this.transform.position, new Vector3(0.0f, -2.0f, 5.0f));
        RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                //Debug.Log(hit.collider.gameObject.name);
                //Debug.Log(hit.point);
                Vector3 pointX = hit.point;
                rayobject.transform.position = pointX;
            }
        Debug.DrawRay(ray.origin, ray.direction * 20, Color.red, 5);
    }
}
