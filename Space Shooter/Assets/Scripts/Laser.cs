using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private float _speed = 5.5f;
    void Start()
    {
        
    }

    void Update()
    {
        laserMovement();
    }
    void laserMovement()
    {
        transform.Translate(Vector3.up * Time.deltaTime * _speed);
        if(transform.position.y > 6.93f)
        {
            Destroy(this.gameObject);
        }
    }
}
