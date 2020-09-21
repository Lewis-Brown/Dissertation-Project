using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookableObjScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if the object has no parent then give it a Rigidbody2D
        if (transform.parent == null)
        {
            gameObject.AddComponent<Rigidbody2D>();
        }
    }
}
