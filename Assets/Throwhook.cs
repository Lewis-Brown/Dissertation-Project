using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Throwhook : MonoBehaviour
{

    
    public GameObject Hook;
    GameObject currentHook;

    public bool ropeActive;
    public Vector2 location;

    // Update is called once per frame
    void Update()
    {
        // if the left mouse button is pressed
        if (Input.GetMouseButtonDown(0))
        {
            // if the rope is not active
            if (ropeActive == false )
            {
                //gets the location of where the player clicked on the screen
                location = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                //creates the hook 
                currentHook = (GameObject)Instantiate(Hook, transform.position , Quaternion.identity);
                
                //sets the locations in the rope script to this location
                currentHook.GetComponent<RopeScript>().location = location;
                //rope active to true
                ropeActive = true;

                transform.GetComponent<controller>().speed = 15;
            }
            else
            {
                //before the rope is deleted, give any hookable objects not parents
                foreach(Transform child in currentHook.transform)
                {
                    if (child.tag == "HookableObj"){
                        child.parent = null;

                    }
                }
                //delete the hook
                Destroy(currentHook);
                ropeActive = false;
                Hook.GetComponent<RopeScript>().hooked = false;
                transform.GetComponent<controller>().speed = 15;
            }
        }
        

    }

    


}
