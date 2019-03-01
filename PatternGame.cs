using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PatternGame : MonoBehaviour
{
    bool noButtons;
    Button button;
    GameObject[] gameover;
    int score;
    string buttonOrder;
    int buttonAmount;
    int timer;
    int currentButton;
    int showTime;

    // Pattern memorizing game

    void Start()
    {
        showTime = 20;
        currentButton = 0;
        buttonAmount = 3;
        noButtons = true;
        score = 0;
        timer = 30;
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
        GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>().SetText("Score: " + score);
        if (timer > 0)
        {
            timer--;
        }
        if (noButtons)
        {
            string tempstring = "";
            for (int i = 0; i < buttonAmount; i++)
            {
                tempstring += Random.Range(0, 9).ToString();
            }
            buttonOrder = tempstring;
            noButtons = false;
        }
        if (timer == showTime)
        {
            for (int i = 0; i < 9; i++)
            {
                Button[] b = GameObject.FindObjectsOfType<Button>();
                b[i].image.color = Color.white;
            }
        }
        if (timer == 0)
        {
            timer = showTime*2;
            if (currentButton < buttonOrder.Length)
            {
                int a = buttonOrder[currentButton] - 48;
                highlightButton(a);
                currentButton++;
            }
            else
            {
                currentButton = 0;
                timer = -1;
            }
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

    public void ButtonPressed(int num)
    {
        if (timer == -1)
        {
            if (currentButton < buttonOrder.Length && num == buttonOrder[currentButton] - 48)
            {
                score++;
                currentButton++;
                if (currentButton >= buttonOrder.Length)
                {
                    timer = showTime * 2;
                    currentButton = 0;
                    noButtons = true;
                }
                DifficultyCheck();
            }
            else
            {
                endGame();
            }
        }
    }

    public void DifficultyCheck()
    {
        if (score < 12) { buttonAmount = 3; showTime = 20; }
        else if (score < 24) { buttonAmount = 4; showTime = 20; }
        else if (score < 49) { buttonAmount = 5; showTime = 15; }
        else if (score < 85) { buttonAmount = 6; showTime = 15; }
        else if (score < 134) { buttonAmount = 7; showTime = 10; }
        else if (score < 198) { buttonAmount = 8; showTime = 10; }
        else if (score < 279) { buttonAmount = 9; showTime = 10; }
        else { buttonAmount = 10; showTime = 5; }
    }

    public void highlightButton(int num)
    {
        switch (num)
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
    }

    public void endGame()
    {
        int oldHighScore = PlayerPrefs.GetInt("PHighScore", 0);
        if (score > oldHighScore)
        {
            PlayerPrefs.SetInt("PHighScore", score);
        }
        for (int i = 0; i < gameover.Length; i++)
        {
            gameover[i].SetActive(true);
        }
    }
}
