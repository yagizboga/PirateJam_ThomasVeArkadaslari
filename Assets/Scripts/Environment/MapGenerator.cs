using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] List<GameObject> maps;
    [SerializeField] AudioClip accelerationSound;
    [SerializeField] AudioClip brakeSound;
    List<GameObject> activemaps;
    Grid grid;
    int count = 1;
    GameObject gas;
    float coalBalance;

    private float speedInit = 0f;
    private float acceleration = 2f;
    private float maxSpeed = 10f;
    private float finalSpeed = 0f;
    private float deceleration = 2.5f;
    private bool wasMoving = false;
    private bool isBraking = false; 
    private AudioSource audioSource;

    void Start()
    {
        gas = GameObject.FindGameObjectWithTag("Gas");
        activemaps = new List<GameObject>();
        grid = new Grid(2, 1, 1000);
        activemaps.Add(Instantiate(maps[0], grid.GetMap(1, 0), Quaternion.identity, this.transform));
        coalBalance = GameObject.FindGameObjectWithTag("Oven").GetComponent<CoalEconomy>().GetCoalBalance();

    
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

    }

    public void GenerateNextMap()
    {
        activemaps.Add(Instantiate(maps[count], grid.GetMap(0, 0), Quaternion.identity, this.transform));
        count += 1;
    }

    void FixedUpdate()
    {
        if (GameObject.FindGameObjectWithTag("repairtrigger").GetComponent<RepairTrigger>().isBroken == false)
        {
            float activeGas = gas.GetComponent<gas>().getactivegas();
            coalBalance = GameObject.FindGameObjectWithTag("Oven").GetComponent<CoalEconomy>().GetCoalBalance();

            if (activeGas <= 0 || coalBalance <= 0)
            {
                isBraking = true;
                speedInit -= deceleration * Time.deltaTime; 
                if (speedInit < 0) speedInit = 0;
            }
            else
            {
                isBraking = false;
                float gasFactor = activeGas / 100f; 
                float coalFactor = coalBalance / 100f; 

                speedInit += acceleration * gasFactor * coalFactor * Time.deltaTime;
                if (speedInit > maxSpeed) speedInit = maxSpeed;
            }

            finalSpeed = 4 * speedInit * Mathf.Clamp(coalBalance / 100f, 0.25f, 1f) * Mathf.Clamp(gas.GetComponent<gas>().getactivegas() / 100f, 0.5f, 1f);
            transform.position += new Vector3(finalSpeed * Time.deltaTime, 0, 0);

            Debug.Log($"SpeedInit: {speedInit}, FinalSpeed: {finalSpeed}, Gas: {activeGas}, Coal: {coalBalance}");

            HandleTrainSounds(); 
        }
        else
        {
            isBraking = true;
            speedInit -= deceleration * Time.deltaTime; 
            if (speedInit < 0) speedInit = 0;
            finalSpeed = 0;
            gas.GetComponent<gas>().setgas0();
            HandleTrainSounds();
        }
    }

    void LateUpdate()
    {
        if (activemaps.Count >= 3)
        {
            Destroy(activemaps[0]);
            activemaps.RemoveAt(0);
        }
    }

    void HandleTrainSounds()
    {
        bool isMoving = finalSpeed > 0;

        if (wasMoving && !isMoving) 
        {
            PlaySound(brakeSound, true);
        }
        else if (!wasMoving && isMoving)
        {
            PlaySound(accelerationSound, false);
        }

        if (finalSpeed == 0 && audioSource.isPlaying) 
        {
            audioSource.Stop(); 
        }

        wasMoving = isMoving;
    }

    void PlaySound(AudioClip clip, bool loop)
    {
        if (clip != null)
        {
            if (audioSource.isPlaying && audioSource.clip == clip) return; 
            audioSource.Stop(); 
            audioSource.clip = clip;
            audioSource.loop = loop;
            audioSource.Play();
        }
    }
}