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
    private List<int> bigIndexes = new(){2};
    

    void Update()
    {
        if (!isAppearing) return;
        timer += Time.deltaTime;
        if (timer > noteDuration)
            Disappear();
    }

    public void Appear(int index)
    {
        if (bigIndexes.Contains(index))
            MakeImageBig();
            
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
        Image.transform.localScale = new Vector3(1, 1, 1);
        MakeImageSmall();
    }

    private void MakeImageBig()
    {
        Image.transform.localScale = new Vector3(3, 3, 0);
        Image.rectTransform.anchoredPosition = new Vector2(0, 0);
        Color currentColor = Image.color;
        Image.color = new Color(currentColor.r, currentColor.g, currentColor.b, 1);
    }

    private void MakeImageSmall()
    {
        Image.transform.localScale = new Vector3(1, 1, 1);
        Image.rectTransform.anchoredPosition = new Vector2(285,-180);
        Color currentColor = Image.color;
        Image.color = new Color(currentColor.r, currentColor.g, currentColor.b, 0.7f);
    }
}