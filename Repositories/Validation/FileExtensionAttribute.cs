﻿using System.ComponentModel.DataAnnotations;

namespace shoes_final_exam.Repositories.Validation
{
	public class FileExtensionAttribute : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (value is IFormFile file)
			{
				var extension = Path.GetExtension(file.FileName);
				string[] extensions = { "jpg", "png", "jpeg" };

				bool result = extension.Any(x => extension.EndsWith(x));

				if (!result)
				{
					return new ValidationResult("Chỉ cho phép tệp jpg hoặc png hoặc jpeg");
				}
			}

			return ValidationResult.Success;
		}
	}
}
