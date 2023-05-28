using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour
{
    
    private bool isStory2Active = false;

    private void Update()
    {
        // Check for space key press to deactivate the story
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DeactivateStory2();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    

    public void ToggleStory2()
    {
        if (isStory2Active)
        {
            DeactivateStory2();
        }
        else
        {
            ActivateStory2();
        }
    }

    private void ActivateStory2()
    {
        Transform[] children = GetComponentsInChildren<Transform>(true);

        foreach (Transform child in children)
        {
            if (child.name == "story2")
            {
                child.gameObject.SetActive(true);
            }
        }

        isStory2Active = true;
    }

    private void DeactivateStory2()
    {
        Transform[] children = GetComponentsInChildren<Transform>(true);

        foreach (Transform child in children)
        {
            if (child.name == "story2")
            {
                child.gameObject.SetActive(false);
            }
        }

        isStory2Active = false;
    }
    public void ReturnToMenu()
    {
    SceneManager.LoadScene("Start Scenes"); // Replace "MainMenu" with the actual scene name for your menu
    }


    

        
        




       
}
