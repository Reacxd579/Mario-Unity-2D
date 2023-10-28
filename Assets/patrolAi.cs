using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patrolAi : MonoBehaviour
{
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private Transform[] puntosMovimiento;
    [SerializeField] private float distaciaMinima;

    private int numeroAleatorio;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        numeroAleatorio = Random.Range(0, puntosMovimiento.Length);
        spriteRenderer = GetComponent<SpriteRenderer>();
        Girar();
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, puntosMovimiento[numeroAleatorio].position, velocidadMovimiento * Time.deltaTime);
    if (Vector2.Distance(transform.position, puntosMovimiento[numeroAleatorio].position) < distaciaMinima )
    {
        numeroAleatorio = Random.Range(0, puntosMovimiento.Length);
        Girar();
    }
  }

  private void Girar()
  {
    if(transform.position.x < puntosMovimiento[numeroAleatorio].position.x)
    {
        spriteRenderer.flipX = true;
    }
    else
    {
        spriteRenderer.flipX = false;
    }
  }
} 
