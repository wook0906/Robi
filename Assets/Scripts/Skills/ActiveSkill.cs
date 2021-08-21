using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActiveSkill : MonoBehaviour
{
    protected BaseController _owner;
    protected Transform _muzzleTransform;
    protected Transform _parent;

    protected string _name;
    protected string _description;

    public virtual void Init(BaseController owner, Transform muzzleTransform, Transform parent = null)
    {
        _owner = owner;
        _muzzleTransform = muzzleTransform;
        _parent = parent;
    }
    public abstract bool UseSkill();
}
