using ManageBooks.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace ManageBooks.Domain.Validation;

public class DomainValidation
{
    public static void NotNull(object? target, string fieldName)
    {
        if (target == null)
        {
            throw new EntityValidationException(
                $"{fieldName} should not be null"
            );
        }
    }

    public static void NotNullOrEmpty(string? target, string fieldName)
    {
        if (String.IsNullOrWhiteSpace(target))
        {
            throw new EntityValidationException(
                $"{fieldName} should not be empty or null"
            );
        }
    }

    public static void MaxLength(string target, int maxLength, string fieldName)
    {
        if (target.Length > maxLength)
        {
            throw new EntityValidationException(
                $"{fieldName} should not be greater than {maxLength} characters long"
            );
        }
    }

    public static void MinLength(string target, int minLength, string fieldName)
    {
        if (target.Length < minLength)
        {
            throw new EntityValidationException(
                $"{fieldName} should not be less than {minLength} characters long"
            );
        }
    }

    public static void ValidEmail(string target, string fieldName)
    {
        string emailPattern = @"^\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Z|a-z]{2,}\b$";

        if (Regex.IsMatch(target, emailPattern, RegexOptions.IgnoreCase) == false)
        {
            throw new EntityValidationException($"{fieldName} should be a valid email");
        }
    }

    public static void ValidYear(int targer, string fieldName)
    {
        if(targer < 1900 || targer > DateTime.Now.Year)
        {
            throw new EntityValidationException($"{fieldName} should be a valid year");
        }
    }

    public static void MaxValue(int target, int maxValue, string fieldName)
    {
        if(target > maxValue)
        {
            throw new EntityValidationException(
                $"{fieldName} should not be greater than {maxValue} value"
            );
        }
    }

    public static void MinValue(int target, int minValue, string fieldName)
    {
        if(target < minValue)
        {
            throw new EntityValidationException(
                $"{fieldName} should not be less than {minValue} value"
            );
        }
    }
}
