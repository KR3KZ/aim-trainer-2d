using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using MySql.Data.MySqlClient;

public class GameControl : MonoBehaviour
{
    Database myDb = new Database("127.0.0.1", "aim-trainer-2d", "root", "");

    [SerializeField]
    private GameObject target, resultsPanel, playPanel, optionsPanel, scoresPanel;

    [SerializeField]
    private Text getReadyText, scoreText, targetsHitText, shotsFiredText, accuracyText, mainPlayerLife, mainScore, mainTargetsRemaining, scoresText;

    public static int score, playerLife, targetsAmount, targetsSpawned;

    public static float accuracy, targetSize, timeSpawn, targetsHit, shotsFired;

    private Vector2 targetRandomPosition;

    public static Boolean gameRunning;

    public InputField optionNbLife, optionNbTarget;

    void Start() {
        gameRunning     = false;
        targetsAmount   = 50;
        score           = 0;
        shotsFired      = 0f;
        targetsHit      = 0f;
        accuracy        = 0f;
        playerLife      = 5;
        targetSize      = 1f;
        timeSpawn       = 1f;
        targetsSpawned  = 0;
        mainScore.enabled               = false;
        mainPlayerLife.enabled          = false;
        mainTargetsRemaining.enabled    = false;
        getReadyText.gameObject.SetActive(false);
        resultsPanel.SetActive(false);
        optionsPanel.SetActive(false);
        updateScoresPanel();
    }

    void Update() {
        if (gameRunning) {
            if (Input.GetMouseButtonDown(0)) {
                shotsFired += 1f;
            }
            if (playerLife < 1) {
                gameRunning = false;
                EndGameAndShowResults();
            }
            //Update main screen
            mainScore.text              = score.ToString();
            mainPlayerLife.text         = playerLife.ToString();
            mainTargetsRemaining.text   = targetsHit.ToString() + "/" + targetsAmount.ToString();
        }
    }

    public void updateScoresPanel() {
        List<object[]> dataList = myDb.getScore();
        string score = "";
        for (int i = dataList.Count; i > 0; i--) {
            UnityEngine.Debug.Log("data: " + dataList[i - 1][1]);
            score += dataList[i - 1][1] + "\n";
        }
        scoresText.text = score;
    }

    private IEnumerator GetReady() {
        for (int i = 3; i >= 1; i--) {
            getReadyText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        getReadyText.text = "Go !";
        yield return new WaitForSeconds(1f);
        getReadyText.gameObject.SetActive(false);

        mainScore.enabled = true;
        mainPlayerLife.enabled = true;
        mainTargetsRemaining.enabled = true;

        StartCoroutine("SpawnTargets");
    }

    private IEnumerator SpawnTargets() {
        gameRunning = true;
        for (int i = targetsAmount; i > 0; i--) {
            if (gameRunning && targetsSpawned < targetsAmount) {
                targetRandomPosition = new Vector2(UnityEngine.Random.Range(-7f, 7f), UnityEngine.Random.Range(-4f, 4f));
                Instantiate(target, targetRandomPosition, Quaternion.identity);
                targetsSpawned++;
                yield return new WaitForSeconds(timeSpawn);
            }
        }
        yield return new WaitForSeconds(2);
        EndGameAndShowResults();
    }

    private void EndGameAndShowResults() {
        resultsPanel.SetActive(true);
        scoreText.text      = "Score : " + score;
        targetsHitText.text = "Cibles touchés : " + targetsHit + "/" + targetsAmount;
        shotsFiredText.text = shotsFired + " tirs";
        accuracy            = calculcateAccuracy(targetsHit, shotsFired);
        accuracyText.text   = "Précision : " + accuracy.ToString("N2") + " %";
    }

    public float calculcateAccuracy(float targetsHit, float shotsFired) {
        return targetsHit / shotsFired * 100f;
    }

    public void StartGetReadyCoroutine() {
        playPanel.SetActive(false);
        getReadyText.gameObject.SetActive(true);
        StartCoroutine("GetReady");
    }

    public void showOptionsPanel() {
        playPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void hideOptionsPanel() {
        optionsPanel.SetActive(false);
        playPanel.SetActive(true);
    }

    public void quitGame() {
        Application.Quit();
    }

    public void setOptionLife() {
        int life;
        if (int.TryParse(optionNbLife.text, out life)) {
            playerLife = life;
        }
    }

    public void setOptionTarget() {
        int target;
        if (int.TryParse(optionNbTarget.text, out target)) {
            targetsAmount = target;
        }
    }

    public void closeResults() {
        Application.LoadLevel(0);
    }

    static public void onTargetClicked() {
        score += 10;
        targetsHit += 1f;
    }

    static public void onTargetUnspawned() {
        score -= 20;
        playerLife -= 1;
    }
}

public class Database
{
    private string server;
    private string databaseName;
    private string user;
    private readonly string pwd;
    public MySqlConnection ctx;

    public Database(string server, string databaseName, string user, string pwd) {
        this.server = server;
        this.databaseName = databaseName;
        this.user = user;
        this.pwd = pwd;
        this.ctx = new MySqlConnection("SERVER=" + server + ";" + "DATABASE=" + databaseName + ";" + "UID=" + user + ";" + "PASSWORD=" + pwd + ";");
    }

    public List<object[]> getScore() {
        this.ctx.Open();
        string query = "SELECT * FROM score ORDER BY id LIMIT 10";
        MySqlCommand cmd = new MySqlCommand(query, this.ctx);
        MySqlDataReader dataReader = cmd.ExecuteReader();
        List<object[]> dataList = new List<object[]>();
        while (dataReader.Read()) {
            object[] tempRow = new object[dataReader.FieldCount];
            for (int i = 0; i < dataReader.FieldCount; i++) {
                tempRow[i] = dataReader[i];
            }
            dataList.Add(tempRow);
        }
        this.ctx.Close();
        return dataList;
    }
}