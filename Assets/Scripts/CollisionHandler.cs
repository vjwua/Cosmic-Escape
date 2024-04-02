using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float nextLevelDelay = 1.0f;
    [SerializeField] AudioClip explosion;
    [SerializeField] AudioClip success;

    [SerializeField] ParticleSystem explosionParticles;
    [SerializeField] ParticleSystem successParticles;


    AudioSource audioSource;

    bool isTransitioning = false;
    bool collisionOn = true;

    private void Start(){
        audioSource = GetComponent<AudioSource>();
    }

    void Update(){
        FastCodes();
    }

    private void OnCollisionEnter(Collision other) {
        if (isTransitioning || !collisionOn) {
            return;
        }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This thing is friendly");
                break;
            case "Finish":
                StartSuccess();
                Debug.Log("You finished!");
                break;
            default:
                StartCrash();
                Debug.Log("You touched something");
                break;
        }
    }

    void FastCodes()
    {
        if (Input.GetKey(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKey(KeyCode.C))
        {
            collisionOn = !collisionOn; //toggle collision
        }
    }

    private void StartCrash()
    {
        explosionParticles.Play();
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(explosion);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", nextLevelDelay);
    }

    private void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void StartSuccess()
    {
        successParticles.Play();
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", nextLevelDelay);
    }

    private void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings) {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
        
    }
}
