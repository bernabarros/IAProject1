using UnityEngine;

/// <summary>
/// FSM responsável por gerir os estados de um agente.
/// </summary>
public class FSM
{
    /// <summary>
    /// Estado atual ativo.
    /// </summary>
    private IState currentState;

    /// <summary>
    /// Altera o estado atual da FSM.
    /// </summary>
    /// <param name="newState">Novo estado a ativar.</param>
    public void ChangeState(IState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }

    /// <summary>
    /// Atualiza o estado atual.
    /// </summary>
    public void Update()
    {
        currentState?.Update();
    }
}
