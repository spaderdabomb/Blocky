using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    public static PowerupManager Instance;

    public List<Powerup.PowerupType> activePowerups;
    public List<GameObject> powerupList;
    public Dictionary<Powerup.PowerupType, bool> powerupsCurrentlyAvailable;
    public Dictionary<Powerup.PowerupType, GameObject> powerupPrefabDict;

    [SerializeField] GameObject powerupDoubleJumpPrefab;
    [SerializeField] GameObject powerupFlyPrefab;
    [SerializeField] GameObject powerupShieldPrefab;
    [SerializeField] GameObject powerupRapidFirePrefab;
    [SerializeField] GameObject powerupHolder;

    [SerializeField] GameObject shieldPrefab;
    [SerializeField] GameObject player;
    [SerializeField] GameObject shield;

    List<float> powerupsDurationRemaining;

    float powerupDuration = 10f;

    private void Awake()
    {
        // Initialize singleton
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Instance = this;

        // Initialize self-dependent variables
        activePowerups = new List<Powerup.PowerupType>();
        powerupList = new List<GameObject>();
        powerupsDurationRemaining = new List<float>();
        powerupsCurrentlyAvailable = new Dictionary<Powerup.PowerupType, bool>()
        {
            { Powerup.PowerupType.DoubleJump, false },
            { Powerup.PowerupType.Fly, false },
            { Powerup.PowerupType.Shield, false },
            { Powerup.PowerupType.RapidFire, false }
        };
        powerupPrefabDict = new Dictionary<Powerup.PowerupType, GameObject>()
        {
            { Powerup.PowerupType.DoubleJump, powerupDoubleJumpPrefab },
            { Powerup.PowerupType.Fly, powerupFlyPrefab },
            { Powerup.PowerupType.Shield, powerupShieldPrefab },
            { Powerup.PowerupType.RapidFire, powerupRapidFirePrefab }
        };
    }

    void Start()
    {

    }

    void Update()
    {
        for (int i = 0; i < powerupsDurationRemaining.Count; i++)
        {
            // Update powerup parameters
            powerupsDurationRemaining[i] -= Time.deltaTime;
            Animator tempAnimator = powerupList[i].GetComponent<Animator>();
            tempAnimator.SetFloat("timeRemaining", powerupsDurationRemaining[i]);

            // Handle powerup ending
            if (powerupsDurationRemaining[i] <= 0)
            {
                RemovePowerupAtIndex(i);
            }
        }
    }

    private void FixedUpdate()
    {

    }

    public void NewPowerupPickedUp(Powerup.PowerupType newPowerup)
    {

        if (!powerupsCurrentlyAvailable[newPowerup])
        {
            activePowerups.Add(newPowerup);
            GameObject instantiatedPowerup = Instantiate<GameObject>(powerupPrefabDict[newPowerup], powerupHolder.transform);
            Animator tempAnimator = instantiatedPowerup.GetComponent<Animator>();
            tempAnimator.SetFloat("timeRemaining", powerupDuration);
            powerupList.Add(instantiatedPowerup);
            float tempDuration = powerupDuration;
            powerupsDurationRemaining.Add(tempDuration);
            powerupsCurrentlyAvailable[newPowerup] = true;

            shield.SetActive(true);
        }
        else
        {
            int powerupIndex = activePowerups.IndexOf(newPowerup);
            float tempDuration = powerupDuration;
            powerupsDurationRemaining[powerupIndex] = tempDuration;
        }
    }

    public void RemovePowerupAtIndex(int index)
    {
        if (activePowerups[index] == Powerup.PowerupType.Shield)
        {
            shield.SetActive(false);
        }

        GameObject tempPowerup = powerupHolder.transform.GetChild(index).gameObject;
        Destroy(tempPowerup);
        powerupList.RemoveAt(index);
        activePowerups.RemoveAt(index);
        powerupsDurationRemaining.RemoveAt(index);
    }

    public void ClearAllPowerups()
    {
        for (int i = 0; i < powerupsDurationRemaining.Count; i++)
        {
            RemovePowerupAtIndex(i);
        }
    }

    public int GetIndexFromPowerup(Powerup.PowerupType powerupType)
    {
        int powerupIndex = activePowerups.IndexOf(powerupType);

        return powerupIndex;
    }
}
