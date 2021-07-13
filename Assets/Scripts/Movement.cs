using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Movement : MonoBehaviour
{
    [SerializeField] float _mainThrust = 100f;
    [SerializeField] float _rotationThrust = 1f;

    [SerializeField] private AudioClip _mainEngine;

    private Rigidbody _rigidbody;
    private AudioSource _audioSource;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }
    
    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            _rigidbody.AddRelativeForce(Vector3.up * (_mainThrust * Time.deltaTime));
            
            if(!_audioSource.isPlaying)
            {
                _audioSource.PlayOneShot(_mainEngine);
            }
        }
        else
        {
            _audioSource.Stop();
        }
    }

    
    void ProcessRotation()
    {
        _rigidbody.freezeRotation = true;
        
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * (_rotationThrust * Time.deltaTime));
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.back * (_rotationThrust * Time.deltaTime));
        }
        
        _rigidbody.freezeRotation = false;
    }

}
