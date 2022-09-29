using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CimMovement : MonoBehaviour
{
    public enum State { Passive, Alert, Stop}

    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;


    [Header("Movement")]
    public State myState = State.Passive;
    public float myspeed = 5f;
    public float alertSpeed = 10f;
    public float sociallyDistant = 10;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = true;

        GotoNextPoint();
    }

    void GotoNextPoint()
    {
        if (points.Length == 0)
            return;

        agent.destination = points[destPoint].position;

        destPoint = (destPoint + 1) % points.Length;
    }
    private void Update()           
    {
        //reference to player so that it can be accessed
        GameObject Player = GameObject.Find("Player");

        switch (myState)
        {
            case State.Passive:
                Passive();
                break;

            case State.Alert:
                Alert();
                break;

            case State.Stop:
                Stop();
                break;

            default:
                break;
        }

        Passive();
        {
            //if we're super close to the waypoint, start heading to the next one
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
                GotoNextPoint();

            //not sure if this is strictly necessary to have here
            else
            {
                myState = State.Alert;
            }
        }

        Alert();
        {
            if (Vector3.Distance(this.gameObject.transform.position, Player.transform.position) <= sociallyDistant)
            {
                MoveToward(Player.transform.position);
            }
            else
            {
                myState = State.Passive;
            }
        }

        Stop();
        {

        }

    }

    void MoveToward(Vector3 position)
    {
        position = new Vector3(position.x, position.y, transform.position.z);
        float step = alertSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, position, step);
    }
void Passive()
    {

    }

    void Alert()
    {

    }

    void Stop()
    {

    }

}

    

    

