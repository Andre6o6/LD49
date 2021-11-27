using System;

public class Trait
{
    public string TraitName { get; }
    public string TraitDescription { get; }
    
    private readonly Func<Minister, bool> _condition;
    private readonly Action<TraitControlledData> _executeApply;
    private readonly Action<TraitControlledData> _executeUnapply;
    private bool _applied;

    public Trait(string traitName, string traitDescription, Action<TraitControlledData> applyCallback)
    {
        TraitName = traitName;
        TraitDescription = traitDescription;
        _executeApply = applyCallback;
    }
    
    public Trait(string traitName, string traitDescription,
        Action<TraitControlledData> applyCallback, 
        Action<TraitControlledData> unapplyCallback, 
        Func<Minister, bool> condition)
    {
        TraitName = traitName;
        TraitDescription = traitDescription;
        
        _condition = condition;
        _executeApply = applyCallback;
        _executeUnapply = unapplyCallback;
    }

    public void OnStart(TraitControlledData data)
    {
        if (_condition == null) _executeApply(data);
    }

    public void OnEveryTurn(Minister minister)
    {
        if (_condition == null) return;

        if (_condition(minister) && !_applied)
        {
            _executeApply(minister.Personality);
            _applied = true;
        }

        if (!_condition(minister) && _applied)
        {
            _executeUnapply(minister.Personality);
            _applied = false;
        }
    }
}
