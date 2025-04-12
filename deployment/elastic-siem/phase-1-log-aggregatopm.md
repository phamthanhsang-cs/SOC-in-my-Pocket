<h1 align="center">
Phase 1 - Initial Log Aggregation for Elastic SIEM
</h1>

## Overview
This phase details initial stage of my SIEM progression - Aggregate logs from SOCIMP infrastructure including: 
- OPNsense Firewall logs
    - Inbound / Outbound traffic
    - Unbound traffic (DNS resolver)
    - VPN traffic
    - Windows Event logs (Security, Application, Sysmon,...)
    - Postfix Mail logs
- Threat Intelligent
    - MISP IoCs 
    - OpenCTI IoCs

## Setup
*Note: Because the main goal of this phase is all about collect as much logs as possible in the project into SIEM, so there's about indexing, parsing, log tunning practices, etc*

### Collect logs from Firewall - OPNsense
The very first data source is Firewall: Filter, VPN, DHCP traffic, etc. 

Method to collect those logs is pretty straight forward, since pfSense integration on Elastic Agent does not collect Suricata logs so i have to split it into seperate collection way - and filebeat comes into play.

![siem1](/images/elasticsiem/firewalllog.png)qw2222`1
