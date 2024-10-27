using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class LighningtBoltController : MonoBehaviour
{
    // Start is called before the first frame update
    private LineRenderer lineRenderer;
    [SerializeField] private Texture[] textures;
    private int animationStep;
    public float framesPerSecond = 30f;
    private float fpsCounter;
    private List<Transform> chainTargets = new List<Transform>();
    private Transform startTarget;
    private int maxTargets;
    private float dmgCooldown;
    private float dmgTimer = 0f;
    private int damage;
    private float lifetime = 0f;
    private float maxLifetime;

    void Start()
    {
        this.lineRenderer = GetComponent<LineRenderer>();
        this.lineRenderer.positionCount = 2;
        this.startTarget = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSprites();
        UpdateTargets();
        DamageTargets();

        this.lifetime += Time.deltaTime;
        if (this.lifetime >= this.maxLifetime)
        {
            Destroy(this.gameObject);
        }
    }

    public void Setup(int maxTargets, float cooldown, int damage, float maxTime)
    {
        this.maxTargets = maxTargets;
        this.dmgCooldown = cooldown;
        this.damage = damage;
        this.maxLifetime = maxTime;
    }

    private void DamageTargets()
    {
        dmgTimer += Time.deltaTime;
        if(dmgTimer > dmgCooldown)
        {
            dmgTimer = 0f;
            foreach (Transform target in this.chainTargets)
            {
                target.GetComponent<EnemyCombat>().TakeDamage(this.damage);
            }
            
        }
    }

    private void UpdateTargets()
    {
        this.chainTargets.Clear();
        GameObject [] targets = GetClosestEnemies(startTarget.position);

        // end lightning attack if no enemys left
        if(targets == null)
        {
            Destroy(this.gameObject);
            return;
        }

        int lineRendererMaxPoints = 1;
        if(targets.Length < maxTargets)
        {
            lineRendererMaxPoints += targets.Length;
        }
        else
        {
            lineRendererMaxPoints += maxTargets;
        }

        Vector3 [] positions = new Vector3[lineRendererMaxPoints];

        positions[0] = startTarget.position;
        // set i to 1 so 0 is always startPoint
        for(int i = 1; i < lineRendererMaxPoints; i+=1)
        {
            this.chainTargets.Add(targets[i-1].transform);
            positions[i] = this.chainTargets[i-1].GetComponent<SpriteRenderer>().bounds.center;
        }

        lineRenderer.positionCount = lineRendererMaxPoints;
        lineRenderer.SetPositions(positions);
    }

    private void UpdateSprites()
    {
        fpsCounter += Time.deltaTime;
        if(fpsCounter >= 1f / framesPerSecond)
        {
            animationStep += 1;
            if(animationStep == textures.Length)
            {
                animationStep = 0;
            }
            lineRenderer.material.SetTexture("_MainTex", textures[animationStep]);
            fpsCounter = 0f;
        }
    }

    private GameObject [] GetClosestEnemies(Vector3 referencePoint)
    {
        GameObject [] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0)
        {
            return null;
        }
        
        // Sort enemies Array by Distance
        System.Array.Sort( enemies, (a, b) => (int)Mathf.Sign(Vector3.Distance(referencePoint, a.transform.position) - Vector3.Distance(referencePoint, b.transform.position)));

        return enemies;
    }
}
