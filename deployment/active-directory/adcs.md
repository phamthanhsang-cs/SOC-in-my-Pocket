# Active Directory Certificate Services (ADCS) for SOCIMP

## Overview
This document outlines the implementation of Active Directory Certificate Services (ADCS) for the SOCIMP project. The main reason behind implement a CA Server is...i just want SOCIMP Domain User ignore red warning when access iRedMail Server, that's it, also help me prepare for simulate ADCS attacks ! But it really took me quite a bit to really figured it out. 

This is not a step-by-step guide on installing Windows Server and ADCS components step-by-step, you could find it anywhere from the internet but the challenges i faced and how to resolved it.

## Setup
Before issue certificate for SOCIMP Mail Server, there are somethings we need to prepare first:

- A Domain Name Record for iRedMail on Domain Controller, mine is mail.socimp.local - 10.0.5.20

![adcs1](https://github.com/phamthanhsang-cs/SOC-in-my-Pocket/blob/main/images/active-directory/adcs1.png)
  
- Set hostname correctly in iRedMail Server - 127.0.0.1 mail.socimp.local - For iRedMail installation, you could found it on their [installation guide](https://docs.iredmail.org/install.iredmail.on.debian.ubuntu.html)

*So far, Domain user now can go to the brower and type mail.socimp.local and able to access RoundCube WebMail, but get red warning because unauthorized certificate.*

### Issue Certificate for iRedMail to get away from red warning



