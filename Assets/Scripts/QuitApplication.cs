using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitApplication : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        QuitFromTheGame();
    }

    void QuitFromTheGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Good Bye");
            Application.Quit();
        }
    }
}
