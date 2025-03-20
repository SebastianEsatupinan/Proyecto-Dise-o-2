using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoRay : MonoBehaviour
{
    public Transform firePoint;
    public int damage = 25;

    public LineRenderer lineRenderer;

    public GameObject shootFx, impact;

    public void Disparar()
    {
        StartCoroutine(Disparo());
    }

    IEnumerator Disparo()
    {
        RaycastHit hit;
        bool hitInfo = Physics.Raycast(firePoint.position, firePoint.forward, out hit, 50f);

        if (hitInfo)
        {
            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, hit.point);

            //Instantiate(impact, hit.point, Quaternion.identity);
        }
        else
        {
            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, firePoint.position + firePoint.forward * 20);
        }

        lineRenderer.enabled = true;

        yield return new WaitForSeconds(0.02f);

        lineRenderer.enabled = false;
    }
}
