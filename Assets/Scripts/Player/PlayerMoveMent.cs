using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveMent : MonoBehaviour
{
    [Header("이동 설정")]
    [SerializeField] private float _moveTime = 0.15f;

    [Header("충돌 설정")]
    public LayerMask obstacleLayer;

    private bool _isMoving = false;

    void Update()
    {
        if (_isMoving)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            StartCoroutine(MovePlayer(Vector3.up));
        }

        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(MovePlayer(Vector3.down));
        }

        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(MovePlayer(Vector3.left));
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            StartCoroutine(MovePlayer(Vector3.right));
        }
    }

    private IEnumerator MovePlayer(Vector3 direction)
    {
        _isMoving = true;

        Vector3 startPos = transform.position;
        Vector3 targetPos = startPos + direction;

        Collider2D hit = Physics2D.OverlapCircle(targetPos, 0.2f, obstacleLayer);

        if (hit != null)
        {
            DoorManager door = hit.GetComponent<DoorManager>();

            if (door != null && door.currentState == "Closed")
            {
                door.TryOpen();   
                _isMoving = false;
                yield break;      
            }

            _isMoving = false;
            yield break;
        }

        float elapsedTime = 0f;
        float jumpHeight = 0.5f;

        while (elapsedTime < _moveTime)
        {
            float normalizedTime = elapsedTime / _moveTime;
            Vector3 currentPos = Vector3.Lerp(startPos, targetPos, normalizedTime);

            float yOffset = Mathf.Sin(normalizedTime * Mathf.PI) * jumpHeight;
            currentPos.y += yOffset;

            transform.position = currentPos;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;
        _isMoving = false;
    }
}
