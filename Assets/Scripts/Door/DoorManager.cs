using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    [Header("문정보")]
    public string doorID;
    public string currentState;
    public string currentDirection;

    [Header("시각적 요소")]
    public SpriteRenderer sr;

    void Start()
    {
        DoorData data = GameDataManager.Instance.GetDoorData(doorID);

        if (data != null)
        {
            ApplyDoorState(data);
        }
    }

    private void ApplyDoorState(DoorData data)
    {
        currentState = data.State;
        currentDirection = data.Direction;

        Debug.Log(data.Name + " 문 적용 중: 상태-" + data.State + " 방향-" + data.Direction);

        if (data.State == "Open")
        {
            gameObject.SetActive(false);
        }

        else
        {
            UpdateSprite(data.State, data.Direction);
        }
    }

    private void UpdateSprite(string state, string direction)
    {
        string spriteName = $"Door_{state}_{direction}";

        Sprite newSprite = Resources.Load<Sprite>($"Sprites/{spriteName}");

        if (newSprite != null)
        {
            sr.sprite = newSprite;

            BoxCollider2D col = GetComponent<BoxCollider2D>();

            if (col != null)
            {
                col.size = sr.sprite.bounds.size;
            }
        }

        else
        {
            Debug.LogError(spriteName + " 이미지를 찾을 수 없습니다! Resources/Sprites 폴더를 확인하세요.");
        }
    }

    public void TryOpen()
    {
        if (currentState == "Closed")
        {
            currentState = "Open";
            gameObject.SetActive(false);
        }
    }
}
