using System;
using System.IO;

public class CredentialsLoader
{
    public static string LoadApiKeyPolygon()
    {
        string filePath = "credentials.txt"; // Path to your credentials file
        
        try
        {
            // Read all lines from the file
            string[] lines = File.ReadAllLines(filePath);

            // Loop through each line to find the API key
            foreach (string line in lines)
            {
                if (line.StartsWith("API_KEY_Polygon="))
                {
                    // Extract the API key value
                    string apiKey = line.Substring("API_KEY_Polygon=".Length).Trim();
                    return apiKey;
                }
            }

            // If API key is not found, handle it accordingly
            throw new InvalidOperationException("API key not found in the credentials file.");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Error reading credentials file: {ex.Message}");
            return null; // Handle the error as needed
        }
    }
        public static string LoadApiKeyFinnhub()
    {
        string filePath = "credentials.txt"; // Path to your credentials file
        
        try
        {
            // Read all lines from the file
            string[] lines = File.ReadAllLines(filePath);

            // Loop through each line to find the API key
            foreach (string line in lines)
            {
                if (line.StartsWith("API_KEY_Finnhub="))
                {
                    // Extract the API key value
                    string apiKey = line.Substring("API_KEY_Finnhub=".Length).Trim();
                    return apiKey;
                }
            }

            // If API key is not found, handle it accordingly
            throw new InvalidOperationException("API key not found in the credentials file.");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Error reading credentials file: {ex.Message}");
            return null; // Handle the error as needed
        }
    }
}
