using System.Collections;
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
        animator.Play("Vibration");
    }
    private void FixedUpdate()
    {
        animatorClips = this.animator.GetCurrentAnimatorClipInfo(0);
        if (animatorClips[0].clip.name == "Start")
            CamChanged();
    }
    private void CamChanged()
    {
        mainCam.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
