using System;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.SceneManagement;
using com.glups.Reward;

public class Main : MonoBehaviour
{
    public static string macAddress;
    private static readonly DateTime date = DateTime.Now;
    private static TimeSpan daysSince1900 = date - new DateTime(1900, 1, 1);
    public static readonly int days = (int)daysSince1900.TotalDays + 2;

    private static Main instance;
    public static RewardModel model = new RewardModel();
    public static View view = new View();
    public static Controller controller = new Controller(model, view);

    //public int Level;
    void Start()
    {
        controller.traceModel(days, macAddress);
        print(PlayerPrefs.GetInt("Player" + PlayerPrefs.GetString("Player") + "Rank"));
        Debug.Log(  "ScoreToatal: " + PlayerPrefs.GetInt("Player" + PlayerPrefs.GetString("Player") + "scoreToatal")
                  + " Score: " + PlayerPrefs.GetInt("Player" + PlayerPrefs.GetString("Player") + "scoreTotal")
                  + " iScore: " +  PlayerPrefs.GetInt("Player" + PlayerPrefs.GetString("Player") + "RewardScore")
                  + " Rank: " + model._rank + " RewardCounter: " + PlayerPrefs.GetInt("Player" + PlayerPrefs.GetString("Player") + "RewardCounter"));
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        GetMacAddress();
        //PlayerPrefs.SetInt("Player" + PlayerPrefs.GetString("Player") + "Rank", model._rank);
        controller.openLevel(0);
    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "map")
        {
            Destroy(gameObject);
        }
        PlayerPrefs.SetInt("Player" + PlayerPrefs.GetString("Player") + "Rank", model._rank);
    }
    private void GetMacAddress()
    {
        macAddress = "";
        NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
        foreach (NetworkInterface adapter in nics)
        {
            PhysicalAddress address = adapter.GetPhysicalAddress();
            byte[] bytes = address.GetAddressBytes();
            for (int i = 0; i < bytes.Length; i++)
            {
                macAddress += bytes[i].ToString("X2");
                if (i != bytes.Length - 1)
                {
                    macAddress += ":";
                }
            }
            if (macAddress.Length > 0)
            {
                break;
            }
        }
    }
}