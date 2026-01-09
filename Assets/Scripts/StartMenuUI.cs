using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuUI : MonoBehaviour
{ 
    public void StartGame()
    {
        SceneManager.LoadScene("Lvl_Nivelin 1");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
