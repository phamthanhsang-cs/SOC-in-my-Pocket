# SOCIMP Deployment

This folder contains all information related to the deployment of the SOCIMP project, connecting all components together to form a fully functional Security Operations Center (SOC).

## SOCIMP Component Deployment

1. **[Firewall Deployment – OPNsense](/deployment/opnsense)**  
   Includes the main OPNsense configuration, such as firewall rules for segmenting the SOCIMP network, Suricata IDS setup, and related settings.

2. **[SIEM Deployment – Elastic Stack / Fleet / ElastAlert2](/deployment/elastic-siem)**  
   Covers the implementation of SIEM components using the Elastic Stack. This includes centralized log management, Elastic Security as the SIEM platform, agent orchestration with Fleet Server, and alerting via ElastAlert2—a Python-based alerting framework for Elasticsearch.

3. **[Threat Intelligence Platform – MISP / OpenCTI](/deployment/tip)**  
   Documents the setup of SOCIMP Threat Intelligence Platforms (TIP), specifically MISP and OpenCTI. Includes threat feed integration and TIP use for threat enrichment across the environment.

4. **[SOAR – Security Orchestration, Automation, and Response](/deployment/ai_driven-soar)**  
   Details the SOAR implementation in SOCIMP by combining multiple platforms such as case management, IOC analyzers, and automation tools (n8n/Shuffle) integrated with AI to handle incidents efficiently and with redundancy.

5. **[Simulated On-Premise Enterprise Network](/deployment/active-directory)**  
   Describes the simulation of an enterprise network using components such as a Domain Controller, Certificate Authority, and Local Mail Server to mimic a real-world organization for both defensive and offensive testing.

## Note

These deployment folders will continue to evolve, adapting to the homelab environment. As new technologies are tested and integrated, expect additional components such as:

- Vulnerability Assessment Platforms  
- Adversary Emulation Frameworks  
- Forensic Data Acquisition Pipelines
...

Some existing components may also be deprecated or removed to ensure better alignment with project goals and lab resources.
