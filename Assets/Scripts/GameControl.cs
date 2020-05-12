using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    [SerializeField]
    private GameObject target, resultsPanel, playPanel, optionsPanel;

    [SerializeField]
    private Text getReadyText, scoreText, targetsHitText, shotsFiredText, accuracyText, mainPlayerLife, mainScore, mainTargetsRemaining;

    public static int score, targetsHit, playerLife, targetsAmount, targetsSpawned;

    public static float shotsFired, accuracy, targetSize, timeSpawn;

    private Vector2 targetRandomPosition;

    public static Boolean gameRunning;

    public InputField optionNbLife, optionNbTarget;

    void Start() {
        gameRunning     = false;
        targetsAmount   = 50;
        score           = 0;
        shotsFired      = 0;
        targetsHit      = 0;
        accuracy        = 0f;
        playerLife      = 5;
        targetSize      = 1f;
        timeSpawn       = 1f;
        targetsSpawned  = 0;
        mainScore.enabled = false;
        mainPlayerLife.enabled = false;
        mainTargetsRemaining.enabled = false;
        getReadyText.gameObject.SetActive(false);
        resultsPanel.SetActive(false);
        optionsPanel.SetActive(false);
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
            mainScore.text = score.ToString();
            mainPlayerLife.text = playerLife.ToString();
            mainTargetsRemaining.text = targetsHit.ToString() + "/" + targetsAmount.ToString();
        }
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
        scoreText.text = "Score : " + score;
        targetsHitText.text = "Cibles touchés : " + targetsHit + "/" + targetsAmount;
        shotsFiredText.text = shotsFired + " tirs";
        accuracy = targetsHit / shotsFired * 100f;
        accuracyText.text = "Précision : " + accuracy.ToString("N2") + " %";
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
        int integer;
        if (int.TryParse(optionNbLife.text, out integer)) {
            playerLife = integer;
        }
    }

    public void setOptionTarget() {
        int integer;
        if (int.TryParse(optionNbTarget.text, out integer)) {
            targetsAmount = integer;
        }
    }

    public void closeResults() {
        Application.LoadLevel(0);
    }
}

