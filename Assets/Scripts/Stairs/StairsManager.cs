using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsManager : MonoBehaviour
{
    [Header("상태")]
    public bool isLocked = true;

    [Header("UI")]
    public GameObject lockUI;

    [Header("계단 이미지 교체")]
    public Sprite lockedSprite;
    public Sprite unLockedSprite;

    private SpriteRenderer _sr;

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        UpdateVisuals();
    }

    public void SetLocked(bool locked)
    {
        isLocked = locked;
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        if (lockUI != null)
        {
            lockUI.SetActive(isLocked);
        }

        if (_sr != null)
        {
            _sr.sprite = isLocked ? lockedSprite : unLockedSprite;
        }
    }

    public void UnlockStage()
    {
        SetLocked(false);
        Debug.Log(gameObject.name + " 해금됨!");
    }

    private void OnMouseDown()
    {
        if (isLocked)
        {
            SetLocked(false);
        }
    }
}
