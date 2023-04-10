using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Info")]
    public float speed;
    public float jumpPower;
    public GameObject respawnPos;
    public int playerHealth = 2;

    [Header("Item")]
    public GameObject seguen;
    public GameObject shield;

    BoxCollider collider;
    Rigidbody2D rb;

    bool isOnGround;
    bool isUseShoes;

    int playerLayer;
    int groundLyaer;

    float maximumHeight;
    float damagedRage = 8;

    public int selector = 0;
    int typeCount = 4;

    private void Start()
    {
        collider = gameObject.GetComponent<BoxCollider>();
        rb = gameObject.GetComponent<Rigidbody2D>();

        playerLayer = 6;
        groundLyaer = 7;

        isOnGround = false;

        gameObject.transform.position = respawnPos.transform.position;
        respawnPos.transform.position = new Vector2(0,0);
    }

    private void Update()
    {
        playerInput();
        scrollInput();
        playerCollison();
        checkMaximumHeight();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        checkOnGround(collision);
    }

    void playerInput()
    {
        float horizontal = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.R)) dead();


        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        if (!isOnGround && Input.GetKeyDown(KeyCode.Space) && isUseShoes)
        {
            isUseShoes = false;
            jump();
        }
        if (isOnGround && Input.GetKeyDown(KeyCode.Space)) jump();

        if (Input.GetKeyDown(KeyCode.G)) shootItem();

    }

    void playerCollison()
    {
        if (!isOnGround)
        {
            if (rb.velocity.y > 0)
                Physics2D.IgnoreLayerCollision(playerLayer, groundLyaer, true);
            else 
                Physics2D.IgnoreLayerCollision(playerLayer, groundLyaer, false);
        }
    }

    void scrollInput()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if(scroll != 0)
        {
            if (scroll > 0)
                selector--;
            else
                selector++;
            if (selector > typeCount - 1)
                selector = 0;
            if (selector < 0)
                selector = typeCount - 1;
        }
    }
    void shootItem()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //세균
        if (selector == 0)
        {
            GameObject instance = Instantiate(seguen, transform.position, Quaternion.identity);
            instance.SetActive(true);
            instance.GetComponent<Rigidbody2D>().AddForce((new Vector2(
                mousePos.x -instance.transform.position.x,
                mousePos.y - instance.transform.position.y).normalized)
                * 10f,
                ForceMode2D.Impulse);
        }
        //방폐
        else if (selector == 1 && Inventory.item_shield > 0)
        {
            GameObject instance = Instantiate(shield, mousePos, Quaternion.identity);
            instance.SetActive(true);
            Inventory.item_shield -= 1;
        }
        //표지판
        else if (selector == 2 && Inventory.item_sign > 0)
        {
            respawnPos.transform.position = gameObject.transform.position;
            Inventory.item_sign -= 1;
        }
        //한번 더 점프 신발
        else if (selector == 3 && Inventory.item_shoes > 0)
        {
            isUseShoes = true;
            Inventory.item_shoes -= 1;
        }
    }
    void jump()
    {
        isOnGround = false;
        rb.AddForce(new Vector2(0,jumpPower), ForceMode2D.Impulse);
    }

    void checkMaximumHeight()
    {
        if(isOnGround == false && rb.velocity.y > 0 && maximumHeight < transform.position.y)
        {
            maximumHeight = transform.position.y;
        }
    }

    void checkOnGround(Collision2D collision)
    {
        if (isOnGround == false && collision.transform.tag == "wall")
        {
            isOnGround = true;
            if (maximumHeight - transform.position.y > damagedRage) drawOffHealth();
            maximumHeight = 0;
        }
    }

    void drawOffHealth()
    {
        playerHealth--;
        if (playerHealth <= 0)
        {
            dead();
        }
    }

    void dead()
    {
        Debug.Log("Die");

        spawn();
        gameObject.tag = "wall";
        gameObject.layer = 7;
        Destroy(gameObject.GetComponent<Player>());
    }

    void spawn()
    {
        Player newPlayer = Instantiate(gameObject).GetComponent<Player>();
    }

    public void addCoin(int count) => Inventory.coin += count;

}

static class Inventory
{
    static public int coin = 100;

    static public int item_shield = 1;
    static public int item_sign = 1;
    static public int item_shoes = 1;
}

