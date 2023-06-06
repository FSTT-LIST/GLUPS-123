using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Clavier3 : MonoBehaviour
{
    public Text result;
    public Text help;
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
    public bool has_help;
    public bool is_muted;
    public int levelIndex;
    public string Player;
    private int essaieNumber;

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
    public GameObject card;
    AudioSource AudioAnswer;

    int Rand;
    public int i = 1;
    int Lenght = 12;
    List<int> list = new List<int>();

    private float timer = 0f;
    private float interval = 2.5f;

    void randomList()
    {
        list = new List<int>(new int[Lenght]);

        for (int j = 1; j < Lenght; j++)
        {
            Rand = Random.Range(1, 12);

            while (list.Contains(Rand))
            {
                Rand = Random.Range(1, 12);
            }
            list[j] = Rand;
            print(list[j]);
        }
        y = list[i] - 1;
    }

    void Start()
    {
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
        randomList();

        if (PlayerPrefs.GetString("sex" + Player) == "femme")
            img.gameObject.GetComponent<Image>().sprite = girlIcon;

        if (PlayerPrefs.GetInt("Player" + Player + "scoreTotal") == 0 & PlayerPrefs.GetInt("Player" + Player + "Level" + levelIndex + "Table" + x + "Score") == 0)
        {
            instructionsImg.gameObject.SetActive(true);
            instructionsCanvas.gameObject.SetActive(true);
            card.gameObject.SetActive(true);
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
        iScore.text = PlayerPrefs.GetInt("Player" + PlayerPrefs.GetString("Player") + "RewardScore").ToString();
    }

    private void Awake()
    {
        Main.controller.openLevel(3);
        x = PlayerPrefs.GetInt("x:");
        Main.controller.openTable(10, 3, x);
    }

    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;

        // Check if the desired interval has elapsed
        if (timer >= interval)
        {
            // Call your custom function here
            Main.controller.traceModel(Main.days, Main.macAddress);

            // Reset the timer
            timer = 0f;
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

        if (is_blocked & has_help)
        {

            helpme(number);
        }
    }

    public void delete()
    {
        if (is_true == false & has_help == false)
            result.text = "?";
    }

    private void helpme(string number)
    {
        if (help.text[0] == number[0]) { result.text = number; }
        if (help.text.Length == 2)
        {
            if (help.text[1] == number[0]) { result.text = result.text + number; }
        }
        if (result.text == help.text)
        {
            StartCoroutine(TestCoroutine());
            help.gameObject.SetActive(false);
        }
    }

    void CheckAnswer()
    {
        if ((result.text == z.ToString() | result.text == "0" + z.ToString()) & is_true == false)
        {

            if (essaieNumber < 2 & y != 0)
            {
                score++;
            }

            i++;

            if (i < 12)
                y = list[i] - 1;

            essaieNumber = 0;

            if (i == 12)
            {


                is_finished = true;

            }

            scoreText.text = score.ToString();

            Debug.Log("bravo");
            AudioAnswer.clip = winAudio;
            AudioAnswer.PlayOneShot(AudioAnswer.clip);

            is_true = true;
            is_blocked = true;
            has_help = false;
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
        yield return new WaitForSeconds(1);

        result.text = "?";
        storing();

        if (is_finished)
        {
            levelUp();
        }
    }

    public void storing()
    {
        if (y != 0)
        {   
            Main.controller.addPositive(1);
        }

        if (PlayerPrefs.GetInt("Player" + Player + "Level" + levelIndex + "Table" + x + "Score") > 8)
        {
            PlayerPrefs.SetString("Player" + Player + "Level" + levelIndex + "Table" + x, "1");
        }
        else
        {
            PlayerPrefs.SetString("Player" + Player + "Level" + levelIndex + "Table" + x, "-1");
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

            result.text = "";
            is_blocked = true;
            help.text = z.ToString();
            help.gameObject.SetActive(true);
            has_help = true;

            Main.controller.addNegative(1);
        }
        AudioAnswer.clip = loseAudio;
        AudioAnswer.PlayOneShot(AudioAnswer.clip);
        essaieNumber++;
    }

    public void levelUp()
    {
        panel.gameObject.SetActive(true);
        song.Pause();
        if (score > 8)
        {
            ShowMsgResult.text = "ﺖﻨﺴﺣأ";
            AudioAnswer.PlayOneShot(winner);
        }
        else
        {
            ShowMsgResult.text = "ﺔﻴﻧﺎﺛ لوﺎﺣ";
            AudioAnswer.PlayOneShot(loser);
        }
        finalScore.text = score.ToString() + "  : ﺔﺠﻴﺘﻨﻟا";
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
}