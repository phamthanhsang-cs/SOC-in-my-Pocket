<h1 align="center">
Phase 1 - Initial Log Aggregation for Elastic SIEM
</h1>

## Overview
This phase is all about kicking off my SIEM journey by pulling in logs from the SOCIMP infrastructure. The idea is to get as many logs as possible into Elastic SIEM to start building a centralized logging system. Here’s what I’m working with:

- **OPNsense Firewall Logs**:
  - Inbound/Outbound traffic
  - Unbound traffic (DNS resolver)
  - VPN traffic
  - Suricata logs (more on this later!)
- **Endpoint Logs**:
  - Windows Event Logs (Security, Application, Sysmon, etc.)
  - Active Directory User Identities for UEBA
  - Postfix Mail Logs
- **Threat Intelligence**:
  - MISP IoCs
  - OpenCTI IoCs

## Setup
*Quick note: The goal here is to collect logs, lots of them. I’m not diving into indexing, parsing, or fine-tuning logs just yet, that’ll come later.*

### Collect OPNsense Firewall Logs
The first stop is the firewall. I’m grabbing logs for things like filter traffic, VPN activity, DHCP traffic, and more.

#### How I Did It
Getting these logs wasn’t too complicated. The pfSense integration in Elastic Agent doesn’t handle Suricata logs, so I had to split things up and bring Filebeat into the mix.

![Firewall Logs Dashboard](/images/elasticsiem/firewalllog.svg)

Setting up remote logging from OPNsense was pretty straightforward. If you’re curious, you can check out the [OPNsense documentation on logging](https://docs.opnsense.org/manual/settingsmenu.html#logging). 

The only prep work I needed to do before forwarding OPNsense syslogs was to set up the Elastic Agent on the remote server (the one OPNsense logs would be sent to) and install the OPNsense/pfSense integration on it.

Since I’m not working in a production environment and wanted to keep things resource-friendly, I decided to use my Fleet Server as the remote log receiver.

![Fleet Server Setup](/images/elasticsiem/firewalllog2.png)

#### Fleet Server Configuration
Fleet is set up to listen on port `5104/UDP` for OPNsense log traffic. 

![Fleet Listening Port](/images/elasticsiem/firewalllog3.png)

The built-in Elastic dashboard for firewall logs is pretty neat. It gives a good overview, but I plan to make it even more useful by correlating logs from multiple sources in the future.

![Firewall Logs Dashboard Example](/images/elasticsiem/firewalllog4.png)

Logs are now flowing into the SIEM, and I can start digging into them.

![Firewall Logs Investigation](/images/elasticsiem/firewalllog5.png)

### Collecting Suricata Logs
Now for the tricky part which is Suricata logs. These logs are available in the OPNsense logging section, but the [OPNsense integration module](https://www.elastic.co/guide/en/integrations/current/pfsense.html) doesn’t include them. 

I had two options:
1. Customize the integration (sounds like a headache and a lot of work).
2. Use Filebeat to forward Suricata logs to the SIEM.

Since Suricata logs are saved as `eve.json` files in `/var/logs/suricata/eve.json`, I decided to go with the Filebeat option. 

#### Challenges with Filebeat
Installing Filebeat on OPNsense (which runs on FreeBSD) wasn’t as easy as I’d hoped. There aren’t many resources online, but I eventually found out that FreeBSD supports Beats through the [FreshPorts repository](https://www.freshports.org/sysutils/beats8/).

Here’s what I had to do:
1. Run this command to prepare the environment:
   ```bash
   opnsense-code ports
    ```

2. Install the `beats8` package and configure the Suricata module.

Filebeat Configuration

Once I got Filebeat installed, I followed Elastic’s documentation to configure it:

- [Filebeat Configuration](https://www.elastic.co/guide/en/beats/filebeat/current/filebeat-installation-configuration.html)
- [Suricata Module Filebeat](https://www.elastic.co/guide/en/beats/filebeat/current/filebeat-module-suricata.html) 

**Result**
After some trial and error, I finally got Suricata logs flowing into the SIEM. Now I can analyze them and start correlating them with other log sources.

![siem6](/images/elasticsiem/firewalllog6.png)

![siem7](/images/elasticsiem/firewalllog7.png)

So far, so good! I’ve got the common logs sorted out, and Suricata logs are coming in too.

### Collect Endpoints Logs
#### Windows Event Logs (Security, Application, Sysmon, etc.)
After [deploy Sysmon](/deployment/active-directory/adds.md) successfully to all domain-joined users, its time to grab those logs includes common Windows Event logs into SIEM. 

![winevent1](/images/elasticsiem/winevent1.svg)

