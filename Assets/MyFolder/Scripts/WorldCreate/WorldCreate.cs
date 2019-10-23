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
    private MapType[,] _roomArray { get; set; }
    public List<EnemyOrb> enemyes { get; set; } = new List<EnemyOrb>();
    System.Random rand = new System.Random(Environment.TickCount);
    public GameObject smallRoom;
    public GameObject outerWall;
    public GameObject block;
    public GameObject enemy;
    public GameObject[] others;

    public Text text;
    
    int horizon, vertical; //迷宮作成時の横(horizon)と縦(vertical)の長さ
    int enemyCount;
    int randomOthersX, randomOthersZ;
    int randomCreate = 0;               //敵・壁などの生成時に使う乱数
    public static int materialType = 0; //壁と床の色を決める乱数

    void Start()
    {
        horizon = SelectStage.horizon;
        vertical = SelectStage.vertical;
        enemyCount = SelectStage.enemyCreateCount;
        if (SelectStage.type == 6) materialType = rand.Next(0, 6);
        else materialType = SelectStage.type;
        //AudioManager.Instance.PlayBGM("Main1");
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
        for (int x = 0; x < horizon; x++)
        {
            for (int y = 0; y < vertical; y++)
            {
                GameObject floor = Instantiate(smallRoom, new Vector3(x * 10, 0, y * 10), Quaternion.identity);
                _roomArray[x * 2 + 1, y * 2 + 1] = MapType.room;
            }
        }
        //外壁の生成
        for (int x = 0; x < _roomArray.GetLength(0); x++)
        {
            for (int y = 0; y < _roomArray.GetLength(1) ; y++)
            {
                //外壁を生成しないところをスキップする
                if((x%2==0 && y%2==0) || (x % 2 == 1 && y % 2 == 1))
                {
                    continue;
                }
                if(!(x == 0 || x == _roomArray.GetLength(0)-1 || y == 0 || y == _roomArray.GetLength(1)-1))
                {
                    continue;
                }
                //外壁を生み出す
                Instantiate(outerWall, new Vector3(x * 5 - 5, 1.25f, y * 5 - 5), Quaternion.identity);
                SetMapType(x, y,MapType.wall);
            }
        }
        //内壁の生成
        for (int x = 0; x < _roomArray.GetLength(0); x++)
        {
            for (int y = 0; y < _roomArray.GetLength(1) ; y++)
            {
                //内壁を生成しないところをスキップする
                if((x%2==0 && y%2==0) || (x % 2 == 1 && y % 2 == 1))
                {
                    continue;
                }
                if((x == 0 || x == _roomArray.GetLength(0)-1) || (y == 0 || y == _roomArray.GetLength(1)-1))
                {
                    continue;
                }
                //内壁をランダムに生み出す
                randomCreate = rand.Next(0, 3);
                if (randomCreate != 1)
                {
                    Instantiate(block, new Vector3(x * 5 - 5, 1.25f, y * 5 - 5), Quaternion.identity);
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
            SetMapType(room,MapType.enemyRoom);
        }

        for(int i = 0; i < others.Length; i++)
        {
            randomOthersX = rand.Next(0, horizon);
            randomOthersZ = rand.Next(0, vertical);
            Instantiate(others[i], new Vector3(randomOthersX * 10, 0, randomOthersZ * 10), Quaternion.identity);
        }

    }

    private void Update()
    {
        text.text = "";
        for (int y = _roomArray.GetLength(1) - 1; y >= 0 ; y--)
        {
            string s = "";
            for (int x = 0; x < _roomArray.GetLength(0); x++)
            {
                switch (GetMapType(x, y))
                {
                    case MapType.wall:
                        s += "か";
                        break;

                    case MapType.room:
                        s += "へ";
                        break;

                    case MapType.empty:
                        s += "〇";
                        break;

                    case MapType.enemyRoom:
                        s += "敵";
                        break;
                }
            }
            text.text += s + "\n";
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
        for (int x = 0; x < _roomArray.GetLength(0); x++)
        {
            for (int y = 0; y < _roomArray.GetLength(1); y++)
            {
                //部屋の位置を取得
                if (GetMapType(x,y) == MapType.room)
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

    public MapType GetMapType(Vector2Int position)
    {
        return _roomArray[position.x, position.y];
    }

    public MapType GetMapType(int x, int y)
    {
        return _roomArray[x, y];
    }

    public void SetMapType(Vector2Int position, MapType type)
    {
        _roomArray[position.x, position.y] = type;
    }
    public void SetMapType(int x, int y, MapType type)
    {
        _roomArray[x, y] = type;
    }
}
