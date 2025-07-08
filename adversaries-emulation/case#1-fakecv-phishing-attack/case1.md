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

This part was actually fun. Initially, I tried using popular tools like `msfvenom`, `Hooka`, and `ScareCrow` to bypass Windows Defender-but none of them worked reliably in my test environment. That pushed me to develop custom tools from scratch, which turned out to be both more effective and educational.

## Attack Flow

I found a great tool from MITRE to visualize attack workflows - [MITRE DEFEND CAD](https://d3fend.mitre.org/cad/)

Below is my Case #1 workflow visualized using the tool. The left side represents the Cyber Kill Chain, while the right side aligns with MITRE ATT&CK.

![case1](/images/case1/case1-mitrecad.png)

## Testing

**IMPORTANT NOTE**: *As of July 2025, my sample has been detected on some Endpoints by Microsoft Defender - probably because I uploaded it to VirusTotal and Hybrid Analysis. Microsoft's Threat Intel team stays on top of these things and regularly updates Defender signatures.*

### Infrastructure Preparation

https://github.com/user-attachments/assets/d2ce0c86-f7a6-495c-b81d-d19583b5933a

From the attacker’s perspective, a few components need to be prepared:
- The C2 channel - a simple TCP server listening on port 31102.
- A file transfer channel - a basic HTTP server listening on port 8888.
- A fake application email sent to the victim.

![pic1](/images/case1/pic1.png)

### Target: Standard Windows Workstation Protected by Microsoft Defender

Now we're on the machine of Jason Muir, a user in the SOCIMP domain, who received and opened the malicious ZIP file.

![pic2](/images/case1/pic2.png)

This machine is fully protected by Microsoft Defender.

![pic3](/images/case1/pic3.png)
![pic4](/images/case1/pic4.png)

The following video demonstrates how the Dropper operates from the victim's point of view, leading to a compromise - all while bypassing native Defender protections.

https://github.com/user-attachments/assets/ccf4c2d3-f9c7-4141-bf55-c2a082964781

This video shows how the attacker gains a reverse shell after the user clicks the malicious file and is able to run remote commands without being flagged by Defender.

https://github.com/user-attachments/assets/c1af9a50-89f9-49f2-8431-6654f053fbea



