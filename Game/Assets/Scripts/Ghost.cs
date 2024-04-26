using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ghost : MonoBehaviour
{
    public float speed = 3f;

    public float radius = 4f;

    //private Vector2 centerPoint;
    private Vector3 centerPoint;

    //private Vector2 direction;
    private Vector3 direction;
    private bool flagToPlayer;
    private bool flagToCentralPoint;
    public float playerDetectionRadius = 2.5f; // Радиус обнаружения игрока
    public Transform player;

    void Start()
    {
        centerPoint = transform.position;
        direction = Random.insideUnitCircle * radius;
    }

    void Update()
    {
        transform.position += (Vector3)direction * (speed * Time.deltaTime);
        var displacementToCentralPoint = centerPoint - transform.position;
        var displacementToPlayer = player.position - transform.position;
        var displacement = transform.position - centerPoint;

        if (displacementToPlayer.magnitude < playerDetectionRadius) // летим на человека
        {
            direction = displacementToPlayer.normalized;
            flagToPlayer = true;
            flagToCentralPoint = false;
        }
        else if (displacementToPlayer.magnitude >= playerDetectionRadius && flagToPlayer &&
                 !flagToCentralPoint) //  только что перестали идти за человеком и ъотим вернуться к точке
        {
            flagToPlayer = false;
            flagToCentralPoint = true;
            direction = displacementToCentralPoint.normalized; // летим к точке
        }
        else if (displacement.magnitude > radius && !flagToCentralPoint) // призрак выходит из окружности точки
            direction = (Random.insideUnitCircle * radius).normalized;
        else if (displacement.magnitude <= radius && flagToCentralPoint) // только что прилетели к точке
        {
            flagToCentralPoint = false;
            direction = (Random.insideUnitCircle * radius).normalized;
        }

        // поворачиваем в сторону движения
        if (direction.x < 0)
            transform.localScale = new Vector2(-Math.Abs(transform.localScale.x), transform.localScale.y);
        else
            transform.localScale = new Vector2(Math.Abs(transform.localScale.x), transform.localScale.y);
    }
}