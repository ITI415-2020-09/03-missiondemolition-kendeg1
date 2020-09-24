using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{

    static public FollowCam S; //followCam singleton

    //fields set in the Unity Inspector
    public float easing = 0.05f;
    public Vector2 minXY;
    public bool ________________________________;

    //fields set dynamically
    public GameObject poi; //point of interest
    public float camZ; //desired Z pos of the camera

    void Awake()
    {
        S = this;
        camZ = this.transform.position.z;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 destination;
        //return to launchPos if there is no poi
        if (poi == null)
        {
            destination = Vector3.zero;
        }
        else
        {
            //get position of the poi
            destination = poi.transform.position;
            //if poi is a Projectile, check to see if it's at rest
            if (poi.tag == "Projectile"){
                //if it is sleeping 
                if (poi.GetComponent<Rigidbody>().IsSleeping())
                {
                    //return to default view
                    poi = null;
                    //in the next update
                    return;
                }
            }
        }
        //limit x and y to minimum values
        destination.x = Mathf.Max(minXY.x, destination.x);
        destination.y = Mathf.Max(minXY.y, destination.y);
        //interpolate from the current camera position toward destination
        destination = Vector3.Lerp(transform.position, destination, easing);
        //retain a destination.z of camZ
        destination.z = camZ;
        //set the camera to the destination
        transform.position = destination;
        //set orthographicSize of the camera to keep Ground in view
        this.GetComponent<Camera>().orthographicSize = destination.y + 10;
    }
}
