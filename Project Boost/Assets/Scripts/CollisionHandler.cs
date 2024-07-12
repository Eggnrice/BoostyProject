using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float reloadLevelDelay = 1f;
    [SerializeField] float nextLevelDelay = 1f;
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip success;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly object hit");
                break;
            case "Finish":
                FinishLevelSequence();
                break;
            case "Fuel":
                Debug.Log("Extra fuel added!");
                break;
            default:
                StartCrashSequence();
                break;

        }
    }

    private void StartCrashSequence()
    {
        audioSource.PlayOneShot(crash);
        // todo add particle effect upon crash
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", reloadLevelDelay);
    }

    private void FinishLevelSequence()
    {
        audioSource.PlayOneShot(success);
        // todo add particle effect upon crash
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", nextLevelDelay);
    }
    private void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    private void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
