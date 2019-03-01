using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ButtonCommands : MonoBehaviour
{
    public static int nextScene = 0;

    void Start()
    {
        Input.backButtonLeavesApp = false;
    }

    void Update()
    {
        GameObject.Find("HighScoreNum").GetComponent<TextMeshProUGUI>().SetText("" + PlayerPrefs.GetInt("SHighScore", 0) + "\n" + 
                                                                                    PlayerPrefs.GetInt("PHighScore", 0) + "\n" + 
                                                                                    PlayerPrefs.GetInt("CHighScore", 0) + "\n");
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameObject b = GameObject.Find("ExitButton");
            if (b != null)
            {
                b.GetComponent<Button>().onClick.Invoke();
            }
            b = GameObject.Find("BackButton");
            if (b != null)
            {
                b.GetComponent<Button>().onClick.Invoke();
            }
        }
    }


    public void ResetHighScore()
    {
        PlayerPrefs.SetInt("SHighScore", 0);
        PlayerPrefs.SetInt("PHighScore", 0);
        PlayerPrefs.SetInt("CHighScore", 0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
