using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Target : MonoBehaviour
{

    private float initializationTime;
    private float timeSinceInitialization;

    void Start() {
        initializationTime = Time.timeSinceLevelLoad;
        gameObject.transform.localScale += new Vector3(GameControl.targetSize, GameControl.targetSize, GameControl.targetSize);
    }

    void Update() {
        if (GameControl.gameRunning) {
            timeSinceInitialization = Time.timeSinceLevelLoad - initializationTime;
            if (timeSinceInitialization > 3) {
                GameControl.score -= 20;
                GameControl.playerLife -= 1;
                if (GameControl.targetSize < 1f) {
                    GameControl.targetSize += 0.1f;
                    UnityEngine.Debug.Log("targetSize: " + GameControl.targetSize);
                }
                if (GameControl.timeSpawn < 1f) {
                    GameControl.timeSpawn += 0.08f;
                    UnityEngine.Debug.Log("timeSpawn: " + GameControl.timeSpawn);
                }

                Destroy(gameObject);
            }
        }
    }

    private void OnMouseDown() {
        if (GameControl.gameRunning) {
            GameControl.score += 10;
            GameControl.targetsHit += 1;
            if (GameControl.targetSize > 0.06f) {
                GameControl.targetSize -= 0.1f;
            }
            if (GameControl.timeSpawn > 0.8f) {
                GameControl.timeSpawn -= 0.08f;
            }
            Destroy(gameObject);
        }
    }
}
