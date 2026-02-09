using System.ComponentModel.DataAnnotations;

namespace AppointmentSystem.WebApi.Attributes;

public class FileLengthAttribute : ValidationAttribute
{
    private readonly int _maxLength;
    public FileLengthAttribute(int maxMb)
    {
        _maxLength = maxMb * 1024 * 1024;
    }
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var list = new List<IFormFile>();
        var file = value as IFormFile;
        if (file != null)
        {
            list.Add(file);
        }
        var files = value as List<IFormFile>;
        if (files != null)
        {
            list.AddRange(files);
        }
        foreach (var f in list)
        {
            if (f.Length > _maxLength)
            {
                return new ValidationResult($"File size must be less than {_maxLength / (1024 * 1024)} MB");
            }
        }
        return ValidationResult.Success;

    }
}
