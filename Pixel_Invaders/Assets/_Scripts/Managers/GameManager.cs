using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviour {
    private UIManager ui;
    private MasterSpawn spawn;
    private PlayerControl pc;
    private BuildingMover[] buildings;

    private string levelDataFile = "/_Data/levels.json";
    private LevelData[] levels;

    private static int level = 0;
    private int score = 0;
    private int stamina = 100;
    private int progress = 0;
    private bool isGameOver;

    public float getTiredInterval = 0.2f;
    public float gainProgressInterval = 0.4f;

    private Coroutine currentGetTiredCoroutine;
    private Coroutine currentGainProgressCoroutine;

	// Use this for initialization
	void Start () {
        ui = FindObjectOfType<UIManager>();
        if (!ui)
        {
            Debug.Log("Failed to load UI Manager in GameManager");
        }
        pc = FindObjectOfType<PlayerControl>();
        if (!pc)
        {
            Debug.Log("Failed to find player control in GameManager");
        }
        buildings = FindObjectsOfType<BuildingMover>();
        spawn = GetComponent<MasterSpawn>();

        LoadLevelData();

        StartLevel(level);
	}

    void LoadLevelData()
    {
        string filePath = Application.dataPath + levelDataFile;
        if (File.Exists(filePath))
        {
            string levelJson = File.ReadAllText(filePath);
        }else
        {
            Debug.Log("Failed to open level json file");
        }
    }

    void StartLevel(int level)
    {
        AddStamina(100);
        AddProgress(-100);
        ui.ShowLevelLabel(level + 1);
        StartCoroutine(HideLevel());

        pc.EnableMovement();

        if (currentGetTiredCoroutine != null)
        {
            StopCoroutine(currentGetTiredCoroutine);
        }
        
        
        if (currentGainProgressCoroutine != null)
        {
            StopCoroutine(currentGainProgressCoroutine);
        }

        currentGetTiredCoroutine = StartCoroutine(GetTired(getTiredInterval));
        currentGainProgressCoroutine = StartCoroutine(GainProgress(gainProgressInterval));
    }

    IEnumerator HideLevel()
    {
        
        yield return new WaitForSeconds(3.0f);
        ui.HideLevelLabel();
    }

    public void AddScore(int increment)
    {
        score += increment * (level + 1);
        ui.UpdateScore(score);
    }

    public void AddStamina(int increment)
    {
        stamina += increment;
        ui.SetStaminaSlider(stamina);
        if (stamina <= 0)
        {
            GameOver();
        }else if (stamina > 100)
        {
            stamina = 100;
        }

    }

    public void AddProgress(int increment)
    {
        progress += increment;
        ui.SetProgressSlider(progress);
        if (progress >= 100)
        {
            progress = 100;
            nextLevel();
        }else if (progress < 0)
        {
            progress = 0;
        }
    }

    IEnumerator GetTired(float interval)
    {
        while (!isGameOver)
        {
            AddStamina(-1);
            yield return new WaitForSeconds(interval);
        }
    }

    IEnumerator GainProgress(float interval)
    {
        while (!isGameOver)
        {
            AddProgress(1);
            yield return new WaitForSeconds(interval);
        }
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }

    public void GameOver()
    {
        level = 0;
        isGameOver = true;
        spawn.StopSpawing();
        spawn.StopMoving();
        ui.ShowGameOver();
        BackgroundScroller.DisableScrolling();
        foreach(BuildingMover building in buildings)
        {
            building.DisableMovement();
        }
        pc.DisableMovement();
        pc.DisableCollsion();
        pc.gameObject.SetActive(false);
    }

    public void Restart()
    {
        score = 0;
        ui.UpdateScore(score);
        SceneManager.LoadScene("level");
    }

	void nextLevel(){
        level++;
        StartLevel(level);
	}

    public static int GetLevel(){
        return level;
    }

    public void GotoTitle()
    {
        SceneManager.LoadScene("main_menu");
    }

    public void MakeGhostsVulnerable()
    {
        spawn.MakeGhostsVulnerable();
    }
}
