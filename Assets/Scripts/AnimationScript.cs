using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    [SerializeField]
    private GameObject mainCam;
    private Animator animator;
    private AnimatorClipInfo[] animatorClips;
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play("Focus");
    }
    private void FixedUpdate()
    {
        animatorClips = animator.GetCurrentAnimatorClipInfo(0);
        if (animatorClips[0].clip.name == "Start")
            CamChanged();
    }
    private void CamChanged()
    {
        mainCam.SetActive(true);
        gameObject.SetActive(false);
    }
}
