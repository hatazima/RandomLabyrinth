using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using System;

public enum EnemyState
{
    idle = 0,
    move
}

public class EnemyOrb : MonoBehaviour
{
    public Vector2Int currentPosition { get; set; }
    public WorldCreate wc { get; set; }
    public GameObject enemyOrbAttack;
    public float moveSpped = 1;
    System.Random rand = new System.Random(Environment.TickCount);
    GameObject player;
    Vector3 nextRoomPosition;
    Vector2Int nextRoom;
    float time = 0, goTiem = 2;
    int hp = 4;
    EnemyState currentState = 0;
    Action<EnemyState> changeEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerBullet"))
        {
            hp--;
            if (hp == 0)
            {
                player.GetComponent<PlayerAttack>().BreakCountPlus();
                Destroy(gameObject);
            }
        }
        if (other.gameObject.CompareTag("PlayerSword"))
        {
            hp -= 2;
            if (hp == 0)
            {
                player.GetComponent<PlayerAttack>().BreakCountPlus();
                Destroy(gameObject);
            }
        }
    }

    void Start()
    {
        player = FindObjectOfType<ThirdPersonCharacter>().gameObject;
        changeEvent = (state) =>
        {
            if (state == EnemyState.idle) return;
            nextRoom = SelectRoom();
            nextRoomPosition = wc.GetRoomPosition(nextRoom);
        };
    }
    
    void Update()
    {
        time += Time.deltaTime;
        
        //プレイヤーとの距離が5未満になったら
        if (AttackRange() < 5)
        {
            Attack();
        }
        if(currentState == EnemyState.idle)
        {
            ChangeState(EnemyState.move);
        }
        else if(transform.position == nextRoomPosition && currentState == EnemyState.move)
        {
            //wc.roomArray[currentPosition.y, currentPosition.x] = MapType.enemyRoom;
            currentPosition = nextRoom;
            ChangeState(EnemyState.idle);
        }

        


        switch(currentState)
        {
            case EnemyState.idle:

                break;
            case EnemyState.move:
                //EnemyMove();
                break;

        }

    }

    //プレイヤーとの距離を測る
    float AttackRange()
    {
        Vector3 p1 = this.transform.position;
        Vector3 p2 = player.transform.position;
        Vector3 dir = p1 - p2;
        float attackRange = dir.magnitude;
        return attackRange;
    }

    void Attack()
    {
        //一度攻撃をしたら2秒間隔を開ける
        if (time > goTiem)
        {
            //弾のプレファブからインスタンスを作る
            GameObject obj = Instantiate(enemyOrbAttack, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
            //弾の発射方向を求める
            Vector3 vec = (player.transform.position - this.transform.position).normalized;
            //弾を発射する
            obj.GetComponent<Rigidbody>().velocity = vec * 10;
            //１秒後に弾を消去する
            Destroy(obj, 1);
            time = 0;
        }
    }

    //次に行く部屋をランダムに選ぶ
    Vector2Int SelectRoom()
    {
        Vector2Int nextRoom;
        List<Vector2Int> rooms = new List<Vector2Int>();
        rooms = wc.GetMovableRoom(currentPosition);
        if(rooms.Count <= 0) return currentPosition;
        nextRoom = rooms[rand.Next(rooms.Count)];
        //wc.roomArray[nextRoom.y, nextRoom.x] = MapType.enemyRoom;
        return nextRoom;
    }

    void EnemyMove()
    {

        transform.position = Vector3.MoveTowards(transform.position, nextRoomPosition, moveSpped * Time.deltaTime);
    }

    void ChangeState(EnemyState state)
    {
        if(currentState == state)
        {
            return;
        }
        if(changeEvent != null)changeEvent(state);
        currentState = state;
    }
}
