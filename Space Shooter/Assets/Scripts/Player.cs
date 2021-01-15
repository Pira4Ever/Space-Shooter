//Código de movimento para jogo 2D
//Feito por Octavio Piratininga

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float _speed = 5f;
    private float _speedMultiplier = 2f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    private float _fireRate = 0.2f;
    private float _canFire = -1f;
    private int _lives = 3;
    private SpawnManager _spawnManager;
    private bool _isTripleShotActive = false;
    private bool _isShieldActive = false;
    [SerializeField]
    private GameObject _shieldVisualizer;
    
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _shieldVisualizer.SetActive(false);

        if (_spawnManager == null)
        {
            Debug.LogError("SpawnManager is NULL");
        }
    }


    void Update()
    {        
        playerMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
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
        if(transform.position.y > 0.5f)
        {
            transform.position = new Vector3(transform.position.x, 0.5f, 0);
        }
        if(transform.position.y < -2.46f)
        {
            transform.position = new Vector3(transform.position.x, -2.46f, 0);
        }
    }

    void fireLaser()
    {
        if(!_isTripleShotActive)
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        }
        else
        {
            Instantiate(_tripleShotPrefab, transform.position + new Vector3(-0.53f, 0, 0), Quaternion.identity);
        }
    }

    public void Damage()
    {
        if(_isShieldActive)
        {
            _isShieldActive = false;
            _shieldVisualizer.SetActive(false);
            return;
        }
        else
        {
            _lives--;
        }
        if(_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine(5f));
    }

    IEnumerator TripleShotPowerDownRoutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        _isTripleShotActive = false;
    }

    public void SpeedBoostActive()
    {
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedBoostPowerDownCoroutine(5f));
    }

    IEnumerator SpeedBoostPowerDownCoroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        _speed /= _speedMultiplier;
    }

    public void ShieldActive()
    {
        _isShieldActive = true;
        _shieldVisualizer.SetActive(true);
    }
}
