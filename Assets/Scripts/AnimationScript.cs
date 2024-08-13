using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    private Animator characterAnimator;
    void Start()
    {
        characterAnimator = GetComponent<Animator>();
    }
    public void JumpAnimation()
    {
        characterAnimator.Play("Jump");
    }
}
