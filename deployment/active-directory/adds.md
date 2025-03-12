# Active Directory Domain Service for SOCIMP

## Overview

This document outlines the Active Directory Domain Services (ADDS) implementation for the SOCIMP project includes organization structure. 

Its not step-by-step implementation, guide you how to install a Windows Server and promote it do Domain Controller, install ADDS, stuffs like that since you could found it from anywhere, methods are the same.

## Design Principles

The ADDS implementation follows these key principles:

- **Security:** Properly structured OUs, minimal privilege principle, and GPO hardening.
- **Scalability:** Designed to support future expansion.
- **Manageability:** Clear distinction between user and computer accounts.

## Organizational Hierarchy

### **Domain:** `socimp.local`

### **Top-Level OUs:**

- **SOCIMP Computers - For SOCIMP User Workstation (e.g: HR Computer, IT Computer,...)**
  - Accounting
  - HR
  - IT
  - Legal
  - Management
  - Marketing
  - Operations
  - PR
  - Purchasing
- **SOCIMP Users - SOCIMP Users (e.g: Jonny Snowman from Accounting Department,...)**
  - Accounting
  - HR
  - IT
  - Legal
  - Management
  - Marketing
  - Operations
  - PR
  - Purchasing
- **SOCIMP Groups**
  - Accounting_Folders
  - Accounting_Local
  - Accounting_Printers
  - HR_Folders
  - HR_local
  - IT_Folders
  - IT_Local
  - IT_Printers
  - Legal_Folders
  - Legal_Printers
  - Management_Folders
  - Management_Printers
  - Marketing_Folders
  - Marketing_local
  - Operations_Folders
  - Operations_Printers
  - PR_Folders
  - PR_Printers
  - Purchasing_Folders
  - Purchasing_Printers
- **SOCIMP Servers Computer - For SOCIMP Servers (e.g: SOCIMP Internal Root-CA Server,..)**
- **SOCIMP Services User- For specific Service Users (DB Account, CA Admin Account,...)**

## Implementation Details

- **User Accounts:** Organized by department for easier permission management.
- **Computer Accounts:** Segmented per department to enforce department-specific policies.
- **Group Policies:** Applied at the OU level to ensure security configurations are enforced.
- **Service Accounts:** Separated into `SOCIMP Services User` for better access control.
- **Server Management:** Dedicated `SOCIMP Servers Computer` OU to apply stricter security measures.

## Bulk OUs / Groups / Users creatation using AD Pro Toolkit

The problem goes here ! How can i deploy these stuffs effeciently. Not gonna lie, it could be a tedious job ! Powershell script is a way to deal with that problem and had to apply in various real enviroment. 

But i wanna introduce to you a tool that i used to create OUs, Groups and a bunch of users - AD Pro Toolkit (Trial version ofc ! xD).

AD Pro Toolkit is a cool tool to manage and maintain active directory. Theres a lot of useful feature like bulk creation, report, monitor,...

For SOCIMP use the tool for create all these stuffs for me.

First, i have to create neccessary data which is csv files contains my SOCIMP OUs, Groups and also Users. You could find these csv files [here](https://github.com/phamthanhsang-cs/SOC-in-my-Pocket/tree/main/.build/active-directory)


### OUs creation
![OUs Creation](https://github.com/phamthanhsang-cs/SOC-in-my-Pocket/blob/main/images/active-directory/OUs-creation.png)

![OUs Report](https://github.com/phamthanhsang-cs/SOC-in-my-Pocket/blob/main/images/active-directory/OUs-report.png)

![AD OUs Check](https://github.com/phamthanhsang-cs/SOC-in-my-Pocket/blob/main/images/active-directory/AD-OUs-Check.png)

### Groups creation
![Groups Creation](https://github.com/phamthanhsang-cs/SOC-in-my-Pocket/blob/main/images/active-directory/Groups-creation.png)

![Groups Report](https://github.com/phamthanhsang-cs/SOC-in-my-Pocket/blob/main/images/active-directory/Groups-report.png)

![AD Groups Check](https://github.com/phamthanhsang-cs/SOC-in-my-Pocket/blob/main/images/active-directory/AD-Groups-Check.png)

### Users creation
![Users Creation](https://github.com/phamthanhsang-cs/SOC-in-my-Pocket/blob/main/images/active-directory/Users-creation.png)

![Users Report](https://github.com/phamthanhsang-cs/SOC-in-my-Pocket/blob/main/images/active-directory/Users-report.png)

![AD User Check Example](https://github.com/phamthanhsang-cs/SOC-in-my-Pocket/blob/main/images/active-directory/AD-Users-Check.png)
