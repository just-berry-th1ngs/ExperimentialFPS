using UnityEngine;
using System.Collections;

public class EnemyHitEffect : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Assign the enemy mesh renderer here.")]
    public Renderer targetRenderer;

    [Tooltip("Duration of the hit glow in seconds.")]
    public float glowDuration = 0.2f;

    [Tooltip("Intensity multiplier for the glow effect.")]
    public float glowIntensity = 2f;

    [Tooltip("Smooth fade out instead of instant cutoff.")]
    public bool useFadeOut = true;

    [Tooltip("Fade out duration (if useFadeOut is true).")]
    public float fadeOutDuration = 0.3f;

    private Material hitMaterial;
    private Color originalEmissionColor;
    private Coroutine activeGlow;
    private bool isInitialized = false;

    private void Start()
    {
        if (!Application.isPlaying) return;

        if (targetRenderer == null)
        {
            targetRenderer = GetComponentInChildren<Renderer>();
            
            if (targetRenderer == null)
            {
                Debug.LogError($"{name}: No Renderer found for EnemyHitEffect!");
                return;
            }
        }

        hitMaterial = targetRenderer.material;

        if (hitMaterial.HasProperty("_EmissionColor"))
            originalEmissionColor = hitMaterial.GetColor("_EmissionColor");
        else
            originalEmissionColor = Color.black;

        isInitialized = true;
    }

    public void TriggerHit(Color glowColor)
    {
        if (!isInitialized || hitMaterial == null) return;

        if (activeGlow != null)
            StopCoroutine(activeGlow);

        activeGlow = StartCoroutine(GlowCoroutine(glowColor));
    }

    private IEnumerator GlowCoroutine(Color glowColor)
    {
        hitMaterial.EnableKeyword("_EMISSION");

        Color targetGlow = glowColor * glowIntensity;
        
        if (hitMaterial.HasProperty("_EmissionColor"))
        {
            hitMaterial.SetColor("_EmissionColor", targetGlow);
            DynamicGI.SetEmissive(targetRenderer, targetGlow);
        }

        yield return new WaitForSeconds(glowDuration);

        if (useFadeOut)
        {
            float elapsed = 0f;
            
            while (elapsed < fadeOutDuration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / fadeOutDuration;
                
                Color currentColor = Color.Lerp(targetGlow, originalEmissionColor, t);
                
                if (hitMaterial.HasProperty("_EmissionColor"))
                {
                    hitMaterial.SetColor("_EmissionColor", currentColor);
                    DynamicGI.SetEmissive(targetRenderer, currentColor);
                }
                
                yield return null;
            }
        }

        if (hitMaterial.HasProperty("_EmissionColor"))
        {
            hitMaterial.SetColor("_EmissionColor", originalEmissionColor);
            DynamicGI.SetEmissive(targetRenderer, originalEmissionColor);
        }

        activeGlow = null;
    }
}