<h1 align="center">
<img src=https://github.com/phamthanhsang-cs/SOC-in-my-Pocket/blob/main/images/logos/misp-logo.png alt="logo" width="200" height="150">


MISP Installation Notes

</h1>

MISP can deploy easily by using provided installer on Ubuntu 24.04, im deploying on a Proxmox LXC Container with an Ubuntu 24.04 image
### Setup
Just get MISP installer file and run it, kinda simple, right ? 

1. Download MISP installer file:
```bash
sudo wget https://raw.githubusercontent.com/MISP/MISP/refs/heads/2.5/INSTALL/INSTALL.ubuntu2404.sh
```

2. Fix action permission of bash file: 
```bash
sudo chmod a+x INSTALL.ubuntu2404.sh                      
```

3. Run the inststaller
```bash
sudo ./INSTALL.ubuntu2404.sh      
```

*You can check details of [the script](https://github.com/phamthanhsang-cs/SOC-in-my-Pocket/blob/main/.build/misp/misp-install.sh) here, or go to MISP's official website*

### Post installation 
1. After installation, go to https://<server_ip> 
2. Default admin credentials are: admin@admin.test / {PASSWORD} (Generate during installation, so don't forget to note it!)
   
![Admin account setup](https://github.com/phamthanhsang-cs/SOC-in-my-Pocket/blob/main/images/misp/misp-login.png)

### Useful info / Important note
* Check out [misp's documentation](https://www.misp-project.org/documentation/) for more information.
* For other linux distribution (Debian, RHEL, AlmaLinux, Fedora,etc) or even Ubuntu version under 24.04, gonna need some tinkering, more insight on [MISP Github Install](https://github.com/MISP/MISP).
* In case you can't connect to MISP GUI, change base url on `/var/www/MISP/app/Config/config.php` and also `/etc/apache2/sites-available/misp-ssl.conf` to your own url or IP Address. 

