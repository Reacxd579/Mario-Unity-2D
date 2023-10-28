using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaEnMovimiento : MonoBehaviour
{
    public GameObject ObjetoAmover;
    public Transform StartPoint;
    public Transform EndPoint;
    public float Velocidad;
    private Vector3 MoverHacia;
    private Vector3 PosicionInicialObjetoAmover;

    // Start is called before the first frame update
    void Start()
    {
        MoverHacia = EndPoint.position;
        PosicionInicialObjetoAmover = ObjetoAmover.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movimiento = MoverHacia - PosicionInicialObjetoAmover;
        ObjetoAmover.transform.position = Vector3.MoveTowards(ObjetoAmover.transform.position, MoverHacia, Velocidad * Time.deltaTime);

        if (ObjetoAmover.transform.position == EndPoint.position)
        {
            MoverHacia = StartPoint.position;
            PosicionInicialObjetoAmover = ObjetoAmover.transform.position - movimiento;
        }
        if (ObjetoAmover.transform.position == StartPoint.position)
        {
            MoverHacia = EndPoint.position;
            PosicionInicialObjetoAmover = ObjetoAmover.transform.position + movimiento;
        }
    }
}
