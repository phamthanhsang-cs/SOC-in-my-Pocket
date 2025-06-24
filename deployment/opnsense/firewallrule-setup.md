# Firewall Rules 

## Overview
This document details the firewall rules implemented for the SOCIMP project to secure and manage network traffic between VLANs, endpoints, and critical infrastructure. Each section outlines the specific rules applied to different VLANs and services based on their purpose and security requirements.

For this project, penetration testing is limited to two key networks: Servers Area (VLAN 5) and User Workstations (VLAN 192). Therefore, the OPNSense firewall rules are focused on securing and monitoring traffic within these networks.

## Table of Contents
1. [General Principles](#general-principles)
2. [Rules by VLAN](#rules-by-vlan)
   - [VLAN 5: local_server_area (Servers Area)](#vlan-5-local_server_area---servers-area)
   - [VLAN 192: user_network (User Workstations)](#vlan-192-user_network---user-workstations)
3. [Penetration Testing Considerations](#socimp-penetration-testing-considerations)
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
  - **Communication with-in subnet**
- **Rule in details (Top-Bottom):**
  
  **Allow Communication with-in Subnet**
  - Action: Pass
  - Direction: In
  - TCP/IP Version: IPv4
  - Protocol: any
  - Source: any
  - Destination: local_server_are net
 
  **Allow Communication between Servers and Fleet**
  - Action: Pass
  - Direction: In
  - TCP/IP Version: IPv4
  - Protocol: TCP
  - Source: any
  - Destination: 10.0.2.20/24 (my Fleet IP Address)
  - Port: 8220
 
  **Allow Communication between Servers and Elasticsearch API**
  - Action: Pass
  - Direction: In
  - TCP/IP Version: IPv4
  - Protocol: TCP
  - Source: any
  - Destination: 10.0.2.10/24 (my Elastic SIEM IP Address)
  - Port: 9200
 
  **Allow RDP from Domain Controller for SysAdmin**
  - Action: Pass
  - Direction: In
  - TCP/IP Version: IPv4
  - Protocol: TCP
  - Source: 10.0.5.10/24 (my DC IP Address)
  - Destination: user_network net 
  - Port: 3389
  - Logging: Enable
 
  **Allow DNS Traffic**
  - Action: Pass
  - Direction: In
  - TCP/IP Version: IPv4
  - Protocol: TCP/UDP
  - Source: any
  - Destination: any 
  - Port: 53
 
  **Block all Traffic to Private Network**
  - Action: Block
  - Direction: In
  - TCP/IP Version: IPv4
  - Protocol: any
  - Source: any
  - Destination: private_network (alias: 10.0.0.0/8, 172.16.0.0/12, 192.168.0.0/16)
  - Port: any
  - Logging: Enable
 
 *WHY `Block` INSTEAD OF `Reject` ? For me, using `Block` instead of `Reject` is a best practice.*
 
*- `Block` silently drops the packet without sending any response back to the sender. This approach makes it more challenging for adversaries to identify the presence of a firewall rule. (e.g: Attacker using tool like NMAP for Reconnaissance for port scanning, we will agaisnt that technique by using Block, make it harder for them to identify the network infrastructure)*

*- By not responding, `Block` creates ambiguity for attackers. They cannot determine whether the packet was dropped due to a rule, a network failure, or an unreachable destination. This increases their difficulty in performing **lateral movement** or **reconnaissance** within the network.*  

*- `Reject`, in contrast, sends a notification back to the sender, informing them that their request was explicitly refused. While this can be useful in some scenarios (e.g., debugging), it may inadvertently give adversaries confirmation about the firewall's behavior.*  
 
  **Allow Internet Traffic**
  - Action: Pass
  - Direction: In
  - TCP/IP Version: IPv4
  - Protocol: any
  - Source: any
  - Destination: any
  - Port: any

<div align="center"> 
  <img src="https://github.com/phamthanhsang-cs/SOC-in-my-Pocket/blob/main/images/opnsense/localserverarearule.jpg" alt="localserverrule" />
</div>



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
  - **DNS Traffics**
- **Rule in details (Top-Bottom):**

  **Allow users communicate with Mail Server via Secure Port**
  - Action: Pass
  - Direction: In
  - TCP/IP Version: IPv4
  - Protocol: TCP
  - Source: any
  - Destination: 10.0.5.20/24 (my iRedMail IP Address)
  - Port: secure_mail_ports (alias: 465, 587, 993, 995)
 
  **Allow users communicate with Mail Server via Insecure Port**
  - Action: Pass
  - Direction: In
  - TCP/IP Version: IPv4
  - Protocol: TCP
  - Source: any
  - Destination: 10.0.5.20/24 (my iRedMail IP Address)
  - Port: less_secure_mail_ports (alias: 25, 110, 143)
 
  **Allow users access Web Mail GUI**
  - Action: Pass
  - Direction: In
  - TCP/IP Version: IPv4
  - Protocol: TCP
  - Source: any
  - Destination: 10.0.5.20/24 (my iRedMail IP Address)
  - Port: 80-443

  **Allow Communication Between Endpoints and Elasticsaerch API**
  - Action: Pass
  - Direction: In
  - TCP/IP Version: IPv4
  - Protocol: TCP
  - Source: any
  - Destination: 10.0.2.10/24 (my Elastic SIEM IP Address)
  - Port: 9200
 
  **Allow Communication Between Endpoints and Fleet**
  - Action: Pass
  - Direction: In
  - TCP/IP Version: IPv4
  - Protocol: TCP
  - Source: any
  - Destination: 10.0.2.20/24 (my Fleet IP Address)
  - Port: 8220
 
  **Allow RPC Dynamic Ports**
  - Action: Pass
  - Direction: In
  - TCP/IP Version: IPv4
  - Protocol: TCP
  - Source: any
  - Destination: 10.0.5.10/24 (my Domain Controller Address)
  - Port: 49152-65535
 
  **Allow user connect to Domain Controller**
  - Action: Pass
  - Direction: In
  - TCP/IP Version: IPv4
  - Protocol: TCP
  - Source: any
  - Destination: 10.0.5.10/24 (my Domain Controller Address)
  - Port: domain_controller_services (alias: 53, 88, 123, 135, 137, 138, 139, 389, 445, 464, 636, 3268, 3269, 9389)
 
  **Allow user connect to Domain Controller**
  - Action: Pass
  - Direction: In
  - TCP/IP Version: IPv4
  - Protocol: TCP
  - Source: any
  - Destination: 10.0.5.10/24 (my Domain Controller Address)
  - Port: domain_controller_services (alias: 53, 88, 123, 135, 137, 138, 139, 389, 445, 464, 636, 3268, 3269, 9389)
    
  **Allow DNS Traffic**
  - Action: Pass
  - Direction: In
  - TCP/IP Version: IPv4
  - Protocol: TCP
  - Source: any
  - Destination: any
  - Port: 53
 
  **Block all Traffic to Private Network**
  - Action: Block
  - Direction: In
  - TCP/IP Version: IPv4
  - Protocol: any
  - Source: any
  - Destination: private_network (alias: 10.0.0.0/8, 172.16.0.0/12, 192.168.0.0/16)
  - Port: any
  - Logging: Enable

  **Allow Internet Traffic**
  - Action: Pass
  - Direction: In
  - TCP/IP Version: IPv4
  - Protocol: any
  - Source: any
  - Destination: any

<div align="center"> 
  <img src="https://github.com/phamthanhsang-cs/SOC-in-my-Pocket/blob/main/images/opnsense/usernetworkrule.jpg" alt="usernetworkrule" />
</div>

## SOCIMP Penetration Testing Considerations
- **VLAN 5 and VLAN 192:** Ports such as 110, 25, and 135 are intentionally opened for testing.
- **Testing Scope:**
- Simulate phishing attacks against the mail server.
- Test user vulnerabilities through exposed ports in VLAN 192.
- Use **AtomicRedTeam** to test network-related attacks


## Known Exceptions
- **Temporary Access:** Some rules, such as insecure mail ports, are temporarily opened for penetration testing and will be closed after testing.
- **Monitoring Traffic:** All allowed and blocked traffic is logged and analyzed in the SIEM.




---

## Change Log
| Date       | Change Description                                   
|------------|-----------------------------------------------------|
| 2024-12-08 | Initial documentation for firewall rules.           | 
| 2024-12-09 | Change Reject to Block for addtional security layer | 


