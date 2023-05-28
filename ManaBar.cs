using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    private Image fillImage;
    private Text manaText; // Reference to the Text component
    private float maxMana = 100f;
    private float currentMana = 0f;
    private int itemCounter = 0; // Counter to track the number of items collected

    public float CurrentMana
    {
        get => currentMana;
        set
        {
            currentMana = value;
            if (currentMana > maxMana)
            {
                currentMana = maxMana;
            }
            else if (currentMana < 0f)
            {
                currentMana = 0f;
            }
        }
    }

    public float MaxMana
    {
        get => maxMana;
    }

    // Start is called before the first frame update
    void Start()
    {
        fillImage = GetComponent<Image>();
        manaText = GetComponentInChildren<Text>(); // Get the Text component from the child GameObject
        UpdateManaText();
    }

    // Update is called once per frame
    void Update()
    {
        fillImage.fillAmount = CurrentMana / MaxMana;
    }

    public void AddMana(float amount)
    {
        CurrentMana += amount;
        UpdateManaText();
    }

    public void UseMana(float amount)
    {
        CurrentMana -= amount;
        UpdateManaText();
    }

    private void UpdateManaText()
    {
        float percentage = (CurrentMana / MaxMana) * 100f;
        manaText.text = "Mana: " + percentage.ToString("0") + "%";
    }

    // Call this method whenever an item is collected
    public void CollectItem()
    {
        itemCounter++;
        float itemMana = 20f; // Amount of mana to add per item collected
        AddMana(itemMana);

        if (itemCounter == 1)
        {
            CurrentMana = itemMana; // Set mana to the amount of the first item
        }
    }
}
