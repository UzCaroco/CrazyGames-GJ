using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private byte indexScene;
    [SerializeField] private GameObject PainelMenu, PainelOptions, painelCredits;

    public void StartGame()
    {
       StartCoroutine(LoadScene(indexScene)); // Start the coroutine to load the scene
    }

    public void OpenOptions()
    {
        PainelMenu.SetActive(false); // Hide the main menu panel
        PainelOptions.SetActive(true); // Show the options panel
    }

    public void CloseOptions()
    {
        PainelMenu.SetActive(true); // Show the main menu panel
        PainelOptions.SetActive(false); // Hide the options panel
    }

    public void OpenCredits()
    {
        PainelMenu.SetActive(false); // Hide the main menu panel
        painelCredits.SetActive(true); // Show the credits panel
    }

    public void CloseCredits()
    {
        PainelMenu.SetActive(true); // Show the main menu panel
        painelCredits.SetActive(false); // Hide the credits panel
    }

    IEnumerator LoadScene(int sceneName)
    {
        // Start loading the scene asynchronously
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
            Debug.Log("Loading scene: " + asyncLoad.progress * 100 + "%"); // Log the loading progress (0 to 1)
        }
    }
}
