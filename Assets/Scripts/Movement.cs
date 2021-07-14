using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Movement : MonoBehaviour
{
    [SerializeField] float _mainThrust = 100f;
    [SerializeField] float _rotationThrust = 1f;

    [SerializeField] private AudioClip _mainEngine;
    
    [SerializeField] private ParticleSystem _mainEngineParticles;
    [SerializeField] private ParticleSystem _leftThrusterParticles;
    [SerializeField] private ParticleSystem _rightThrusterParticles;

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
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    
    void ProcessRotation()
    {
        _rigidbody.freezeRotation = true;
        
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }

        else
        {
            StopRotating();
        }
        
        _rigidbody.freezeRotation = false;
    }

    private void RotateLeft()
    {
        transform.Rotate(Vector3.forward * (_rotationThrust * Time.deltaTime));

        if (!_rightThrusterParticles.isPlaying)
        {
            _rightThrusterParticles.Play();
        }
    }

    private void RotateRight()
    {
        transform.Rotate(Vector3.back * (_rotationThrust * Time.deltaTime));
            
        if (!_leftThrusterParticles.isPlaying)
        {
            _leftThrusterParticles.Play();
        }
    }

    private void StopRotating()
    {
        _rightThrusterParticles.Stop();
        _leftThrusterParticles.Stop();
    }
    
    void StartThrusting()
    {
        _rigidbody.AddRelativeForce(Vector3.up * _mainThrust * Time.deltaTime);
        if (!_audioSource.isPlaying)
        {
            _audioSource.PlayOneShot(_mainEngine);
        }
        if (!_mainEngineParticles.isPlaying)
        {
            _mainEngineParticles.Play();
        }
    }



    private void StopThrusting()
    {
        _audioSource.Stop();
        _mainEngineParticles.Stop();
    }


}
