using UnityEngine;

public class HierbaAlta : MonoBehaviour
{
    public HierbaState currentState = HierbaState.Tranquila;

    public void Simulate(float deltaTime)
    {
        switch (currentState)
        {
            case HierbaState.Tranquila:
                EsperarEncuentro();
                break;

            case HierbaState.Encuentro:
                GenerarEncuentro();
                break;
        }
    }

    void EsperarEncuentro()
    {
        // Simula probabilidad de aparici�n de Pok�mon
        if (Random.value < 0.03f)
        {
            currentState = HierbaState.Encuentro;
        }
    }

    void GenerarEncuentro()
    {
        Debug.Log("�Un Pok�mon salvaje ha aparecido!");
        currentState = HierbaState.Tranquila;
    }
}
