using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{
    public Image Image;
    public Sprite[] sprites;
    private readonly float noteDuration = 10f;
    private bool isAppearing = false;
    private float timer = 0f;
    private Texture texture;

    void Update()
    {
        if (!isAppearing) return;
        timer += Time.deltaTime;
        if (timer > noteDuration)
            Disappear();
    }

    public void Appear(int index)
    {
        Image.sprite = sprites[index]; // устанавливаем передаваемую из квеста картинку
        Image.gameObject.SetActive(true);
        isAppearing = true;
        timer = 0f;
    }
    
    public void Disappear()
    {
        Image.gameObject.SetActive(false);
        isAppearing = false;
        timer = 0f;
    }
}