using Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace MoreDoors
{
    public class Enemy : MonoBehaviour, IEnemy {

        public float walkSpeed = 4.0f;
        public float runSpeed = 8.0f;
        [Range(0.0f, 100.0f)] public float health = 100.0f;

        private GameObject target = null;
        private States currentstate = States.IDIE;
        private NavMeshAgent agent;

        public enum States
        {
            IDIE,
            WALKING,
            RUNNING,
            ATTACKING
        }

        public GameObject getTarget()
        {
            return this.target;
        }

        public void setState(States state)
        {
            this.currentstate = state;
        }

        public States getState(){
            return this.currentstate;
        }

        public float GetHealth()
        {
            return this.health;
        }

        public void setTarget(GameObject target)
        {
            this.target = target;

        }

        public void primAttack()
        {

        }

        public void secdAttack()
        {

        }

        public void TakeDamage(float damage)
        {
            this.health = this.health - damage;
            if (this.health < 0.0f)
            {
                Die();

            }

        }

        public void SetHealth(float health)
        {
            if( health < 0.0f)
            {
                Die();

            }

            this.health = health;

        }

        public void Die()
        {   //Add nice ragdoll here?
            Object.Destroy(this.gameObject);

        }

        public void walkTo(Vector3 vec){

            this.setState(States.WALKING);

            agent = GetComponent<NavMeshAgent>();
            agent.speed = walkSpeed;

            agent.destination = vec;

        }

        public void runTo(Vector3 vec)
        {
            this.setState(States.RUNNING);

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
}
