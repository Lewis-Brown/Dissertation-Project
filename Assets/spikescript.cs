using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikescript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            Destroy(GameObject.FindGameObjectWithTag("Player"));
            Application.LoadLevel(Application.loadedLevel);
        }
    }
}
