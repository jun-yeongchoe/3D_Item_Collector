using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score_Text;
    [SerializeField] private TextMeshProUGUI time_Text;
    [SerializeField] private TextMeshProUGUI[] quest_Text;

    private float time;
    private float remit_Time = 60f;
    #region: 각 아이템별 개수
    List<int> count = new List<int>(5);
    #endregion
    

    private void Start()
    {
        foreach (var i in PoolManager.Instance.pools)
        {
            Debug.Log(i.Value);
            // 20251114 여기까지 작성 -> 오브젝트 풀 아래의 비활성 객체 갯수 받아서 현재/전체 UI 구성중
        }
        time = remit_Time;

    }

    void Update()
    {
        for (int i = 0; i < count.Count; i++) 
        {
            Debug.Log($"{i} 번째 항목의 전체 갯수 : {count[i]}");
        }

        time -= Time.deltaTime;
        if (time < 0)
        {
            time = 0;
            // 게임오버 표시
        }

        score_Text.text = $"Score : {GameManager.Score}";
        time_Text.text = $"{ToSSMS(time)}";
    }

    string ToSSMS(float total)
    {
        int s = Mathf.FloorToInt(total % 60f);
        int ms = Mathf.FloorToInt((total - Mathf.Floor(total)) * 1000f);

        return $"{s:00}:{ms:00}";
    }

}
