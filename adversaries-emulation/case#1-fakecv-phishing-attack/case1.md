# CASE #1 - Simulated Phishing-Based Targeted Attack

Jason Muir, an HR staff member at SOCIMP Company, receives an email from a candidate applying for a SOC Analyst position. The email contains a password-protected ZIP file with the candidate's résumé and portfolio. 

In this simulation, I crafted a realistic phishing attack to mimic common real-world scenarios.

## Components

### 1. **Dropper**
A custom C# [Dropper](/adversaries-emulation/case#1-fakecv-phishing-attack/cvdropper.cs) was created to:
- Download and open a fake résumé (PDF) to distract the victim.
- Inject shellcode (converted from the implant executable using [donut](https://github.com/TheWover/donut)) into a legitimate process (e.g., `explorer.exe`).
- Self-delete after execution to reduce visibility.

### 2. **Implant**
The implant is a custom C# ["backdoor"](/adversaries-emulation/case#1-fakecv-phishing-attack/implant.cs) capable of:
- Executing basic remote commands like `ipconfig`, `systeminfo`, `download`, and `loadexe`.
- Running entirely in memory after being converted to shellcode using `donut`.

Both the Dropper and Implant were obfuscated using **ConfuserEx** to make static analysis more difficult.

### 3. **Command and Control (C2)**
A basic [TCP-based C2 server](/adversaries-emulation/case#1-fakecv-phishing-attack/tcp_c2_server.py) was implemented in Python to communicate with the implant.

## Why Custom Tools?
Kinda fun when talk about why i did those stuffs, initially, I tried using common tools like `msfvenom`, `Hooka`, and `ScareCrow` to bypass Windows Defender but had no success. This led me to develop custom tools, which proved to be more effective and educational.

## Attack Flow 
I found out a tool from MITRE which is very cool to visualize workflow - [MITRE DEFEND CAD](https://d3fend.mitre.org/cad/)

Below is my case#1 workflow by using the tool. The left-side the Cyber Kill Chain while the right side is MITRE ATT&CK.

![case1](/images/case1/case1-mitrecad.png)

## Testing 
Demo video: 