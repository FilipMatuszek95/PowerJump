using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField]
    Rigidbody2D rb;

    [SerializeField]
    float releaseTime = .15f;

    [SerializeField]
    Transform mainCamera;

    [SerializeField]
    SpringJoint2D sj;

    [NonSerialized]
    GameObject hook;

    [NonSerialized]
    Rigidbody2D hookRb;

    [NonSerialized]
    bool isPressed = false;

    [NonSerialized]
    bool playerIsKillable = false;

    void LateUpdate()
    {
        if (isPressed)
        {
            rb.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else
        {
            if (transform.position.y > mainCamera.position.y)
            {
                mainCamera.position = new Vector3(mainCamera.position.x, transform.position.y, mainCamera.position.z);
            }
        }

        if (playerIsKillable)
        {
            if (rb.position.y + 4 < mainCamera.position.y)
            {
                SceneManager.LoadScene("MainLevel");
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        playerIsKillable = true;
        isPressed = false;
        rb.isKinematic = false;

        StartCoroutine(Release());
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
        playerIsKillable = false;
        rb.isKinematic = true;
        hook = new GameObject();
        hookRb = hook.AddComponent<Rigidbody2D>();
        hookRb.bodyType = RigidbodyType2D.Dynamic;
        hookRb.position = rb.position;
        sj.enabled = true;
    }

    IEnumerator Release()
    {
        yield return new WaitForSeconds(releaseTime);

        sj.enabled = false;
        Destroy(hook);
    }
}
