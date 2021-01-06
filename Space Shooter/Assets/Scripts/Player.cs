//Código de movimento para jogo 2D
//Feito por Octavio Piratininga

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class Player : MonoBehaviour
{
    private float _speed = 5f;
    [SerializeField]
    private GameObject _laserPrefab;
    private float _fireRate = 0.3f;
    private float _canFire = -1f;
    private int _lives = 3;
    private SpawnManager _spawnManager;
    SerialPort sPort = new SerialPort("COM5", 9600);
    int shoot;


    
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();


        if (_spawnManager == null)
        {
            Debug.LogError("SpawnManager is NULL");
        }
    }


    void Update()
    {
        try
        {
            sPort.Open();
        }
        finally
        {
            if (sPort.IsOpen)
            {
                shoot = sPort.ReadChar();
            }
        }
        
        
        playerMovement();
        if (Input.GetKeyDown(KeyCode.Space) || shoot == 1 && Time.time > _canFire)
        {
            _canFire = Time.time + _fireRate;
            fireLaser();
        }
    }

    void playerMovement()
    {
        float _horizontalInput = Input.GetAxis("Horizontal");
        float _verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.right * Time.deltaTime * _speed * _horizontalInput);
        transform.Translate(Vector3.up * Time.deltaTime * _speed * _verticalInput);

        if(transform.position.x > 12.22042f)
        {
            transform.position = new Vector3(-12.63f, transform.position.y, 0);
        }
        if(transform.position.x < -12.63f)
        {
            transform.position = new Vector3(12.22042f, transform.position.y, 0);
        }
        if(transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0f, 0);
        }
        if(transform.position.y < -4.02f)
        {
            transform.position = new Vector3(transform.position.x, -4.02f, 0);
        }
    }

    void fireLaser()
    {
        Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
    }

    public void Damage()
    {
        _lives--;

        if(_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            sPort.Close();
            Destroy(this.gameObject);
        }
    }
}
