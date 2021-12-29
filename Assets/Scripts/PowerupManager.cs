using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    public static PowerupManager Instance;

    public List<PowerupType> activePowerups;
    public Dictionary<PowerupType, bool> powerupsCurrentlyAvailable;
    public List<GameObject> powerupList;

    [SerializeField] GameObject powerupBluePrefab;
    [SerializeField] GameObject powerupRedPrefab;
    [SerializeField] GameObject powerupYellowPrefab;
    [SerializeField] GameObject powerupGreenPrefab;
    [SerializeField] GameObject powerupHolder;

    List<float> powerupsDurationRemaining;

    float powerupDuration = 5f;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    void Start()
    {
        activePowerups = new List<PowerupType>();
        powerupList = new List<GameObject>();
        powerupsDurationRemaining = new List<float>();
        powerupsCurrentlyAvailable = new Dictionary<PowerupType, bool>()
        {
            { PowerupType.DoubleJump, false }
        };
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < powerupsDurationRemaining.Count; i++)
        {
            powerupsDurationRemaining[i] -= Time.fixedDeltaTime;
            if (powerupsDurationRemaining[i] <= 0)
            {
                GameObject tempPowerup = powerupHolder.transform.GetChild(i).gameObject;
                Destroy(tempPowerup);
                powerupList.RemoveAt(i);
                powerupsDurationRemaining.RemoveAt(i);
            }
        }
    }

    public void NewPowerupPickedUp(PowerupType newPowerup)
    {
        activePowerups.Add(newPowerup);

        if (newPowerup == PowerupType.DoubleJump)
        {
            if (!powerupsCurrentlyAvailable[PowerupType.DoubleJump])
            {
                GameObject instantiatedPowerup = Instantiate<GameObject>(powerupBluePrefab, powerupHolder.transform);
                powerupList.Add(instantiatedPowerup);
                float tempDuration = powerupDuration;
                powerupsDurationRemaining.Add(tempDuration);
                powerupsCurrentlyAvailable[PowerupType.DoubleJump] = true;
            }
            else
            {

            }
        }
    }

    public enum PowerupType
    {
        DoubleJump
    }

}
