using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ColorGame : MonoBehaviour
{
    bool noButtons;
    int resetTime;
    int reactTime;
    Button button;
    Slider slider;
    GameObject[] gameover;
    int score;

    // Color matching game

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
            //randomColor = color at the top
            Color randomColor = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
            Button[] buttonlist = GameObject.FindObjectsOfType<Button>();
            for (int i = 0; i < buttonlist.Length; i++)
            {
                Color c = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
                for (int j = 0; j < 5; j++) //making sure the random color isn't too close to the matching color
                {
                    float r = randomColor.r - c.r;
                    float g = randomColor.g - c.g;
                    float b = randomColor.b - c.b;
                    float total = r + g + b;
                    if (total < 0.2f && total > -0.2f)
                    {
                        c = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
                        Debug.Log("difference was: " + total);
                    }
                    else
                    {
                        break;
                    }
                }
                buttonlist[i].image.color = c;
            }
            button = GameObject.Find("ColorButton").GetComponent<Button>();
            button.image.color = randomColor;
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
                button.image.color = randomColor;
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
            rcttime = 40;
            resetTime = 10;
        }
        else if (score <= 60)
        {
            rcttime = 30;
            resetTime = 10;
        }
        else if (score <= 70)
        {
            rcttime = 20;
            resetTime = 10;
        }
        else if (score <= 80)
        {
            rcttime = 15;
            resetTime = 10;
        }
        reactTime = rcttime + resetTime + 30;
        slider.maxValue = rcttime + resetTime;
        resetTime = 3;
    }

    public void endGame()
    {
        int oldHighScore = PlayerPrefs.GetInt("CHighScore", 0);
        if (score > oldHighScore)
        {
            PlayerPrefs.SetInt("CHighScore", score);
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
