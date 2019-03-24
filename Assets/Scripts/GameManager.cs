using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public void ChangeLevel(string name)
    {
        Debug.Log("Load request for level " + name);
        SceneManager.LoadScene(name);
    }

    public void QuitGame()
    {
        Debug.Log("Quit requested... Bye Bye!");
        Application.Quit();
    }
    


}
