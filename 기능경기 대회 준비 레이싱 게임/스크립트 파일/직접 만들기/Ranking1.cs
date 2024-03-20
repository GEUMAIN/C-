using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ranking1 : MonoBehaviour
{

    public static Ranking1 instance;

    [SerializeField]
    private Text first_text;
    [SerializeField]
    private Text second_text;
    [SerializeField]
    private Text third_text;

    [SerializeField]
    public static int score;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        PlayerPrefs.DeleteAll();

        PlayerPrefs.SetInt("Rank1", 0);
        PlayerPrefs.SetInt("Rank2", 0);
        PlayerPrefs.SetInt("Rank3", 0);

        first_text.text = "1등 : " + " - " + PlayerPrefs.GetInt("Rank1");
        second_text.text = "2등 : " + " - " + PlayerPrefs.GetInt("Rank2");
        third_text.text = "3등 : " + " - " + PlayerPrefs.GetInt("Rank3");
    }



    public void Insert()
    {
        if (PlayerPrefs.GetInt("Rank1") <= score)
        {
            PlayerPrefs.SetInt("Rank3", PlayerPrefs.GetInt("Rank2"));

            PlayerPrefs.SetInt("Rank2", PlayerPrefs.GetInt("Rank1"));

            PlayerPrefs.SetInt("Rank1", score);
        }
        else if (PlayerPrefs.GetInt("Rank2") <= score)
        {
            PlayerPrefs.SetInt("Rank3", PlayerPrefs.GetInt("Rank2"));

            PlayerPrefs.SetInt("Rank2", score);
        }
        else if (PlayerPrefs.GetInt("Rank3") <= score)
        {
            PlayerPrefs.SetInt("Rank3", score);
        }
    }

    public void Commit()
    {
        first_text.text = "1등 : " +" - " + PlayerPrefs.GetInt("Rank1");
        second_text.text = "2등 : " + " - " + PlayerPrefs.GetInt("Rank2");
        third_text.text = "3등 : " + " - " + PlayerPrefs.GetInt("Rank3");

    }
}
