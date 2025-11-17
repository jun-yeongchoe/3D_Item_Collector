using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score_Text;
    [SerializeField] private TextMeshProUGUI time_Text;
    [SerializeField] private TextMeshProUGUI[] quest_Text;

    public float time;
    private float remit_Time = 60f;
    #region: 각 아이템별 개수
    int count = 0;
    [SerializeField] private string[] poolKeys;
    private IPoolInfo[] questPools;
    #endregion

    [SerializeField] public GameObject gameOverPanel;


    private void Start()
    {
        time = remit_Time;
        
        int len = Mathf.Min(poolKeys.Length, quest_Text.Length);
        questPools = new IPoolInfo[len];

        for (int i = 0; i < len; i++)
        {
            string key = poolKeys[i];

            if (PoolManager.Instance.pools.TryGetValue(key, out var box) && box is IPoolInfo info)
            {
                questPools[i] = info;
            }
        }

    }
    void Update()
    {
        time -= Time.deltaTime;
        if (time < 0)
        {
            time = 0;
            // 게임오버 표시
            GameManager.isGameOver = true;
        }

        score_Text.text = $"Score : {GameManager.Score}";
        time_Text.text = $"{ToSSMS(time)}";
        for (int i = 0; i < questPools.Length; i++)
        {
            var info = questPools[i];
            var txt = quest_Text[i];

            if (info == null || txt == null) continue;
            int collected = info.DisabledCount;
            int totalActive = info.DisabledCount + info.ActiveCount;

            txt.text = $"{poolKeys[i]} : {collected} / {totalActive}";

        }
    }

    public void ResetTimer() 
    { 
        time = 60f; 
    }

    string ToSSMS(float total)
    {
        int s = Mathf.FloorToInt(total % 60f);
        int ms = Mathf.FloorToInt((total - Mathf.Floor(total)) * 1000f);

        return $"{s:00}:{ms:00}";
    }

}
