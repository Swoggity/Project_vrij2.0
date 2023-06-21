using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class INTRO : MonoBehaviour
{
    VideoPlayer play;
    [SerializeField] GameObject YouCanSkipText;
    bool YouCanSkip = false;
    void Start()
    {
        play = GetComponent<VideoPlayer>();
        StartCoroutine(SkipEventually());
    }

    IEnumerator SkipEventually()
    {
        yield return new WaitForSeconds(5);
        YouCanSkip = true;
        YouCanSkipText.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && YouCanSkip) SceneManager.LoadScene("MainScene");
    }
}
