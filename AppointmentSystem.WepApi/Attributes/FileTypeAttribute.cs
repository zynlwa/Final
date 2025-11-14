using System.ComponentModel.DataAnnotations;

namespace AppointmentSystem.WebApi.Attributes;
public class FileTypeAttribute : ValidationAttribute
{
    private readonly string[] _type;
    public FileTypeAttribute(params string[] type)
    {
        _type = type;
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
            if (!_type.Any(t => f.ContentType.Contains(t)))
            {
                return new ValidationResult($"File type must be one of the following: {string.Join(", ", _type)}");
            }
        }
        return ValidationResult.Success;
    }

}
