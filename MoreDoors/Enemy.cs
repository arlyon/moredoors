using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

    public float walkSpeed = 4.0f;
    public float runSpeed = 8.0f;
    [Range(0.0f, 100.0f)] public float health = 100.0f;

    private GameObject target = null;
    private states currentstate = states.IDIE;
    private NavMeshAgent agent;

    private enum states
    {
        IDIE,
        WALKING,
        RUNNING,
        ATTACKING
    }

    GameObject getTarget()
    {
        return this.target;
        
    }

    void setState(states state)
    {
        this.currentstate = state;

    }

    states getState(){
         return this.currentstate;

    }

    float getHealth()
    {
        return this.health;

    }

    void setTarget(GameObject target)
    {
        this.target = target;

    }

    void primAttack()
    {

    }

    void secdAttack()
    {

    }

    void takeDamage(float damage)
    {
        this.health = this.health - damage;
        if (this.health < 0.0f)
        {
            death();

        }

    }

    void setHealth(float health)
    {
        if( health < 0.0f)
        {
            death();

        }

        this.health = health;

    }

    void death()
    {   //Add nice ragdoll here?
        Object.Destroy(this.gameObject);

    }

    void walkTo(Vector3 vec){

        this.setState(states.WALKING);

        agent = GetComponent<NavMeshAgent>();
        agent.speed = walkSpeed;

        agent.destination = vec;

    }

    void runTo(Vector3 vec)
    {
        this.setState(states.RUNNING);

        agent = GetComponent<NavMeshAgent>();
        agent.speed = runSpeed;

        agent.destination = vec;

    }

    // Use this for initialization
    void Start () {
        this.walkTo( new Vector3(-3.36f, 5.0f, 3.0f ) );

    }
	
	// Update is called once per frame
	void Update () {

        GameObject target = this.getTarget();

        //This if will block the one below with each update call!
        if (target != null)
        {
            this.runTo(target.transform.position);


        }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                this.runTo( hit.point );

            }
        }

    }
}
