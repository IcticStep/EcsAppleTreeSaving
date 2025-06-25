using System;
using Code.Runtime.Infrastructure.Windows;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine;

namespace Code.Tests.OlfEditorTests.UI
{
  [TestFixture]
  public class WindowsConfigTests 
  {
    private Runtime.Infrastructure.Windows.Configs.WindowsConfig _windowsConfig;
  
    [SetUp]
    public void SetUp() =>
      _windowsConfig = Resources.Load<Runtime.Infrastructure.Windows.Configs.WindowsConfig>("Configs/UI/WindowsConfig");

    [Test]
    public void AllWindowsConfigKeysShouldBeValidWindowTypeIdEnums()
    {
      if(_windowsConfig.Windows.Count == 0)
      {
        Assert.Pass();
        return;
      }
      
      _windowsConfig
        .Windows
        .Keys
        .Should()
        .OnlyContain(
          key => Enum.IsDefined(typeof(WindowTypeId), key),
          because: $"All keys in the dictionary should be valid {nameof(WindowTypeId)} enum values.");
    }
  }
}