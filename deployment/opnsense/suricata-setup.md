# Suricata - IDS / IPS Setup

## Overview
This document details the Suricata implemented for the SOCIMP project to monitoring network traffic between VLANs, endpoints, and critical infrastructure.

For this project, penetration testing is limited to two key networks: Servers Area (VLAN 5) and User Workstations (VLAN 192). Therefore, the OPNSense Suricata IDS are focused on securing and monitoring traffic within these networks.

## Table of Contents
1. [General Principles](#general-principles)
2. [Rules by VLAN](#rules-by-vlan)
3. [Penetration Testing Considerations](#socimp-penetration-testing-considerations)
4. [Known Exceptions](#known-exceptions)