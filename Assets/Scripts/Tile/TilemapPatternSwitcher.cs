using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilemapPatternSwitcher : MonoBehaviour
{
    [Header("≈∏¿œ∏ ")]
    [SerializeField] private GameObject _tilemapA;
    [SerializeField] private GameObject _tilemapB;

    public void TogglePattern()
    {
        if (_tilemapA == null || _tilemapB == null)
        {
            return;
        }

        bool isActive = _tilemapA.activeSelf;

        _tilemapA.SetActive(!isActive);
        _tilemapB.SetActive(isActive);
    }
}
