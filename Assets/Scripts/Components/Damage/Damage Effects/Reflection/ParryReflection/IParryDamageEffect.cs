using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IParryDamageEffect
{
    public UnityEvent SuccessfulParryEvent { get; }
}
