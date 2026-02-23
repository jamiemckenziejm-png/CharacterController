using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] string playScene = "SampleScene";
    [SerializeField] string mainMenuScene = "StartScene";

    public void StartGame()
    {
        Cursor.visible = false; // Hide the cursor when starting the game
        SceneManager.LoadScene(playScene); // Load the specified scene for playing the game
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game"); // Log a message to the console when quitting the game
        Application.Quit(); // Quit the application
    }
}
