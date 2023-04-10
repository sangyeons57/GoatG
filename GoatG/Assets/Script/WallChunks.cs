using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallChunks : MonoBehaviour
{
    public List<GameObject> chunks = new List<GameObject>();
    public bool isFakeWall = false;

    SpriteRenderer renderer;

    private void Start()
    {

        renderer = GetComponent<SpriteRenderer>();

        for (int i = 0; i < gameObject.transform.childCount; i++) addChunk(gameObject.transform.GetChild(i));

        readyFake();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player") showFake();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        readyFake();
    }

    void addChunk(Transform obj)
    {
        chunks.Add(obj.gameObject);
        obj.parent = transform;
    }

    void showFake()
    {
        renderer.color = Color.black;
        foreach(GameObject chunk in chunks) chunk.gameObject.active = false;
    }

    void readyFake()
    {
        renderer.color = Color.white;
        foreach(GameObject chunk in chunks) chunk.gameObject.active = true;
    }
}
