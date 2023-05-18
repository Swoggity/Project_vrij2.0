using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    [SerializeField] GameObject krant1ob;
    [SerializeField] GameObject krant2ob;
    [SerializeField] Texture2D krant1;
    [SerializeField] Texture2D krant2;
    [SerializeField] Texture2D krant1o;
    [SerializeField] Texture2D krant2o;
    Camera cam;
    [SerializeField] SpriteRenderer fade;
    [SerializeField] float fadeSpeed = 0.15f;
    private bool enablePencil = true;
    private float fadeAlpha = 2.0f;
    private float pencilAlpha = 0.0f;
    private float KrantenZoom = 1.8f;
    private int Stage = 0;
    void Start()
    {
        cam = Camera.main;
        Graphics.CopyTexture(krant1o, krant1);
        Graphics.CopyTexture(krant2o, krant2);
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, pencilAlpha);
        krant1.Apply();
        krant2.Apply();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 gopos = cam.ScreenToWorldPoint(Input.mousePosition);
        float offset = Input.GetMouseButton(0) ? 0.0f : 0.5f;
        transform.position = new Vector3(gopos.x + offset, gopos.y + offset, 0);
        if (KrantenZoom > 1.0f)
        {
            KrantenZoom -= 0.02f * Time.deltaTime;
            if (Stage < 3 && KrantenZoom < 1.01f) { Stage = 3; }
        }
        Vector3 krant = new Vector3(KrantenZoom, KrantenZoom, 1.0f);
        krant1ob.transform.localScale = krant;
        krant2ob.transform.localScale = krant;

        switch (Stage) {
            case 0: //Fade in everything
                fadeAlpha -= fadeSpeed * Time.deltaTime;
                if (fadeAlpha < 0.0f) { Stage ++; fadeAlpha = 0.0f; }
                fade.color = new Color(0, 0, 0, fadeAlpha);
                break;
            case 1: //Fade in Pencil
                pencilAlpha += fadeSpeed * Time.deltaTime;
                if (pencilAlpha > 1.0f) { Stage ++; pencilAlpha = 1.0f; }
                GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, pencilAlpha);
                break;
            case 2: //Pencil has appeared
                break;
            case 3: //Fade out everything
                fadeAlpha += fadeSpeed * Time.deltaTime;
                if (fadeAlpha > 1.0f) { Stage++; fadeAlpha = 1.0f; }
                fade.color = new Color(0, 0, 0, fadeAlpha);
                break;
        }
        if (Stage > 1)
        {
            if (Input.GetMouseButton(0))
            {
                //108 pixels per unit
                int radius = 64;
                Vector3 vec2 = new Vector3(krant1.width / 2, krant1.height / 2, 0);
                Vector3 pixelpos = (gopos * (108 / KrantenZoom)) + vec2;
                int xmin = (int)Mathf.Clamp(pixelpos.x - radius, 0, krant1.width);
                int ymin = (int)Mathf.Clamp(pixelpos.y - radius, 0, krant1.height);
                int xmax = (int)Mathf.Clamp(pixelpos.x + radius, 0, krant1.width);
                int ymax = (int)Mathf.Clamp(pixelpos.y + radius, 0, krant1.height);
                for (int i = xmin; i < xmax; i++)
                {
                    for (int i2 = ymin; i2 < ymax; i2++)
                    {
                        krant1.SetPixel(i, i2, Color.clear);
                    }
                }
                krant1.Apply();
                krant2.Apply();
            }
        }
    }
}
