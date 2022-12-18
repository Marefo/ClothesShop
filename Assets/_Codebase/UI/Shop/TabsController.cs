using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Codebase.UI.Shop
{
  public class TabsController : MonoBehaviour
  {
    [SerializeField] private List<Tab> _tabs;
    
    private void OnEnable() => _tabs.ForEach(tab => tab.Clicked += OnTabClick);
    private void OnDisable() => _tabs.ForEach(tab => tab.Clicked -= OnTabClick);

    private void Start() => _tabs.First().Open();

    private void OnTabClick(Tab clickedTab)
    {
      _tabs.ForEach(tab => tab.Close());
      clickedTab.Open();
    }
  }
}