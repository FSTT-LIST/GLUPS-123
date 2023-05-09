using UnityEngine;
using System.IO;
using System.Net.NetworkInformation;
using System;

public class StatsController2 : MonoBehaviour
{
    private string filePath;
    private int scoreTotal;
    private int scoreLevel;
    private string macAddress;
    private string Player;
    public clavier2 clavier;

    private void Awake()
    {
        filePath = Path.Combine(Application.persistentDataPath, "data.csv");
    }

    private void Start()
    {
        Player = PlayerPrefs.GetString("Player");

        // Call the function to write statistics in the CSV file and pass the current date/time and "S" (start) as parameters.
        WriteToFile(DateTime.Now, "S");
    }


    private void OnDestroy()
    {
        // Call the function to write statistics in the CSV file and pass the current date/time and "E" (end) as parameters.
        WriteToFile(DateTime.Now, "E");
    }



    // Function to retrieve Mac Address
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


    // Function to write statistics in the CSV file with two parameters
    private void WriteToFile(DateTime date, string inputOutput)
    {
        string id = PlayerPrefs.GetString("Player");
        string playerNom = PlayerPrefs.GetString("name:" + id);
        string level = PlayerPrefs.GetString("level");

        GetMacAddress();


        // Get the new score after playing
        CalculateTotalScore();


        // wrong answers = total questions - correct answers ⚠⚠⚠ < cause a problem when a player replay a multiplication table >
        int wrongAnswers = 10 - clavier.score;


        // Calculate the number of days since January 1, 1900
        TimeSpan daysSince1900 = date - new DateTime(1900, 1, 1);
        int days = (int)daysSince1900.TotalDays + 1;


        // Get the level score
        CalculateLevelScore();


        // Open or create the file for writing
        StreamWriter fileWriter = new StreamWriter(new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite));

        // Write the data in the CSV file
        string csvData = string.Format("{0}; {1}; {2}; {3}.{4}.{5}; {6}; {7}; {8}; {9}; {10}; {11}",
            days, inputOutput, Application.version, macAddress, id, playerNom, "FRS", level,
            inputOutput == "S" ? 0 : clavier.score,
            inputOutput == "S" ? 0 : wrongAnswers,
            scoreLevel, scoreTotal);
        fileWriter.WriteLine(csvData);

        // Close the file
        fileWriter.Close();
    }



    // Function to retrieve current Total score
    private void CalculateTotalScore()
    {
        scoreTotal = 0;
        for (int i = 1; i < 5; i++)
        {
            for (int j = 1; j < 10; j++)
            {
                scoreTotal = scoreTotal + PlayerPrefs.GetInt("Player" + PlayerPrefs.GetString("Player") + "lev" + i + "x" + j + "score");
            }
        }
    }

    // Function to retrieve current Level score
    public void CalculateLevelScore()
    {

        int lev = int.Parse(PlayerPrefs.GetString("level")[5].ToString());
        scoreLevel = 0;

        for (int i = 0; i < 9; i++)
        {
            int j = i + 1;
            scoreLevel += PlayerPrefs.GetInt("Player" + Player + "lev" + lev + "x" + j + "score");
        }

    }

}