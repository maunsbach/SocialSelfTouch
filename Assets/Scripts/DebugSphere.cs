using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugSphere : MonoBehaviour
{
    private bool _drawGizmo;

    private Coroutine coroutine;
    public void ShowSphere()
    {
        _drawGizmo = true;
    }

    public void ShowSphere(float time)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        _drawGizmo = true;
        IEnumerator enumerator = StopDebug(time);
        coroutine = StartCoroutine(enumerator);
    }

    IEnumerator StopDebug(float time)
    {
        yield return new WaitForSeconds(time);
        DisableSphere();
    }

    public void DisableSphere()
    {
        _drawGizmo = false;
    }

    void OnDrawGizmosSelected()
    {
        if (_drawGizmo == true)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, 0.003f);
        }
    }
}
