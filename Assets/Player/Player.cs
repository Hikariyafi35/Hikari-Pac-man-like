using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Action onPowerUpStart;
    public Action onPowerUpStop;


    private Rigidbody _rigidbody;
    [SerializeField]
    private Camera _camera;
    [SerializeField] 
    private float _speed;
    [SerializeField]
    private float _powerUpDuration;
    [SerializeField]
    private int _health;
    [SerializeField]
    private TMP_Text _healthText;
    [SerializeField]
    private Transform _respawnPoint;
    
    private Coroutine _powerUpCoroutine;
    private bool _isPowerUpActive = false;
    
    public void Dead()
    {
        _health -= 1;
        if( _health >= 0 )
        {
            transform.position = _respawnPoint.position;
        }
        else
        {
            _health = 0;
            Debug.Log("Lose");
        }
        UpdateUI();
    }
    public void PickPowerUp()
    {
        if(_powerUpCoroutine != null)
        {
            StopCoroutine(_powerUpCoroutine);
        }
        _powerUpCoroutine = StartCoroutine(StartPowerUp());
    }

    private IEnumerator StartPowerUp()
    {
        _isPowerUpActive = true;
        if(onPowerUpStart != null)
        {
            onPowerUpStart();
        }
        yield return new WaitForSeconds(_powerUpDuration);
        _isPowerUpActive = false;
        if(onPowerUpStop != null)
        {
            onPowerUpStop();
        }
    }

    private void Awake() {
        UpdateUI();
        _rigidbody = GetComponent<Rigidbody>();
        //HideAndLockCursor();
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    // private void HideAndLockCursor()
    // {
    //     Cursor.visible = false;
    //     Cursor.lockState = CursorLockMode.Locked;
    // }
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 horizontalDirection = horizontal * _camera.transform.right;
        Vector3 verticalDirection = vertical * _camera.transform.forward;
        verticalDirection.y = 0;
        horizontalDirection.y = 0;    

        Vector3 movementDirection = horizontalDirection + verticalDirection;
        _rigidbody.velocity = movementDirection *_speed * Time.fixedDeltaTime;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(_isPowerUpActive)
        {
            if(collision.gameObject.CompareTag("Enemy"))
            {
                collision.gameObject.GetComponent<Enemy>().Dead();
            }
        }
    }

    private void UpdateUI()
    {
        _healthText.text = "Health : " + _health;
    }
}
