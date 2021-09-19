using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField]
    Rigidbody2D rb;

    [NonSerialized]
    GameObject hook;

    public float releaseTime = .15f;
    private bool isPressed = false;

    void Update()
    {
        if (isPressed)
        {
            rb.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("The mouse click was released");
        isPressed = false;
        rb.isKinematic = false;

        StartCoroutine(Release());
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("The mouse click");
        isPressed = true;
        rb.isKinematic = true;
        hook = new GameObject();
        hook.AddComponent<Rigidbody2D>();
        hook.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        hook.GetComponent<Rigidbody2D>().position = rb.position;
        GetComponent<SpringJoint2D>().enabled = true;
    }

    IEnumerator Release()
    {
        yield return new WaitForSeconds(releaseTime);

        GetComponent<SpringJoint2D>().enabled = false;
        Destroy(hook);
    }
}
