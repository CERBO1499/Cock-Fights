using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class LaunchedWord : MonoBehaviour
{
    //Rigidbody2D rb;
    [SerializeField] float speed;
    [SerializeField] float lifeTime;
    [SerializeField] private string side = "";
    [SerializeField] string text = "";
    [SerializeField] TextMeshPro tmpText;

    public float Speed { get => speed; set => speed = value; }
    public string Side { get => side; set => side = value; }
    public string Text { get => text; set => text = value; }

    void Start()
    {
        tmpText.text = text;
        //rb.GetComponent<Rigidbody2D>();
        //rb.velocity = Vector2.up * speed;
        //transform.position =
        StartCoroutine(Release());
    }

    private void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private IEnumerator Release()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    //public void OnDrag(PointerEventData eventData)
    //{
    //    throw new NotImplementedException();
    //}
}
