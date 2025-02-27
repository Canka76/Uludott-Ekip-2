using UnityEngine;
using UnityEngine.Serialization;

public class ParticleFire : MonoBehaviour
{
    [SerializeField, Range(0f,1f)] private float currentIntensity = 1f;
    [SerializeField] ParticleSystem[] fireParticleSystems = null;
    [SerializeField] private float[] startIntesity = new float[0];
    public float regenDelay = 2f;
    public float regenRate = .2f;
    private float timeLastWatered = 0f;
    
    bool isLit= true;
    
    void Start()
    {
       startIntesity = new float[fireParticleSystems.Length];

       for (int i = 0; i < fireParticleSystems.Length; i++)
       {
           startIntesity[i] = fireParticleSystems[i].emission.rateOverTime.constant;
       }
    }
    
    void ChangeIntensity()
    {
        for (int i = 0; i < fireParticleSystems.Length; i++)
        {
            var emission = fireParticleSystems[i].emission;
            emission.rate = startIntesity[i] * currentIntensity;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isLit && currentIntensity < 1f &&(Time.time - timeLastWatered >= regenDelay))
        {
            currentIntensity += regenRate * Time.deltaTime;
            ChangeIntensity();
        }
    }

    public bool TryExtinguishFire(float amount)
    {
        timeLastWatered = Time.time;
        currentIntensity -= amount;
        ChangeIntensity();
        
        isLit = currentIntensity <= 0;
        return isLit;
    }
}
