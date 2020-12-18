using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class PasserbyMove : MonoBehaviour {

    //private GameObject[] allPositions;
    private float[] allDistances;
    private int currentPosition = 0;
    private float minDistance;
    public List<GameObject> allPositions = new List<GameObject>();
    public float walkSpeed;
    public int i = 0;
    private float timer = 0f;
    private float time = 3.0f;
    void Update()
    {
        timer += Time.deltaTime;
        
        PMoveBehaviour();
        if (timer > time)
        {
            if (Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag(Tags.player).transform.position) <= 35.0f && Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag(Tags.player).transform.position) >= 34.0f)
            {
                walkSpeed = 0.0f;
                Bounds b = this.GetComponent<Collider>().bounds;
                GraphUpdateObject guo = new GraphUpdateObject(b);
                AstarPath.active.UpdateGraphs(guo);
                timer = 0;
            }
        }
        if(Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag(Tags.player).transform.position) > 40.0f)
        {
            walkSpeed = 7.0f;
        }
        
    }

    public void PMoveBehaviour()
    {
        transform.LookAt(allPositions[i].transform.position);
        //transform.position = Vector3.Lerp(transform.position, allPositions[i].transform.position, Time.deltaTime * walkSpeed);
        transform.Translate(Vector3.forward*Time.deltaTime * walkSpeed);

    
        if (Vector3.Distance(transform.position, allPositions[i].transform.position) < 1)
        {
            i++;
            i %= allPositions.Count;
        }
    
      
    }

   
}
