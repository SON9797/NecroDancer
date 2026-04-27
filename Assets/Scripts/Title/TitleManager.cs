using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    private bool _isLoading = false;

    void Update()
    {
        if (Input.anyKeyDown && !_isLoading)
        {
            LoadNextScene();
        }
    }

    private void LoadNextScene()
    {
        _isLoading = true;

        SceneManager.LoadScene("LobbyScene");
    }
}
