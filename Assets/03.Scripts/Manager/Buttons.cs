using UnityEngine;

public class Buttons : MonoBehaviour
{
    [SerializeField] private GameObject itemListPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject player;

    [SerializeField] private UIManager uiManager;
    [SerializeField] private ItemSpawner itemSpawner;
    public void ItemListBtnToClick()
    {
        itemListPanel.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void CloseToClick()
    {
        this.gameObject.SetActive(true);
        itemListPanel.SetActive(false);
    }

    public void Restart()
    {
        gameOverPanel.SetActive(false);
        GameManager.isGameOver = false;
        uiManager.ResetTimer();
        player.transform.position = new Vector3(721.0532f, 0, 51f);
        PoolManager.Instance.ResetAllPools();
        itemSpawner.PlaceItem();
        GameManager.Score = 0;
    }

    public void Exit()
    {
        gameOverPanel.SetActive(false);
        Application.Quit();
    }
}
