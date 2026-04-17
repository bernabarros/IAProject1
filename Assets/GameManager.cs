using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private int deathCount = 0;


    [SerializeField]private TextMeshProUGUI deathTextUI;

    private void Awake()
    {
        Instance = this;
        UpdateUI(); 
    }

    /// <summary>
    /// add 1 to counter of deaths and updates UI
    /// </summary>
    public void RegisterDeath()
    {
        deathCount++;
        UpdateUI();
    }

    /// <summary>
    /// Update UI
    /// </summary>
    private void UpdateUI()
    {
        if (deathTextUI != null)
        {
            deathTextUI.text = "Deaths: " + deathCount;
        }
    }
}
