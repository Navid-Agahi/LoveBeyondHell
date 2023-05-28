using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public float timerDuration = 300f; // 5 minutes in seconds
    public Text timerText;
    public Text deathCounterText;
    public int maxDeaths = 3; // Maximum number of allowed deaths
    public GameObject manaPillar; // Reference to the mana pillar GameObject

    public float currentTime;
    private int deathCounter;
    private ManaBar manaBar; // Reference to the ManaBar script
    private RectTransform manaPillarRectTransform;
    [SerializeField] private GameObject gameoverCanvas;

    void Start()
    {
        currentTime = timerDuration;
        deathCounter = 0;
        manaBar = GetComponentInChildren<ManaBar>(); // Get the ManaBar script attached to a child GameObject
        UpdateTimerText();
        UpdateDeathCounterText();
        manaPillarRectTransform = manaPillar.GetComponent<RectTransform>();
    }

    void Update()
    {
        // Update the timer
        currentTime -= Time.deltaTime;
        if (currentTime <= 0f)
        {
            // Timer has run out
            currentTime = 0f;
            // Perform game over logic
            gameoverCanvas.SetActive(true);
            GameOver();
        }
        UpdateTimerText();
        float pillarScaleX = manaBar.CurrentMana / manaBar.MaxMana;
        float currentScaleY = manaPillarRectTransform.localScale.y; // Get the current Y scale
        manaPillarRectTransform.localScale = new Vector3(pillarScaleX, currentScaleY, 1f);
    }

    void GameOver()
    {
        // Perform game over logic here
        Debug.Log("Game Over");
    }

    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void UpdateDeathCounterText()
    {
        deathCounterText.text = "Deaths: " + deathCounter + " / " + maxDeaths;
    }

    public void IncrementDeathCounter()
    {
        deathCounter++;
        UpdateDeathCounterText();
    }

    public void IncrementMana()
    {
        if (manaBar != null)
        {
            manaBar.AddMana(1f); // Call the AddMana method in the ManaBar script
        }
    }
}
