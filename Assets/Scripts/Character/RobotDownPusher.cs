using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class RobotDownPusher : MonoBehaviour
{
    [SerializeField] private Robot _robot;
    [SerializeField] private float _distance = 2f;
    [SerializeField] private float _power = 500f;
    private Robot Enemy => _robot.EnemyRobot;
    private float RandomValue => Random.Range(-1f, 1f);
    private Vector3 RandomVector => new Vector3(RandomValue, 0f, RandomValue);
    private void FixedUpdate()
    {
        if (Enemy == null)
            return;
        
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.down));
        Debug.DrawRay(ray.origin, ray.direction,  Color.cyan, 1f);
        if(Physics.Raycast(ray, out hit, _distance))
        {
            if (hit.collider.gameObject == Enemy.gameObject)
            {
                Enemy.Rigidbody.AddForce(RandomVector* _power);
                Enemy.MakeStun();
            }
        }
    }
}
