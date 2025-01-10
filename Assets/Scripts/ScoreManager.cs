using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public TextMeshProUGUI scoreText;
    public int score;

    private void Awake()
    {
        MakeSingleton();
    }

    private void Start()
    {
        
        AddScore(0);
    }

    private void Update()
    {
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        scoreText.text = score.ToString();
    }

    private void MakeSingleton()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            
            DontDestroyOnLoad(gameObject);
        }
    }

    public void AddScore(int point)
    {
        score += point;
        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            Debug.Log("1");
            PlayerPrefs.SetInt("HighScore", score);
            Debug.Log("2");
        }
        
        scoreText.text = score.ToString();
    }

    public void ResetScore()
    {
        score = 0;
    }
}