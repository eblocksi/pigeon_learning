using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float sceneLoadDelay = 0.5f;
    [SerializeField] float mainMenuClickDelay = 2f;

    AudioPlayer audioPlayer;
    private void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void LoadFirstLevel()
    {
        audioPlayer.
        StartCoroutine(WaitAndLoad(1, mainMenuClickDelay)); ;
        audioPlayer.PlayMenuSelectClip();
    }

    public void LoadNextLevel()
    {
        StartCoroutine(WaitAndLoad(SceneManager.GetActiveScene().buildIndex + 1, sceneLoadDelay));
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad(SceneManager.sceneCount - 1, sceneLoadDelay));
    }

    IEnumerator WaitAndLoad(int sceneIndex, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneIndex);
    }
}
