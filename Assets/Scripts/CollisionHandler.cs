using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] AudioClip explosionSound;
    [SerializeField] AudioClip successSound;

    AudioSource audioSource;

    bool isTransitioning = false;

    readonly float timeDelayed = 2f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Stop();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning) return;

        switch(collision.gameObject.tag)
        {
            case "Start":
                break;
            case "Fuel":
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                Debug.Log("You die!");
                StartCrushSequence();
                break;
        }
    }


    public void StartCrushSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(explosionSound);
        DisableComponents();
        Invoke(nameof(ReloadCurrentScene), timeDelayed);
    }
    
    public void StartSuccessSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(successSound);
        DisableComponents();
        Invoke(nameof(GoToNextLevel), timeDelayed);
    }

    public void DisableComponents()
    {
        GetComponent<Movement>().enabled = false;
    }

    public void ReloadCurrentScene()
    {
        int currentSceneIndex = GetIndexOfScene();
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void GoToNextLevel()
    {
        int nextScene = GetIndexOfScene() + 1;
        if (nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }
        SceneManager.LoadScene(nextScene);
    }

    public int GetIndexOfScene()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }
}
