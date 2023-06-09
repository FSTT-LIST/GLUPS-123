using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Clavier4 : MonoBehaviour
{
    public Text result;
    public Text time;
    public Text num;
    public Text scoreText;
    public Text PlayeName;
    public Text ShowMsgResult;
    public Text finalScore;
    public Text iScore;

    public int score;
    public int x;
    public int y;
    private int z;
    public bool is_true = false;
    public bool is_false = false;
    private bool is_blocked = false;
    public bool is_finished = false;
    public bool is_muted;
    public bool is_counting;
    public int levelIndex;
    public string Player;
    public float timer;
    private int min;
    public int essaieNumber;

    public Animator girl;
    public Animator boy;
    public GameObject Boy;
    public GameObject Girl;
    public AudioClip loseAudio;
    public AudioClip winAudio;
    public AudioClip loser;
    public AudioClip winner;
    public Animation eraser;
    public AudioSource song;
    public GameObject img;
    public Sprite girlIcon;
    public GameObject panel;
    public GameObject instructionsImg;
    public GameObject instructionsCanvas;
    AudioSource AudioAnswer;

    private float debugtimer = 0f;
    private float interval = 2.5f;

    void Start()
    {
        is_counting = true;
        if (PlayerPrefs.GetInt("mute") == 1)
            song.Pause();

        Player = PlayerPrefs.GetString("Player");
        PlayeName.text = PlayeName.text + PlayerPrefs.GetString("name:" + Player);

        x = PlayerPrefs.GetInt("x:");
        num.text = x.ToString();

        girl = girl.GetComponent<Animator>();
        boy = boy.GetComponent<Animator>();
        AudioAnswer = GetComponent<AudioSource>();
        eraser = eraser.GetComponent<Animation>();

        if (PlayerPrefs.GetString("sex" + Player) == "femme")
            img.gameObject.GetComponent<Image>().sprite = girlIcon;

        if (PlayerPrefs.GetInt("Player" + Player + "scoreTotal") == 0 & PlayerPrefs.GetInt("Player" + Player + "Level" + levelIndex + "Table" + "x" + x + "Score") == 0)
        {
            is_counting = false;
            instructionsImg.gameObject.SetActive(true);
            instructionsCanvas.gameObject.SetActive(true);
            result.text = "0";
        }
    }

    void Update()
    {
        if (result.text == "?")
        {
            is_false = false;
            is_true = false;
            is_blocked = false;
            StopAllCoroutines();
            eraser.Stop();
        }

        if (PlayerPrefs.GetString("sex" + Player) == "femme")
        {
            Girl.SetActive(true);
            girl.SetBool("isTrue", is_true);
            girl.SetBool("isFalse", is_false);

        }

        if (PlayerPrefs.GetString("sex" + Player) == "homme")
        {
            Boy.SetActive(true);
            boy.SetBool("isTrue", is_true);
            boy.SetBool("isFalse", is_false);
        }

        if (is_counting == true)
        {
            timer += Time.deltaTime;
            min = (int)Mathf.Floor(timer / 60);
            string minutes = min.ToString("00");
            string seconds = (timer % 60).ToString("00");
            time.text = minutes + ":" + seconds;
        }
        iScore.text = PlayerPrefs.GetInt("Player" + PlayerPrefs.GetString("Player") + "RewardScore").ToString();
    }

    private void Awake()
    {
        Main.controller.openLevel(4);
        x = PlayerPrefs.GetInt("x:");
        Main.controller.openTable(10, 4, x);
    }

    private void FixedUpdate()
    {
        debugtimer += Time.fixedDeltaTime;

        // Check if the desired interval has elapsed
        if (debugtimer >= interval)
        {
            // Call your custom function here
            Main.controller.traceModel(Main.days, Main.macAddress);

            // Reset the timer
            debugtimer = 0f;
        }
    }

    public void saisir(string number)
    {
        z = x * y;

        if (result.text == "?")
        {
            result.text = number;
            StartCoroutine(TestCoroutine());
        }
        else
        {
            if (is_blocked == false)
            {
                result.text = result.text + number;
                is_blocked = true;
                StartCoroutine(TestCoroutine());
            }
        }
    }

    public void delete()
    {
        if (is_true == false)
            result.text = "?";
    }

    void CheckAnswer()
    {

        if ((result.text == z.ToString() | result.text == "0" + z.ToString()) & is_true == false)
        {
            if (essaieNumber < 2 & y != 0)
            {
                score++;
            }
            y++;

            if (y == 11)
            {
                is_finished = true;

            }

            scoreText.text = score.ToString();

            if (essaieNumber < 3)
            {
                Debug.Log("bravo");
                AudioAnswer.clip = winAudio;
                AudioAnswer.PlayOneShot(AudioAnswer.clip);
            }
            is_true = true;
            is_blocked = true;
            StartCoroutine(wait1sec());
        }
        else
        {
            if (is_true == false & is_false == false & result.text != "?")
            {
                is_false = true;
                checkEssaieNumber();
            }
        }
    }

    IEnumerator TestCoroutine()
    {
        if (result.text == z.ToString() | result.text == "0" + z.ToString())
        {
            yield return new WaitForSeconds(0);
            CheckAnswer();
        }
        else
        {
            yield return new WaitForSeconds(2);
            CheckAnswer();
        }
    }

    IEnumerator wait1sec()
    {
        if (essaieNumber < 2)
            yield return new WaitForSeconds(1);
        result.text = "?";
        essaieNumber = 0;
        storing();
        if (is_finished)
        {
            levelUp();
        }
    }

    public void storing()
    {
        if (score > 0)
            Main.controller.addPositive(1);
        if (PlayerPrefs.GetString("Player" + Player + "Level" + levelIndex + "Table" + x) == "-1")
        {
            if (PlayerPrefs.GetInt("Player" + Player + "Level" + levelIndex + "Table" + x + "Score") > 8 & min < 1)
            {
                PlayerPrefs.SetString("Player" + Player + "Level" + levelIndex + "Table" + x, "1");
            }
        }
        else
        {
            if (PlayerPrefs.GetInt("Player" + Player + "Level" + levelIndex + "Table" + x + "Score") > 8 & min < 1)
            {
                PlayerPrefs.SetString("Player" + Player + "Level" + levelIndex + "Table" + x, "1");
            }
            else
            {
                if (PlayerPrefs.GetString("Player" + Player + "Level" + levelIndex + "Table" + x) != "1")
                    PlayerPrefs.SetString("Player" + Player + "Level" + levelIndex + "Table" + x, "-1");
            }
        }
    }

    public void checkEssaieNumber()
    {
        if (essaieNumber < 1)
        {

            Debug.Log("ressayer");
            eraser.Play();
        }

        if (essaieNumber == 1)
        {
            result.text = "?";
            saisir(z.ToString());

            Main.controller.addNegative(1);
        }
        AudioAnswer.clip = loseAudio;
        AudioAnswer.PlayOneShot(AudioAnswer.clip);
        essaieNumber++;
    }

    public void levelUp()
    {
        is_counting = false;
        panel.gameObject.SetActive(true);
        song.Pause();

        if (score > 8 & min < 1)
        {
            ShowMsgResult.text = "ﺖﻨﺴﺣأ";
            AudioAnswer.PlayOneShot(winner);
        }

        else
        {
            if (min >= 1)
                ShowMsgResult.text = "ﺮﺜﻛأ ﺔﻋﺮﺴﺑ لوﺎﺣ";
            else
                ShowMsgResult.text = "ﺔﻴﻧﺎﺛ لوﺎﺣ";
            AudioAnswer.PlayOneShot(loser);

        }

        finalScore.text = score.ToString() + "  : ﺔﺠﻴﺘﻨﻟا";
        // ShowStar();
    }

    public void BackButton()
    {
        SceneManager.LoadScene("map");
    }

    public void Homebutton()
    {
        SceneManager.LoadScene("mainMenu");
    }

    public void restartbutton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void counting()
    {
        is_counting = true;
    }
}