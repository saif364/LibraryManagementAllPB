using Microsoft.AspNetCore.Http;

public static class FileHelper
{
    public static string SaveFile(IFormFile file, string uploadPath)
    {
        if (file == null || file.Length == 0)
            return "";
        if (!Directory.Exists(uploadPath))
            Directory.CreateDirectory(uploadPath);
        var uniqueFileName = GenerateUniqueFileName(file.FileName);
        var fileName = Path.GetFileName(uniqueFileName);
        var filePath = Path.Combine(uploadPath, uniqueFileName);

        using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
        {
            file.CopyTo(stream);
        }
        return uniqueFileName;
    }

    public static bool  DeleteFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            return true;
        }
        return false;
    }

    public static FileStream FileStream(string filePath)
    {
        if (File.Exists(filePath))
        {
            return new FileStream(filePath, FileMode.Open, FileAccess.Read);
        }

        return null;
    }

    public static string GetFilePath(string uploadPath, string fileName)
    {
        return Path.Combine(uploadPath, fileName);
    }

    public static string GetContentType(string fileName)
    {
        var fileExtension = Path.GetExtension(fileName).ToLower();

        switch (fileExtension)
        {
            case ".pdf":
                return "application/pdf";
            case ".jpg":
            case ".jpeg":
                return "image/jpeg";
            case ".png":
                return "image/png";
            case ".txt":
                return "text/plain";
            case ".doc":
                return "application/msword";
            case ".docx":
                return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            default:
                return "application/octet-stream";
        }
    }

    public static string GenerateUniqueFileName(string fileName)
    {
        return Guid.NewGuid()+ fileName;
    }
} 
