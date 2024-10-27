<h1 align="center">
<img src=https://github.com/phamthanhsang-cs/SOC-in-my-Pocket/blob/main/images/logos/shuffle-logo.png alt="logo" width="120" height="120">

Shuffle Installation Notes

</h1>

I think Shuffle Insllation is pretty easy to understand and straighforward, so many installation method are there such as via docker, kubenetes,...

### Docker - *nix
The Docker setup is the default setup, and is ran with docker compose. This is [NOT a scalable build](https://shuffler.io/docs/configuration#production-readiness) without changes.

1. Make sure you have [Docker](https://docs.docker.com/get-docker/) installed, and that you have a minimum of **2Gb of RAM** available.
2. Download Shuffle
```bash
git clone https://github.com/Shuffle/Shuffle
cd Shuffle
```

3. Fix prerequisites for the Opensearch database (Elasticsearch): 
```bash
mkdir shuffle-database                    # Create a database folder
sudo chown -R 1000:1000 shuffle-database  # IF you get an error using 'chown', add the user first with 'sudo useradd opensearch'

sudo swapoff -a                           # Disable swap
```

4. Run docker-compose.
```bash
docker compose up -d
```

5. Recommended for Opensearch to work well
```bash
sudo sysctl -w vm.max_map_count=262144             # https://www.elastic.co/guide/en/elasticsearch/reference/current/vm-max-map-count.html
```


### After installation 
1. After installation, go to http://localhost:3001 (or your servername - https is on port 3443)
2. Set up your admin account (username & password). 
3. Sign in with the same Username & Password! Go to /apps and see if you have any apps yet
4. Check out https://shuffler.io/docs/configuration as it has a lot of useful information to get started

![Admin account setup](https://github.com/Shuffle/Shuffle/blob/main/frontend/src/assets/img/shuffle_adminaccount.png?raw=true)

### Useful info
* Check out [getting started](https://shuffler.io/docs/getting_started)
* Further configurations can be done in docker-compose.yml and .env.
* Default database location is in the same folder: ./shuffle-database


