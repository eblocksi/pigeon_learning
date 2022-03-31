using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveAction
{
    public string name;
    public float delay;
    public string tag;
    public int spawnCount;
    public string message;
}

[System.Serializable]
public class Wave
{
    public string name;
    public List<WaveAction> actions;
}
public class WaveGenerator : MonoBehaviour
{
    public float difficultyFactor = 0.9f;
    public List<Wave> waves;

    private Wave m_CurrentWave;
    public Wave CurrentWave { get { return m_CurrentWave; } }

    private float m_DelayFactor = 1.0f;
    private float[] startingPositions = new float[] {-6.75f, 0f, 6.75f};
    private int randomStartingX;

    private void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        m_DelayFactor = 1.0f;
        
        while (true)
        {
            foreach(Wave W in waves)
            {
                m_CurrentWave = W;
                foreach(WaveAction waveAction in W.actions)
                {
                    if (waveAction.delay > 0)
                    {
                        yield return new WaitForSeconds(waveAction.delay * m_DelayFactor);
                    }
                    if (waveAction.spawnCount > 0)
                    {
                        for(int i = 0; i < waveAction.spawnCount; i++)
                        {
                            GameObject obj = ObjectPooler.SharedInstance.GetPooledObject(waveAction.tag);
                            //randomStartingX = Random.Range(0, 3);
                            obj.transform.position = new Vector2(startingPositions[i], FindObjectOfType<Player>().transform.position.y + 20f);
                            obj.SetActive(true);
                        }
                    }
                }
                Debug.Log("This is wave: " + W.name);
                yield return null;
            }
            m_DelayFactor *= difficultyFactor;
            yield return null;
        }
    }
}
