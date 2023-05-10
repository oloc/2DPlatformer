using UnityEngine;

// On d�clare les trois �tats du personnage joueur
public enum PlayerState
{
    GROUNDED,
    JUMPING,
    FALLING,
}

public class StateMachine : MonoBehaviour
{
    private PlayerState _currentState;          // On stocke l'�tat courant dans une variable globale

    private Animator _animator;                 // On stocke les composants Animator
    private Player _player;                     // et Player dans des variables globales

    private void Awake()
    {
        _animator = GetComponent<Animator>();   // On mets en cache nos composants Animator
        _player = GetComponent<Player>();       // et Player
    }
    private void Update()
    {
        OnStateUpdate(_currentState);           // Update de l'�tat en cours
    }

    // M�thode appel�e lorsque l'on entre dans un �tat
    private void OnStateEnter(PlayerState state)
    {
        switch (state)
        {
            case PlayerState.GROUNDED:
                OnEnterGrounded();
                break;

            case PlayerState.JUMPING:
                OnEnterJump();
                break;

            case PlayerState.FALLING:
                OnEnterFall();
                break;

            default:
                Debug.LogError("OnStateEnter: Invalid state " + state.ToString());
                break;
        }
    }
    // M�thode appel�e � chaque frame dans un �tat
    private void OnStateUpdate(PlayerState state)
    {
        switch (state)
        {
            case PlayerState.GROUNDED:
                OnUpdateGrounded();
                break;

            case PlayerState.JUMPING:
                OnUpdateJump();
                break;

            case PlayerState.FALLING:
                OnUpdateFall();
                break;

            default:
                Debug.LogError("OnStateUpdate: Invalid state " + state.ToString());
                break;
        }
    }
    // M�thode appel�e lorsque l'on sort dans un �tat
    private void OnStateExit(PlayerState state)
    {
        switch (state)
        {
            case PlayerState.GROUNDED:
                OnExitGrounded();
                break;

            case PlayerState.JUMPING:
                OnExitJump();
                break;

            case PlayerState.FALLING:
                OnExitFall();
                break;

            default:
                Debug.LogError("OnStateExit: Invalid state " + state.ToString());
                break;
        }
    }
    // M�thode appel�e pour passer d'un �tat � un autre
    private void TransitionToState(PlayerState toState)
    {
        OnStateExit(_currentState);
        _currentState = toState;
        OnStateEnter(_currentState);
    }


    // Ce qu'il se passe lorsque l'on entre dans l'�tat GROUNDED
    private void OnEnterGrounded()
    {
        _animator.SetBool("isGrounded", true);
        _player.ResetJumpCounter();
    }
    // Ce qu'il se passe � chaque frame dans l'�tat GROUNDED
    private void OnUpdateGrounded()
    {
        _animator.SetFloat("HorizontalSpeed", _player.GetHorizontalVelocity());
        if (_player.CanJump())
        {
            TransitionToState(PlayerState.JUMPING);
            return;
        }
        if (!_player.IsGrounded())
        {
            TransitionToState(PlayerState.FALLING);
            return;
        }
    }
    // Ce qu'il se passe lorsque l'on sort de l'�tat GROUNDED
    private void OnExitGrounded()
    {
        _animator.SetBool("isGrounded", false);
    }


    // Ce qu'il se passe lorsque l'on entre dans l'�tat JUMPING
    private void OnEnterJump()
    {
        _animator.SetBool("isJumping", true);
        _player.Jump();
    }
    // Ce qu'il se passe � chaque frame dans l'�tat JUMPING
    private void OnUpdateJump()
    {
        if (_player.CanJump())
        {
            TransitionToState(PlayerState.JUMPING);
            return;
        }
        if (_player.GetVerticalVelocity() < 0f)
        {
            TransitionToState(PlayerState.FALLING);
            return;
        }
    }
    // Ce qu'il se passe lorsque l'on sort de l'�tat JUMPING
    private void OnExitJump()
    {
        _animator.SetBool("isJumping", false);
    }


    // Ce qu'il se passe lorsque l'on entre dans l'�tat FALLING
    private void OnEnterFall()
    {
        _animator.SetBool("isFalling", true);
    }
    // Ce qu'il se passe � chaque frame dans l'�tat FALLING
    private void OnUpdateFall()
    {
        if (_player.CanJump())
        {
            TransitionToState(PlayerState.JUMPING);
            return;
        }
        if (_player.IsGrounded())
        {
            TransitionToState(PlayerState.GROUNDED);
            return;
        }
    }
    // Ce qu'il se passe lorsque l'on sort de l'�tat FALLING
    private void OnExitFall()
    {
        _animator.SetBool("isFalling", false);
    }
}