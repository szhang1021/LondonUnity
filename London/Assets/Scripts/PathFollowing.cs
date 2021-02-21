using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class PathFollowing : MonoBehaviour
{
   public PathCreator pathcreator;
   public float speed = 5;
   float distanceTravelled;
    // Update is called once per frame
    void Update()
    {
        distanceTravelled += speed * Time.deltaTime;
        transform.position = pathcreator.path.GetPointAtDistance(distanceTravelled);
        //transform.rotation = pathcreator.path.GetRotationAtDistance(distanceTravelled);
    }
}
