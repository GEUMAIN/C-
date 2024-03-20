using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSystem : MonoBehaviour
{
    [SerializeField]
    public static float firstendscore;
    [SerializeField]
    public static float firstscore;
    public float addscore;
    [SerializeField]
    GameObject rankingstate;
    [SerializeField]
    GameObject ranktext;
    [SerializeField]
    private Text endtext;

    public Ranking1 ranking1;
    public Roaded roaded;

    private void Awake()
    {
       
    }
    private void Start()
    {
        firstendscore = 60;
        ranking1 = GetComponent<Ranking1>();
    }

    private void Update()
    {
        addscore = Enemy.i;
        firstscore = firstscore + Time.deltaTime;
        endtext.text = "³¡" + ":" + firstendscore;

        if (firstscore >= firstendscore)
        {
            SceneManager.LoadScene("");
            firstendscore = firstscore * 100 - (5000+addscore);
            Ranking1.score = (int)firstendscore;
            rankingstate.layer = 7;
            ranktext.SetActive(true);
            Ranking1.instance.Insert();
            Ranking1.instance.Commit();

            Invoke("Remove", 3f);
        }

    }

    public void Remove()
    {
        roaded.enabled = false;
    }
}
