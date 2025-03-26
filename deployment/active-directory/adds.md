# Active Directory Domain Services (ADDS) for SOCIMP

## Overview

This document outlines the implementation of Active Directory Domain Services (ADDS) for the SOCIMP project, including the organizational structure.

This is not a step-by-step guide on installing Windows Server, promoting it to a Domain Controller, or setting up ADDS, as such information is widely available. Instead, this document focuses on the design and implementation specifics tailored for SOCIMP.

## Design Principles

The ADDS implementation follows these key principles:

- **Security:** Properly structured Organizational Units (OUs), the principle of least privilege, and Group Policy Object (GPO) hardening.
- **Scalability:** Designed to support future expansion.
- **Manageability:** Clear distinction between user and computer accounts for streamlined management.

## Organizational Hierarchy

### **Domain:** `socimp.local`

### **Top-Level OUs:**

- **SOCIMP Workstations** – Organizational units for SOCIMP user workstations (e.g., HR, IT, Accounting computers)
  - Accounting
  - HR
  - IT
  - Legal
  - Management
  - Marketing
  - Operations
  - PR
  - Purchasing

- **SOCIMP Users** – Contains SOCIMP user accounts organized by department (e.g., Timmy Snowman from Accounting)
  - Accounting
  - HR
  - IT
  - Legal
  - Management
  - Marketing
  - Operations
  - PR
  - Purchasing

- **SOCIMP Groups** – Security and distribution groups for access control and resource management
  - Accounting_Folders, Accounting_Local, Accounting_Printers
  - HR_Folders, HR_Local
  - IT_Folders, IT_Local, IT_Printers
  - Legal_Folders, Legal_Printers
  - Management_Folders, Management_Printers
  - Marketing_Folders, Marketing_Local
  - Operations_Folders, Operations_Printers
  - PR_Folders, PR_Printers
  - Purchasing_Folders, Purchasing_Printers

- **SOCIMP Servers** – Contains SOCIMP server accounts (e.g., SOCIMP Internal Root-CA Server)
- **SOCIMP Service Accounts** – Dedicated accounts for specific services (e.g., Database Admin, Certificate Authority Admin)

## Implementation Details

- **User Accounts:** Organized by department for easier permission and access control management.
- **Computer Accounts:** Segmented by department to enforce department-specific policies.
- **Group Policies:** Applied at the OU level to ensure security configurations are enforced.
- **Service Accounts:** Managed separately under `SOCIMP Service Accounts` to enhance security and control.
- **Server Management:** Dedicated `SOCIMP Servers` OU for applying stricter security measures.

## Bulk Creation of OUs, Groups, and Users Using AD Pro Toolkit

Deploying the entire AD structure manually can be tedious and time-consuming. While PowerShell scripts are commonly used for bulk creation in real-world environments, this document introduces an alternative tool: **AD Pro Toolkit** (Trial version).

AD Pro Toolkit is a powerful tool for managing and maintaining Active Directory, offering features such as bulk creation, reporting, and monitoring.

For the SOCIMP project, AD Pro Toolkit was used to automate the creation of OUs, Groups, and Users.

### Preparing Data Files

To streamline the process, necessary data is prepared in CSV files:
- **OUs.csv** – Defines the Organizational Units
- **Groups.csv** – Lists the security and distribution groups
- **socimpusers.csv** – Contains user account details

These files can be accessed [here](https://github.com/phamthanhsang-cs/SOC-in-my-Pocket/tree/main/.build/active-directory).

### Organizational Units (OUs) Creation

1. Open AD Pro Toolkit and navigate to **Other Tools → Import OUs**.
2. Click **Import**, then select `OUs.csv`.
3. Click **Run**.

![OUs Creation](https://github.com/phamthanhsang-cs/SOC-in-my-Pocket/blob/main/images/active-directory/OUs-creation.png)

To verify the import:
- Go to **Report → OUs Report → Run**.

![OUs Report](https://github.com/phamthanhsang-cs/SOC-in-my-Pocket/blob/main/images/active-directory/OUs-report.png)

You can also confirm directly in the Active Directory Users and Computers (ADUC) tool.

![AD OUs Check](https://github.com/phamthanhsang-cs/SOC-in-my-Pocket/blob/main/images/active-directory/AD-OUs-Check.png)

### Groups Creation

1. After importing OUs, navigate to **Group Tools → Bulk Create Groups**.
2. Click **Import**, then select `Groups.csv`.
3. Click **Run**.

![Groups Creation](https://github.com/phamthanhsang-cs/SOC-in-my-Pocket/blob/main/images/active-directory/Groups-creation.png)

To verify the import:
- Go to **Report → Groups Report → All Groups → Run**.

![Groups Report](https://github.com/phamthanhsang-cs/SOC-in-my-Pocket/blob/main/images/active-directory/Groups-report.png)

You can also confirm directly in ADUC.

![AD Groups Check](https://github.com/phamthanhsang-cs/SOC-in-my-Pocket/blob/main/images/active-directory/AD-Groups-Check.png)

### Users Creation

1. Navigate to **User Tools → Bulk Create Users**.
2. Click **Import**, then select `Users.csv`.
3. Click **Run**.

![Users Creation](https://github.com/phamthanhsang-cs/SOC-in-my-Pocket/blob/main/images/active-directory/Users-creation.png)

To verify the import:
- Go to **Report → Users Report → All Users → Run**.

![Users Report](https://github.com/phamthanhsang-cs/SOC-in-my-Pocket/blob/main/images/active-directory/Users-report.png)

You can also confirm directly in ADUC. Below is an example of SOCIMP's IT user accounts.

![AD User Check Example](https://github.com/phamthanhsang-cs/SOC-in-my-Pocket/blob/main/images/active-directory/AD-Users-Check.png)

## Testing

To validate the setup, a test user was selected from the SOCIMP organization. The user was able to successfully log in to the domain.

![Domain User Login Test](https://github.com/phamthanhsang-cs/SOC-in-my-Pocket/blob/main/images/active-directory/Test-AD-User.png)

The account details were also verified.

![Domain User Information](https://github.com/phamthanhsang-cs/SOC-in-my-Pocket/blob/main/images/active-directory/Test-AD-User-Infor.png)

## Sysmon Deployment via GPO


