using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    private bool isPressed = false;
    public Rigidbody2D rb;
    public float releaseTime = .15f;
    public float maxDragDistance = 2f;
    public Rigidbody2D hook;
    public GameObject nextBall;

    private void OnMouseDown()
    {
        isPressed = true;
        rb.isKinematic = true;
    }
    private void OnMouseUp()
    {
        isPressed = false;
        rb.isKinematic = false;
        StartCoroutine(Release());

    }
    private void Update()
    {
        if (isPressed)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vector3.Distance(mousePos, hook.position) > maxDragDistance)
            {
                rb.position = hook.position + (mousePos - hook.position).normalized * maxDragDistance;
            }
            else
            {
                rb.position = mousePos;
            }
        }
    }

    IEnumerator Release()
    {
        yield return new WaitForSeconds(releaseTime);

        GetComponent<SpringJoint2D>().enabled = false;
        this.enabled = false;
        yield return new WaitForSeconds(2f);

        if (nextBall != null)
        {
            nextBall.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
        
    }
}
