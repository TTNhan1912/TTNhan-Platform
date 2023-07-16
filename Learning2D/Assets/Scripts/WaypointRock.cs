using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointRock : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private Animator ani;
    private int currentWaypointIndex = 0;
    [SerializeField] private float speed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < .1f)
        {
            ani.SetBool("Va_Cham_Left", true);
            ani.SetBool("Va_Cham_Left", false);

            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {

                currentWaypointIndex = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);

    }
}
