using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public static gameManager I;

    public GameObject square;
    public GameObject endPanel;
    public Animator anim;
    public Text timeText;
    public Text maxScoreText;
    public Text thisScoreText;
    float alive = 0f;
    bool isRunning = true;

    private void Awake() {
        I = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        InvokeRepeating("makeSquare", 0.0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if(isRunning){
            alive += Time.deltaTime;
            timeText.text = alive.ToString("N2");
        }
        
    }
    void makeSquare(){
        Instantiate(square);
    }

    public void gameOver(){
        isRunning = false;
        anim.SetBool("isDie", true);
        Invoke("timeStop", 0.5f);
        endPanel.SetActive(true);
        thisScoreText.text = alive.ToString("N2");
        if (PlayerPrefs.HasKey("bestScore") == false){
            PlayerPrefs.SetFloat("bestScore", alive);
        }
        else{
            if(alive > PlayerPrefs.GetFloat("bestScore")){
                PlayerPrefs.SetFloat("bestScore", alive);
            }
        }
        float maxScore = PlayerPrefs.GetFloat("bestScore");
        maxScoreText.text = maxScore.ToString("N2");
    }

    public void retry(){
        SceneManager.LoadScene("MainScene");
    }

    void timeStop(){
        Time.timeScale = 0.0f;
    }
}
