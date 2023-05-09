using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
public class PrizeSceneController : MonoBehaviour
{
    private string Player;
    private string TotalScore;
    public Text PlayerText;
    public Text ScoreText;
    public Text PuzzleNumber;
    public GameObject GirlBoyIcon;
    public Sprite girlIcon;
    public GameObject[] RankProgressBarFill = new GameObject[12];
    public GameObject[] puzzles;
    public int[][] ordre = new int[4][];
    public int[] ordre1;
    public int[] ordre2;
    public int[] ordre3;
    public int[] ordre4;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Player = PlayerPrefs.GetString("Player");
        TotalScore = PlayerPrefs.GetInt("Player" + Player + "scoreToatal").ToString();

        PlayerText.text = PlayerPrefs.GetString("name:" + Player).ToString();
        ScoreText.text = TotalScore;

        if (PlayerPrefs.GetString("sex" + Player) == "femme")
            GirlBoyIcon.GetComponent<Image>().sprite = girlIcon;

        ordre[0] = ordre1;
        ordre[1] = ordre2;
        ordre[2] = ordre3;
        ordre[3] = ordre4;

        RankProgressBar();
        ShowPuzzle();
        UpdatePuzzleNumber();
    }

    private void RankProgressBar()
    {
        int totalScore = PlayerPrefs.GetInt("Player" + Player + "scoreToatal");

        for (int i = 0; i < RankProgressBarFill.Length; i += 2)
        {
            if (totalScore >= GetRankScore(i))
            {
                RankProgressBarFill[i].SetActive(true);
                RankProgressBarFill[i + 1].SetActive(true);
            }
            else
            {
                RankProgressBarFill[i].SetActive(false);
                RankProgressBarFill[i + 1].SetActive(false);
            }
        }
    }

    private int GetRankScore(int rankIndex)
    {
        switch (rankIndex)
        {
            case 0:
                return 0;
            case 2:
                return 50;
            case 4:
                return 120;
            case 6:
                return 200;
            case 8:
                return 320;
            case 10:
                return 360;
            default:
                return 0;
        }
    }

    public void ShowPuzzle()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                puzzles[i].transform.GetChild(j).gameObject.SetActive(false);
            }

        }

        CheckPuzzle();
    }

    public void CheckPuzzle()
    {
        int x = int.Parse(TotalScore) / 10;

        if (int.Parse(TotalScore) >= 10 && int.Parse(TotalScore) < 91)
        {
            for (int i = 0; i < x; i++)
            {
                puzzles[0].transform.GetChild(ordre[0][i]).gameObject.SetActive(true);
            }
        }
        else if (int.Parse(TotalScore) >= 100 && int.Parse(TotalScore) < 181)
        {
            x = x - 9;
            for (int i = 0; i < 9; i++)
            {
                puzzles[0].transform.GetChild(ordre[0][i]).gameObject.SetActive(true);
            }
            for (int j = 0; j < x; j++)
            {
                puzzles[1].transform.GetChild(ordre[1][j]).gameObject.SetActive(true);
            }
        }
        else if (int.Parse(TotalScore) >= 190 && int.Parse(TotalScore) < 271)
        {
            x = x - 18;
            for (int i = 0; i < 9; i++)
            {
                puzzles[0].transform.GetChild(ordre[0][i]).gameObject.SetActive(true);
            }
            for (int j = 0; j < 9; j++)
            {
                puzzles[1].transform.GetChild(ordre[1][j]).gameObject.SetActive(true);
            }
            for (int j = 0; j < x; j++)
            {
                puzzles[2].transform.GetChild(ordre[2][j]).gameObject.SetActive(true);
            }
        }
        else if (int.Parse(TotalScore) >= 280 && int.Parse(TotalScore) < 361)
        {
            x = x - 27;
            for (int i = 0; i < 9; i++)
            {
                puzzles[0].transform.GetChild(ordre[0][i]).gameObject.SetActive(true);
            }
            for (int i = 0; i < 9; i++)
            {
                puzzles[1].transform.GetChild(ordre[1][i]).gameObject.SetActive(true);
            }
            for (int i = 0; i < 9; i++)
            {
                puzzles[2].transform.GetChild(ordre[2][i]).gameObject.SetActive(true);
            }
            for (int i = 0; i < x; i++)
            {
                puzzles[3].transform.GetChild(ordre[3][i]).gameObject.SetActive(true);
            }
        }
    }

    public void NextPuzzleButton()
    {
        for (int i = 0; i < puzzles.Length; i++)
        {
            if (puzzles[i].activeSelf)
            {
                puzzles[i].SetActive(false);

                if (i != 3)
                {
                    puzzles[i + 1].SetActive(true);
                }
                else
                {
                    puzzles[0].SetActive(true);
                }
                break;
            }
        }
    }

    public void PreviousPuzzleButton()
    {
        for (int i = 0; i < puzzles.Length; i++)
        {
            if (puzzles[i].activeSelf)
            {
                puzzles[i].SetActive(false);

                if (i != 0)
                {
                    puzzles[i - 1].SetActive(true);
                }
                else
                {
                    puzzles[3].SetActive(true);
                }
                break;
            }
        }
    }

    public void UpdatePuzzleNumber()
    {
        for (int i = 0; i < puzzles.Length; i++)
        {
            if (puzzles[i].activeSelf)
            {
                PuzzleNumber.text = (i+1).ToString(); 
            }
        }
    }

    public void ShareDataFile()
    {
        // Get the path to the data file
        string filePath = Path.Combine(Application.persistentDataPath, "data.csv");

        // Make sure that the file exists
        if (!File.Exists(filePath))
        {
            Debug.LogError("File does not exist: " + filePath);
            return;
        }

        // Create a new intent to share the file
        AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
        AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
        intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
        intentObject.Call<AndroidJavaObject>("setType", "text/csv");

        // Get the URI for the file
        AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
        AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "file://" + filePath);
        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);

        // Start the activity to share the file
        AndroidJavaClass unityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unityClass.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject chooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, "Share File");
        currentActivity.Call("startActivity", chooser);
    }
}