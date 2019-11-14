using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.UI;

public enum MapType
{
 empty = 0,
 room,
 enemyRoom,
 wall
}

public class WorldCreate : MonoBehaviour
{
    public MapType[,] _roomArray { get; set; }
    public List<EnemyOrb> enemyes { get; set; } = new List<EnemyOrb>();
    System.Random rand = new System.Random(Environment.TickCount);
    public GameObject smallRoom; //迷宮の1マス
    public GameObject outerWall; //外壁
    public GameObject block;     //内壁
    public GameObject enemy;     //敵
    public GameObject[] others;  //その他
    
    int randomOthersX, randomOthersZ;
    int enemyCount;                     //敵の数
    int horizon, vertical;              //迷宮作成時の横(horizon)と縦(vertical)の長さ
    int randomCreate = 0;               //敵・壁などの生成時に使う乱数
    public static int type = 0;         //壁と床の色を決める乱数

    void Start()
    {
        horizon = SelectStage.horizon;             //横の長さ
        vertical = SelectStage.vertical;           //縦の長さ
        enemyCount = SelectStage.enemyCreateCount; //敵の数
        //迷宮タイプの決定
        if (SelectStage.type == 6) type = rand.Next(0, 6);
        else type = SelectStage.type;
        //BGMの決定
        switch (type)
        {
            case 0:
                AudioManager.Instance.PlayBGM("Main1");
                break;
            case 1:
                AudioManager.Instance.PlayBGM("Main2");
                break;
            case 2:
                AudioManager.Instance.PlayBGM("Main3");
                break;
            case 3:
                AudioManager.Instance.PlayBGM("Main4");
                break;
            case 4:
                AudioManager.Instance.PlayBGM("Main5");
                break;
            case 5:
                AudioManager.Instance.PlayBGM("Main6");
                break;
        }
        /* ×外×外×外×
         * 外へ壁へ壁へ外
         * ×壁×壁×壁×
         * 外へ壁へ壁へ外
         * ×壁×壁×壁×
         * 外へ壁へ壁へ外
         * ×外×外×外×
         * 部屋の大きさ＊2+1*/
        _roomArray = new MapType[horizon * 2 + 1, vertical * 2 + 1];

        //部屋の生成
        for (int y = 0; y < vertical; y++)
        {
            for (int x = 0; x < horizon; x++)
            {
                GameObject floor = Instantiate(smallRoom, new Vector3(x * 10, 0, y * 10), Quaternion.identity);
                SetMapType(x * 2 + 1, y * 2 + 1,MapType.room);
            }
        }

        //外壁の生成
        for (int y = 0; y < _roomArray.GetLength(1); y++)
        {
            for (int x = 0; x < _roomArray.GetLength(0) ; x++)
            {
                //外壁を生成しないところをスキップする
                if((y%2==0 && x%2==0) || (y % 2 == 1 && x % 2 == 1))
                {
                    continue;
                }
                if(!(y == 0 || y == _roomArray.GetLength(1)-1 || x == 0 || x == _roomArray.GetLength(0)-1))
                {
                    continue;
                }
                //外壁を生み出す
                Instantiate(outerWall, new Vector3(x * 5 - 5, 1.25f, y * 5 - 5), Quaternion.identity);
                SetMapType(x, y, MapType.wall); 
            }
        }
        //内壁の生成
        for (int y = 0; y < _roomArray.GetLength(1); y++)
        {
            for (int x = 0; x < _roomArray.GetLength(0) ; x++)
            {
                //内壁を生成しないところをスキップする
                if((y%2==0 && x%2==0) || (y % 2 == 1 && x % 2 == 1))
                {
                    continue;
                }
                if((y == 0 || y == _roomArray.GetLength(1)-1) || (x == 0 || x == _roomArray.GetLength(0)-1))
                {
                    continue;
                }
                //内壁をランダムに生み出す
                randomCreate = rand.Next(0, 3);
                if (randomCreate != 1)
                {
                    Block _block = Instantiate(
                        block,
                        new Vector3(x * 5 - 5, 1.25f, y * 5 - 5),
                        Quaternion.identity).GetComponent<Block>();
                    _block.roomPosition = new Vector2Int(x, y);
                    SetMapType(x, y, MapType.wall);
                }
            }
        }
        //敵の生成
        foreach (Vector2Int room in GetEnemySpawnRoom())
        {
            EnemyOrb spawnEnemy = Instantiate(
                enemy, 
                new Vector3((room.x - 1) / 2 * 10 , 1f, (room.y - 1) / 2 * 10), 
                Quaternion.identity
                ).GetComponent<EnemyOrb>();
            //生成と同時に部屋の位置座標を入れる
            spawnEnemy.currentPosition = room;
            spawnEnemy.wc = this;
            enemyes.Add(spawnEnemy);
            SetMapType(room, MapType.enemyRoom);
        }

        //その他をランダムに生み出す
        for(int i = 0; i < others.Length; i++)
        {
            randomOthersX = rand.Next(0, horizon);
            randomOthersZ = rand.Next(0, vertical);
            Instantiate(others[i], new Vector3(randomOthersX * 10, 0, randomOthersZ * 10), Quaternion.identity);
        }
    }

    /// <summary>
    /// 敵を生成する部屋をランダムで選ぶ
    /// </summary>
    /// <returns>ランダムに選んだ部屋</returns>
    List<Vector2Int> GetEnemySpawnRoom()
    {
        //ルームの座標一覧
        List<Vector2Int> roomIndex = new List<Vector2Int>();
        for (int y = 0; y < _roomArray.GetLength(1); y++)
        {
            for (int x = 0; x < _roomArray.GetLength(0); x++)
            {
                //部屋の位置を取得
                if (GetMapType(x, y) == MapType.room)
                {
                    roomIndex.Add(new Vector2Int(x, y));
                }
            }
        }
        // リストをランダムに並べ替える
        roomIndex = roomIndex.OrderBy(a => Guid.NewGuid()).Take(enemyCount).ToList();
        return roomIndex;
    }

    public List<Vector2Int> GetMovableRoom(Vector2Int currentPosition)
    {
        List<Vector2Int> movableRoomList = new List<Vector2Int>();
        Vector2Int[] difference = new Vector2Int[] {
            new Vector2Int(0,1),new Vector2Int(0,-1),
            new Vector2Int(1,0),new Vector2Int(-1,0),
        };

        foreach (Vector2Int diff in difference)
        {
            Vector2Int candidatePos = currentPosition + diff * 2;
            Vector2Int tempPos = currentPosition + diff;
            if (GetMapType(tempPos) == MapType.empty && 
                GetMapType(candidatePos) == MapType.room)
            {
                movableRoomList.Add(candidatePos);
            }
        }
        return movableRoomList;
    }

    public Vector3 GetRoomPosition(Vector2Int roomIndex)
    {
        return new Vector3((roomIndex.x - 1) / 2 * 10, 1f, (roomIndex.y - 1) / 2 * 10);
    }

    public void SetMapType(int x,int y, MapType type)
    {
        _roomArray[x, y] = type;
    }

    public void SetMapType(Vector2Int pos,MapType type)
    {
        _roomArray[pos.x, pos.y] = type;
    }

    public MapType GetMapType(int x,int y)
    {
        return _roomArray[x, y];
    }

    public MapType GetMapType(Vector2Int pos)
    {
        return _roomArray[pos.x, pos.y];
    }
}
