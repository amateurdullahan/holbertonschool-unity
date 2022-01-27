using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
    public static ARPlane plane = null;
    public GameObject UI;
    public int targets = 5;
    public int ammoCount = 7;
    public int score = 0;
    public GameObject targetPrefab;
    public GameObject ammoPrefab;
    public Text scoreText;
    public Text ammoText;
    public GameObject playAgain;
    public GameObject leaderboard;
    public Text leaderboardText;

    private ARRaycastManager rayMan;
    private ARPlaneManager planeMan;
    private List<ARRaycastHit> hitResults = new List<ARRaycastHit>();
    private GameObject ammo;
    private List<int> hiscores = new List<int>();
    private bool complete = false;

    // Start is called before the first frame update
    void Start()
    {
        rayMan = GetComponent<ARRaycastManager>();
        planeMan = GetComponent<ARPlaneManager>();
        hiscores.Add(PlayerPrefs.GetInt("first", 0));
        hiscores.Add(PlayerPrefs.GetInt("second", 0));
        hiscores.Add(PlayerPrefs.GetInt("third", 0));
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = $"Score: {score}";
        ammoText.text = $"Ammo: {ammoCount}";

        if ((ammoCount == 0 || score == 10 * targets) && !complete) {
            CompleteGame();
            complete = true;
        }

        if (Input.touchCount == 0)
            return;
            
        if (rayMan.Raycast(Input.GetTouch(0).position, hitResults) && plane == null) {
            plane = planeMan.GetPlane(hitResults[0].trackableId);

            foreach (ARPlane p in planeMan.trackables) {
                if (p != plane)
                    p.gameObject.SetActive(false);
            }

            planeMan.enabled = false;

            UI.SetActive(true);
            
            for (int i = 0; i < targets; i++) {
                GameObject t = Instantiate(targetPrefab, plane.center + new Vector3(0, 0.05f, 0), Quaternion.identity);
                t.transform.parent = plane.transform;
            }
        }
    }

    public void StartGame() {
        ammo = Instantiate(ammoPrefab);
    }

    public void Restart() {
        SceneManager.LoadScene(0);
    }

    public void Exit() {
        Application.Quit();
    }

    public void Reset() {
        ammoCount = 7;
        score = 0;
        foreach (TargetController target in Resources.FindObjectsOfTypeAll<TargetController>()) {
            target.gameObject.SetActive(true);
        }
        Invoke("SpawnAmmo", 1.0f);
        complete = false;
    }

    private void SpawnAmmo() {
        ammo = Instantiate(ammoPrefab);
    }

    private void CompleteGame() {
        Destroy(ammo);
        playAgain.SetActive(true);

        for (int i = 0; i < 3; i++) {
            if (score > hiscores[i]) {
                hiscores.Insert(i, score);
                hiscores.RemoveAt(3);
                break;
            }
        }

        PlayerPrefs.SetInt("first", hiscores[0]);
        PlayerPrefs.SetInt("second", hiscores[1]);
        PlayerPrefs.SetInt("third", hiscores[2]);

        leaderboardText.text = $"Your Score: {score}\n\n1st - {hiscores[0]}\n\n2nd - {hiscores[1]}\n\n3rd - {hiscores[2]}";
        leaderboard.SetActive(true);
    }
}
