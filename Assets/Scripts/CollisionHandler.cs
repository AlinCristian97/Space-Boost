using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float _levelLoadDelay = 2f;
    [SerializeField] private AudioClip _success;
    [SerializeField] private AudioClip _crash;

    private AudioSource _audioSource;

    private bool _isTransitioning = false;

    void Start() 
    {
        _audioSource = GetComponent<AudioSource>();
    }


    void OnCollisionEnter(Collision other) 
    {
        if (_isTransitioning) return;
        
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
        
        _audioSource.PlayOneShot(_success);
        // todo add particle effect upon crash
        GetComponent<Movement>().enabled = false;
        Invoke(nameof(LoadNextLevel), _levelLoadDelay);
    }

    void StartCrashSequence()
    {
        _isTransitioning = true;
        _audioSource.Stop();

        _audioSource.PlayOneShot(_crash);
        GetComponent<Movement>().enabled = false;
        Invoke(nameof(ReloadLevel), _levelLoadDelay);
    }
}
