using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Code.Runtime.Infrastructure.Windows;
using FluentAssertions;
using NUnit.Framework;

namespace Code.Tests.OlfEditorTests.Common
{
  [TestFixture]
  public class EnumUniqueTests
  {
    [Test]
    public void EnumTypeIdsNumericValuesShouldBeUnique()
    {
      foreach(Type enumType in GetAllEnumTypesInProject())
      {
        IEnumerable<int> enumValues = Enum.GetValues(enumType).Cast<int>();
        enumValues.Should().OnlyHaveUniqueItems($"Each {enumType.Name} enum value should be unique.");
      }
    }

    private static IEnumerable<Type> GetAllEnumTypesInProject() =>
      Assembly
        .GetAssembly(typeof(WindowTypeId))
        .GetTypes()
        .Where(type => type.IsEnum);
  }
}