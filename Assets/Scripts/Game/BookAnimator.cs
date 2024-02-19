using UnityEngine;

public class BookAnimator : MonoBehaviour
{
    [SerializeField] private Animator bookAnimation;

    void Start()
    {
        bookAnimation = bookAnimation.GetComponent<Animator>();
    }

    public void bookUpend()
    {
        bookAnimation.Play("Upend");
        bookAnimation.SetBool("Play",false);
    }

}
