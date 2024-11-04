# Installation Notes
This folder contains the essential notes on how each component in the project was set up and configured to get everything up and running.


## Folder Structure:
To keep things organized, each component is categorized into its own subfolder:

- **Cyber Threat Intelligence (CTI) or Threat Intelligence Platform (TIP)**: MISP and OpenCTI
- **Firewall**: OPNSense
- **SIEM**: Elastic Stack (Elasticsearch, Kibana and Fleet) and Elastic Defend as **"mini" EDR**
- **SOAR**: Shuffle, TheHive and Cortex

```text
├── installation-notes
│   ├── cyber-threat-intelligence
│   │     ├── misp.md
│   │     ├── opencti.md
│   ├── firewall
│   │     ├── opnsense.md
│   ├── siem
│   │     ├── elastic-stack.md
│   ├── soar
│   │     ├── cortex.md
│   │     ├── shuffle.md
│   │     ├── thehive.md
│   ├── README.md

```
## Important Note !
* My documentation does not provide a detailed, step-by-step installation guide. Instead, it highlights my experiences, troubleshooting tips, and solutions to potential issues you may encounter during setup.
* The focus here is on getting each platform up and accessible. For guidance on configuring and integrating all components together, please refer to the [Deployment](https://github.com/phamthanhsang-cs/SOC-in-my-Pocket/tree/main/deployment) section.

