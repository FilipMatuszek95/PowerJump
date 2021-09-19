using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    private GameObject hook;
    public float releaseTime = .15f;
    private bool isPressed = false;

    void Update()
    {
        if (isPressed)
        {
            rb.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    void OnMouseDown()
    {
        isPressed = true;
        rb.isKinematic = true;
        hook = new GameObject();
        hook.AddComponent<Rigidbody2D>();
        hook.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        hook.GetComponent<Rigidbody2D>().position = rb.position;
        GetComponent<SpringJoint2D>().enabled = true;
    }

    void OnMouseUp()
    {
        isPressed = false;
        rb.isKinematic = false;

        StartCoroutine(Release());
    }

    IEnumerator Release()
    {
        yield return new WaitForSeconds(releaseTime);

        GetComponent<SpringJoint2D>().enabled = false;
        Destroy(hook);
    }
}
