using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Unity.VisualScripting.Member;

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
    [SerializeField] GameObject propaScreen;
    float fadeTutorial = 0.0f;
    [SerializeField] GameObject tutorialPop; //This screen tells the player to shoot
    [SerializeField] GameObject player;
    [SerializeField] EnemySpawner spawner;
    [SerializeField] AudioSource OST;
    [SerializeField] GameObject transmissionUI;
    [SerializeField] GameObject ArtilleryPrefab;
    bool isSelfAware = false;
    bool gamePaused = true;
    float cinematicMoveOverride = 0f;
    int playerScore = 0;
    IEnumerator CountMission()
    {
        while (MissionCounter < 600)
        {
            MissionCounter++;
            if (FindObjectsOfType<Enemy>().Length < 2)
            {
                spawner.waveTimer = 0;
            }
            if (MissionCounter == 50) spawner.difficultyLevel++; //About 1.3 minutes in
            if (MissionCounter == 100) spawner.difficultyLevel++; //About 1.9 minutes in
            if (MissionCounter == 150) spawner.difficultyLevel++; //About 2.8 minutes in
            if (MissionCounter == 200) spawner.difficultyLevel++; //About 3.7 minutes in
            if (MissionCounter == 250) spawner.difficultyLevel++; //About 4.6 minutes in
            if (MissionCounter == 5) StartCoroutine(sevenMinutes()); //About 5.5 minutes in =300
            if (MissionCounter == 20) StartCoroutine(nineMinutes()); //About 7.5 minutes in =420
            if (MissionCounter == 450) StartCoroutine(endingArtillery()); //About 8 minutes in
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
        playSound(Resources.Load<AudioClip>("SFX/OST_Title"), false);
        Sprite[] spritlist = { null };
        int Change = 0;
        propaScreen.SetActive(true);

        while (Change < 12)
        {
            Change++;
            propaScreen.GetComponent<Image>().sprite = spritlist[Mathf.FloorToInt(Random.Range(0, spritlist.Length))];
            yield return new WaitForSeconds(0.2f);
        }
        propaScreen.SetActive(false);
        StartCoroutine(StartMissionCam());
        while (fadeAlpha > 0.0f)
        {
            fadeAlpha -= Time.deltaTime * 1f;
            fadeScreen.GetComponent<Image>().color = new Color(1, 1, 1, fadeAlpha);
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
        mainOST(Resources.Load<AudioClip>("SFX/OST_WarIsGood"), 0.8f, true);
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
        player.GetComponent<PlayerMovement>().rightLimit = 150;
        cinematicMoveOverride = 0f;
        yield return new WaitForSeconds(5);
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
        bool firstNot = true;
        foreach (Enemy enem in FindObjectsOfType<Enemy>()) //Make Enemies Run
        {
            if (!firstNot)
            {
                enem.transform.localScale = new Vector3(-1, 1, 1);
                enem.speed = -5;
            }
            firstNot = false;
        }
        foreach (GameObject play in OtherList) //Delete other characters
        {
            Destroy(play);
        }
        spawner.gameObject.SetActive(true);
        StartCoroutine(CountMission()); //End of cinematic: Spawner online and Mission counter begins
        bool MustDelete = true;
        while (MustDelete)
        {
            MustDelete = false;
            foreach (Enemy enem in FindObjectsOfType<Enemy>()) //Make Enemies Run
            {
                if (enem.speed < 0)
                {
                    MustDelete = true;
                    if (enem.transform.position.x > player.transform.position.x+14) Destroy(enem.gameObject);
                }
            }
            yield return new WaitForSeconds(1);
        }
    }
    public void playSound(AudioClip cli, bool loop)
    {
        AudioSource source = GetComponent<AudioSource>();
        source.clip = cli;
        source.volume = 0.8f;
        source.loop = loop;
        source.Play();
    }

    public void setOSTVolume(float volume)
    {
        AudioSource source = GetComponent<AudioSource>();
        source.volume = volume;
    }
    public void setMainOSTVolume(float volume)
    {
        AudioSource source = OST;
        source.volume = volume;
    }
    public void mainOST(AudioClip cli, float volume, bool loop) //Run after creating prefab
    {
        AudioSource source = OST;
        source.clip = cli;
        source.volume = volume;
        source.loop = loop;
        source.Play();
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

    IEnumerator sevenMinutes()
    {
        float Volumos = 0.8f;
        float Pitchos = 1.0f;
        bool startRing = false;
        while (Volumos > 0.2f)
        {
            Volumos -= Time.deltaTime * 0.2f;
            Pitchos -= Time.deltaTime * 0.1f;
            setMainOSTVolume(Volumos);
            OST.pitch = Pitchos;
            if (Volumos < 0.5f && !startRing)
            {
                startRing = true;
                playSound(Resources.Load<AudioClip>("SFX/RingInEar"), false);
            }
        }

        yield return new WaitForSeconds(2f);
        isSelfAware = true;
        while (Volumos < 0.8f) {
            Volumos += Time.deltaTime * 0.2f;
            Pitchos += Time.deltaTime * 0.1f;
            setMainOSTVolume(Volumos);
            OST.pitch = Pitchos;
        }
        setMainOSTVolume(1);
        OST.pitch = 1;
        //Ring in ears
        yield return null;
    }
    IEnumerator nineMinutes()
    {
        //Artillery Announcement
        //Move position of Transmission downward
        while (transmissionUI.transform.localPosition.y > -75)
        {
            transmissionUI.transform.localPosition = new Vector3(transmissionUI.transform.localPosition.x, transmissionUI.transform.localPosition.y+(225*Time.deltaTime*0.5f), 0);
            yield return null;
        }
        //Play Voiceline?
        //Move position of Transmission upward
        while (transmissionUI.transform.localPosition.y < 150)
        {
            transmissionUI.transform.localPosition = new Vector3(transmissionUI.transform.localPosition.x, transmissionUI.transform.localPosition.y - (225 * Time.deltaTime * 0.5f), 0);
            yield return null;
        }
    }
    IEnumerator endingArtillery()
    {
        float Progress = 0f;
        //Ending Artillery
        yield return null;
    }

    public bool selfAware()
    {
        return isSelfAware;
    }
}
