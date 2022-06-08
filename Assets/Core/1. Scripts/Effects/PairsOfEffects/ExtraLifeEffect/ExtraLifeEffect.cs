using UnityEngine;
using Zenject;

public class ExtraLifeEffect : Effect
{
    //[Inject] [SerializeField] private LivesManager _livesManager;
    private int _lives;

    public void Activate(int lives)
    {
        _lives = lives;
        Activate();
    }

    public override void Activate()
    {
        Debug.Log(_lives);
        //_livesManager.AddLife(_lives);
    }

    public override void Deactivate()
    {
    }

    public class Factory : PlaceholderFactory<ExtraLifeEffect>
    {
    }
}
