<h1 align="center">
Phase 1 - Initial Log Aggregation for Elastic SIEM
</h1>

## Overview
This phase detailed initial phase of my SIEM progression - Aggregate logs from SOCIMP infrastructure including: 
- OPNsense Firewall logs
    - Inbound / Outbound traffic
    - Unbound traffic (DNS resolver)
    - VPN traffic
    - Suricata - I'll talk about it later ! 
- Endpoints logs 
    - Windows Event logs (Security, Application, Sysmon,...)
    - Postfix Mail logs
- Threat Intelligent
    - MISP IoCs 
    - OpenCTI IoCs

## Setup
*Note: Because the main goal of this phase is about collect as much logs as possible in the project into SIEM, so i do not refer about indexing, parsing, tuning logs, etc*

### Collect logs from Firewall - OPNsense
The very first data source is Firewall: Filter, VPN, DHCP traffic, etc. 

Method to collect those logs is pretty straight forward, since pfSense integration on Elastic Agent does not collect Suricata logs so i have to split it.

![siem1](/images/elasticsiem/firewalllog.png)

