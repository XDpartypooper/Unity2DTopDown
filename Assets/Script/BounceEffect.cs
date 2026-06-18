using System.Collections;
using UnityEngine;

public class BounceEffect : MonoBehaviour
{
    public float bounceHeight = 0.3f;
    public float bounceDuration = 0.4f;
    public int bounceCount = 2;

    public void startBounch()
    {
        StartCoroutine(BounceHandler());
    }

    private IEnumerator BounceHandler()
    {
        Vector3 StartPos = transform.position;
        float localHeight = bounceHeight;
        float localDuration = bounceDuration;

        for (int i = 0; i < bounceCount; i++)
        {
            yield return Bounce( StartPos, localHeight, localDuration / 2);
            localHeight *= 0.5f;
            localDuration *= 0.8f;
        }

        transform.position = StartPos;

        yield return null;
    }

    private IEnumerator Bounce( Vector3 StartPos, float height, float duration)
    {
        Vector3 peak = StartPos + Vector3.up * height;
        float TimeElasped = 0f;

        while (TimeElasped < duration)
        {
            transform.position = Vector3.Lerp(StartPos, peak, TimeElasped / duration);
            TimeElasped += Time.deltaTime;
            yield return null;
        }

        while (TimeElasped < duration)
        {
            transform.position = Vector3.Lerp(peak, StartPos, TimeElasped / duration);
            TimeElasped += Time.deltaTime;
            yield return null;
        }
    }

}
