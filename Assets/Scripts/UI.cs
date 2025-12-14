using TMPro;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public static UI instance;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI killCountText;
    [Space]
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject WinUI;

    private int killCount;

    private void Awake()
    {
        instance = this;
        Time.timeScale = 1f;
    }

    private void Update()
    {
        timerText.text = Time.time.ToString("F2") + "s";
    }

    public void EnableGameOverUI(){ 
        Time.timeScale = 0.5f;
        gameOverUI.SetActive(true);
    }

    public void AddKillCount()
    {
        killCount++;
        killCountText.text = killCount.ToString();
    }

    public void EnableWinUI()
    {
        Time.timeScale = 0f;
        WinUI.SetActive(true);
    }

    public void ResetLevel()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
    }

    public int getKillCount()
    {
        return killCount;
    }
}
