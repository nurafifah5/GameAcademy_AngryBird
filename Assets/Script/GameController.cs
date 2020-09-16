using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public SlingShooter SlingShooter;
    public TrailController TrailController;
    private UIController uiControl;
    public List<Bird> Birds;
    public List<Enemy> Enemies;
    private bool isGameEnded = false;
    private bool isFail = false;
    private Bird shotBird;
    public BoxCollider2D TapCollider;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Birds.Count; i++)
        {
            Birds[i].OnBirdDestroyed += ChangeBird;
            Birds[i].OnBirdShot += AssignTrail;
        }

        for(int i = 0; i<Enemies.Count; i++)
        {
            Enemies[i].OnEnemyDestroyed += CheckGameEnd;
        }

        TapCollider.enabled = false;
        SlingShooter.InitiateBird(Birds[0]);
        shotBird = Birds[0];
        uiControl = GameObject.Find("Canvas").GetComponent<UIController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Enemies.Count == 0)
        {
            isGameEnded = true;
        } 
        else
        {
            if (Birds.Count == 0)
            {
                isGameEnded = true;
                isFail = true;
            }
        }
        if (isGameEnded)
        {
            if (isFail)
            {
                uiControl.failGame();
            }
            else
            {
                uiControl.endGame();
            }
        }
    }

    public void ChangeBird()
    {
        TapCollider.enabled = false;
        if (isGameEnded)
        {
            return;
        }

        Birds.RemoveAt(0);
        if(Birds.Count > 0)
        {
            SlingShooter.InitiateBird(Birds[0]);
            shotBird = Birds[0];
        }
    }

    public void  AssignTrail(Bird bird)
    {
        TrailController.SetBird(bird);
        StartCoroutine(TrailController.SpawnTrail());
        TapCollider.enabled = true;
    }

    //kalau yellowBird dilontarkan akan auto menambahkan boost kecepatan. 
    void OnMouseUp()
    {
        if (shotBird != null)
        {
            shotBird.OnTap();
        }
    }

    public void CheckGameEnd(GameObject destroyedEnemy)
    {
        for(int i = 0; i < Enemies.Count; i++)
        {
            if(Enemies[i].gameObject == destroyedEnemy)
            {
                Enemies.RemoveAt(i);
                break;
            }
        }

        if(Enemies.Count == 0)
        {
            isGameEnded = true;
        } 
    }

}
