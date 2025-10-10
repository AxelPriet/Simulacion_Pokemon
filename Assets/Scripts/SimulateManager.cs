using System.Collections.Generic;
using UnityEngine;

public class SimulateManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float secondsPerIteration = 0.01f;
    private float time = 1f;

    [Header("Entidades")]
    public List<Pokemons> pokemones = new List<Pokemons>();
    public List<Entrenador> entrenadores = new List<Entrenador>();
    public List<HierbaAlta> zonasHierba = new List<HierbaAlta>();

    void Start()
    {

        // Evita que los objetos caigan debido a los coliders asignados 
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.gravityScale = 0; 
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        // Buscar todas las entidades en la escena
        Pokemons[] foundPokemones = FindObjectsByType<Pokemons>(FindObjectsSortMode.InstanceID);
        pokemones = new List<Pokemons>(foundPokemones);

        Entrenador[] foundEntrenadores = FindObjectsByType<Entrenador>(FindObjectsSortMode.InstanceID);
        entrenadores = new List<Entrenador>(foundEntrenadores);

        HierbaAlta[] foundHierbas = FindObjectsByType<HierbaAlta>(FindObjectsSortMode.InstanceID);
        zonasHierba = new List<HierbaAlta>(foundHierbas);

        Debug.Log($"Simulación iniciada con {pokemones.Count} Pokémon salvajes, {entrenadores.Count} entrenadores y {zonasHierba.Count} zonas de hierba.");
    }

    void Update()
    {
        time += Time.deltaTime;

        if (time >= secondsPerIteration)
        {
            time = 0f;
            Simulate();
        }
    }

    void Simulate()
    {
        // Hierba alta 
        foreach (HierbaAlta hierba in zonasHierba)
        {
            if (hierba != null)
                hierba.Simulate(secondsPerIteration);
        }

        //Pokémon salvajes
        foreach (Pokemons p in pokemones)
        {
            if (p != null)
                p.Simulate(secondsPerIteration);
        }

        //Entrenadores
        foreach (Entrenador e in entrenadores)
        {
            if (e != null)
                e.Simulate(secondsPerIteration);
        }
    }
}
