using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public GameObject player;
    [SerializeField] EnemySpawner spawner;
    [SerializeField] AudioSource OST;
    [SerializeField] GameObject transmissionUI;
    Vector3 transmissionAnchor;
    [SerializeField] TextMeshProUGUI scoreTexto;
    [SerializeField] TextMeshProUGUI scoreNum;
    [SerializeField] TextMeshProUGUI endingText;
    [SerializeField] TextMeshProUGUI tutorialPopText;
    [SerializeField] GameObject Ability1;
    [SerializeField] GameObject Ability2;
    bool isSelfAware = false;
    public bool becomeAlly = false;
    bool gamePaused = true;
    float cinematicMoveOverride = 0f;
    int playerScore = 0;
    public int abilitiesUnlocked = 0;
    float scoreMulti = 1.0f;
    float idleTime = 0f;
    IEnumerator CountMission()
    {
        while (MissionCounter < 600)
        {
            MissionCounter++;
            if (FindObjectsOfType<Enemy>().Length < 2)
            {
                spawner.waveTimer = 0;
            }
            if (!OST.isPlaying)
            {
                mainOST(Resources.Load<AudioClip>("SFX/OST_Destroyer"), 0.8f, false);
            }
            if (MissionCounter == 2) StartCoroutine(tutorialMove());
            if (MissionCounter == 1) { StartCoroutine(tutorialAbility()); abilitiesUnlocked++; Ability1.SetActive(true); } //20
            if (MissionCounter == 30) spawner.difficultyLevel++; //About 1.3 minutes in
            if (MissionCounter == 3) { abilitiesUnlocked++; Ability2.SetActive(true); } //60
            if (MissionCounter == 70) spawner.difficultyLevel++; //About X minutes in
            if (MissionCounter == 100) spawner.difficultyLevel++; //About 1.9 minutes in
            if (MissionCounter == 150) spawner.difficultyLevel++; //About 2.8 minutes in
            if (MissionCounter == 180) spawner.difficultyLevel++; //About 3.7 minutes in
            if (MissionCounter == 210) spawner.difficultyLevel++; //About X minutes in
            if (MissionCounter == 250) spawner.difficultyLevel++; //About 4.6 minutes in
            if (MissionCounter == 300) StartCoroutine(sevenMinutes()); //About 5.5 minutes in =300
            if (MissionCounter == 420) StartCoroutine(nineMinutes()); //About 7.5 minutes in =420
            if (MissionCounter == 450) StartCoroutine(endingArtillery()); //About 8 minutes in =450
            yield return new WaitForSeconds(1);
        }

    }
    private void Update()
    {
        if (startScreen == null) return; //If CO is only here for testing, don't do anything
        if (Input.anyKeyDown)
        {
            if (startAlpha == 1.0f)
            {
                StartCoroutine(StartMission());
            }
        }
        if (scoreMulti > 1.0f)
        {
            scoreMulti -= Time.deltaTime*0.6f;
            if (scoreMulti > 4f) scoreMulti -= Time.deltaTime * 0.6f;
        }
    }

    private void Start()
    {
        gamePaused = true;
        cam = FindObjectOfType<CameraTest>();
        transmissionAnchor = transmissionUI.transform.position;
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
        cinematicMoveOverride = 0.37f;
        yield return new WaitForSeconds(1);
        //Propaganda opening HERE
        Sprite[] spritlist = {
            Resources.Load<Sprite>("Sprites/Propa1"),
            Resources.Load<Sprite>("Sprites/Propa2"),
            Resources.Load<Sprite>("Sprites/Propa3"),
            Resources.Load<Sprite>("Sprites/Propa4")
        };
        int Change = 0;
        propaScreen.SetActive(true);
        int Previous = 0;

        while (Change < 20)
        {
            yield return new WaitForSeconds(0.1f);
            Change++;
            playSoundSoft(Resources.Load<AudioClip>("SFX/MG_BEEFY_SHOT_"+Mathf.FloorToInt(Random.Range(1,7)).ToString()), false);
            int ren = Mathf.FloorToInt(Random.Range(0, spritlist.Length));
            if (ren == Previous) ren++;
            if (ren >= spritlist.Length) ren = 0;
            Previous = ren;
            propaScreen.GetComponent<Image>().sprite = spritlist[ren];
        }
        playSound(Resources.Load<AudioClip>("SFX/OST_Title"), false);
        propaScreen.SetActive(false);
        yield return new WaitForSeconds(2.5f);
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
        mainOST(Resources.Load<AudioClip>("SFX/OST_WarIsGood"), 0.8f, false);
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
        player.GetComponent<PlayerMovement>().rightLimit = 350;
        cinematicMoveOverride = 0f;
        yield return new WaitForSeconds(5);
        tutorialPop.SetActive(true);
        fadeTutorial = 1.0f;
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
    IEnumerator tutorialMove()
    {
        tutorialPop.SetActive(true);
        tutorialPopText.text = "Press Z and C to move!";
        while (!Input.GetKeyDown(KeyCode.Z) && !Input.GetKeyDown(KeyCode.C) && MissionCounter < 20)
        {
            yield return null;
        }
        tutorialPop.SetActive(false);
        if (MissionCounter > 10) MissionCounter = 10;
    }
    IEnumerator tutorialAbility()
    {
        tutorialPop.SetActive(true);
        tutorialPopText.text = "Press (1) for ability!";
        while (!Input.GetKeyDown(KeyCode.Alpha1) && MissionCounter < 80)
        {
            yield return null;
        }
        tutorialPop.SetActive(false);
    }
    public void playSound(AudioClip cli, bool loop)
    {
        AudioSource source = GetComponent<AudioSource>();
        source.clip = cli;
        source.volume = 0.8f;
        source.loop = loop;
        source.Play();
    }
    public void playSoundSoft(AudioClip cli, bool loop)
    {
        AudioSource source = GetComponent<AudioSource>();
        source.clip = cli;
        source.volume = 0.5f;
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
        fadeTutorial = 0f;
        while (fadeTutorial < 1 && tutorialPop.activeSelf)
        {
            fadeTutorial += Time.deltaTime * 0.5f;
            tutorialPop.GetComponent<Image>().color = new Color(1, 1, 1, fadeTutorial);
            yield return null;
        }
        tutorialPop.GetComponent<Image>().color = new Color(1, 1, 1, 1);
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
        score = Mathf.RoundToInt(score*scoreMulti);
        playerScore += score;
        POP Popup = Resources.Load<POP>("POP");
        Vector3 rand = new Vector3(Random.Range(-0.5f,0.5f), Random.Range(-0.5f, 0.5f), 0);
        Instantiate(Popup, pos+rand, transform.rotation).InitPop(score); //Spawn damage popup
        scoreNum.text = playerScore.ToString("000000000");
        if (scoreMulti > 5f) scoreMulti += 0.2f;
        else scoreMulti += 1f;
    }
    public void loseScore(int score, Vector3 pos)
    {
        playerScore -= score / Mathf.RoundToInt(scoreMulti);
        POP Popup = Resources.Load<POP>("POP");
        Vector3 rand = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);
        Instantiate(Popup, pos + rand, transform.rotation).InitPop(score); //Spawn damage popup
        scoreNum.text = playerScore.ToString("000000000");
    }

    IEnumerator sevenMinutes()
    {
        float Volumos = 0.8f;
        float Pitchos = 1.0f;
        bool startRing = false;
        Image fadeim = fadeScreen.GetComponent<Image>();
        fadeim.sprite = Resources.Load<Sprite>("Vinai");
        fadeScreen.SetActive(true);
        while (Volumos > 0.2f)
        {
            Volumos -= Time.deltaTime * 0.2f;
            Pitchos -= Time.deltaTime * 0.1f;
            fadeim.color = new Color(1, 1, 1, 0.8f - Volumos);
            setMainOSTVolume(Volumos);
            OST.pitch = Pitchos;
            if (Volumos < 0.5f && !startRing)
            {
                startRing = true;
                playSound(Resources.Load<AudioClip>("SFX/RingInEar"), false);
            }
            yield return null;
        }

        yield return new WaitForSeconds(2f);
        isSelfAware = true;
        while (Volumos < 0.8f) {
            Volumos += Time.deltaTime * 0.2f;
            Pitchos += Time.deltaTime * 0.1f;
            fadeim.color = new Color(1, 1, 1, 0.8f - Volumos);
            setMainOSTVolume(Volumos);
            OST.pitch = Pitchos;
            yield return null;
        }
        fadeScreen.SetActive(false);
        setMainOSTVolume(1);
        OST.pitch = 1;
        StartCoroutine(goForPeace());
        //Ring in ears
        yield return null;
    }

    IEnumerator goForPeace()
    {
        while (!fadeScreen.activeSelf)
        {
            idleTime += Time.deltaTime;
            if (Input.GetKey(KeyCode.Space) || Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2)) idleTime = 0;
            Debug.Log(idleTime);
            if (idleTime > 8f) { 
                StartCoroutine(enterPeace());
                StartCoroutine(drainScore());
            }
            yield return null;
        }
    }
    IEnumerator drainScore()
    {
        while (idleTime > 8f && playerScore > 0)
        {
            loseScore(100, player.transform.position);
            yield return new WaitForSeconds(0.2f);
        }
    }
    IEnumerator enterPeace()
    {
        float Volumos = 0.8f;
        float Pitchos = 1.0f;
        bool startRing = false;
        Image fadeim = fadeScreen.GetComponent<Image>();
        fadeim.sprite = Resources.Load<Sprite>("Vinai");
        fadeScreen.SetActive(true);
        while (idleTime > 8f)
        {
            while (Volumos > 0.2f && idleTime > 8f)
            {
                Volumos -= Time.deltaTime * 0.1f;
                Pitchos -= Time.deltaTime * 0.05f;
                fadeim.color = new Color(1, 1, 1, 0.8f - Volumos);
                setMainOSTVolume(Volumos);
                OST.pitch = Pitchos;
                if (Volumos < 0.5f && !startRing)
                {
                    startRing = true;
                    //playSound(Resources.Load<AudioClip>("SFX/RingInEar"), false);
                }
                yield return null;
            }

            yield return new WaitForSeconds(2f);
            if (idleTime > 8f)
            {
                becomeAlly = true;
                loseScore(playerScore, player.transform.position);
                while (Volumos < 0.8f)
                {
                    Volumos += Time.deltaTime * 0.2f;
                    Pitchos += Time.deltaTime * 0.05f; //0.1f to end up even/properly
                    fadeim.color = new Color(1, 1, 1, 0.8f - Volumos);
                    setMainOSTVolume(Volumos);
                    OST.pitch = Pitchos;
                    yield return null;
                }
                //WE ARE AWAKE
                if (MissionCounter < 419)
                {
                    //Skip to Artillery Ending
                    MissionCounter = 419;
                    yield return new WaitForSeconds(6f);
                }
                scoreTexto.text = "RUN";
                scoreNum.text = "EAST";
                idleTime = 0f;
                StopCoroutine(goForPeace());
                yield return null;
            }
        }
        fadeScreen.SetActive(false);
        setMainOSTVolume(1);
        OST.pitch = 1;
    }
    IEnumerator nineMinutes()
    {
        //Artillery Announcement
        //Move position of Transmission downward
        StopCoroutine(goForPeace());
        while (transmissionUI.transform.position.y > transmissionAnchor.y-225)
        {
            transmissionUI.transform.position = new Vector3(transmissionUI.transform.position.x, transmissionUI.transform.position.y-(225*Time.deltaTime*0.5f), 0);
            yield return null;
        }
        //Play Voiceline?
        //Move position of Transmission upward
        yield return new WaitForSeconds(9f);
        while (transmissionUI.transform.position.y < transmissionAnchor.y)
        {
            transmissionUI.transform.position = new Vector3(transmissionUI.transform.position.x, transmissionUI.transform.position.y + (225 * Time.deltaTime * 0.5f), 0);
            yield return null;
        }
    }
    IEnumerator endingArtillery()
    {
        float Progress = 0f;
        //Ending Artillery
        float fadeToWhite = 0f;
        fadeScreen.SetActive(true);
        Image fadeim = fadeScreen.GetComponent<Image>();
        fadeim.sprite = Resources.Load<Sprite>("White");
        fadeim.color = new Color(1, 1, 1, fadeToWhite);

        //Spawn background artillery first
        int Arts = 10;
        while (Arts > 0)
        {
            Arts--;
            //Spawn Arty Background
            Vector3 vec = player.transform.position + new Vector3(Random.Range(-10, 18), -0.6f, 0);
            Instantiate(Resources.Load<GameObject>("ArtilleryImpactCinematicBack"), vec, Quaternion.identity);
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(4);
        //Then spawn foreground artillery
        Arts = 10;
        while (Arts > 0)
        {
            Arts--;
            //Spawn Arty Foreground
            Vector3 vec = player.transform.position + new Vector3(Random.Range(-10, 18), -0.6f, 0);
            Instantiate(Resources.Load<GameObject>("ArtilleryImpactCinematic"), vec, Quaternion.identity);
            yield return new WaitForSeconds(0.2f);
        }
        //5 second impact delay
        yield return new WaitForSeconds(2f);
        //Immediate fade to white
        while (fadeToWhite < 0.2f)
        {
            fadeToWhite += Time.deltaTime*0.2f;
            fadeim.color = new Color(1, 1, 1, fadeToWhite);
            yield return null;
        }
        //Artillery IMPACT
        while (fadeToWhite < 1f)
        {
            fadeToWhite += Time.deltaTime;
            fadeim.color = new Color(1, 1, 1, fadeToWhite);
            yield return null;
        }
        gamePaused = true;
        yield return new WaitForSeconds(2f);
        //Placeholder: Going to ENDING immediately
        if (becomeAlly) {
            yield return new WaitForSeconds(2f);
            endingText.text = "You survived.";
            yield return new WaitForSeconds(4f);
            SceneManager.LoadScene("Ending"); 
        }
        else { 
            endingText.text = "You died gloriously! No one will remember you!\r\nSCORE: " + playerScore; 
        }
    }

    public bool selfAware()
    {
        return isSelfAware;
    }
}
