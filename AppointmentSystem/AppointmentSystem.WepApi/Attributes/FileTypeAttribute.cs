using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

[AttributeUsage(AttributeTargets.Property)]
public class FileTypeAttribute : ValidationAttribute
{
    private readonly string[] _allowedTypes;

    public FileTypeAttribute(params string[] allowedTypes)
    {
        _allowedTypes = allowedTypes;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var file = value as IFormFile;
        if (file == null)
            return ValidationResult.Success;

        // MIME tipini kiçik hərflə yoxlayırıq
        var fileType = file.ContentType.ToLower();

        // Elastik yoxlama: jpeg həm image/jpeg, həm image/jpg, həm image/pjpeg qəbul edilir
        bool isValid = _allowedTypes.Any(t =>
            fileType.Contains(t.Replace("image/", "").ToLower()) // jpeg, png
        );

        if (!isValid)
        {
            return new ValidationResult($"Only the following file types are allowed: {string.Join(", ", _allowedTypes)}");
        }

        return ValidationResult.Success;
    }
}
