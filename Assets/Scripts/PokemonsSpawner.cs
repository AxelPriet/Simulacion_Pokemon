using UnityEngine;

public class PokemonSpawner : MonoBehaviour
{
    [Header("Configuraci�n de Spawner")]
    public GameObject prefabPokemon;        // Prefab del Pok�mon salvaje
    public int cantidadInicial = 1;         // Cu�ntos aparecer�n al inicio
    public float radioZona = 3f;            // Radio de aparici�n (por si la HierbaAlta no lo tiene)

    private HierbaAlta hierba;              // Zona de hierba asignada

    void Start()
    {
        hierba = GetComponent<HierbaAlta>();
        if (hierba == null)
        {
            Debug.LogWarning("No tiene componente HierbaAlta, se usar� radio local {radioZona}");
        }

        // Crear los Pok�mon al inicio
        GenerarPokemons();
    }

    void GenerarPokemons()
    {
        if (prefabPokemon == null)
        {
            Debug.LogError("No hay prefab de Pok�mon asignado en el spawner.");
            return;
        }

        int cantidad = Mathf.Max(1, cantidadInicial);

        for (int i = 0; i < cantidad; i++)
        {
            Vector2 offset = Random.insideUnitCircle * (hierba != null ? hierba.radioZona : radioZona);
            Vector3 spawnPos = transform.position + new Vector3(offset.x, offset.y, 0);
            Instantiate(prefabPokemon, spawnPos, Quaternion.identity);
        }

        Debug.Log("Se han generado Pok�mon salvajes.");
    }

    
}

