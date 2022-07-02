using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    Animator animator;
    public District district;
    Rigidbody rb;
    bool isRunning;
    bool isWalking;
    float runSpeed = 3;
    float range = 2;
    bool hasDest = false;
    Vector3 currentDest;
    float seeEnemyRange = 5;
    Soldier attacking;
    protected float health = 20;
    public float damage = 10;
    float attackCooldown = 1.5f;
    float worth = 10;
    public Collider col;
    public Soldier soldier;
    Color color;
    bool playerCommand = false;
    Vector3 playerOrderPos;
    bool inSelection = false;
    void Start()
    {
        soldier = this;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        color = Initializer.colors[teamNumber];
        InvokeRepeating("updateDest", 1.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        updatePosition();
    }

    void updateDest()
    {
        if(playerCommand) return;
        if (attacking != null) return;
        else CancelInvoke("attack");
        Soldier soldier = findNearestEnemyInRange();
        if (soldier == null)
        {
            clearDestination();
        }
        else if (inRange(soldier))
        {
            clearDestination();
            attacking = soldier;
            InvokeRepeating("attack", 0, attackCooldown);
        }
        else
        {
            setDestination(soldier.transform.position);
        }
    }

    void attack()
    {
        animator.SetTrigger("attack");
        StartCoroutine(attackDelay());
    }

    IEnumerator attackDelay()
    {
        yield return new WaitForSeconds(.7f);
        if (attacking != null)
        {
            attackEnemy(attacking);
        }
    }

    void attackEnemy(Soldier enemy)
    {
        enemy.getAttacked(this);
    }


    virtual protected void getAttacked(Soldier enemy)
    {
        takeDamage(enemy.damage, false);
        if (checkDead())
        {
            enemy.killed(this);
            die();
        }
    }

    protected void takeDamage(float damage, bool checkDead = true)
    {
        health -= damage;
        if (checkDead && health <= 0)
        {
            Debug.Log("Destroying");
            die();
        }
    }

    protected bool checkDead()
    {
        if (health <= 0) return true;
        else return false;
    }

    void die()
    {
        Destroy(this.gameObject);
        if(inSelection){
            SoldierMover.selectedSoldiers.Remove(this);
        }
    }

    protected void killed(Soldier enemy)
    {
        district.capital.player.earnMoney(enemy.worth);
    }

    public void killedDistrict(District district){
        district.districtSwapTeam(teamNumber);
        district.districtSoldier.health = 100;
        attacking = null;
    }

    void lookAt(Vector3 target)
    {
        transform.LookAt(new Vector3(target.x, transform.position.y, target.z));
    }

    void updatePosition()
    {
        if(playerCommand) executePlayerOrder();
        else if (hasDest)
        {
            move();
        }
    }

    void move()
    {
        moveTowards(currentDest, runSpeed);
    }

    bool inRange(Soldier target)
    {
        return Vector3.Distance(transform.position, target.transform.position) < range + Mathf.Max(target.col.bounds.extents.x, target.col.bounds.extents.y);
    }

    void setDestination(Vector3 destination)
    {
        currentDest = destination;
        hasDest = true;
        updateRunning(true);
    }

    void clearDestination()
    {
        hasDest = false;
        updateRunning(false);
    }

    void updateWalking(bool b)
    {
        isWalking = b;
        animator.SetBool("isWalking", b);
    }

    void updateRunning(bool b)
    {
        isRunning = b;
        animator.SetBool("isRunning", b);
    }

    void moveTowards(Vector3 position, float speed)
    {
        lookAt(position);
        transform.position = Vector3.MoveTowards(transform.position, position, speed * Time.deltaTime);
    }

    Soldier findNearestEnemyInRange()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, seeEnemyRange);
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Collider col in colliders)
        {
            if (isEnemySoldierCollider(col))
            {
                float dist = Vector3.Distance(col.transform.position, currentPos);
                if (dist < minDist)
                {
                    tMin = col.transform;
                    minDist = dist;
                }
            }
            else if (isEnemyDistrictCollider(col))
            {
                float dist = Vector3.Distance(col.transform.position, currentPos);
                float districtWeightedDist = dist * 1.2f;
                if (districtWeightedDist < minDist)
                {
                    tMin = col.transform;
                    minDist = districtWeightedDist;
                }
            }
        }
        if (tMin == null)
        {
            return null;
        }
        return tMin.GetComponent<Soldier>();
    }

    bool isEnemySoldierCollider(Collider col)
    {
        if (col.tag == "Soldier")
        {
            if (col.GetComponent<Soldier>().teamNumber != teamNumber)
            {
                return true;
            }
        }
        return false;
    }

    bool isEnemyDistrictCollider(Collider col)
    {
        if (col.tag == "District")
        {
            if (col.GetComponent<District>().teamNumber != teamNumber)
            {
                return true;
            }
        }
        return false;
    }

    public void selected(){
        setSoldierColor(color * .4f);
        soldier.inSelection = true;
    }

    public void deselected(){
        setSoldierColor(color);
        soldier.inSelection = false;
    }

    public void setSoldierColor(Color color){
        foreach(SkinnedMeshRenderer mesh in GetComponentsInChildren<SkinnedMeshRenderer>()){
            mesh.material.color = color;
        }
    }

    public void playerOrder(Vector3 position){
        playerCommand = true;
        updateRunning(true);
        playerOrderPos = position;
    }

    void executePlayerOrder(){
        moveTowards(playerOrderPos, runSpeed);
        if(isNear(playerOrderPos)){
            playerCommand = false;
            updateRunning(false);
        }
    }

    bool isNear(Vector3 position){
        return Vector3.Distance(transform.position, position) < 3;
    }

    public int teamNumber { get => district.capital.player.teamNumber; set { district.capital.player.teamNumber = value; } }
}
