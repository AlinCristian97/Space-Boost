using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float _levelLoadDelay = 2f;
    [SerializeField] private AudioClip _successAudio;
    [SerializeField] private AudioClip _crashAudio;
    
    [SerializeField] private ParticleSystem _successParticles;
    [SerializeField] private ParticleSystem _crashParticles;

    private AudioSource _audioSource;

    private bool _isTransitioning;
    private bool _collisionDisabled;

    void Start() 
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        RespondToDebugKeys();
    }

    private void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            _collisionDisabled = !_collisionDisabled;
        } 
    }


    void OnCollisionEnter(Collision other) 
    {
        if (_isTransitioning || _collisionDisabled) { return; }
        
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This thing is friendly");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }
    
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
    
    void StartSuccessSequence()
    {
        _isTransitioning = true;
        _audioSource.Stop();
        
        _audioSource.PlayOneShot(_successAudio);
        _successParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke(nameof(LoadNextLevel), _levelLoadDelay);
    }

    void StartCrashSequence()
    {
        _isTransitioning = true;
        _audioSource.Stop();

        _audioSource.PlayOneShot(_crashAudio);
        _crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke(nameof(ReloadLevel), _levelLoadDelay);
    }
}
