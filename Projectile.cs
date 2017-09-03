using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class Projectile : MonoBehaviour
{
    public Vector3 target;
    public float firingAngle = 45.0f;
    public float speed = 10f;

    public Transform arrowModel;

    float elapsedTime;
    public float flightDuration { get; private set; }

    Vector2 velocity;
    Vector3 lastPosition;

    public bool enableRandomTargetArea = true;
    public float randomRadius = 2f;
    public bool enableRandomSpeed = true;
    public float randomSpeedMin = 8f;
    public float randomSpeedMax = 20f;

    public bool isFlying;
    public float visibleTime = 5;

    public void Initialize(Vector3 _target, NavMeshAgent _targetNavMeshAgent)
    {
        isFlying = true;

        if (enableRandomTargetArea)
        {
            Vector2 randomCircle = Random.insideUnitCircle * randomRadius;
            Vector3 finalCircle = new Vector3(randomCircle.x, 0, randomCircle.y);
            target = _target + finalCircle;
        }

        if (enableRandomSpeed) speed = Random.Range(randomSpeedMin, randomSpeedMax);

        //calculate distance to target
        float dist = Vector3.Distance(transform.position, target);

        float vel = Mathf.Sqrt((dist * speed) / Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad));

        velocity.x = vel * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        velocity.y = vel * Mathf.Sin(firingAngle * Mathf.Deg2Rad);


        //calculate flight time
        flightDuration = dist / velocity.x;
        //add extra time
        //flightDuration += 0.1f;



        bool isMoving = (_targetNavMeshAgent.velocity == Vector3.zero) ? false : true;//this will tell me in effect if the target is moving
        //Debug.Log(isMoving);
        //speed here is actually a constant value, so the below works if the target is moving, but stops working if the target is still, so i need to do a check if the target is moving before applying the below
        if (isMoving)
            target += _targetNavMeshAgent.speed * flightDuration * _targetNavMeshAgent.transform.forward;


        //rotate projectile to face the target
        Vector3 dir = target - transform.position;
        if (dir != Vector3.zero) transform.rotation = Quaternion.LookRotation(dir);

        elapsedTime = 0;

        lastPosition = transform.position;
    }

    private void Update()
    {


        if (elapsedTime < flightDuration && isFlying)
        {
            //translate the x component on the z-axis => forward vector (rotiation)
            transform.Translate(0, (velocity.y - (speed * elapsedTime)) * Time.deltaTime, velocity.x * Time.deltaTime);



        }
        else if (elapsedTime > flightDuration + visibleTime)
        {

            //deactivate obj
            gameObject.SetActive(false);
        }

        elapsedTime += Time.deltaTime;

        Vector3 dir = transform.position - lastPosition;
        if (dir != Vector3.zero) arrowModel.rotation = Quaternion.LookRotation(dir);
        lastPosition = transform.position;

    }


}
