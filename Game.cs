using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Game : MonoBehaviour
{
    bool noButtons;
    int resetTime;
    int reactTime;
    Button button;
    Slider slider;
    GameObject[] gameover;
    int score;

    //Basic speed tester

    void Start()
    {
        noButtons = true;
        resetTime = 60;
        reactTime = 3600;
        score = 0;
        gameover = GameObject.FindGameObjectsWithTag("gameOver");
        for (int i = 0; i < gameover.Length; i++)
        {
            gameover[i].SetActive(false);
        }
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
        Input.backButtonLeavesApp = false;
    }


    void Update()
    {
        if (resetTime > 0) { resetTime--; }
        if (reactTime > 0) { reactTime--; }
        GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>().SetText("Score: " + score);
        slider = GameObject.Find("TimeSlider").GetComponent<Slider>();
        slider.value = reactTime;
        if (reactTime <= 0)
        {
            endGame();
        }
        if (noButtons && resetTime == 0)
        {
            int number = Random.Range(0, 9);
            switch (number)
            {
                case 0:
                    button = GameObject.Find("ButtonLeft").GetComponent<Button>();
                    break;
                case 1:
                    button = GameObject.Find("ButtonMiddle").GetComponent<Button>();
                    break;
                case 2:
                    button = GameObject.Find("ButtonRight").GetComponent<Button>();
                    break;
                case 3:
                    button = GameObject.Find("Button1Left").GetComponent<Button>();
                    break;
                case 4:
                    button = GameObject.Find("Button1Middle").GetComponent<Button>();
                    break;
                case 5:
                    button = GameObject.Find("Button1Right").GetComponent<Button>();
                    break;
                case 6:
                    button = GameObject.Find("Button2Left").GetComponent<Button>();
                    break;
                case 7:
                    button = GameObject.Find("Button2Middle").GetComponent<Button>();
                    break;
                case 8:
                    button = GameObject.Find("Button2Right").GetComponent<Button>();
                    break;
            }
            if (button != null)
            {
                button.image.color = Color.red;
            }
            noButtons = false;
        }
        if (gameover[0].activeSelf && Input.anyKeyDown)
        {
            SceneManager.LoadScene(0);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
        
    }

    public void leftPressed(Button pressed)
    {
        if (pressed.Equals(button))
        {
            score++;
            buttonDeselect();
        }
        else
        {
            endGame();
        }
    }

    public void buttonDeselect()
    {
        int rcttime = 120;
        button.image.color = Color.white;
        button = null;
        noButtons = true;
        if (score <= 10)
        {
            rcttime = 120;
            resetTime = 30;
        }
        else if (score <= 20)
        {
            rcttime = 100;
            resetTime = 20;
        }
        else if (score <= 30)
        {
            rcttime = 80;
            resetTime = 20;
        }
        else if (score <= 40)
        {
            rcttime = 60;
            resetTime = 10;
        }
        else if (score <= 50)
        {
            rcttime = 50;
            resetTime = 10;
        }
        else if (score <= 60)
        {
            rcttime = 45;
            resetTime = 10;
        }
        else if (score <= 70)
        {
            rcttime = 40;
            resetTime = 10;
        }
        else if (score <= 80)
        {
            rcttime = 35;
            resetTime = 10;
        }
        else if (score <= 90)
        {
            rcttime = 30;
            resetTime = 10;
        }
        else if (score <= 90)
        {
            rcttime = 20;
            resetTime = 10;
        }
        reactTime = rcttime + resetTime;
        slider.maxValue = rcttime + resetTime;        
    }

    public void endGame()
    {
        int oldHighScore = PlayerPrefs.GetInt("SHighScore", 0);
        if (score > oldHighScore)
        {
            PlayerPrefs.SetInt("SHighScore", score);
        }
        for (int i = 0; i < gameover.Length; i++)
        {
            gameover[i].SetActive(true);
        }
    }

    public void SliderColor(Slider slider1)
    {
        float q = (slider1.value / slider1.maxValue);
        slider1.targetGraphic.color = Color.Lerp(Color.red, Color.white, q);
    }
}
