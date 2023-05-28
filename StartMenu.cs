using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    private bool isStoryActive = false;

    private void Update()
    {
        // Check for space key press to deactivate the story
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DeactivateStory();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }

    public void ToggleStory()
    {
        if (isStoryActive)
        {
            DeactivateStory();
        }
        else
        {
            ActivateStory();
        }
    }

    private void ActivateStory()
    {
        Transform[] children = GetComponentsInChildren<Transform>(true);

        foreach (Transform child in children)
        {
            if (child.name == "story")
            {
                child.gameObject.SetActive(true);
            }
        }

        isStoryActive = true;
    }

    private void DeactivateStory()
    {
        Transform[] children = GetComponentsInChildren<Transform>(true);

        foreach (Transform child in children)
        {
            if (child.name == "story")
            {
                child.gameObject.SetActive(false);
            }
        }

        isStoryActive = false;
    }
}
