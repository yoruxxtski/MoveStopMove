using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagStill : MonoBehaviour
{
   private Camera cam;

    void Awake()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (cam != null) {
            transform.LookAt( - (cam.transform.position + transform.forward) );
        }
        
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
