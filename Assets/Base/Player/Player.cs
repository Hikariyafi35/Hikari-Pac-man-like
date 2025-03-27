using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Action onPowerUpStart;
    public Action onPowerUpStop;


    private Rigidbody _rigidbody;
    [SerializeField]
    private Transform _camera;
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
    [SerializeField]
    private float _rotationTime  = 0.1f;
    [SerializeField]
    private Animator _animator;
    
    private Coroutine _powerUpCoroutine;
    private bool _isPowerUpActive = false;
    private float _rotationVelocity;
    
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
            SceneManager.LoadScene("LoseScreen");
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

        Vector3 movementDirection = new Vector3(horizontal, 0, vertical);
        
        _rigidbody.velocity = movementDirection *_speed * Time.fixedDeltaTime;
        if(movementDirection.magnitude >= 0.1)
        {
        float rotationAngle = Mathf.Atan2(movementDirection.x, movementDirection.z) * 
        Mathf.Rad2Deg + _camera.eulerAngles.y;
        float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationAngle, ref _rotationVelocity, _rotationTime);
        transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);
        movementDirection = Quaternion.Euler(0f, rotationAngle, 0f) * Vector3.forward;
        }
        _rigidbody.velocity = movementDirection * _speed * Time.deltaTime;
        _animator.SetFloat("velocity",_rigidbody.velocity.magnitude);
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
