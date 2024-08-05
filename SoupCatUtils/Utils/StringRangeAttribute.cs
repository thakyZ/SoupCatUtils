using System.ComponentModel.DataAnnotations;

namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Utils;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class StringRangeAttribute : ValidationAttribute {
  public List<string> AllowableValues { get; set; } = [];

  public StringRangeAttribute(params string[] allowableValues) {
    AllowableValues.AddRange(allowableValues);
  }

  public StringRangeAttribute(int min, int max, params string[] allowableValues) {
    AllowableValues.AddRange(allowableValues);
    for (int i = min; i <= max; i++) {
      AllowableValues.Add(i.ToString());
    }
  }

  public StringRangeAttribute(Type type, params string[] allowableValues) {
    AllowableValues.AddRange(allowableValues);
    foreach (object item in Enum.GetValues(type)) {
      AllowableValues.Add(((Enum)item).ToDescriptionString());
    }
  }

  protected override ValidationResult IsValid(object? value, ValidationContext validationContext) {
    if (AllowableValues?.Contains(value?.ToString() ?? "null") == true) {
      return ValidationResult.Success!;
    }

    string msg = $"Please enter one of the allowable values: {string.Join(", ", AllowableValues ?? ["No allowable values found"])}.";
    return new ValidationResult(msg);
  }
}
