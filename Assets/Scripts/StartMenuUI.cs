using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuUI : MonoBehaviour
{ 
    public void StartGame()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
