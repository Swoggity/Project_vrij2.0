using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CO : MonoBehaviour
{
    // Code written by Ryan
    // CO is our holy shrine- the game's controller
    int MissionCounter = 0;
    CameraTest cam;
    float startAlpha = 1.0f;
    [SerializeField] float startCamOffset = -7;
    float camOffset = 0;
    [SerializeField] float standardCamOffset = 7;
    [SerializeField] GameObject startScreen; //This screen is the hi-score screen at the beginning of the game
    float fadeAlpha = 1.0f;
    [SerializeField] GameObject fadeScreen; //This screen is used for fade to black and fade to white animations
    float fadeTutorial = 0.0f;
    [SerializeField] GameObject tutorialPop; //This screen tells the player to shoot
    [SerializeField] GameObject player;
    [SerializeField] EnemySpawner spawner;
    bool gamePaused = true;
    float cinematicMoveOverride = 0f;
    int playerScore = 0;
    IEnumerator CountMission()
    {
        while (MissionCounter < 600)
        {
            MissionCounter++;
            yield return new WaitForSeconds(1);
        }

    }
    private void Update()
    {
        if (startScreen == null) return; //If CO is only here for testing, don't do anything
        if (Input.GetMouseButtonUp(0))
        {
            if (startAlpha == 1.0f)
            {
                StartCoroutine(StartMission());
            }
        }
    }

    private void Start()
    {
        gamePaused = true;
        cam = FindObjectOfType<CameraTest>();
        if (startScreen == null)
        {
            //If CO is here for testing, change variables
            gamePaused = false;
        }
    }

    IEnumerator StartMission()
    {
        camOffset = startCamOffset;
        cam.cameraOffset = camOffset;
        PlayerMovement[] Others = FindObjectsOfType<PlayerMovement>();
        List<GameObject> OtherList = new List<GameObject>();
        while (startAlpha > 0.0f)
        {
            startAlpha -= Time.deltaTime*0.5f;
            startScreen.GetComponent<Image>().color = new Color(1, 1, 1, startAlpha);
            yield return null;
        }
        cinematicMoveOverride = 0.4f;
        yield return new WaitForSeconds(1);
        //Propaganda opening HERE
        StartCoroutine(StartMissionCam());
        while (fadeAlpha > 0.0f)
        {
            fadeAlpha -= Time.deltaTime * 0.5f;
            fadeScreen.GetComponent<Image>().color = new Color(0, 0, 0, fadeAlpha);
            yield return null;
        }
        startScreen.SetActive(false);
        fadeScreen.SetActive(false);
        while (fadeAlpha > 0.0f)
        {
            fadeAlpha -= Time.deltaTime * 0.5f;
            fadeScreen.GetComponent<Image>().color = new Color(0, 0, 0, fadeAlpha);
            yield return null;
        }
        while (player.transform.position.x < 45.5f) //Wait until just before protestors, after which others stop following
        {
            yield return null;
        }
        StartCoroutine(StartMissionCam2());
        foreach (PlayerMovement play in Others)
        {
            if (!play.isMainPlayer) { OtherList.Add(play.gameObject); Destroy(play);}
        }
        foreach (PlayerAnimation play in FindObjectsOfType<PlayerAnimation>())
        {
            if (!play.isMainPlayer) {
                play.mAnimator.SetBool("RunningBack", false);
                play.mAnimator.SetBool("Running", false);
                Destroy(play); 
            }
        }
        while (player.transform.position.x < 50) //Wait until in front of protestors: Takes approximately 25 seconds
        {
            yield return null;
        }
        player.GetComponent<PlayerMovement>().leftLimit = 50;
        cinematicMoveOverride = 0f;
        yield return new WaitForSeconds(4);
        tutorialPop.SetActive(true);
        fadeTutorial = 0.0f;
        StartCoroutine(fadeTut());
        while (!Input.GetKey(KeyCode.Space))
        {
            yield return null;
        }
        tutorialPop.SetActive(false);
        player.GetComponent<PlayerShoot>().isFiring = true;
        gamePaused = false;
        foreach (GameObject play in OtherList) //Delete other characters
        {
            Destroy(play);
        }
        spawner.gameObject.SetActive(true);
        StartCoroutine(CountMission()); //End of cinematic: Spawner online and Mission counter begins
    }

    IEnumerator fadeTut()
    {
        while (fadeTutorial < 1 && tutorialPop.activeSelf)
        {
            fadeTutorial += Time.deltaTime * 0.5f;
            tutorialPop.GetComponent<Image>().color = new Color(1, 1, 1, fadeTutorial);
            yield return null;
        }
    }

    IEnumerator StartMissionCam()
    {
        while (camOffset < standardCamOffset-3)
        {
            camOffset += (standardCamOffset-3 - startCamOffset) * Time.deltaTime / 8f;
            cam.cameraOffset = camOffset;
            yield return null;
        }
        cam.cameraOffset = standardCamOffset-3;
    }
    IEnumerator StartMissionCam2()
    {
        while (camOffset < standardCamOffset)
        {
            camOffset += (standardCamOffset - startCamOffset) * Time.deltaTime / 8f;
            cam.cameraOffset = camOffset;
            yield return null;
        }
        cam.cameraOffset = standardCamOffset;
    }

    public bool isGamePaused()
    {
        return gamePaused;
    }

    public float MoveOverride()
    {
        return cinematicMoveOverride;
    }
    public void addScore(int score, Vector3 pos)
    {
        playerScore += score;
        POP Popup = Resources.Load<POP>("POP");
        Vector3 rand = new Vector3(Random.Range(-0.5f,0.5f), Random.Range(-0.5f, 0.5f), 0);
        Instantiate(Popup, pos+rand, transform.rotation).InitPop(score); //Spawn damage popup
    }
}
