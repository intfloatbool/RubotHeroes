using System;
using Interfaces.Triggers;
using UnityEngine;
using Random = UnityEngine.Random;

public class RobotDownPusher : MonoBehaviour
{
    [SerializeField] private Robot _robot;
    [SerializeField] private float _distance = 2f;
    [SerializeField] private float _power = 500f;
    [SerializeField] private Vector3 _posToPush;
    private Robot Enemy => _robot.EnemyRobot;
    private void FixedUpdate()
    {
        if (Enemy == null)
            return;
        
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.down));
        Debug.DrawRay(ray.origin, ray.direction,  Color.cyan, 1f);
        if (Physics.Raycast(ray, out hit, _distance))
        {
            ICollidable collidable = hit.collider.gameObject.GetComponent<ICollidable>();
            if (collidable != null)
            {
                _posToPush = (transform.position - collidable.Rigidbody.transform.position).normalized;
                _robot.Rigidbody.AddForce(_posToPush * _power); 
            }
        }
    }
}
