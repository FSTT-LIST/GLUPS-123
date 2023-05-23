using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class levelSelection : MonoBehaviour
{
    public GameObject[] BarFill1;
    public GameObject[] BarFill2;
    public GameObject[] BarFill3;
    public GameObject[] BarFill4;

    public Text levelText;
    public string level;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("x:") == 0)
        {
            PlayerPrefs.SetInt("x:", 1);
        }
        levelText.text = level;
    }

    // Update is called once per frame
    void Update()
    {
        //PlayerPrefs used to Store and accesses player preferences between game sessions.
        storing();
    }

    // we call this function to show the BarFill that the actual player has
    private void storing()
    {
        int[,] levelTableScores = new int[4, 9]; // 4 levels, 9 scores per level
        int[] levelScores = new int[4];
        int[] levelCounts = new int[4]; // 4 levels

        // Retrieve level scores
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                levelTableScores[i, j] = PlayerPrefs.GetInt("Player" + PlayerPrefs.GetString("Player") + "lev" + (i + 1) + "x" + (j + 1) + "score");
            }
        }

        // Calculate level counts
        for (int i = 0; i < 4; i++)
        {
            int levelScore = 0;
            for (int j = 0; j < 9; j++)
            {
                levelScore += levelTableScores[i, j];
            }
            levelScores[i] = levelScore;
            levelCounts[i] = (levelScore / 30) + 1;
        }

        // Update score bars
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                int count = levelCounts[i];
                if (j < count)
                {
                    if (i == 0)
                    {
                        BarFill1[j].SetActive(true);
                    }
                    else if (i == 1)
                    {
                        BarFill2[j].SetActive(true);
                    }
                    else if (i == 2)
                    {
                        BarFill3[j].SetActive(true);
                    }
                    else if (i == 3)
                    {
                        BarFill4[j].SetActive(true);
                    }
                }
                else
                {
                    if (i == 0)
                    {
                        BarFill1[j].SetActive(false);
                    }
                    else if (i == 1)
                    {
                        BarFill2[j].SetActive(false);
                    }
                    else if (i == 2)
                    {
                        BarFill3[j].SetActive(false);
                    }
                    else if (i == 3)
                    {
                        BarFill4[j].SetActive(false);
                    }
                }
            }

            // Special case for the first score bar, which should always be visible if there is a non-zero score
            if (levelScores[0] == 0)
                BarFill1[0].SetActive(false);
            if (levelScores[1] == 0)
                BarFill2[0].SetActive(false);
            if (levelScores[2] == 0)
                BarFill3[0].SetActive(false);
            if (levelScores[3] == 0)
                BarFill4[0].SetActive(false);
        }

    }

    public void pressSelection(string levelName)
    {
        PlayerPrefs.SetString("level", levelName);
        levelText.text = level;
    }

    public void BackButton()
    {
        SceneManager.LoadSceneAsync("mainMenu");
    }

    public void PrizeSceneButton()
    {
        SceneManager.LoadSceneAsync("Prizes");
    }

    public void choisirX(int x)
    {
        // x is the number of the table selected
        PlayerPrefs.SetInt("x:", x);
        SceneManager.LoadScene(PlayerPrefs.GetString("level"));
    }
}