using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] GameObject gameOverPanel;


    void Update()
    {
        Debug.Log("게임오버 상태 : " + GameManager.isGameOver);
        Debug.Log("현재 맥스 스코어 : " + GameManager.MaxScore);
        if (GameManager.Score >= GameManager.MaxScore) GameManager.isGameOver = true;
        if (GameManager.isGameOver) ActiveGameOver();
    }

    public void ActiveGameOver()
    {
        if (!GameManager.isGameOver) return;
        gameOverPanel.SetActive(true);
    }
}
