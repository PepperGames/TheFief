using UnityEngine;
using Zenject;

public class X2ExtraPointsEffect : Effect
{
    //[Inject] [SerializeField] private ScoreManager _scoreManager;

    public override void Activate()
    {
        //Debug.Log(_scoreManager);
        //_scoreManager.OnCollectPoint += Execute;
    }

    public override void Deactivate()
    {
        //_scoreManager.OnCollectPoint -= Execute;
    }

    private void Execute(int count)
    {
        //_scoreManager.ChangeScore(count);
    }

    public class Factory : PlaceholderFactory<X2ExtraPointsEffect>
    {
    }
}
