using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectileSpawner2D : MonoBehaviour
{
    [SerializeField] GameObject proyectile = null;

    [SerializeField] float proyectileSpeed = 3f;

    [SerializeField] float proyectileRange = 5f;

    [SerializeField] ProyectileDirection proyectileDirection = ProyectileDirection.Left;
    public enum ProyectileDirection
    {
        Right,
        Left,
        Up,
        Down
    }

    private GameObject proyectileSpawned;

    private void Start()
    {
        if (proyectile != null)
        {
            proyectileSpawned = Instantiate(proyectile, transform.position, Quaternion.identity);
            proyectileSpawned.transform.parent = gameObject.transform;
        }

    }

    private void Update()
    {
        if (proyectile == null)
            return;

        MoveProyectile();
    }

    private void MoveProyectile()
    {
        Vector3 endPos = FindEndPosition();

        proyectileSpawned.transform.position = Vector3.MoveTowards(proyectileSpawned.transform.position, endPos, proyectileSpeed * Time.deltaTime);

        float distance = Vector3.Distance(proyectileSpawned.transform.position, endPos);

        if (distance < 0.001f)
            proyectileSpawned.transform.position = transform.position;
    }

    Vector3 FindEndPosition()
    {
        Vector3 endPos = Vector3.zero;

        switch (proyectileDirection)
        {
            case ProyectileDirection.Right:
                endPos = new Vector3(transform.position.x + proyectileRange, transform.position.y, transform.position.z);
                break;

            case ProyectileDirection.Left:
                endPos = new Vector3(transform.position.x - proyectileRange, transform.position.y, transform.position.z);
                break;

            case ProyectileDirection.Up:
                endPos = new Vector3(transform.position.x, transform.position.y + proyectileRange, transform.position.z);
                break;

            case ProyectileDirection.Down:
                endPos = new Vector3(transform.position.x, transform.position.y - proyectileRange, transform.position.z);
                break;
        }

        return endPos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, FindEndPosition());
    }
}
