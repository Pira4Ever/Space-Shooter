using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _speed = 3.5f;
    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
        if(transform.position.y < -5.2f)
        {
            transform.position = new Vector3(Random.Range(-9.75f, 9.75f), 7.18f, transform.position.z);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Player _player = other.transform.GetComponent<Player>();
            if (_player != null)
            {
                _player.Damage();
            }
            else
            {
                Debug.LogError("Player is null");
            }
            Destroy(this.gameObject);
        }
        if(other.tag == "Laser")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
        
    }
}
