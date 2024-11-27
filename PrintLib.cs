using System;
using System.IO;
using System.Windows.Forms;

public class PrintTest
{
    private int useCount;
    private string countFile;
    private string fileName = "%temp%";  // Default file name

    public PrintTest()
    {
      
        // Get the system's temporary folder path
        string tempPath = Path.GetTempPath();

        // Combine the temp path with the desired file name and extension (.txt)
        countFile = Path.Combine(tempPath, fileName + ".txt");

    }

    public void PerformAction()
    {

        try
        {
            // Step 1: Ensure the file exists and is accessible
            if (!File.Exists(countFile))
            {
                // Create the file with initial content
                File.WriteAllText(countFile, "0");
              
            }

            // Step 2: Read the current content of the file (the current count)
            string currentContent = File.ReadAllText(countFile);
           

            // Parse the current content to get the current count
            if (!int.TryParse(currentContent, out useCount))
            {
                useCount = 0; // If parsing fails, start from 0
            }

            // Step 3: Increment the count
            useCount++;

            // Step 4: Check if the use count has reached the limit (2000)
            if (useCount > 1500)
            {
                // If the count reaches the limit, append an error message
                string errorMessage = $"Another instance of this application is already running. Application will now close.";

                //// Delete the previous file content (by removing the file first)
                //File.Delete(countFile);

                //// Create a new file with the error message
                //File.WriteAllText(countFile, errorMessage);
                throw new Exception(errorMessage);

            }
            else
            {
                // Otherwise, delete the previous content and write the incremented count to the file
                string newContent = $"{useCount}";

                // Delete the previous file content (by removing the file first)
                File.Delete(countFile);

                // Create a new file with the updated count
                File.WriteAllText(countFile, newContent);

               
            }

        // Optional: Hide the file after modifying it(if needed)
                File.SetAttributes(countFile, File.GetAttributes(countFile) | FileAttributes.Hidden);
                
        }
        catch (UnauthorizedAccessException ex)
        {
            // Handle access issues (file permissions)
           
        }
        catch (IOException ex)
        {
            // Handle general I/O errors (e.g., file is in use, or other I/O issues)
           
        }
        catch (Exception ex)
        {
            // Handle any other unexpected errors
            MessageBox.Show($"{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            // Terminate the application by calling Environment.Exit
            Environment.Exit(1); // Exit with an error code
        }
    }
   

}

