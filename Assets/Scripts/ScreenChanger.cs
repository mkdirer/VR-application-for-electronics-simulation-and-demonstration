using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenChanger : MonoBehaviour
{
    public string sceneToLoad;
    public float deleyTime;

    public void LoadHomeScene()
    {
        Invoke("GoToHome", deleyTime);
    }

    public void GoToHome()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void ExitMode()
    {
        Application.Quit();
    }
}
