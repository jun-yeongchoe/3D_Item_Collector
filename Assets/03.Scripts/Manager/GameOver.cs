using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] GameObject gameOverPanel;
    void Update()
    {
        Debug.Log(GameManager.isGameOver);
        Debug.Log(GameManager.MaxScore);
        if (GameManager.Score >= GameManager.MaxScore) GameManager.isGameOver = true;
        if (GameManager.isGameOver) ActiveGameOver();
    }

    public void ActiveGameOver()
    {
        if (!GameManager.isGameOver) return;
        gameOverPanel.SetActive(true);
    }
}
