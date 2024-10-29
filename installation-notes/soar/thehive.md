<h1 align="center">
<img src=https://github.com/phamthanhsang-cs/SOC-in-my-Pocket/blob/main/images/logos/thehive-logo.png alt="logo" width="400">

TheHive Installation Notes

</h1>

TheHive can be installed via **automated installation script**, simplify the setup process

### TheHive Setup


1. Make sure you meet the requirements for TheHive installation:
- 4 vCPU
- 16 GB of RAM 
  
2. Download TheHive
```bash
wget -q -O /tmp/install.sh https://archives.strangebee.com/scripts/install.sh ; sudo -v ; bash /tmp/install.sh

```



### Post installation 
1. After installation, go to http://<server_ip>:9000
2. Default admin credentials (user/password) are admin / secret
   
![Admin account setup](https://github.com/phamthanhsang-cs/SOC-in-my-Pocket/blob/main/images/thehive/thehive-login.png)

### Useful info / Important note
* Check out **[TheHive's documentation](https://docs.strangebee.com/thehive/overview/)** for more information about it
* Since all the Infrastructure are running on Proxmox, so i had test TheHive on Proxmox LXC Containter and run it successfully
