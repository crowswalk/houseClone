using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) //resets the scene
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
