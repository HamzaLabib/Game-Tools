using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode] // call the update wihtout run the game
public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public List<Transform> waypoints;
    int currentIndex = 0;
    Vector3 offset = new Vector3(0, -1f, 5f);

    void Update()
    {
        Movement();
        Camera.main.transform.position = transform.position - offset;
    }

    void Movement()
    {
        if (waypoints.Count > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoints[currentIndex].position, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, waypoints[currentIndex].position) < 1)
            {
                currentIndex++;
            }

            if (currentIndex == waypoints.Count)
            {
                currentIndex = 0;
            }
        }
    }

    public void OnDrawGizmos()
    {
        for (int i = 0; i < waypoints.Count; i++)
        {
            if (i < waypoints.Count - 1)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
            }
            else
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(waypoints[i].position, waypoints[0].position);
            }
        }

        //Gizmos.color = Color.red;
        //Gizmos.DrawWireCube(new Vector3(20,20,0), new Vector3(50,50,0));
    }
}
