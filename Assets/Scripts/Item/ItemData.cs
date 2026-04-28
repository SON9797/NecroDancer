using UnityEngine;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class ItemData
{
    public string ID;
    public string Name;
    public string Type;
    public string Icon;       
    public string ShadowIcon;
    public string StatName;
    public float StatValue;  
    public string Description;

    public ItemData(string id, string name, string type, string icon, string shadowIcon, string statName,string statValue, string description)
    {
        this.ID = id;
        this.Name = name;
        this.Type = type;
        this.Icon = icon;
        this.ShadowIcon = shadowIcon;
        this.StatName = statName;
        this.StatValue = float.TryParse(statValue, out float val) ? val : 0 ;
        this.Description = description;
    }
}

[CreateAssetMenu(fileName = "New Item Database", menuName = "Game Data/Item Database")]
public class ItemDatabase : ScriptableObject
{
    [Header("구글 시트 CSV 웹게시 링크")]
    public string sheetURL = "";

    [Header("불러온 아이템 데이터")]
    public List<ItemData> itemList = new List<ItemData>();

    public ItemData GetItemById(string id)
    {
        return itemList.Find(item => item.ID == id);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(ItemDatabase))]
public class ItemDatabaseEditor : Editor
{
    ItemDatabase data;

    void OnEnable()
    {
        data = (ItemDatabase)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.Label("\n\n아이템 스프레드 시트 읽어오기", EditorStyles.boldLabel);

        if (GUILayout.Button("데이터 읽기 (Item CSV)", GUILayout.Height(30)))
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
                data.itemList.Clear();

                string[] rows = csvData.Replace("\r", "").Split('\n');

                for (int i = 1; i < rows.Length; i++)
                {
                    if (string.IsNullOrWhiteSpace(rows[i])) continue;

                    string[] columns = rows[i].Split(',');

                    if (columns.Length >= 7)
                    {
                        data.itemList.Add(new ItemData(
                            columns[0].Trim(),
                            columns[1].Trim(),
                            columns[2].Trim(),
                            columns[3].Trim(),
                            columns[4].Trim(),
                            columns[5].Trim(),
                            columns[6].Trim(),
                            columns[7].Trim()
                        ));
                    }
                }

                EditorUtility.SetDirty(data);
                AssetDatabase.SaveAssets();
                Debug.Log($"아이템 데이터 불러오기 성공! 총 {data.itemList.Count}개 아이템 로드됨.");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("다운로드 실패: " + e.Message);
        }
    }
}
#endif