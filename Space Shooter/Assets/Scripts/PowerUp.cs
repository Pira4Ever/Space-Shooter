using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private float _speed = 3;
    void Start()
    {
        
    }

    void Update()
    {
        calculateMovement();
    }

    private void calculateMovement()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
        if(transform.position.y <= -4.5f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Player _player = other.transform.GetComponent<Player>();
            Destroy(this.gameObject);
            if(_player != null)
            {
                _player.TripleShotActive();
            }
            else
            {
                Debug.LogError("Player é vazio");
            }
        }
    }
}
