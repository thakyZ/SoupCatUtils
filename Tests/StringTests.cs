using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using NUnit;
using NUnit.Compatibility;
using NUnit.Framework;
using NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Extensions;

namespace SoupCatUtils.Test;

public class Tests {
  [SetUp]
  public void Setup() {
  }
  
  [TestCase("unit-testing-with-n-unit", ExpectedResult = "unitTestingWithNUnit", Category = "Kebab Case  1", Description = "Kebab Case  to Camel Case 1")]
  [TestCase("UnitTestingWithNUnit",     ExpectedResult = "unitTestingWithNUnit", Category = "Pascal Case 1", Description = "Pascal Case to Camel Case 1")]
  [TestCase("unit_testing_with_n_unit", ExpectedResult = "unitTestingWithNUnit", Category = "Snake Case  1", Description = "Snake Case  to Camel Case 1")]
  [TestCase("Unit testing with NUnit",  ExpectedResult = "unitTestingWithNUnit", Category = "Title Case  1", Description = "Title Case  to Camel Case 2")]
  [TestCase("unit-testing-with-nunit",  ExpectedResult = "unitTestingWithNunit", Category = "Pascal Case 2", Description = "Pascal Case to Camel Case 2")]
  [TestCase("unitTestingWithNunit",     ExpectedResult = "unitTestingWithNunit", Category = "Kebab Case  2", Description = "Kebab Case  to Camel Case 2")]
  [TestCase("unit_testing_with_nunit",  ExpectedResult = "unitTestingWithNunit", Category = "Snake Case  2", Description = "Snake Case  to Camel Case 2")]
  [TestCase("Unit testing with Nunit",  ExpectedResult = "unitTestingWithNunit", Category = "Title Case  2", Description = "Title Case  to Camel Case 2")]
  [Test]
  public string ToCamelCase(string value) {
    return value.ToCamelCase();
  }
  
  [TestCase("unitTestingWithNUnit",     ExpectedResult = "unit-testing-with-n-unit", Category = "Camel Case  1", Description = "Camel Case  to Kebab Case 1")]
  [TestCase("UnitTestingWithNUnit",     ExpectedResult = "unit-testing-with-n-unit", Category = "Pascal Case 1", Description = "Pascal Case to Kebab Case 1")]
  [TestCase("unit_testing_with_n_unit", ExpectedResult = "unit-testing-with-n-unit", Category = "Snake Case  1", Description = "Snake Case  to Kebab Case 1")]
  [TestCase("Unit testing with NUnit",  ExpectedResult = "unit-testing-with-n-unit", Category = "Title Case  1", Description = "Title Case  to Kebab Case 1")]
  [TestCase("UnitTestingWithNunit",     ExpectedResult = "unit-testing-with-nunit",  Category = "Pascal Case 2", Description = "Pascal Case to Kebab Case 2")]
  [TestCase("unitTestingWithNunit",     ExpectedResult = "unit-testing-with-nunit",  Category = "Camel Case  2", Description = "Camel Case  to Kebab Case 2")]
  [TestCase("unit_testing_with_nunit",  ExpectedResult = "unit-testing-with-nunit",  Category = "Snake Case  2", Description = "Snake Case  to Kebab Case 2")]
  [TestCase("Unit testing with Nunit",  ExpectedResult = "unit-testing-with-nunit",  Category = "Title Case  2", Description = "Title Case  to Kebab Case 2")]
  [Test]
  public string ToKebabCase(string value) {
    return value.ToKebabCase();
  }

  [TestCase("unitTestingWithNunit",     ExpectedResult = "UnitTestingWithNunit", Category = "Camel Case  1", Description = "Camel Case to Pascal Case 1")]
  [TestCase("unit-testing-with-nunit",  ExpectedResult = "UnitTestingWithNunit", Category = "Kebab Case  1", Description = "Kebab Case to Pascal Case 1")]
  [TestCase("unit_testing_with_nunit",  ExpectedResult = "UnitTestingWithNunit", Category = "Snake Case  1", Description = "Snake Case to Pascal Case 1")]
  [TestCase("Unit testing with Nunit",  ExpectedResult = "UnitTestingWithNunit", Category = "Title Case  1", Description = "Title Case to Pascal Case 1")]
  [TestCase("unitTestingWithNUnit",     ExpectedResult = "UnitTestingWithNUnit", Category = "Camel Case  2", Description = "Camel Case to Snake Case  2")]
  [TestCase("unit-testing-with-n-unit", ExpectedResult = "UnitTestingWithNUnit", Category = "Kebab Case  2", Description = "Kebab Case to Snake Case  2")]
  [TestCase("unit_testing_with_n_unit", ExpectedResult = "UnitTestingWithNUnit", Category = "Snake Case  2", Description = "Snake Case to Snake Case  2")]
  [TestCase("Unit testing with NUnit",  ExpectedResult = "UnitTestingWithNUnit", Category = "Title Case  2", Description = "Title Case to Snake Case  2")]
  [Test]
  public string ToPascalCase(string value) {
    return value.ToPascalCase();
  }
  
  [TestCase("unitTestingWithNunit",     ExpectedResult = "unit_testing_with_nunit",  Category = "Camel Case  1", Description = "Camel Case  to Snake Case 1")]
  [TestCase("unit-testing-with-nunit",  ExpectedResult = "unit_testing_with_nunit",  Category = "Kebab Case  1", Description = "Kebab Case  to Snake Case 1")]
  [TestCase("UnitTestingWithNunit",     ExpectedResult = "unit_testing_with_nunit",  Category = "Pascal Case 1", Description = "Pascal Case to Snake Case 1")]
  [TestCase("Unit testing with Nunit",  ExpectedResult = "unit_testing_with_nunit",  Category = "Title Case  1", Description = "Title Case  to Snake Case 1")]
  [TestCase("unitTestingWithNUnit",     ExpectedResult = "unit_testing_with_n_unit", Category = "Camel Case  2", Description = "Camel Case  to Snake Case 2")]
  [TestCase("unit-testing-with-n-unit", ExpectedResult = "unit_testing_with_n_unit", Category = "Kebab Case  2", Description = "Kebab Case  to Snake Case 2")]
  [TestCase("UnitTestingWithNUnit",     ExpectedResult = "unit_testing_with_n_unit", Category = "Pascal Case 2", Description = "Pascal Case to Snake Case 2")]
  [TestCase("Unit testing with NUnit",  ExpectedResult = "unit_testing_with_n_unit", Category = "Title Case  2", Description = "Title Case  to Snake Case 2")]
  [Test]
  public string ToSnakeCase(string value) {
    return value.ToSnakeCase();
  }
  
  [TestCase("unitTestingWithNunit",     ExpectedResult = "Unit Testing With Nunit", Category = "Camel Case  1", Description = "Camel Case  to Title Case 1")]
  [TestCase("unit-testing-with-nunit",  ExpectedResult = "Unit Testing With Nunit", Category = "Kebab Case  1", Description = "Kebab Case  to Title Case 1")]
  [TestCase("UnitTestingWithNunit",     ExpectedResult = "Unit Testing With Nunit", Category = "Pascal Case 1", Description = "Pascal Case to Title Case 1")]
  [TestCase("unit_testing_with_nunit",  ExpectedResult = "Unit Testing With Nunit", Category = "Snake Case  1", Description = "Snake Case  to Title Case 1")]
  [TestCase("unitTestingWithNUnit",     ExpectedResult = "Unit Testing With NUnit", Category = "Camel Case  2", Description = "Camel Case  to Title Case 2")]
  [TestCase("unit-testing-with-n-unit", ExpectedResult = "Unit Testing With NUnit", Category = "Kebab Case  2", Description = "Kebab Case  to Title Case 2")]
  [TestCase("UnitTestingWithNUnit",     ExpectedResult = "Unit Testing With NUnit", Category = "Pascal Case 2", Description = "Pascal Case to Title Case 2")]
  [TestCase("unit_testing_with_n_unit", ExpectedResult = "Unit Testing With NUnit", Category = "Snake Case  2", Description = "Snake Case  to Title Case 2")]
  [Test]
  public string ToTitleCase(string value) {
    return value.ToTitleCase();
  }
}
