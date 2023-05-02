using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowdownZone : MonoBehaviour
{
    [SerializeField] private SlowEffectData slowEffectData;

    private SlowEffect effect;
    private List<Collider2D> slowedCharacters = new();

    private void Awake()
    {
        effect = new(slowEffectData);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (slowedCharacters.Contains(collision))
        {
            return;
        }        

        if (collision.TryGetComponent(out IEffectable effectableCharacter) == false)
        {
            return;
        }

        effectableCharacter.EffectManager.AddEffect(effect);
        slowedCharacters.Add(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (slowedCharacters.Contains(collision))
        {
            collision.GetComponent<IEffectable>().EffectManager.RemoveEffect(effect);
            slowedCharacters.Remove(collision);
        }
    }

    private void OnDestroy()
    {
        foreach(Collider2D slowedCharacter in slowedCharacters)
        {
            slowedCharacter.GetComponent<IEffectable>().EffectManager.RemoveEffect(effect);
        }

        slowedCharacters.Clear();
    }
}
