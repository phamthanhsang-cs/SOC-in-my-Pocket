# Firewall Rules 

## Overview
This document outlines the firewall rules implemented for the **SOCIMP** project to secure network traffic between VLANs, endpoints, and critical infrastructure. Each section specifies the rules applied to different VLANs and services based on their purpose and security requirements.

Since i only apply penetration test on two networks: **Servers Area** & **User Workstation**





## Table of Contents
1. [General Principles](#general-principles)
2. [Rules by VLAN](#rules-by-vlan)
   - [VLAN 5: local_server_area (Servers Area)](#vlan-5-local_server_area---servers-area)
   - [VLAN 192: user_network (User Workstations)](#vlan-192-user_network---user-workstations)
3. [Penetration Testing Considerations](#penetration-testing-considerations)
4. [Known Exceptions](#known-exceptions)



## General Principles
- **Zero Trust Policy:** Default all traffic to `DENY` unless explicitly allowed.
- **Least Privilege Access:** Only the necessary ports and protocols are opened.
- **Segmentation:** Strict segregation between VLANs to minimize attack surfaces.
- **Monitoring:** Firewall logs are forwarded to the SIEM (Elastic SIEM) for analysis.



## Rules by VLAN

### VLAN 5: local_server_area - Servers Area
- **Purpose:** Mail Server, Domain Controller, DNS Server.
- **Allowed Traffic:**
- **To VLAN 2 (SIEM):**
  - Fleet: Ports 8220
  - Elasticsearch: Port 9200
- **To VLAN 192 (User Workstation):**
  - MS RDP: Port 3389
- **Rule in details:**



### VLAN 192: user_network - User Workstations
- **Purpose:** Simulates real-world user environments.
- **Allowed Traffic:**
- **To VLAN 5 (Server Area):**
  - Mail Server: Ports 25, 110, 143, 465, 587, 993, 995
  - Webmail GUI: Port 80-443
  - Domain Controller: Ports: 88 (Kerberos), 123 (NTP), 135 (RPC), etc.
- **To VLAN 2 (SIEM):**
  - Fleet: Ports 8220
  - Elasticsearch: Port 9200
- **Rule in details:**




## Penetration Testing Considerations
- **VLAN 5 and VLAN 192:** Ports such as 110, 25, and 135 are intentionally opened for testing.
- **Testing Scope:**
- Simulate phishing attacks against the mail server.
- Test user vulnerabilities through exposed ports in VLAN 192.
- Use **AtomicRedTeam** to test some network-related attacks
- 
**Rules Adjusted for Testing:**




## Known Exceptions
- **Temporary Access:** Some rules, such as insecure mail ports, are temporarily opened for penetration testing and will be closed after testing.
- **Monitoring Traffic:** All allowed and blocked traffic is logged and analyzed in the SIEM.




---

## Change Log
| Date       | Change Description                                   
|------------|-----------------------------------------------------|
| 2024-12-08 | Initial documentation for firewall rules.           | 
| YYYY-MM-DD |   | 


