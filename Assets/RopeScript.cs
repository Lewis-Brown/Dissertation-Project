using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeScript : MonoBehaviour
{
    public Vector2 location;
    private Vector2 temp;

    public float speed = 1;

    public float ropespacing = 1f;

    public bool hooked = false;

    public bool ascending = false;

    public GameObject ropePrefab;
    public GameObject player;
    public GameObject lastNode;
    public List<GameObject> Nodes = new List<GameObject>();
    int vertexCount = 2;
    
    public LineRenderer lr;
   

    
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();

        player = GameObject.FindGameObjectWithTag ("Player");

        lastNode = transform.gameObject;

        Nodes.Add(transform.gameObject);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if rope is not hooked onto object
        if (!hooked)
        {
            //move the hook of the rope to the position clicked on the screen
            transform.position = Vector2.MoveTowards(transform.position, location, speed);

            //if the location of the hook is not at the location and that the rope nodes is less that 35
            if ((Vector2)transform.position != location && Nodes.Count < 35)
            {

                //if the distance between the rope and last node is greater than the rope spacing
                if (Vector2.Distance(player.transform.position, lastNode.transform.position) > ropespacing)
                {
                    //Call method
                    CreateNode();
                    //RenderLine();

                }
                else
                {
                    return;
                }

            }
            else
            {
                //delete the hook game object
                Destroy(gameObject);
                //set the rope to not active
                player.GetComponent<Throwhook>().ropeActive = false;

            }

        }
        else
        {
            //if the distance between the rope and last node is greater than the rope spacing
            if ((Vector2.Distance(player.transform.position, lastNode.transform.position) > ropespacing) && !ascending )
            {
                //create node after rope has been hooked incase player has moved whilst hook was travelling
                CreateNode();
                //connect hinge joint from the last node to the player
                lastNode.GetComponent<HingeJoint2D>().connectedBody = player.GetComponent<Rigidbody2D>();
                //RenderLine();
            }
            // if the nodes go over 35
            if (Nodes.Count > 35)
            {
                //delete the hook game object
                Destroy(gameObject);
                //set the rope to not active
                player.GetComponent<Throwhook>().ropeActive = false;
                //set the player speed to defualt
                player.GetComponent<controller>().speed = 15;
            }
            //set player speed to 50 if hooked
            player.GetComponent<controller>().speed = 50;


            ascending = false;
            //if the W key is pressed
            if (Input.GetKey("w") == true)
            {
                ascending = true;
                //the player will move towards from the hook
                player.transform.position = Vector2.MoveTowards(player.transform.position, lastNode.transform.position, speed / 4);
                while(Vector2.Distance(player.transform.position, lastNode.transform.position) < 2.0f && Nodes.Count > 1)
                {
                    DeleteNode();
                    vertexCount--;
                    RenderLine();
                    
                }
 
            }

            //if the S key is pressed
            if (Input.GetKey("s") == true && Nodes.Count < 35)
            {
                //the player will move away from the hook
                player.transform.position = Vector2.MoveTowards(player.transform.position, transform.position, -1 * speed / 4);
                RenderLine();
            }
        }
        //call method
        RenderLine();
        
    }
    
    void RenderLine() {
        //render line method creates a array of game objects and renders a line between the nodes and player
        lr.SetVertexCount(vertexCount);

        int i;
        for (i=0;i < Nodes.Count; i++)
        {
            lr.SetPosition(i, Nodes[i].transform.position);
            
        }
        lr.SetPosition(i, player.transform.position);
        lr.material.color = Color.green;
    }

 

    void CreateNode()
    {
        //Create Node funtion creates a node that is put into the nodes array
        Vector2 makeNode = player.transform.position - lastNode.transform.position;
        makeNode.Normalize();
        makeNode *= ropespacing;
        makeNode += (Vector2)lastNode.transform.position;
        
        
        GameObject ropeNode = (GameObject) Instantiate(ropePrefab, makeNode, Quaternion.identity);
        
        //set the ropeNode as a child to the hook
        ropeNode.transform.SetParent(transform);

        //creates a hinge joing between the node and the previous node
        lastNode.GetComponent<HingeJoint2D>().connectedBody = ropeNode.GetComponent<Rigidbody2D>();
        lastNode = ropeNode;
        Nodes.Add(lastNode);
        vertexCount++;
        
    }
    void DeleteNode()
    {
        Nodes.Remove(lastNode);
        Destroy(lastNode);
        lastNode = Nodes[Nodes.Count-1];
        lastNode.GetComponent<HingeJoint2D>().connectedBody = player.GetComponent<Rigidbody2D>();
    }
    
    //looks for a trigger event and does parameters if conditions are met
    void OnTriggerEnter2D(Collider2D col)
    {
        // if the hook interates with a structure
        if (col.gameObject.tag == "Hookable")
        {
            //set hooked to true
            hooked = true;
     
        }
        
        //if the hook interacts with a hookable object
        if (col.gameObject.tag == "HookableObj")
        {
            //set hooked to true
            hooked = true;
            col.transform.parent = transform;
            Destroy(col.attachedRigidbody);
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            
        }
        Debug.Log(hooked);
        
    }

}
   