using UnityEngine;
/// <summary>
/// Interface que define o comportamento de um estado da FSM.
/// </summary>
public interface IState
{
    /// <summary>
    /// Executado ao entrar no estado.
    /// </summary>
    void Enter();
    /// <summary>
    /// Executado a cada frame enquanto o estado está ativo.
    /// </summary>
    void Update();
    /// <summary>
    /// Executado ao sair do estado.
    /// </summary>
    void Exit();
    
}
