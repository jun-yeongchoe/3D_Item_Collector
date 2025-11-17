using System.Collections;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using Unity.EditorCoroutines.Editor;

//구글스프레드 시트에서 아이템 데이터 들고와서 스크립터블 오브젝트로 변환
public class ItemDataImporter : EditorWindow
{
    //URL
    public string csvURL = "https://docs.google.com/spreadsheets/d/1SR5FTdisKCxgnLH2NQQnlUIteVXC6MNWVOK5pjfWipg/export?format=csv";

    //스크립터블 오브젝트를 저장할 경로
    private string savePath = "Assets/Data/Items";

    [MenuItem("Tools/Import Item Data From Google Sheets")]
    public static void ShowWindow()
    {
        //Item CSV Importer라는 이름의 에디터 창을 생성
        GetWindow(typeof(ItemDataImporter), false, "Item CSV Importer");
    }
    private void OnGUI()
    {
        //라벨
        GUILayout.Label("Google Sheet CSV URL", EditorStyles.boldLabel);

        csvURL = EditorGUILayout.TextField(csvURL);

        if (GUILayout.Button("Download and Generate SO"))
        {
            //코루틴 실행
            EditorCoroutineUtility.StartCoroutineOwnerless(ImportCSV());
        }
    }

    //실제로 csv를 가져와서 스크립터블오브젝트를 생성하는 코루틴
    IEnumerator ImportCSV()
    {
        if (!Directory.Exists(savePath)) {
            Directory.CreateDirectory(savePath);
        }

        //구글시트에서 csv 텍스트 데이터를 받아오기 위한 요청 생성
        UnityWebRequest www = UnityWebRequest.Get(csvURL);
        yield return www.SendWebRequest();

        //실패하면 에러 뱉고 끝내라
        if (www.result != UnityWebRequest.Result.Success) {
            Debug.Log("Downloat failed");
            yield break;
        }
        string csvText = www.downloadHandler.text;
        if (!string.IsNullOrEmpty(csvText) && csvText.Length > 0 && csvText[0] == '\uFEFF')
            csvText = csvText.Substring(1);        // BOM 제거
        csvText = csvText.Replace("\r", "");
        string[] lines = csvText.Split('\n');

        for (int i = 1; i< lines.Length; i++) //첫번째 줄은 Header이기 때문에 두번째줄부터 반복해서 출력하겠다.
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;

            string[] values = lines[i].Split(','); // 각줄을 다시 한번 콤마(,)로 나눔
            var idStr = values[0].Trim();
            var nameStr = values[1].Trim();
            var pointStr = values[2].Trim();

            if (!int.TryParse(idStr, out int id)) continue;
            if (!int.TryParse(pointStr, out int point)) continue;

            ItemSO item = ScriptableObject.CreateInstance<ItemSO>();
            item.ID = id;
            item.itemName = nameStr;
            item.point = point;

            string assetPath = $"{savePath}/Item_{item.ID}_{item.itemName}.asset"; // asset 파일 경로지정(Item_1_sword.asset)

            AssetDatabase.CreateAsset(item, assetPath);
            
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("아이템 데이터 ScriptableObject 생성완료!");
    }
}
