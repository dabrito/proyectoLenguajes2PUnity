using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
  public void PlayGame()
  {
    SceneManager.LoadScene(1);
  }
  public void QuitGame()
  {
    Application.Quit();
#if UNITY_EDITOR
      UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
  }
  public void ResetGameManagerAndGoToMenu()
  {
    if (GameManager.instance != null)
    {
      Destroy(GameManager.instance.gameObject);
    }
    SceneManager.LoadScene(0);
  }
}
