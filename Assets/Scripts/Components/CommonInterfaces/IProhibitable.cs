using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IProhibitable
{
    public void Prohibit(object prohibitor);
    public void Allow(object prohibitor);
    public bool IsProhibited { get; }
    public UnityEvent ProhibitionEvent { get; }
    public UnityEvent AllowanceEvent { get; }
}
