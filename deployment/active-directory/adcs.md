# Active Directory Certificate Services (ADCS) for SOCIMP

## Overview
This document outlines the implementation of Active Directory Certificate Services (ADCS) for the SOCIMP project. The primary goal of setting up a Certificate Authority (CA) server is to eliminate the browser's red warning when SOCIMP domain users access the iRedMail server. Additionally, this setup helps simulate ADCS attacks for security testing purposes. 

This is not a step-by-step guide for installing Windows Server and ADCS components, as such guides are widely available online. Instead, it focuses on the challenges encountered during the setup and how they were resolved.

## Setup

### Pre-requisites
Before issuing a certificate for the SOCIMP Mail Server, ensure the following prerequisites are met:

- **Domain Name Record**: Create a DNS record for the iRedMail server on the Domain Controller. For example:
  - `mail.socimp.local` pointing to `10.0.5.20`.

- **Hostname Configuration**: Set the hostname correctly on the iRedMail server. For example:
  - `127.0.0.1 mail.socimp.local`. Refer to the [iRedMail installation guide](https://docs.iredmail.org/install.iredmail.on.debian.ubuntu.html) for more details.

- **ADCS Installation**: Ensure ADCS is installed on a Windows Server.

- **Web Server Template**: Configure a Web Server Template in ADCS.

*At this point, SOCIMP domain users can access the RoundCube WebMail interface by navigating to `mail.socimp.local`. However, they will encounter a red warning due to the unauthorized certificate.*

### Issuing a Certificate for iRedMail
To remove the red warning, follow these steps:

1. **Create a Custom Certificate Request**:
   - Navigate to `Certificate -> Personal -> Certificates` and create a custom `.req` file.
   - Include `mail.socimp.local` in the Subject Alternative Name (SAN).
   - Ensure the key file is exportable.

2. **Request and Download the Certificate**:
   - Open a browser and navigate to `<ADCS Server IP Address>/certsrv`.
   - Request the certificate and download the `.cer` file along with the certificate chain (`.p7x` file).

3. **Import and Export the Certificate**:
   - Import the `.cer` file into the Personal Certificate store.
   - Export the private key of the `.cer` file to obtain `.cer`, `.p7x`, and `.pfx` files.

4. **Transfer Files to the iRedMail Server**:
   - Move the `.cer`, `.p7x`, and `.pfx` files to the iRedMail server using a suitable method (e.g., FTP or HTTP). For example:
     - Start a Python HTTP server: `python3 -m http.server`.
     - Use `curl` on the iRedMail server to download the files.

5. **Transform the Certificate**:
   - Run the following OpenSSL commands on the iRedMail server:
     ```bash
     openssl pkcs12 -in certkey.pfx -clcerts -nokeys -out cert.crt
     openssl pkcs12 -in certkey.pfx -nocerts -out encrypted.key
     openssl rsa -in encrypted.key -out cert.key
     openssl pkcs12 -in certkey.pfx -out cert.pem -nodes
     ```

6. **Replace Existing Certificate Files**:
   - Backup the existing certificate files:
     ```bash
     mv /etc/ssl/certs/iRedMail.crt{,.bak}
     mv /etc/ssl/private/iRedMail.key{,.bak}
     ```
   - Copy the new `.crt` and `.key` files to their respective locations.

7. **Restart Services**:
   - Restart the necessary services to apply the changes:
     ```bash
     systemctl restart postfix dovecot nginx mysql
     ```
     *(Adjust the services based on your environment.)*

### Testing
- Log in as a random user in the SOCIMP domain.
- Open a browser and navigate to `mail.socimp.local`.
- Verify that the red warning is no longer displayed and the certificate is working correctly.

![Certificate Details](/images/active-directory/adcs2.png)

Details of the certificate:

![Certificate Details Expanded](/images/active-directory/adcs3.png)

## References
- [Microsoft Docs - ADCS](https://docs.microsoft.com/en-us/windows-server/identity/ad-cs/)
- [MITRE ATT&CK - ADCS Exploitation](https://attack.mitre.org/techniques/T1649/)
- [Chain of Trust](https://en.wikipedia.org/wiki/Chain_of_trust)
- Various security research papers on PKI security


