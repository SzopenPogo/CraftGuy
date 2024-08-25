using UnityEngine;

public abstract class BaseState<T> : State where T : StateMachine
{
    protected T StateMachine { get; private set; }

    //Animation
    private const int MainAnimatorLayerIndex = 0;
    private const float AnimatorInstantDampTime = 0f;

    public BaseState(T stateMachine)
    {
        StateMachine = stateMachine;
    }

    #region Animation

    #region Animation Managment
    protected void SetAnimation(string animationName, float transitionTime)
    {
        SetAnimation(animationName, transitionTime, MainAnimatorLayerIndex);
    }

    protected void SetAnimation(string animationName, float transitionTime, int layerIndex)
    {
        StateMachine.Animator.CrossFade(Animator.StringToHash(animationName), transitionTime, layerIndex);
    }

    protected float GetNormalizedTime(string animationName) =>
        GetNormalizedTime(animationName, MainAnimatorLayerIndex);

    protected float GetNormalizedTime(string animationTag, int layerIndex)
    {
        AnimatorStateInfo currentInfo = StateMachine.Animator.GetCurrentAnimatorStateInfo(layerIndex);
        AnimatorStateInfo nextInfo = StateMachine.Animator.GetNextAnimatorStateInfo(layerIndex);

        if (StateMachine.Animator.IsInTransition(layerIndex) && nextInfo.IsTag(animationTag))
        {
            return nextInfo.normalizedTime;
        }
        else if (!StateMachine.Animator.IsInTransition(layerIndex) && currentInfo.IsTag(animationTag))
        {
            return currentInfo.normalizedTime;
        }
        else
        {
            return 0f;
        }
    }
    #endregion

    #region Animator Values
    protected void SetAnimatorFloat(int animatorVariable, float value, float blendDampTime, float deltaTime)
    {
        StateMachine.Animator.SetFloat(animatorVariable, value, blendDampTime, deltaTime);
    }

    protected void SetAnimatorFloatInstant(int animatorVariable, float value, float deltaTime)
    {
        StateMachine.Animator.SetFloat(animatorVariable, value, AnimatorInstantDampTime, deltaTime);
    }

    protected float GetAnimatorFloat(int animatorVariableId)
    {
        return StateMachine.Animator.GetFloat(animatorVariableId);
    }
    #endregion

    #endregion
}
