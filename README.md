# SOC IN MY POCKET

<a id="readme-top"></a>

[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![Issues][issues-shield]][issues-url]
[![MIT License][license-shield]][license-url]
[![LinkedIn][linkedin-shield]][linkedin-url]



<!-- PROJECT LOGO -->
<br />
<div align="center">
  <a href="https://github.com/phamthanhsang-cs/SOC-in-my-Pocket">
    <img src="images/logos/blueteam.png" alt="Logo" width="150" height="150">
  </a>

<h3 align="center">SOC Analyst</h3>

  <p align="center">
   To be a part of y'all
    <br />
    <br />
    <a href="https://github.com/phamthanhsang-cs/SOC-in-my-Pocket">My Demo</a>
    ·
    <a href="https://github.com/phamthanhsang-cs/SOC-in-my-Pocket/issues/new?labels=bug&template=bug-report---.md">Your Suggestion / Review</a>
    ·
    <a href="https://github.com/phamthanhsang-cs/phamthanhsang-cs">Other Projects</a>
  </p>
</div>



<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#introduction">Introduction</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#adversary-emulation">Adversary Emulation</a></li>
      </ul>
    </li>
    <li><a href="#incident-response">Incident Response</a></li>
    <li><a href="#lets-go">Let's go</a></li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#contact">Contact</a></li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## About The Project

<div align="center"> 
  <img src="https://github.com/phamthanhsang-cs/SOC-in-my-Pocket/blob/main/images/proposals/birdseyeview.jpg" alt="screenshot" />
</div>

<div align="center"> 
  Bird's Eye View of my SOC Infrastructure (Dec/2024)
</div>

<p align="right">(<a href="#readme-top">back to top</a>)</p>



### Implement With

[![OPNSense][OPNSense]][OPNSense-url]
[![Elastic][Elastic]][Elastic-url]
[![Shuffle][Shuffle]][Shuffle-url]
[![Cortex][Cortex]][Cortex-url]
[![TheHive][TheHive]][TheHive-url]
[![MISP][MISP]][MISP-url]
[![OpenCTI][OpenCTI]][OpenCTI-url]
[![AtomicRedTeam][AtomicRedTeam]][AtomicRedTeam-url]
[![ParrotSec][ParrotSec]][ParrotSec-url]


<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- GETTING STARTED -->
## Introduction

**SOC in My Pocket (SOCIMP)** is my very first and flagship cybersecurity project so far. This SOC project is designed for **Security and SOC Analysts**, centered around the core pillars of **People, Process, and Technologies (PPT)** – the foundation of effective SOC operations.

By focusing on adversaries' **Tactics, Techniques, and Procedures (TTPs)** for threat detection and response, SOCIMP helps me learn how to proactively defend against complex cyber threats. 

With its advanced monitoring, automation, and response capabilities, this SOC setup showcases my knowledge of cybersecurity and reflects my vision of a safer digital world for everyone.


### Prerequisites
***2 THINGS: ME AND A DEDICATED WORKSTATION***

Since SOCIMP includes essential SOC components designed to deliver comprehensive cybersecurity operations:

- **SIEM**: The Elastic Stack is my SIEM solution of choice. All logs and data sources (Workstations, Servers, Firewalls, Web Applications, etc.) are forwarded into the Elastic Stack and centrally managed through a Fleet Server.
  
- **EDR**: Elastic Agent, integrated with Elastic Defend, provides robust endpoint detection and response (EDR) capabilities that work seamlessly within the Elastic ecosystem.

- **SOAR**: Tools like **Shuffle**, **TheHive**, and **Cortex** offer high levels of automation, flexibility, and extensive integration across various components for streamlined security operations and incident response.

- **TIP**: **MISP** is a powerful threat intelligence platform that integrates smoothly with TheHive and Cortex for effective incident enrichment. **OpenCTI** enhances this with visually rich dashboards and multiple connectors to gather comprehensive threat intelligence data.

- **Firewall**: I opted for **OPNSense**, which offers a user-friendly dashboard, advanced traffic inspection, and a variety of built-in security features to protect the network.

*You might be wondering: **Where is the Vulnerability Management like Tenable, Qualys, or OpenVAS?** While these tools are important, my current focus is on gaining in-depth experience with incident analysis and hands-on practice using the tools mentioned above.*


**To serve those things, the SOCIMP project is built on a Workstation powered by:**
- **CPU**: Intel Xeon (18 cores / 36 threads)
- **RAM**: 96 GB
- **Storage**: 1 TB SSD

This infrastructure hosts multiple VMs and containers, ensuring scalability and performance across all SOC components.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

## Adversary Emulation

For **adversary emulation**, I chose to use **[Atomic Red Team](https://atomicredteam.io/)**. It’s lightweight, portable, and allows me to quickly test my environment.
And i also used Parrot-OS

*In the future, I may also explore **[Caldera](https://caldera.mitre.org/)** for more advanced adversary simulation capabilities.*




<p align="right">(<a href="#readme-top">back to top</a>)</p>




## Incident Response

For incident response, I rely on the well-structured **NIST Framework** to handle security incidents effectively. This framework provides a standardized approach, ensuring a thorough and consistent response to potential security threats. Four main stages of the NIST Incident Response Lifecycle:

1. **Preparation** – Establishing and maintaining an incident response capability. This involves creating an incident response policy, identifying resources, and training the response team.

2. **Detection and Analysis** – Monitoring systems to detect suspicious activities and analyzing potential incidents. This stage focuses on identifying and validating incidents accurately to minimize false positives.

3. **Containment, Eradication, and Recovery** – Limiting the impact of the incident, eliminating the root cause, and restoring systems to normal operation. This stage is crucial for controlling the spread and impact of the incident on the organization.

4. **Post-Incident Activity** – Learning from the incident to improve future responses. This involves documenting the incident, conducting a post-mortem analysis, and updating policies and procedures.

See more details on **[NIST Framework Incident Response](https://github.com/phamthanhsang-cs/SOC-in-my-Pocket/blob/main/external-documents/NIST-Framework-for-IR.pdf)**. 


<p align="right">(<a href="#readme-top">back to top</a>)</p>




## Let's Go

In the **SOCIMP** project, I will guide you through all stages, starting with **installation notes**, followed by the **deployment** where i *config* and *integrate* all components, **preparation for adversary emulation**, and **threat hunting, analysis, response to security incidents** (blue teaming). 

*This project will also cover future development plans.*

* [Installation Notes](https://github.com/phamthanhsang-cs/SOC-in-my-Pocket/tree/main/installation-notes): My setup processes, challenges encountered, and troubleshooting techniques for various components.
* [Deployment](https://github.com/phamthanhsang-cs/SOC-in-my-Pocket/tree/main/deployment): Procedures for deploying and integrating tools to create a functional SOC infrastructure.
* [Adversary Emulation](https://github.com/phamthanhsang-cs/SOC-in-my-Pocket/tree/main/adversaries-emulation): How i setting up adversary environments, building attack plans, and replicating real-world behaviors using the MITRE ATT&CK Framework.
* [Blue Teaming](https://github.com/phamthanhsang-cs/SOC-in-my-Pocket/tree/main/blue-teaming): Processes for monitoring alerts, detecting threats, and responding to incidents. This is the location that you might need to see since its the main part of my project and also **WHY I'M HERE** ! 


<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- CONTRIBUTING -->
## Contributing
<p align="right">(<a href="#readme-top">back to top</a>)</p>

### Top contributors:

<a href="https://github.com/phamthanhsang-cs/SOC-in-my-Pocket/graphs/contributors">
  <img src="https://contrib.rocks/image?repo=phamthanhsang-cs/SOC-in-my-Pocket" alt="contrib.rocks image" />
</a>


<!-- CONTACT -->
## Contact
[Pham Thanh Sang](https://www.linkedin.com/in/phamthanhsang0311/) - [@telegram](https://t.me/sangpham0311) - sang3112002@gmail.com

**My Projects:** 
  - [SOC in my Pocket - SOCIMP](https://github.com/phamthanhsang-cs/SOC-in-my-Pocket)
  - [Boss of the SOC - BOTS](https://github.com/phamthanhsang-cs/Boss-of-the-SOC)
  
**My Blog site:** https://phamthanhsang-cs.site/

<p align="right">(<a href="#readme-top">back to top</a>)</p>





<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[contributors-shield]: https://img.shields.io/github/contributors/phamthanhsang-cs/SOC-in-my-Pocket.svg?style=for-the-badge
[contributors-url]: https://github.com/phamthanhsang-cs/SOC-in-my-Pocket/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/phamthanhsang-cs/SOC-in-my-Pocket.svg?style=for-the-badge
[forks-url]: https://github.com/phamthanhsang-cs/SOC-in-my-Pocket/network/members
[stars-shield]: https://img.shields.io/github/stars/phamthanhsang-cs/SOC-in-my-Pocket.svg?style=for-the-badge
[stars-url]: https://github.com/phamthanhsang-cs/SOC-in-my-Pocket/stargazers
[issues-shield]: https://img.shields.io/github/issues/phamthanhsang-cs/SOC-in-my-Pocket.svg?style=for-the-badge
[issues-url]: https://github.com/phamthanhsang-cs/SOC-in-my-Pocket/issues
[license-shield]: https://img.shields.io/github/license/phamthanhsang-cs/SOC-in-my-Pocket.svg?style=for-the-badge
[license-url]: https://github.com/phamthanhsang-cs/SOC-in-my-Pocket/blob/master/LICENSE.txt
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url]: https://www.linkedin.com/in/phamthanhsang0311/
[OPNSense]: https://img.shields.io/badge/OPNSense-%23FF5200.svg?style=flat&logo=opnsense&logoColor=white
[OPNSense-url]: https://opnsense.org/
[Elastic]: https://img.shields.io/badge/Elastic-%23005571.svg?style=flat&logo=elastic&logoColor=white
[Elastic-url]: https://www.elastic.co/
[Shuffle]: https://img.shields.io/badge/Shuffle-%23FF6F00.svg?style=flat&logo=hackthebox&logoColor=white
[Shuffle-url]: https://shuffler.io/
[Cortex]: https://img.shields.io/badge/Cortex-%2380F5D2.svg?style=flat&logo=serverless&logoColor=white
[Cortex-url]: https://strangebee.com/cortex/
[TheHive]: https://img.shields.io/badge/TheHive-%23FFCD00.svg?style=flat&logo=hive&logoColor=white
[TheHive-url]: https://strangebee.com/thehive/
[MISP]: https://img.shields.io/badge/MISP-%23248BFB.svg?style=flat&logo=wechat&logoColor=white
[MISP-url]: https://www.misp-project.org/
[OpenCTI]: https://img.shields.io/badge/OpenCTI-%23003399.svg?style=flat&logo=nextdns&logoColor=white
[OpenCTI-url]: https://filigran.io/solutions/open-cti/
[AtomicRedTeam]: https://img.shields.io/badge/AtomicRedTeam-%23FF0000.svg?style=flat&logo=wagtail&logoColor=white
[AtomicRedTeam-url]: https://atomicredteam.io/
[ParrotSec]: https://img.shields.io/badge/ParrotSec-%23003399.svg?style=flat&logo=parrotsecurity&logoColor=black"
[ParrotSec-url]: https://www.parrotsec.org/
