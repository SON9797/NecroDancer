using UnityEngine;
using GoogleSheetsToUnity;
using System.Collections.Generic;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class DoorData
{
    public string ID;
    public string Name;
    public string State;
    public string Direction;

    public DoorData(string id, string name, string state, string direction)
    {
        this.ID = id;
        this.Name = name;
        this.State = state;
        this.Direction = direction;
    }
}

[CreateAssetMenu(fileName = "New Door Database", menuName = "Game Data/Door Database")]
public class DoorDatabase : ScriptableObject
{
    [Header("구글 시트 CSV 웹게시 링크")]
    [Tooltip("구글 시트 -> 파일 -> 공유 -> 웹에 게시 -> CSV 형식으로 나온 긴 링크를 전체 복사해서 넣으세요.")]
    public string sheetURL = "";

    [Header("불러온 문 데이터")]
    public List<DoorData> doorList = new List<DoorData>();
}

#if UNITY_EDITOR
[CustomEditor(typeof(DoorDatabase))]
public class DoorDatabaseEditor : Editor
{
    DoorDatabase data;

    void OnEnable()
    {
        data = (DoorDatabase)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.Label("\n\n스프레드 시트 읽어오기", EditorStyles.boldLabel);

        if (GUILayout.Button("데이터 읽기 (안전한 방식)", GUILayout.Height(30)))
        {
            DownloadData();
        }
    }

    void DownloadData()
    {
        if (string.IsNullOrEmpty(data.sheetURL) || !data.sheetURL.StartsWith("http"))
        {
            Debug.LogError("유효한 구글 시트 CSV 링크를 입력해주세요!");
            return;
        }

        try
        {
            using (System.Net.WebClient client = new System.Net.WebClient())
            {
                string csvData = client.DownloadString(data.sheetURL);
                data.doorList.Clear();

                string[] rows = csvData.Replace("\r", "").Split('\n');

                for (int i = 1; i < rows.Length; i++)
                {
                    if (string.IsNullOrWhiteSpace(rows[i])) continue;

                    string[] columns = rows[i].Split(',');

                    if (columns.Length >= 4)
                    {
                        data.doorList.Add(new DoorData(
                            columns[0].Trim(),
                            columns[1].Trim(),
                            columns[2].Trim(),
                            columns[3].Trim()
                        ));
                    }
                }

                EditorUtility.SetDirty(data);
                AssetDatabase.SaveAssets();
                Debug.Log("구글 시트 데이터 불러오기 성공! 총 개수: " + data.doorList.Count);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("다운로드 실패: " + e.Message);
        }
    }
}
#endif