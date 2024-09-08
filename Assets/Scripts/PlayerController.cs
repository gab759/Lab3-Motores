using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using TMPro;

public class PlayerController : MonoBehaviour
{
    //[Header("Eventos")]
    public event Action<int> OnLifeUpdated;
    public event Action<int> OnScoreUpdated;
    public event Action OnPlayerWin;
    public event Action OnPlayerLoss;

    [Header("GameObject")]
    public GameObject player;
    public GameObject lifeObject;
    public GameObject scoreObject;

    public int score = 0;
    private float speed = 3f;
    public float direction;
    public LayerMask layerMask;
    public bool suelo;
    public bool doubleJump;
    public float jumpForce = 4.5f;
    private bool jumpVerificacion; 
    Rigidbody2D _compRigidbody2D;
    public int playerColor;
    public int life = 10;
    public Slider barLife;
    
    private bool canChangeColor = true;
    public TextMeshProUGUI scoreText;
    private void Awake()
    {
        _compRigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        OnLifeUpdated += UpdateLifeUI;
        OnScoreUpdated += UpdateScoreUI;
        OnPlayerWin += LoadWinScene;
        OnPlayerLoss += LoadLossScene;
    }

    private void OnDisable()
    {
        OnLifeUpdated -= UpdateLifeUI;
        OnScoreUpdated -= UpdateScoreUI;
        OnPlayerWin -= LoadWinScene;
        OnPlayerLoss -= LoadLossScene;
    }
    private void UpdateLifeUI(int currentLife)
    {
        barLife.value = currentLife;  
        Debug.Log("Vida actualizada: " + currentLife);
    }

    private void UpdateScoreUI(int currentScore)
    {
        Debug.Log("Puntaje actualizado: " + currentScore);
    }
    private void LoadWinScene()
    {
        SceneManager.LoadScene("Win");
    }

    private void LoadLossScene()
    {
        SceneManager.LoadScene("Loss");
    }
    private void FixedUpdate()
    {
        _compRigidbody2D.velocity = new Vector2(direction * speed, _compRigidbody2D.velocity.y);

        Check();

        if (jumpVerificacion)
        {
            Jump();
            jumpVerificacion = false; 
        }
    }

    private void Update()
    {
        direction = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpVerificacion = true;
        }
    }
    public void DecreaseLife(int amount)
    {
        life -= amount;
        OnLifeUpdated?.Invoke(life);
        if (life <= 0)
        {
            OnPlayerLoss?.Invoke();
        }
    }
    public void IncreaseLife(int amount)
    {
        life += amount;
        OnLifeUpdated?.Invoke(life);  
    }
    public void IncreaseScore(int points)
    {
        score += points;
        OnScoreUpdated?.Invoke(score);  
    }
    private void Jump()
    {
        if (suelo || doubleJump)
        {
            if (!suelo)
            {
                doubleJump = false; 
            }
            _compRigidbody2D.velocity = new Vector2(_compRigidbody2D.velocity.x, jumpForce);
        }
    }

    private void Check()
    {
        Debug.DrawLine(transform.position, transform.position + Vector3.down, Color.blue);
        if (Physics2D.Raycast(transform.position, Vector2.down, 1f, layerMask))
        {
            suelo = true;
            doubleJump = true;
        }
        else
        {
            suelo = false;
        }
    }
    public void Red()
    {
        if (canChangeColor)
        {
            player.GetComponent<SpriteRenderer>().color = Color.red;
            playerColor = 1;
        }
    }

    public void Blue()
    {
        if (canChangeColor)
        {
            player.GetComponent<SpriteRenderer>().color = Color.blue;
            playerColor = 2;
        }
    }

    public void Yellow()
    {
        if (canChangeColor)
        {
            player.GetComponent<SpriteRenderer>().color = Color.yellow;
            playerColor = 3;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Door"))
        {
            SceneManager.LoadScene("Level2");
        }
        if (collision.CompareTag("Win"))
        {
            OnPlayerWin?.Invoke();
        }
        if (collision.CompareTag("Life"))
        {
            IncreaseLife(2);
           
            Destroy(lifeObject);
            Debug.Log("vida restada: " + life);
            barLife.value = life;
        }
        if (collision.CompareTag("Points"))
        {
            IncreaseScore(20);
            Destroy(scoreObject);
            Debug.Log("puntos actuales: " + score);
            scoreText.text = "Puntos:" + score.ToString("f0");
        }
        switch (collision.tag)
        {
            case "Red":
                if (playerColor != 1)
                {
                    DecreaseLife(2);
                    barLife.value = life;
                }
                break;

            case "Blue":
                if (playerColor != 2)
                {
                    DecreaseLife(2);
                    barLife.value = life;
                }
                break;

            case "Yellow":
                if (playerColor != 3)
                {
                    DecreaseLife(2);
                    barLife.value = life;
                }
                break;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Red") || collision.CompareTag("Blue") || collision.CompareTag("Yellow"))
        {
            canChangeColor = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Red") || collision.CompareTag("Blue") || collision.CompareTag("Yellow"))
        {
            canChangeColor = true;
        }
    }
}