using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using FFXIVClientStructs.Havok;

namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Utils;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class StringRangeAttribute : ValidationAttribute {
  public List<string> AllowableValues { get; set; } = new();

  public StringRangeAttribute(params string[] allowableValues) {
    AllowableValues.AddRange(allowableValues);
  }

  public StringRangeAttribute(int min, int max, params string[] allowableValues) {
    AllowableValues.AddRange(allowableValues);
    for (int i = min; i <= max; i++) {
      AllowableValues.Add($"{i}");
    }
  }

  public StringRangeAttribute(Type type, params string[] allowableValues) {
    AllowableValues.AddRange(allowableValues);
    int lastIndex = allowableValues.Length;
    foreach (object item in Enum.GetValues(type)) {
      AllowableValues.Add(((Enum)item).ToDescriptionString());
    }
  }

  protected override ValidationResult IsValid(object? value, ValidationContext validationContext) {
    if (AllowableValues?.Contains(value?.ToString() ?? "null") == true) {
      return ValidationResult.Success!;
    }

    string msg = $"Please enter one of the allowable values: {string.Join(", ", AllowableValues ?? new() { "No allowable values found" })}.";
    return new ValidationResult(msg);
  }
}
