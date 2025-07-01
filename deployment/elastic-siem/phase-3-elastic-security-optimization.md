# Phase 3 - Elastic Security Optimization

## Overview

With the Elastic Stack up and running smoothly, the next step is configuring **Elastic Security Detection Rules (SIEM)** to start generating meaningful security alerts.

![pic11](/images/elasticsiem/pic11.png)

## Best Practices

Below are some personal best practices I learned during the setup of detection rules. This is not a step-by-step guide since enabling rules in Elastic is straightforward. Instead, these notes are intended to help avoid common pitfalls and optimize detection logic:

- **Avoid enabling all detection rules blindly**: In my initial deployment, I enabled over 1,200 built-in rules, which quickly flooded the system with false positives and irrelevant alerts. Start small and scale based on your environment.

- **Duplicate rules before modifying them**: If you want to adjust a rule’s frequency or change the query logic (e.g., polling every 1 minute instead of the default 5), **duplicate** the original rule and make your changes on the copy. This preserves the original and gives you full control for tuning.

- **Enable only relevant rules**: Tailor the rule set to your environment. For example, if your infrastructure consists only of Windows endpoints and services, there’s no need to enable rules targeting AWS, GCP, or Linux threats. This keeps your alerting focused and your system efficient.

Since my lab environment includes only Windows and Linux endpoints, I selected and enabled only a subset of [built-in detection rules](/.build/elasticsiem/rules_export.ndjson).  
You can download and import this rule set directly into your Elastic SIEM instance.

For the remainder of this project, I plan to explore **Detection as Code (DaC)**. This will allow me to manage and enrich detection rules programmatically, ensuring scalability, consistency, and automation in rule deployment.
