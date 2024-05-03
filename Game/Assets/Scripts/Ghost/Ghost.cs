using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class Ghost : MonoBehaviour
{
    public float speed = 3f;
    public float radius = 4f;
    private Vector3 centerPoint;
    private Vector3 direction;
    private float epsilon = 0.1f;
    private bool flagToPlayer;
    private bool flagToCentralPoint;
    public float playerDetectionRadius = 2.5f; // Радиус обнаружения игрока
    public Transform player1;
    public Transform player2;
    private Transform goalPlayer;
    private Animator anim;

    void Start()
    {
        centerPoint = transform.position;
        direction = Random.insideUnitCircle * radius;
        anim = GetComponent<Animator>();
    }

    private void ChoosePlayerToAttack(Vector3 displacementToPlayer1, Vector3 displacementToPlayer2)
    {
        flagToPlayer = true;
        flagToCentralPoint = false;
        Vector3 displacement;
        if (displacementToPlayer1.magnitude < displacementToPlayer2.magnitude)
        {
            goalPlayer = player1;
            displacement = displacementToPlayer1;
        }
        else
        {
            goalPlayer = player2;
            displacement = displacementToPlayer2;
        }

        if (displacement.magnitude < epsilon)
            direction = Vector3.zero;
        else
            direction = displacement.normalized;

        if (Math.Abs(goalPlayer.position.x - transform.position.x) < 1.4 &&
            Math.Abs(goalPlayer.position.y - transform.position.y) < 1.4)
            anim.SetBool("attack", true);
    }

    private void TurnToDirection()
    {
        if (direction.x < 0)
            transform.localScale = new Vector2(-Math.Abs(transform.localScale.x), transform.localScale.y);
        else
            transform.localScale = new Vector2(Math.Abs(transform.localScale.x), transform.localScale.y);
    }

    void Update()
    {
        transform.position += (Vector3)direction * (speed * Time.deltaTime);
        var displacementToCentralPoint = centerPoint - transform.position;
        var displacementToPlayer1 = player1.position - transform.position;
        var displacementToPlayer2 = player2.position - transform.position;
        var displacementToCenterPoint = transform.position - centerPoint;
        anim.SetBool("attack", false);

        if (displacementToPlayer1.magnitude < playerDetectionRadius ||
            displacementToPlayer2.magnitude < playerDetectionRadius) // летим на человека
            ChoosePlayerToAttack(displacementToPlayer1, displacementToPlayer2);
        else if (goalPlayer != null && (goalPlayer.position - transform.position).magnitude >= playerDetectionRadius
                                    && flagToPlayer
                                    &&! flagToCentralPoint) //  только что перестали идти за человеком и хотим вернуться к точке
        {
            flagToPlayer = false;
            flagToCentralPoint = true;
            direction = displacementToCentralPoint.normalized; // летим к точке
        }
        else if (displacementToCenterPoint.magnitude > radius
                 && !flagToCentralPoint) // призрак выходит из окружности точки
            direction = (Random.insideUnitCircle * radius).normalized;
        else if (displacementToCenterPoint.magnitude <= radius && flagToCentralPoint) // только что прилетели к точке
        {
            flagToCentralPoint = false;
            direction = (Random.insideUnitCircle * radius).normalized;
        }

        TurnToDirection();
    }
}