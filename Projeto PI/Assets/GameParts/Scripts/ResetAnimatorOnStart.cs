using UnityEngine;

public class ResetAnimatorOnStart : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private string initialStateName = "NomeDoEstadoInicial"; // Nome do estado no Animator Controller

    void Start()
    {
        animator = GetComponent<Animator>();

        // Force o Animator a reiniciar a animação no frame 0
        animator.Play(initialStateName, 0, 0f);
    }
}