using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public AudioSource theMusic;

    public bool startPlaying;

    public BeatScroller theBS;

    public static GameManager instance;

    public int currentScore;
    public int scorePerNote = 100;
    public int scorePerGoodNote = 125;
    public int scorePerPerfectNote = 150;

    public int currentMultiplier;
    public int multiplierTracker;
    public int[] multiplierThresholds; 

    public Text scoreText;
    public Text multiText;

    public float totalNotes;
    public float normalHits;
    public float goodHits;
    public float perfectHits;
    public int missedHits;

    public GameObject resultsScreen;
    public Text percentHitText, normalsText, goodsText, perfectsText, missesText, rankText, finalScoreText;

    public GameObject failText;

    public Animator dancer; //аниматор танцора
    public Animator monster; //аниматор монстра

    public Image scrollBar;
    public GameObject monstrSymbol;

    void Start()
    {
        //Time.timeScale = 0.2f;
        instance = this;

        scoreText.text = "Score: 0";
        currentMultiplier = 1;

        totalNotes = FindObjectsOfType<NoteObject>().Length;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (!startPlaying)
        {
            if (Input.anyKeyDown)
            {
                startPlaying = true;
                theBS.hasStarted = true;

                theMusic.Play();
            }
        } else
        {
            if(!theMusic.isPlaying && !resultsScreen.activeInHierarchy)
            {

                resultsScreen.SetActive(true);

                normalsText.text = "" + normalHits;
                goodsText.text = goodHits.ToString();
                perfectsText.text = perfectHits.ToString();
                missesText.text = "" + missedHits;

                float totalHit = normalHits + goodHits + perfectHits;
                float percentHit = (totalHit / totalNotes) * 100f;

                percentHitText.text = percentHit.ToString("F1") + "%";

                string rankVal = "F";

                if (percentHit > 40)
                {
                    rankVal = "D";
                    if (percentHit > 55)
                    {
                        rankVal = "C";
                        if (percentHit > 70)
                        {
                            rankVal = "B";
                            if (percentHit > 85)
                            {
                                rankVal = "A";
                                if (percentHit > 95)
                                {
                                    rankVal = "S";
                                }
                            }
                        }
                    }
                }

                rankText.text = rankVal;

                finalScoreText.text = currentScore.ToString();
            }
        }

        monster.SetInteger("Misses", missedHits); //приравнивает int в аниматоре к intу в геймменеджере

        if (missedHits >= 6)
        {
            failText.SetActive(true);
            theBS.hasStarted = false;
            StartCoroutine(waito());
        }
    }
    public void NoteHit()
    {
        Debug.Log("Hit On Time");

        if (currentMultiplier - 1 < multiplierThresholds.Length)
        {
            multiplierTracker++;

            if (multiplierThresholds[currentMultiplier - 1] <= multiplierTracker)
            {
                multiplierTracker = 0;
                currentMultiplier++;
            }
        }

        multiText.text = "Multiplier: x" + currentMultiplier;

        //currentScore += scorePerNote * currentMultiplier;
        scoreText.text = "Score: " + currentScore;
    }

    public void NormalHit()
    {
        currentScore += scorePerNote * currentMultiplier;
        NoteHit();

        normalHits++;
    }

    public void GoodHit()
    {
        currentScore += scorePerGoodNote * currentMultiplier;
        NoteHit();
        goodHits++;
    }

    public void PerfectHit()
    {
        currentScore += scorePerPerfectNote * currentMultiplier;
        NoteHit();
        perfectHits++;
    }

    public void NoteMissed()
    {
        Debug.Log("Missed Note");

        currentMultiplier = 1;
        multiplierTracker = 0;

        multiText.text = "Multiplier: x" + currentMultiplier;

        missedHits++;

        Debug.Log((6 - missedHits) * 0.16f);

        float cool = ((6 - missedHits) * 0.16f) + 0.32f;

        scrollBar.fillAmount = (6 - missedHits) * 0.16f;
        monstrSymbol.transform.position = Vector2.Lerp(new Vector2(-11.14f, 9.49f), new Vector2(-6.88f, 9.49f), cool);
    }

    IEnumerator waito()
    {
        yield return new WaitForSeconds(2f);
        End();
    }

    public void End()
    {
        resultsScreen.SetActive(true);

        normalsText.text = "" + normalHits;
        goodsText.text = goodHits.ToString();
        perfectsText.text = perfectHits.ToString();
        missesText.text = "" + missedHits;

        float totalHit = normalHits + goodHits + perfectHits;
        float percentHit = (totalHit / totalNotes) * 100f;

        percentHitText.text = percentHit.ToString("F1") + "%";

        string rankVal = "F";

        if (percentHit > 40)
        {
            rankVal = "D";
            if (percentHit > 55)
            {
                rankVal = "C";
                if (percentHit > 70)
                {
                    rankVal = "B";
                    if (percentHit > 85)
                    {
                        rankVal = "A";
                        if (percentHit > 95)
                        {
                            rankVal = "S";
                        }
                    }
                }
            }
        }

        rankText.text = rankVal;

        finalScoreText.text = currentScore.ToString();
    }
}
