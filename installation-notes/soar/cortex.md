<h1 align="center">
<img src=https://github.com/phamthanhsang-cs/SOC-in-my-Pocket/blob/main/images/logos/cortex-logo.png alt="logo" width="400">

TheHive Installation Notes

</h1>

Like TheHive, Cortex can be installed via **automated installation script**

### Cortex Setup


1. Make sure you meet the requirements for Cortex installation:
- 4 vCPU
- 16 GB of RAM 
  
2. Download TheHive
```bash
wget -q -O /tmp/install.sh https://archives.strangebee.com/scripts/install.sh ; sudo -v ; bash /tmp/install.sh

```



### Post installation 
After installation, go to http://<server_ip>:9001
   
![Admin account setup](https://github.com/phamthanhsang-cs/SOC-in-my-Pocket/blob/main/images/cortex/cortex-login.png)

### Useful info / Important note
* Check out **[Cortex's documentation](https://docs.strangebee.com/cortex/#installation-and-configuration-guides)** for more information about it
* Since all the Infrastructure are running on Proxmox, so i had test TheHive on Proxmox LXC Containter and run it successfully
