using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text gameText;

    [Range(1f,100f)]public float difficultyMultiplier=1f;
    private void Awake()
    {
        gameText.enabled = true;

        //changes the multiplier to be inverse of its value and in percent
        difficultyMultiplier = 1-(difficultyMultiplier*0.01f);
    }

    //Creates a method that disables the text object when called
    public void StartGame()
    {
        gameText.enabled = false;
    }

    //Creates a method that stops time and enables the game over text
    public void GameOver()
    {
        Time.timeScale = 0f;

        gameText.text = ($"Game Over!\n\n\nPress Space to Restart");
        gameText.enabled = true;
    }

    //creates a method to set the time back to 1 and also reloads the scene, after pressing the Space button
    public void RestartGame()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(0);
        }
    }
}
