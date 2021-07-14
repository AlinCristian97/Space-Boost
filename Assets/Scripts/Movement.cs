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
            _rigidbody.AddRelativeForce(Vector3.up * (_mainThrust * Time.deltaTime));
            
            if(!_audioSource.isPlaying)
            {
                _audioSource.PlayOneShot(_mainEngine);
            }

            if (!_mainEngineParticles.isPlaying)
            {
                _mainEngineParticles.Play();
            }
        }
        else
        {
            _audioSource.Stop();
            _mainEngineParticles.Stop();
        }
    }

    
    void ProcessRotation()
    {
        _rigidbody.freezeRotation = true;
        
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * (_rotationThrust * Time.deltaTime));

            if (!_rightThrusterParticles.isPlaying)
            {
                _rightThrusterParticles.Play();
            }
        }
        
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.back * (_rotationThrust * Time.deltaTime));
            
            if (!_leftThrusterParticles.isPlaying)
            {
                _leftThrusterParticles.Play();
            }
        }

        else
        {
            _rightThrusterParticles.Stop();
            _leftThrusterParticles.Stop();
        }
        
        _rigidbody.freezeRotation = false;
    }

}
