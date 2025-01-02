using System.ComponentModel.DataAnnotations;

namespace NekoBoiNick.FFXIV.DalamudPlugin.SoupCatUtils.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class StringRangeAttribute<T> : ValidationAttribute where T : struct, Enum {
  public List<string> AllowableValues { get; set; } = [];

  public StringRangeAttribute(int min, int max, params string[] allowableValues) {
    AllowableValues.AddRange(allowableValues);
    for (var i = min; i <= max; i++) {
      AllowableValues.Add(i.ToString());
    }
  }

  public StringRangeAttribute(params string[] allowableValues) {
    AllowableValues.AddRange(allowableValues);
    foreach (var item in Enum.GetValues<T>().Select(item => item.ToDescriptionString()).Where(item => item is not null)) {
      if (item is null) {
        continue;
      }
      AllowableValues.Add(item);
    }
  }

  protected override ValidationResult IsValid(object? value, ValidationContext validationContext) {
    if (AllowableValues?.Contains(value?.ToString() ?? "null") == true) {
      return ValidationResult.Success!;
    }

    var msg = $"Please enter one of the allowable values: {string.Join(", ", AllowableValues ?? ["No allowable values found"])}.";
    return new ValidationResult(msg);
  }
}
