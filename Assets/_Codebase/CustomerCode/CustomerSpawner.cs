using System;
using System.Collections;
using System.Collections.Generic;
using _Codebase.Extensions;
using _CodeBase.Points;
using UnityEngine;

namespace _Codebase.CustomerCode
{
  public class CustomerSpawner : MonoBehaviour
  {
    [SerializeField] private DefaultPointsStorage _storePoints;
    [SerializeField] private DefaultPointsStorage _spawnPoints;
    [SerializeField] private SpawnerSettings _settings;

    private readonly List<Customer> _customers = new List<Customer>();

    private void Start() => StartCoroutine(SpawnCoroutine());

    private void OnDestroy() => 
      _customers.ForEach(customer => customer.Destroyed -= OnCustomerDestroy);

    private IEnumerator SpawnCoroutine()
    {
      while (true)
      {
        if(_customers.Count < _settings.MaxOnSceneAmount)
          Spawn();
        
        yield return new WaitForSeconds(_settings.SpawnFrequency.GetRandomValue());
      }
    }

    private void Spawn()
    { 
      var customer = Instantiate(_settings.Prefab, _spawnPoints.GetRandomPoint().Position, Quaternion.identity)
        .GetComponent<Customer>();
      
      customer.Initialize(_storePoints, _spawnPoints);
      customer.Destroyed += OnCustomerDestroy;
      _customers.Add(customer);
    }

    private void OnCustomerDestroy(Customer customer)
    {
      customer.Destroyed -= OnCustomerDestroy;
      _customers.Remove(customer);
    }
  }
}