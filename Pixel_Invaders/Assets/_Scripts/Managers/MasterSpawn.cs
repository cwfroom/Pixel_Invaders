using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterSpawn : MonoBehaviour {
    private bool spawning = true;
    public float spawnInterval = 0.2f;
    public int riverInterval = 100;
    public float lowerX = -1.28f;
    //public float higherX = 1.28f;
    public float initialY = 6.36f;
    public float unit = 0.65f;
    public static Dictionary<string, Stack<GameObject>> buffer = new Dictionary<string, Stack<GameObject>>();
    private static float[] initialX;

    private List<EnemyData> EnemyTypes;
    private List<CollectibleData> CollectibleTypes;
	private List<RiverData> RiverTypes;

	// Use this for initialization
	void Awake () {
        //Calculate spawning x positions
        initialX = new float[5];
        for (int i = 0; i < 5; i++)
        {
            initialX[i] = lowerX + unit * i;
        }

        //Create all enemy types here
        EnemyTypes = new List<EnemyData>();
        EnemyData goomba = new EnemyData("Goomba",50.0f, 20);
        EnemyTypes.Add(goomba);
        EnemyData spaceinvader = new EnemyData("SpaceInvader",50.0f, 30);
        EnemyTypes.Add(spaceinvader);

        CollectibleTypes = new List<CollectibleData>();
        CollectibleData coin = new CollectibleData("Coin", 50.0f, 10, 0);
        CollectibleTypes.Add(coin);
        CollectibleData banana = new CollectibleData("Banana", 50.0f, 20, 5);
        CollectibleTypes.Add(banana);
        CollectibleData cherry = new CollectibleData("Cherry", 50.0f, 25, 10);
        CollectibleTypes.Add(cherry);

		RiverTypes = new List<RiverData>();
		RiverData river = new RiverData("River",50.0f, 100);
		RiverTypes.Add(river);

        buffer = new Dictionary<string, Stack<GameObject>>();
        foreach (EnemyData e in EnemyTypes)
        {
            buffer.Add(e.name, new Stack<GameObject>());
        }
        foreach (CollectibleData c in CollectibleTypes)
        {
            buffer.Add(c.name, new Stack<GameObject>());
        }
		foreach (RiverData r in RiverTypes)
		{
			buffer.Add(r.name, new Stack<GameObject>());
		}

        StartSpawning();
        
	}

    public void StartSpawning()
    {
        spawning = true;
       
        StartCoroutine(SpawnWaves());
    }

    public void StopSpawing()
    {
        spawning = false;
    }

    public void StopMoving()
    {
        foreach (KeyValuePair<string,Stack<GameObject>> pair in buffer)
        {
            Stack<GameObject> objectBuffer = pair.Value;
            foreach (GameObject i in objectBuffer)
            {
                ObjectControl ic = i.GetComponent<ObjectControl>();
                if (ic)
                {
                    ic.DisableMovement();
                }
            }
        }
    }

    IEnumerator SpawnWaves()
    {
        
        while (spawning)
        {
            int randX = Random.Range(0, initialX.Length);
           
            Vector3 randPos = new Vector3(initialX[randX], initialY, -0.1f);

            //Since river means instant death, make sure there is an interval between two rivers
            int riverCount = 0;
            int currentLevel = GameManager.GetLevel() + 1;
            if (currentLevel > EnemyTypes.Count)
            {
                currentLevel = EnemyTypes.Count;
            }


            int typeRand = Random.Range(0, 100);
            if (typeRand < 70)
            {
                

                int randType = Random.Range(0, currentLevel);

                if (buffer[EnemyTypes[randType].name].Count > 0)
                {
                    GameObject g = buffer[EnemyTypes[randType].name].Pop();
                    g.transform.position = randPos;
                    g.SetActive(true);
                }
                else
                {
                    EnemyControl.Create(EnemyTypes[randType], randPos);

                }
                riverCount--;
            }
			else if(typeRand < 98)
            {
                int randType = Random.Range(0, CollectibleTypes.Count);
                if (buffer[CollectibleTypes[randType].name].Count > 0)
                {
                    GameObject c = buffer[CollectibleTypes[randType].name].Pop();
                    c.transform.position = randPos;
                    c.SetActive(true);
                }
                else
                {
                    CollectibleControl.Create(CollectibleTypes[randType], randPos);

                }
                riverCount--;

            }
			else
			{
                
                if (riverCount == 0 && currentLevel > 1)
                {
                    riverCount = riverInterval;
                    int randType = Random.Range(0, RiverTypes.Count);
                    if (buffer[RiverTypes[randType].name].Count > 0)
                    {
                        GameObject r = buffer[RiverTypes[randType].name].Pop();
                        r.transform.position = randPos;
                        r.SetActive(true);
                    }
                    else
                    {
                        RiverControl.Create(RiverTypes[randType], randPos);

                    }
                }
                
                
			}


            yield return new WaitForSeconds(spawnInterval);
        }
        
    }

	public void SpawnGoal(int level){
		
	}

    public static float[] GetInitialX(){
        return initialX;
    }


}
