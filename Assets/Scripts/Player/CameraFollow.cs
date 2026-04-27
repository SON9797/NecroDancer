using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("플레이어")]
    [SerializeField] private Transform _player;

    [Header("카메라 설정")]
    [Range(0.01f, 1f)]
    [SerializeField] private float _smoothTIme = 0.15f;

    [Header("카메라 위치 보정값")]
    [SerializeField] private Vector3 _offset = new Vector3(0f, 0f, -10f);

    private Vector3 _velocity = Vector3.zero;

    private void LateUpdate()
    {
        if (_player != null)
        {
            Vector3 desiredPosition = _player.position + _offset;

            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref _velocity, _smoothTIme);
        }
    }
}
