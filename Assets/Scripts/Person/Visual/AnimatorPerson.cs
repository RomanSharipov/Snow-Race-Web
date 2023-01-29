using DG.Tweening;
using UnityEngine;

public class AnimatorPerson : MonoBehaviour
{
    private const string SlowRun = "SlowRun";
    private const string FastRun = "FastRun";

    [SerializeField] private ParticleSystem _effect;

    private Animator _animator;
    private static readonly int FallingDown = Animator.StringToHash("FallingDown");
    private static readonly int Climbing = Animator.StringToHash("Climbing");
    private static readonly int Jump = Animator.StringToHash("Jump");
    private static readonly int JumpTime = Animator.StringToHash("JumpTime");
    private static readonly int SillyDancing = Animator.StringToHash("SillyDancing");
    private static readonly int Idle = Animator.StringToHash("Idle");

    private void Start()
    {
        _animator = GetComponent<Animator>();

        if (_effect != null)
            PlayStop();
    }

    public void SlowRunState()
    {
        PlayStop();
        _animator.Play(SlowRun);
    }

    public void FastRunState()
    {
        PlayEffect();
        _animator.Play(FastRun);
    }

    public void JumpState(float time)
    {
        _animator.Play(Jump);

        DOTween.To(() => _animator.GetFloat(JumpTime), x => _animator.SetFloat(JumpTime, x), 1, time)
            .OnKill(() => _animator.SetFloat(JumpTime, 0));
    }

    public void FallingDownState()
    {
        PlayStop();
        _animator.Play(FallingDown);
    }

    public void PlayIdle()
    {
        _animator.Play(Idle);
    }

    public void ClimbingState() => _animator.Play(Climbing);

    public void SillyDancingState()
    {
        Player player = GetComponentInParent<Player>();

        if (player != null)
            PlayStop();

        _animator.Play(SillyDancing);
    }

    private void PlayStop()
    {
        if (_effect != null)
            _effect.gameObject.SetActive(false);
    }

    private void PlayEffect()
    {
        if (_effect != null)
            _effect.gameObject.SetActive(true);
    }
}
