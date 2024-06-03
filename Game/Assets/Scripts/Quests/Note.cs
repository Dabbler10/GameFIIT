using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{
    [Serializable]
    public struct SpritePair
    {
        public Sprite sprite1;
        public Sprite sprite2;
    }

    [SerializeField] public SpritePair[] sprites;


    public Image Image;
    public Canvas Canvas;
    private readonly float noteDuration = 10f;
    private bool isAppearing = false;
    private float timer = 0f;
    private static bool firstTimeShowingMovement = true;
    [SerializeField] private AudioClip noteSound;
    float screenWidth = Screen.width;
    float screenHeight = Screen.height;


    void Update()
    {
        if (isAppearing)
            timer += Time.deltaTime;
        if ((Input.GetKeyDown(KeyCode.Return) && !firstTimeShowingMovement) || (isAppearing && timer > noteDuration))
            Disappear();
    }

    public void Appear(int index, int item)
    {
        if (item == 1)
        {
            AppearSmallImage();
            Image.sprite = sprites[index].sprite1;
        }

        else
        {
            AppearBigImage();
            Image.sprite = sprites[index].sprite2;
        }


        Image.gameObject.SetActive(true);
    }

    private void AppearBigImage()
    {
        SoundManager.instance.PlaySound(noteSound);
        MakeImageBig();
        isAppearing = false;
        Canvas.GetComponent<GameState>().Pause();
    }

    private void AppearSmallImage()
    {
        MakeImageSmall();
        isAppearing = true;
        timer = 0f;
    }

    public void Disappear()
    {
        Canvas.GetComponent<GameState>().Resume();
        Image.gameObject.SetActive(false);
        isAppearing = false;
        timer = 0f;
        firstTimeShowingMovement = false;
    }

    private void MakeImageBig()
    {
        Image.rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        Image.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        Image.rectTransform.pivot = new Vector2(0.5f, 0.5f);
        Image.transform.localScale = new Vector3(7.5f, 7.5f, 0);
        Image.rectTransform.anchoredPosition = new Vector2(0, 0);
        Color currentColor = Image.color;
        Image.color = new Color(currentColor.r, currentColor.g, currentColor.b, 1);
    }

    private void MakeImageSmall()
    {
        Image.transform.localScale = new Vector3(3, 3, 1);
        Image.rectTransform.anchorMin = new Vector2(1, 0);
        Image.rectTransform.anchorMax = new Vector2(1, 0);
        Image.rectTransform.pivot = new Vector2(0.5f, 0.5f);
        Color currentColor = Image.color;
        Image.color = new Color(currentColor.r, currentColor.g, currentColor.b, 0.7f);
    }
}