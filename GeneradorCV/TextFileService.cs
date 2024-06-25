using System;
using System.IO;
using System.Threading.Tasks;

public class TextFileService
{
    private readonly string _filePath;

    public TextFileService(string fileName)
    {
        // Obtener la ruta completa del archivo en la carpeta de datos de la aplicación
        _filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), fileName);
    }

    public async Task WriteTextAsync(string text)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(_filePath, false)) // false para sobrescribir el archivo
            {
                await writer.WriteAsync(text);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al escribir en el archivo: {ex.Message}");
        }
    }

    public async Task<string> ReadTextAsync()
    {
        try
        {
            using (StreamReader reader = new StreamReader(_filePath))
            {
                return await reader.ReadToEndAsync();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al leer el archivo: {ex.Message}");
            return string.Empty;
        }
    }
}
