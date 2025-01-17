using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIDisplay : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] Health playerHealth;
    [SerializeField] Slider healthSlider;

    [Header("Score")]
    ScoreKeeper scoreKeeper;
    [SerializeField] TextMeshProUGUI scoreText;
    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }
    void Start()
    {
        healthSlider.maxValue = playerHealth.GetHealth();
        healthSlider.value = playerHealth.GetHealth();
    }

    void Update()
    {
        int currentHealth = playerHealth.GetHealth();
        healthSlider.value = currentHealth;
        scoreText.text = scoreKeeper.GetScore().ToString("00000000");
    }
}
