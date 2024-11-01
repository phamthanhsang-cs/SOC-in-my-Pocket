<h1 align="center">
<img src=https://github.com/phamthanhsang-cs/SOC-in-my-Pocket/blob/main/images/logos/opnsense-logo.png alt="logo" width="400">

OPNSense Firewall Installation Notes

</h1>
At the Frontline of SOCIMP project is OPNSense -  an open source, easy-to-use and easy-to-build FreeBSD based firewall and routing platform. 
<br>
<br>
I chose to use OPNSense over pfSense since it has nicer interface (in my opinion), Suricata built-in which is important for Network Security and also integration capabilities with other components in the project. Also, i never work with OPNSense before, so it's good time to learn new things. 

### Setup
Install OPNSense on Proxmox in a VM is easy as drinking a glass of water: *Just like you install windows or linux via installation media*

**NOTICE**: When downloading the [OPNSense installation image](https://opnsense.org/download/), choose the *dvd* version instead of *vga*, so you can boot it in Proxmox VMs using an *ISO* image.

During OPNSense installation, configure at least **two** network interfaces: one for the **WAN** and another for the **LAN**. 

You can add additional interfaces if you want to enable network configurations like LACP, bonding,etc or create a separate interface for a dedicated zone.

<div align="center">
    <img src="https://github.com/phamthanhsang-cs/SOC-in-my-Pocket/blob/main/images/opnsense/opnsense-pre-install-setup.png" alt="Pre-Install Setup">
</div>

**CAUTION** ! After the initial boot, OPNSense will run in a *live environment*. To complete the installation (i.e., to boot from the hard disk), log into the shell using the username `installer` instead of `root`. If the firewall goes down or re-installation is needed, you will need to perform this step again.



### Post installation 
 - Change the IP address of the device connected to OPNSense’s LAN to match the **192.168.1.x/24** subnet
 - Access the OPNSense GUI by navigating to **192.168.1.1**
 - Default of OPNSense credentials are `root` / `opnsense`

![Admin account setup](https://github.com/phamthanhsang-cs/SOC-in-my-Pocket/blob/main/images/opnsense/OPNSense-login.png)



### Useful info / Important note
- Since infrastructure runs behind the firewall, my first configuration was setting up a WireGuard VPN for secure remote access. This allows me to reach all SOCIMP components directly from my workstation, instead of having to log into individual VMs within OPNSense’s network

   *You can get a better idea of the setup by checking out the [Birds Eye View](https://github.com/phamthanhsang-cs/SOC-in-my-Pocket/blob/main/images/proposals/birdseyeview.png)*
