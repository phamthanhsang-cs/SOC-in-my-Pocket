# Phase 2 - Data Viewing Optimization

## Overview

After collecting logs from various sources—such as network devices (firewall traffic, IDS) and endpoints (syslog, Windows event logs, EDR)—it is essential to categorize these logs for better visibility and to reduce noise. Without proper structuring, analyzing this data becomes overwhelming and inefficient.

## Setup

Before diving into the setup, it's important to understand why configuring your own data views in Elastic is necessary.

By default, when logs are ingested from data sources into Elastic, they are stored in general-purpose indices such as:

```bash
.alerts-security.alerts-default, apm--transaction, auditbeat-, endgame-,filebeat-, logs-, packetbeat-, traces-apm, winlogbeat-*,-elastic-cloud-logs-
```


Using these unfiltered indices as your primary data source for analysis will quickly result in being "drowned" in data. Therefore, categorizing logs into specific Data Views is crucial for streamlined analysis.

In this project, the following categories were defined:

- **Sysmon & PowerShell logs** from Windows Endpoints  
- **Windows Event Logs** (Application / Security / System) from Windows Endpoints  
- **LDAP User Information** from Active Directory  
- **Syslog** from iRedMail Server  
- **Firewall Logs** from OPNsense  
- **Suricata Logs** from OPNsense  
- **Elastic Defend Logs**

### Creating Data Views in Kibana

In Kibana, navigate to:

☰ → **Management** → **Stack Management** → **Data Views** → **Create data view**

![pic1](/images/elasticsiem/pic1.png)

---

### Sysmon & PowerShell Logs (Windows Endpoint)

![pic2](/images/elasticsiem/pic2.png)

---

### Windows Event Logs (Application / Security / System)

![pic3](/images/elasticsiem/pic3.png)

---

### LDAP User Information (Active Directory)

![pic4](/images/elasticsiem/pic4.png)

---

### Syslog (iRedMail Server)

![pic5](/images/elasticsiem/pic5.png)

---

### Firewall Logs (OPNsense)

![pic6](/images/elasticsiem/pic6.png)

---

### Suricata Logs (OPNsense)

![pic7](/images/elasticsiem/pic7.png)

---

### Elastic Defend Logs

![pic8](/images/elasticsiem/pic8.png)

---

These custom Data Views significantly improve the querying and analysis experience. If you're familiar with Splunk, think of them as similar to defining indexes tied to specific sourcetypes.

## Testing

For example, suppose you want to query Sysmon data. Below is what the Kibana view looked like **before** setting up custom Data Views — over **20,000** documents:

![pic10](/images/elasticsiem/pic10.png)

After applying Data View filtering, the number of relevant Sysmon events is reduced to just **160+**, dramatically cutting down on noise:

![pic9](/images/elasticsiem/pic9.png)
