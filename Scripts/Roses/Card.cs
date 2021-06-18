using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    protected bool facedown;
    protected bool taped;
    protected bool hasMoved;
    protected bool isDead;
    protected GameObject owner;
    protected Tile tile;
    protected CardMover mover;


    public int attack;
    public int health;
    private int start_health;
    private int start_attack;
    public int movement;

    public bool flipEffect;
    public bool tapEffect;

    public bool isUnit;
    public bool isCommander;
    public bool isSpell;
    public bool isTrap;
    public bool isItem;

    // Start is called before the first frame update
    void Start()
    {
        mover = GetComponent<CardMover>();
        if(!isCommander)
            facedown = true;
        else
            facedown = false;
        taped = false;
        hasMoved = false;
        isDead = false;

        movement = 1;

        start_health = health;
        start_attack = attack;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool GetFaceDown()
    {
        return facedown;
    }

    public void Flip()
    {
        if (facedown == true)
        {
            facedown = false;
            mover.SetStartFlip();
            //trigger flip effect
        }
        else
            facedown = true;
    }

    public bool GetTaped()
    {
        return taped;
    }

    public void Tap()
    {
        if (taped == false && !isCommander)
        {
            taped = true;
            transform.eulerAngles = transform.eulerAngles + new Vector3(0, -90, 0);
        }
        else if(taped == false)
        {
            taped = true;
        }
        else
            facedown = false;
    }

    public bool HasFlipEffect()
    {
        return flipEffect;
    }

    public bool HasTapEffect()
    {
        return tapEffect;
    }

    public void SetTile(Tile newTile)
    {
        tile = newTile;
    }

    public Tile GetTile()
    {
        return tile;
    }

    public void SetTilePosition(Tile newTile)
    {
        tile = newTile;
        if(mover == null)
            mover = GetComponent<CardMover>();
        mover.SetStartPos(tile);
        tile.SetUnit(this);
        tile.SetEdge(true);
    }

    public bool GetHasMoved()
    {
        return hasMoved;
    }

    public void SetHasMoved(bool new_moved)
    {
        hasMoved = new_moved;
    }

    public void SetOwner(GameObject new_owner)
    {
        owner = new_owner;
    }

    public GameObject GetOwner()
    {
        return owner;
    }

    public void NewTurn()
    {
        taped = false;
        hasMoved = false;
    }

    public void TakeDamage(int dmg)
    {
        health -= dmg;
        Card_Text temp = GetComponent<Card_Text>();
        if (temp != null)
            temp.SetHealth(health, health < start_health);

        if (health <= 0)
            Die();
    }

    public void SetStats(int att, int hp)
    {
        attack += att;
        health += hp;
        Card_Text temp = GetComponent<Card_Text>();
        if (temp != null)
        {
            temp.SetHealth(health, health < start_health);
            temp.SetAttack(attack, attack > start_attack);
        }
    }

    public int GetAttack()
    {
        return attack;
    }

    public bool IsUnit()
    {
        return isUnit;
    }

    public bool IsCommander()
    {
        return isCommander;
    }

    public bool IsSpell()
    {
        return isSpell;
    }

    public bool IsTrap()
    {
        return isTrap;
    }

    public bool IsItem()
    {
        return isItem;
    }

    public bool IsDead()
    {
        return isDead;
    }

    public void Die()
    {
        isDead = true;
        Destroy(gameObject);
    }
}
