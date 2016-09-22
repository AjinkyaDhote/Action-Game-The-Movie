/*
	This script is placed in public domain. The author takes no responsibility for any possible harm.
	Contributed by Jonathan Czeck
*/
using UnityEngine;
using System.Collections;

public class LightningBolt : MonoBehaviour
{
    
    public int zigs = 100;
    public float speed = 1f;
    public float scale = 1f;

    private Transform endLightTransform;
    private Transform target;
    private Perlin noise;
    private float oneOverZigs;

    private ParticleSystem myParticleSystem;
    private ParticleSystem.Particle[] particles;
    private int numberOfParticles;

    void Start()
    {
        target = GameObject.Find("PayLoad").GetComponent<Transform>().FindChild("EndLightPosition");
        endLightTransform = transform.GetChild(1).transform;
        myParticleSystem = GetComponent<ParticleSystem>();

        oneOverZigs = 1f / zigs;
        //particleSystem.emit = false;

        myParticleSystem.Emit(zigs);
        particles = new ParticleSystem.Particle[myParticleSystem.maxParticles];
        numberOfParticles = myParticleSystem.GetParticles(particles);
    }

    void Update()
    {
        if (noise == null)
            noise = new Perlin();

        float timex = Time.time * speed * 0.1365143f;
        float timey = Time.time * speed * 1.21688f;
        float timez = Time.time * speed * 2.5564f;

        for (int i = 0; i < particles.Length; i++)
        {
            Vector3 position = Vector3.Lerp(transform.position, target.position, oneOverZigs * i);
            Vector3 offset = new Vector3(noise.Noise(timex + position.x, timex + position.y, timex + position.z),
                                        noise.Noise(timey + position.x, timey + position.y, timey + position.z),
                                        noise.Noise(timez + position.x, timez + position.y, timez + position.z));
            position += (offset * scale * (i * oneOverZigs));

            particles[i].position = position;
            particles[i].startColor = Color.white;
            particles[i].lifetime = 1f;
        }

        myParticleSystem.SetParticles(particles, numberOfParticles);

        if (myParticleSystem.particleCount >= 2)
        {
            //if (startLight)
            //    startLight.transform.position = particles[0].position;
            if (endLightTransform)
                endLightTransform.position = particles[particles.Length - 1].position;
        }
    }
}