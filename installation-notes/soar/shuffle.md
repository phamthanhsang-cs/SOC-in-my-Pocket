<h1 align="center">
<img src=https://github.com/phamthanhsang-cs/SOC-in-my-Pocket/blob/main/images/logos/shuffle-logo.png alt="logo" width="120" height="120">

Shuffle Installation Notes

</h1>

I think Shuffle Insllation is pretty easy to understand and straighforward, so many installation method are there such as via docker, kubenetes,...

### Default Setup
Since i want to simplify the installation process, i go with default installation, not scalable build but easy to follow and enough for my project


1. Make sure you have [Docker](https://docs.docker.com/get-docker/) installed
2. Download Shuffle
```bash
git clone https://github.com/Shuffle/Shuffle
cd Shuffle
```

3. Fix prerequisites for the Opensearch database (Elasticsearch): 
```bash
mkdir shuffle-database                    
sudo chown -R 1000:1000 shuffle-database  

sudo swapoff -a                           
```

4. Run docker-compose.
```bash
docker compose up -d
```

5. Recommended for Opensearch to work well
```bash
sudo sysctl -w vm.max_map_count=262144             
```


### Post installation 
1. After installation, go to http://<server_ip>:3001 (3443 for https)
2. Set up your admin account (username & password). Mine is sang3112002@gmail.com
   
![Admin account setup](https://github.com/phamthanhsang-cs/SOC-in-my-Pocket/blob/main/images/shuffle/shuffle-login.png)

### Useful info / Important note
* Check out [getting started](https://shuffler.io/docs/getting_started)
* Further configurations can be done in docker-compose.yml and .env., or if you wanna go further with high scalable build 
* Default database location is in the same folder: ./shuffle-database
* Since all the Infrastructure are running on Proxmox, so i had test the Shuffle on Proxmox LXC Containter and switched to VMs due to some conflicts during installation, just let you know.


