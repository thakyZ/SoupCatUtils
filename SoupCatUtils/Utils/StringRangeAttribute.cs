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
  public string[] AllowableValues { get; set; }

  public StringRangeAttribute(params string[] allowableValues) {
    AllowableValues = allowableValues;
  }

  public StringRangeAttribute(int min, int max, params string[] allowableValues) {
    AllowableValues = allowableValues;
    for (int i = min; i <= max; i++) {
      AllowableValues[allowableValues.Length + (i - 1)] = $"{i}";
    }
  }

  public StringRangeAttribute(Type type, params string[] allowableValues) {
    AllowableValues = allowableValues;
    int lastIndex = allowableValues.Length;
    foreach (object item in Enum.GetValues(type)) {
      AllowableValues[lastIndex] = ((Enum)item).ToDescriptionString();
      lastIndex++;
    }
  }

  protected override ValidationResult IsValid(object? value, ValidationContext validationContext) {
    if (AllowableValues?.Contains(value?.ToString()) == true) {
      return ValidationResult.Success!;
    }

    string msg = $"Please enter one of the allowable values: {string.Join(", ", AllowableValues ?? new string[] { "No allowable values found" })}.";
    return new ValidationResult(msg);
  }
}
